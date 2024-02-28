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
