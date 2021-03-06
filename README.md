# Reflekt

Use lambda expression syntax to get MemberInfo objects, eliminating magic strings from certain reflection lookups. 

Particularly useful for diagnostics (e.g. get method name for log) and situations where you need to call a proxy by name (e.g. calling a SignalR hub method from C# code)

But best of all avail yourself of the benefits of rename refactoring without breaking your reflection, serialisation or proxying code. Or indeed any other situation where the name of a particular member known at compile time needs to be used as a string.

You can find Reflekt at https://www.nuget.org/packages/Marsman.Reflekt/ or install using

```
install-package marsman.reflekt
```

### Note

This library has not been thoroughly tested with the new separated TypeInfo classes in .NET 4.6. I've had no problems yet but not every scenario has been robustly tested.

## Usage

Add a using to get things going.

```csharp
using Marsman.Reflekt;
```

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

And you can get a constructor for a constructed generic type using runtime type arguments too

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

### Placeholder type arguments

The type ```T1``` used in the examples as a generic type argument is a placeholder type included with the Reflekt library.
If you are reflecting generic types/methods with generic type constraints then you may need to use
different placeholder types. Unconstrained generics and generics with ```new()``` or ```class``` constraints can use
the ```T1```...```T8``` placeholder types from Reflekt, which keeps things looking a bit neater and shaves valuable keystrokes
off the workload standing between you and the pub.

Placeholder types are only meaningful - and always either used or discarded - when ```WithTypeArguments()``` is called. If no runtime types are specified then a generic method definition is returned, otherwise a type/method constructed with the runtime types is returned.

Note that you must always specify type arguments if ```WithTypeArguments()``` is called after ```Constructor()```, as constructors do not exist for unconstructed generic types. Calling ```WithTypeArguments()``` with no parameters only works after ```Method()```, and the MethodInfo returned won't be invokable until it is constructed with some real types.

If you don't call ```WithTypeArguments()``` then any generic type arguments you specify either in Reflekt calls or in the lambda selector will be preserved. That is ``` Reflekt<List<T1>>().Constructor().Parameterless(x => new List<T1>()) ``` will actually return a constructor which produces instances of ```List<T1>```. No spooky magic happens just because a Reflekt placeholder type was used.

If you do call ```WithTypeArguments()``` then any generic type arguments you specify which correspond to generic parameters on the target member are treated as placeholders and removed or replaced. Nothing is preserved or ignored using spooky magic just because it isn't a built-in Reflekt placeholder type. After all there are many situations where you need to use some arbitrary placeholder type. This does mean that the number of type arguments supplied in ```WithTypeArguments()``` must match the number of type arguments on the target member exactly (or be zero when getting a generic method definition). Partial type argument injection is not supported.

>#### Tip
>To help keep code readable there is a parameterless method named ```GenericDefinition()``` which can be used in place of ```WithTypeArguments()``` and which does the same thing as calling ```WithTypeArguments()``` with no parameters. The ```GenericDefinition()``` method is only available after calling ```Reflekt().Method()```.


### Overview of a Reflekt call

As a rule a Reflekt statement reads from left to right as such:

* Start reflekt call
* Choose method, property or constructor
* Optionally specify runtime types by calling WithTypeArguments() with Type instances as parameters, or specify that a generic method definition should be returned by calling WithTypeArguments() with no parameters
* Specify parameter types
* Specify exact member with lambda


### Compile-time Safety

You'll notice that if the return type, parameter types and any generic arguments specified in the ```Reflekt<Type<Arg>>``` statement don't match those used (or implied) in the lambda selector you'll get a compiler error. This can seem annoyingly verbose when there's only one overload of a method but it is essential to allow the correct method to be chosen when there are multiple overloads in the method group.

It is required that the same placeholder types are used in the corresponding generic type argument "places" in the lambda as well as in the ```Reflekt<Type<args>>``` statement and ```Method<Treturn>()``` call, even when those types are going to be replaced by runtime types. For instance this will compile:

```csharp
Reflekt<List<T1>>.Method().WithTypeArguments(typeKnownAtRuntime).Parameters<T1>(x => x.Add);
```

But this won't because the compiler quite rightly says that the parameter type of the Add method should match the generic type argument of the ```List<T>``` type:

```csharp
Reflekt<List<T1>>.Method().WithTypeArguments(typeKnownAtRuntime).Parameters<T2>(x => x.Add);
```

In this situation you'll see error highlighting under the ```x.Add``` in Visual Studio, with the error message ```"No overload for 'Add' matches delegate 'System.Action<T2>'"```

### The lambda selectors

The content of the selector lambdas is never invoked, so don't worry about things like the ```(x,y) => new ExampleType(x,y)```, they're just you telling Reflekt what to get and they never create pointless instances of things or pointlessly call any methods.

### Getting Static Methods and Properties

It is possible to get a MethodInfo or PropertyInfo for a static method/property, regardless of whether the class they're a member of is marked static. Everything is the same as normal, except the main type argument to Reflekt<> should be some dummy type (typically object) and the selector should ignore the input parameter and instead reference the relevant static member directly.

```csharp
Reflekt<object>.Method<Guid>().Parameters<string>(x => Guid.Parse)
```
