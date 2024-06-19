# UnionOf
C# Implementation of Discriminated unions made easy.

UnionOf [![NuGet Badge](https://buildstats.info/nuget/UnionOf)](https://www.nuget.org/packages/UnionOf/)

UnionOf.Generator [![NuGet Badge](https://buildstats.info/nuget/UnionOf.Generator)](https://www.nuget.org/packages/UnionOf.Generator/)

[![publish to nuget](https://github.com/vicosanz/UnionOf/actions/workflows/main.yml/badge.svg)](https://github.com/vicosanz/UnionOf/actions/workflows/main.yml)


## Buy me a coffee
If you want to reward my effort, :coffee: https://www.paypal.com/paypalme/vicosanzdev?locale.x=es_XC


All unions are source generated, you must create a struct specifying types to join in three ways:

1. Using attribute decorating a struct

```csharp
    [UnionOf(typeof(int), typeof(string))] 
    public readonly partial struct IntOrString
    {
    }

    ...
    IntOrString valorInt = 9;
    IntOrString valorStr = "9";

```


2. Inheriting from IUnionOf<>

```csharp
    [UnionOf]
    public readonly partial struct IntOrString : IUnionOf<int, string>
    {
    }

    ...
    IntOrString valorInt = 9;
    IntOrString valorStr = "9";
```

3. Inheriting from pre-generated typed structs

```csharp
    UnionOf<int, string> valorInt = 9;
    UnionOf<int, string> valorStr = "9";
```

You can add logic to generated UnionOfs

```csharp
    [UnionOf]
    public readonly partial struct IntOrString : IUnionOf<int, string>
    {
        // User logic
        public int GetInt() => Value switch
        {
            int number => number,
            string text => ParseString(text),
            _ => throw new InvalidOperationException()
        };

        private static int ParseString(string s) => int.TryParse(s, out int num) ? num : 0;
        // End of User logic
    }
    
```

You can add define how to handle nulls defining a Default value if null is received

```csharp
    [UnionOf]
    public readonly partial struct IntOrString : IUnionOf<int, string>, IHandleDefaultValue
    {
        public object ParseNull() => "none";
    }
    
    ...
    IntOrString value1 = new();
    Console.WriteLine(value1); // output: none (default value)
```


You can use UnionOfs as Result type with signed types

```csharp
    //Predefined ErrOr type present in UnionOf dll, included here for illustration
    [UnionOf]
    public readonly partial struct ErrOr<T0> : IUnionOf<T0, Exception>, IErrOr
    {
    }
    
    ErrOr<bool> resultbool = ProcessData(false);
    ErrOr<bool> resulterr = ProcessData(true);

    ErrOr<bool> ProcessData(bool fail)
    {
        if (fail) return new AccessViolationException();
        return true;
    }


    //Method to Register Customer, implicit Exception in ErrOr
    ErrOr<bool, List<ValidationError>> result = RegisterCustomer(customer);

    ErrOr<bool, List<ValidationError>> RegisterCustomer(Customer customer)
    {
        try
        {
            if (!customer.Validate(out errors))
            {
                return errors;
            }
            bool newCustomer = RepositoryCustomer.Save(customer);
            return newCustomer;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
```

You can implement shortcircuit pattern with ErrOr values

```csharp
    var response = ErrOr.Of(request)
        .Map(ValidateNonEmpty)
        .Map(ToUpper)
        .Bind(ToResponse)
        .Match(
            (response) => response.Result,
            (exception) => exception.Message
        );

    private static ErrOr<Request> ValidateNonEmpty(Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            //short circuit
            return new Exception("Name is empty");
        }
        return request;
    }
    private static ErrOr<Request> ToUpper(Request request) =>
        request with
        {
            Name = request.Name.ToUpperInvariant()
        };
    private static ErrOr<Response> ToResponse(Request request) =>
        new Response(request.Name);

```

You can include subtypes into struct as result types and specialized methods with additional parameters.

```csharp

    [UnionOf]
    public readonly partial struct ResultOperation : IUnionOf<ResultOperation.Ok, ResultOperation.Err>
    {
        public record Ok { }
        public static ResultOperation IsOk() => Create(new Ok());


        public record Err(string Message);
        public static ResultOperation IsErr(string message) => Create(new Err(message));

    }

    ...
    ResultOperation operation;
    
    operation = ResultOperation.IsOk();

    or

    operation = ResultOperation.IsErr("Invalid name");

```


In order to discriminated values use c# match pattern

```csharp
    [UnionOf]
    public readonly partial struct CatDog : IUnionOf<Cat, Dog>
    {
    }

    public class Cat
    {
        public string Type => "Cat";
    }

    public class Dog
    {
        public string Type => "Dog";

    }

    ...

    CatDog pet = new Dog();
    EvaluatePet(pet);

    static void EvaluatePet(CatDog pet)
    {
        var thisType = pet.Value switch
        {
            Cat cat => cat.Type,
            Dog dog => dog.Type,
            _ => throw new Exception("")
        };

        Console.WriteLine($"Pet is {thisType}");
    }
```

Also you can implement stages

```csharp

    [UnionOf]
    public readonly partial struct Operation : IUnionOf<Operation.Initialized, Operation.Started, Operation.Completed, Operation.Failed>
    {
        public record Initialized(DateTime initDate);
        public record Started(Initialized init, DateTime startDate, int param1);
        public record Completed(Started started, string Result);
        public record Failed(Started started, string Reason);

        public string Log() => Value switch
        {
            Initialized init => $"Operation init at {init.initDate}",
            Started started => $"Operation started, init {started.init.initDate}, started at {started.startDate}, with param {started.param1}",
            Completed completed => $"Operation completed, init {completed.started.init.initDate}, started at {completed.started.startDate}, with param {completed.started.param1}, result = {completed.Result}",
            Failed failed => $"Operation failed, init {failed.started.init.initDate}, started at {failed.started.startDate}, with param {failed.started.param1}, reason = {failed.Reason}",
            _ => throw new NotImplementedException(),
        };
    }

    ...

    Operation operation1 = new Operation.Initialized(DateTime.Now);
    Console.WriteLine(operation1.Log());
    operation1 = new Operation.Started(operation1.ValueOperation_Initialized, DateTime.Now, -1212);
    Console.WriteLine(operation1.Log());
    operation1 = new Operation.Completed(operation1.ValueOperation_Started, "successful");
    Console.WriteLine(operation1.Log());


    //result
    Operation init at 19/6/2024 12:37:26
    Operation started, init 19/6/2024 12:37:26, started at 19/6/2024 12:37:26, with param -1212
    Operation completed, init 19/6/2024 12:37:26, started at 19/6/2024 12:37:26, with param -1212, result = successful
```

You can also implement optional pattern

```csharp
    record Persona(string FirstName, Optional<string> LastName);

    var persona = new Persona("John", "Smith");

    var fullname = persona.LastName
        .Map(_ => $"{persona.FirstName} {persona.LastName}")
        .Reduce(() => persona.FirstName);

    Console.WriteLine(fullname);
```


```csharp
    string empty = "";
    var whentest = Optional.Of(empty).WhenNot(string.IsNullOrWhiteSpace).Reduce("is empty");
    Console.WriteLine(whentest);
```
