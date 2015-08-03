# Reflekt

Use lambda expression syntax to get MemberInfo objects, eliminating magic strings from certain reflection lookups. 

Particularly useful for diagnostics (e.g. get method name for log) and situations where you need to call a proxy by name (e.g. calling a SignalR hub method from C# code)

But best of all avail yourself of the benefits of rename refactoring without breaking your reflection, serialisation or proxying code. Or indeed any other situation where the name of a particular member known at compile time needs to be used as a string.

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

The type T1 used in the examples as a generic type argument is a placeholder type included with the Reflekt library.
If you are reflecting generic types or methods with generic type constraints then you may need to use
different placeholder types. Unconstrained generics and generics with new() or class constraints can use
the T1...T8 placeholder types from Reflekt, which keeps things looking a bit neater and shaves valuable keystrokes
off the workload standing between you and the pub.

Placeholder types are only meaningful - and always either used or discarded - when WithTypeArguments() is called. If no runtime types are specified then a generic method definition is returned, otherwise a type/method constructed with the runtime types is returned.

Note that you must always specify type arguments if WithTypeArguments() is called after Constructor(), as constructors do not exist for unconstructed generic types. Calling WithTypeArguments() with no parameters only works after Method(), and the MethodInfo returned won't be invokable until it is constructed with some real types.

If you don't call WithTypeArguments() then any generic type arguments you specify either in Reflekt calls or in the lambda selector will be preserved. That is ``` Reflekt<List<T1>>().Constructor().Parameterless(x => new List<T1>()) ``` will actually return a constructor which produces instances of List&lt;T1&gt;. No spooky magic happens just because a Reflekt placeholder type was used.

If you do call WithTypeArguments() then any generic type arguments you specify which correspond to generic parameters on the target member are treated as placeholders and removed or replaced. Nothing is preserved or ignored using spooky magic just because it isn't a built-in Reflekt placeholder type. After all there are many situations where you need to use some arbitrary placeholder type.


### Overview of a Reflekt call

As a rule a Reflekt statement reads from left to right as such:

* Start reflekt call
* Choose method, property or constructor
* Optionally specify runtime types by calling WithTypeArguments() with Type instances as parameters, or specify that a generic method definition should be returned by calling WithTypeArguments() with no parameters
* Specify parameter types
* Specify exact member with lambda


### The lambda selectors

The content of the selector lambdas is never invoked, so don't worry about things like the "(x,y) => new ExampleType(x,y)", they're just you telling Reflekt what to get and they never create pointless instances of things or pointlessly call any methods.
