// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using ConsoleApp2;
using UnionOf;

Console.WriteLine("Hello, World!");


Cat mycat = new();
CatDog pet = new Dog();
CatDog pet2 = mycat;
CatDog pet3 = new Cat();

EvaluatePet(pet);
//Console.WriteLine($"equal {pet.Equals(pet2)}");

//pet.Value = mycat;

//EvaluatePet(pet);
//Console.WriteLine($"equal {pet.Equals(pet2)}");
//Console.WriteLine($"equal pet3 {pet.Equals(pet3)}");

//Serialize(pet);

//IntOrString value1 = 8;
//IntOrString value2 = 9;
//IntOrString value1a = "8";
//IntOrString value2a = "9";
//IntOrString value2c = 9;

//Console.WriteLine($"equal {value2.Equals(value2c)}");
//Console.WriteLine($"equal {value2.Equals(value2a)}");
//Console.WriteLine($"equal {value2 == value2c}");

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

//static void Serialize(CatDog pet)
//{
//    string jsonString = JsonSerializer.Serialize(pet);
//    Console.WriteLine(jsonString);
//}

IntOrString valor = 9;
Console.WriteLine(valor.GetInt() + 1);

Result result = Result.Create(true);
Result result2 = new Exception("Error");
Console.WriteLine($"{result2}");


ResultOperation operation = ResultOperation.IsOk();
ResultOperation operation2 = ResultOperation.IsErr("Invalid name");

ErrOr<bool> resultbool = ProcessData(false);
ErrOr<bool> resulterr = ProcessData(true);

Console.WriteLine($"resultbool fail: {resultbool.IsFail()}");
Console.WriteLine($"resulterr fail: {resulterr.IsFail()}");
if (resulterr.Value is Exception ex)
{
    Console.WriteLine(ex.Message);
}

if (resulterr.IsFail(out Exception exception)) Console.WriteLine(exception.Message);
if (resulterr.IsFail()) Console.WriteLine(resulterr.Error);

Console.WriteLine(resultbool.ValueT0);
Console.WriteLine(resulterr.ValueT0.ToString());

UnionOf<int, string> valorIntString = 9;

static ErrOr<bool> ProcessData(bool fail)
{
    if (fail) return new AccessViolationException();
    return true;
}

ErrOr<bool, List<string>> apiResponse = new List<string>();
Console.WriteLine(apiResponse.Value is List<string>);


var persona = new Persona("John", "Smith");

var fullname = persona.LastName
    .Map(_ => $"{persona.FirstName} {persona.LastName}")
    .Reduce(() => persona.FirstName);

Console.WriteLine(fullname);


string? empty = "x";
var whentest = Optional.Of(empty).WhenNot(string.IsNullOrWhiteSpace).Reduce("is empty");
Console.WriteLine(whentest);
record Persona(string FirstName, Optional<string> LastName);