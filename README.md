# Reflekt

Reflekt is for:

* Using lambda expression syntax to get MemberInfo objects. 
* Easily injecting generic type parameters at runtime. 
* Creating delegates and delegate factories for generic methods, allowing runtime-bound calling with the same performance as virtual calls (_much_ faster than reflection-based calls)
* Enumerating object trees using efficient delegates (think `for (var key in object)` but in C#...)
* Filtering enumerated items and explored branches by type
* Enumerating trees depth-first (efficient for visiting entire graph) or breadth-first (efficient for finding matching value closest to root)

You can find Reflekt at https://www.nuget.org/packages/Marsman.Reflekt/ or install using

```
install-package marsman.reflekt
```

## Accessing MemberInfo objects

Getting the name of a property is extra simple.

```csharp
                    //On type            //Get the          //For this property
                    //ExampleType        //prop name   
string propertyName = Reflekt<ExampleType>.PropertyName(x => x.Property1);
```

Getting the PropertyInfo object for a property is basically the same, but different.

```csharp
                          //On type            //Get the      //For this property
                          //ExampleType        //info   
PropertyInfo propertyInfo = Reflekt<ExampleType>.Property(x => x.Property2);
```

Getting a MethodInfo for a method with no return type and no parameters is fairly terse

```csharp
                      //On type             //get method with void  //Select the member
                      //List<string>        //return type and no
                                            //parameters
MethodInfo methodInfo = Reflekt<List<string>>.Method().Parameterless(x => x.Clear);
```

Getting a method with parameters and a return type gets a little bit more sticky. 

```csharp
                      //On type             //get method     //and with       //Select the member
                      //List<string>        //with string    //1 param of
                                            //return type    //type int
MethodInfo methodInfo = Reflekt<List<string>>.Method<string>().Parameters<int>(x => x.ElementAt);
```

You can also construct a concrete generic method using types only known at runtime, by calling ```WithTypeArguments()```

```csharp
                        //On type           //get method     //with a generic type                 //and with         //Select the member
                        //ExampleType       //with string    //argument known only                 //no parameters   
                                            //return type,   //at runtime
MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);
```

And you can get an open generic method definition, by calling ```AsGenericDefinition()```

```csharp
                        //On type           //get method     //with a generic type   //and with         //Select the member
                        //ExampleType       //with string    //argument known only   //no parameters   
                                            //return type,   //at runtime
MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().AsGenericDefinition().Parameterless(x => x.GenericMethod<T1>);
```

You can get a constructor for a constructed generic type using runtime type arguments too

```csharp
                                           //On type GenericType<>  //get the ctr //for a concrete type                 //Where the ctr has 2            //Select the
                                                                                  //using the runtime type              //params, int and                //constructor
                                                                                  //args                                //string
ConstructorInfo genericTypeConstructorInfo = Reflekt<GenericType<T1>>.Constructor().WithTypeArguments(typeKnownAtRuntime).Parameters<int,string>((x, y) => new GenericType<T1>(x, y));
```

There are also some extension methods you can call to execute Reflekt statements on the type of an object instance.

```csharp
List<string> testInstance = new List<string>();
PropertyInfo countProperty = testInstance.Reflekt().property(x => x.Count);
```

### Getting Static Methods and Properties

It is possible to get a MethodInfo or PropertyInfo for a static method/property, regardless of whether the class they're a member of is marked static. Everything is the same as normal, except the main type argument to Reflekt<> should be some dummy type (typically object) and the selector should ignore the input parameter and instead reference the relevant static member directly.

```csharp
Reflekt<object>.Method<Guid>().Parameters<string>(x => Guid.Parse)
```

## Creating Delegates and Delegate Factories

Delegates can be created for non-generic and closed generic methods using AsDelegate()

```csharp
                        //On type           //get method     //create delegate of closed     //method with      //Select the member
                        //ExampleType       //with string    //generic method using          //no parameters    //(T1 will be replaced with typeKnownAtRuntime)
                                            //return type,   //type known only at runtime
MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().AsDelegate(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);

                        //On type           //get method     //create      //method with        //Select the member
                        //ExampleType       //with string    //delegate    //no parameters      
                                            //return type,   
MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().AsDelegate().Parameterless(x => x.NonGenericMethod);
```

Delegate factories allow efficient creation of "open generic delegates" (not actually a thing...) - basically they pre-compute as much as possible
about the delegate before the generic type arguments are known. Then `CreateWithTypeArguments(...)` can be called repeatedly with different type arguments
to efficiently create delegates of closed generic methods.

```csharp
var factory = Reflekt<ExampleType>.Method<T1>().AsDelegateFactory().Parameters<T1>(x => x.GenericMethodEx);
var stringDelegate = factory.CreateWithTypeArguments(typeof(string));
var intDelegate = factory.CreateWithTypeArguments(typeof(int));
var uriDelegate = factory.CreateWithTypeArguments(typeof(Uri));
```

### Internal caching

A lot of reflection bullshit is required to build typed delegates at runtime. Much of that computation is statically cached internally by Reflekt, so the
first call on a given type may be slower than subsequent calls, as that is when the delegate is built.

## Enumerating Object Trees

The extension methods `AsTreeEnumerable<Tvalue>(...)` and `AsTreeEnumerableWithContext<Tvalue>(...)` return an IEnumerable which enumerates the objects
which can be traversed to by starting at the input object. The type argument `<Tvalue>` specifies which types of object will be returned by the enumerator.
All items returned will be of that type or a more derived type. The properties which are traversed can be determined uisng the additional arguments to
the extension methods.

* branchingStrategy - whether to execute a depth-first (default) or breadth-first traversal
* enumerationStrategy - controls which objects are traversed; all property values, property values which are assignable to Tvalue or property values where the declared property type is assignable to Tvalue
* filters - accepts a params collection of `Filter.ExcludeBranches<Tbranch>()` (and other filter types) arguments to further restrict both the traversal and value selection.

The contextual versions return a wrapper item which contains the value, its depth within the graph and the PropertyInfo via which it was accessed.

The tree enumerator uses an instance of `System.Serialization.ObjectIDGenerator` to detect circular references and stop traversal. No node is
visited more than once, and every node that can be traversed to (given the specified strategies/filters) is guaranteed to be visited once. The root/input is _not_
returned by the enumerator.

In a depth-first traversal the `Depth` property of the context represents only the branch count of traversal, not
necessarily the true depth of the node within the graph (i.e. shorter routes to that node may exist, but only the path encountered first
is measured). Conversely in a (much slower) breadth-first traversal the true minimum distance of each node from root would be discovered.

```csharp
var tree = obj.AsTreeEnumerable<ITreeNode>(
                        enumerationStrategy: TreeEnumerationStrategy.BreadthFirst,
                        branchingStrategy: BranchingStrategy.PropertyTypeIsTvalue);
```
```csharp

var firstEmptyStringCollection = obj.AsTreeEnumerable<ICollection<string>>()
                                    .FirstOrDefault(x => x.Count() == 0);
```
```csharp
var result = obj.AsTreeEnumerable<object>(
               enumerationStrategy: TreeEnumerationStrategy.BreadthFirst,
               Filter.ExcludeBranchesAndValues<string>(),
               Filter.ExcludeBranchesAndValues<IEnumerable>(),
               Filter.ExcludeBranchesAndValues<Uri>(),
               Filter.ExcludeValues<int>(),
               Filter.ExcludeBranches<NotNode>());
```
```csharp
var result = obj.AsTreeEnumerableWithContext<object>(
               enumerationStrategy: TreeEnumerationStrategy.DepthFirst,
				 branchingStrategy: TreeBranchingStrategy.PropertyTypeIsTvalue,
               Filter.ExcludeBranchesAndValues<string>(),
               Filter.ExcludeBranchesAndValues<IEnumerable>(),
               Filter.ExcludeBranchesAndValues<Uri>(),
               Filter.ExcludeValues<int>(),
               Filter.ExcludeBranches<NotNode>());
```