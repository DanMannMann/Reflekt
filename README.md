# Reflekt

Use lambda expression syntax to get MemberInfo objects, eliminating magic strings from certain reflection lookups. 

Particularly useful for diagnostics (e.g. get method name for log) and situations where you need to call a proxy by name (e.g. calling a SignalR hub method from C# code)

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

Getting a method with parameters and a return type gets a little bit more sticky. 

```csharp
                      //On type             //get method     //and with       //Select the member
                      //List<string>        //with string    //1 param of
                                            //return type    //type int
MethodInfo methodInfo = Reflekt<List<string>>.Method<string>().Parameters<int>(x => x.ElementAt);
```

You can also construct a concrete generic method using types only known at runtime, by calling .WithTypeArguments()

```csharp
                        //On type           //get method     //with a generic type                 //and with         //Select the member
                        //ExampleType       //with string    //argument known only                 //no parameters   
                                            //return type,   //at runtime
MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);
```

And you can get a constructor for a constructed generic type using runtime type arguments too

```csharp
                                           //On type GenericType<>  //get the ctr //for a concrete type       //Where the ctr has 2            //Select the
                                                                                  //using the runtime type    //params, int and                //constructor
                                                                                  //args                      //string
ConstructorInfo genericTypeConstructorInfo = Reflekt<GenericType<T1>>.Constructor().Generic(typeKnownAtRuntime).Parameters<int,string>((x, y) => new GenericType<T1>(x, y));
```

There are also some extension methods you can call to execute Reflekt statements on the type on an object instance.

```csharp
List<string> testInstance = new List<string>();
PropertyInfo countProperty = testInstance.Reflekt().property(x => x.Count);
```

As a rule a Reflekt statement reads from left to right as such:

- Start reflekt call -> 
- Choose method, property or constructor -> 
- Optionally specify runtime types, or tell Reflekt to get the generic definition, by calling .WithTypeArguments() -> 
- Specify parameter types -> 
- specify exact member with lambda

The generic type T1 used in the examples is a placeholder type included with the Reflekt library.
If you are reflecting generic types or methods with generic type constraints then you may need to use
different placeholder types. Unconstrained generics and generics with the new() or class constraints can use
the T1...T8 placeholder types from Reflekt, which keeps things looking a bit neater and shaves valuable keystrokes
off the workload standing between you and the pub.

Placeholder types are only meaningful - and always either used or discarded - when .Generic() or .WithTypeArguments() is called. 
Either a generic type/method definition is returned, if no runtime types are specified, or a type/method constructed with the runtime types is returned.

The content of the selector lambdas is never invoked, so don't worry about things like the "(x,y) => new ExampleType(x,y)", they're just you telling Reflekt what to get and they never create pointless instances of things or pointlessly call any methods.
