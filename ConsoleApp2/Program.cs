// See https://aka.ms/new-console-template for more information
using ConsoleApp2;
using System.Text.Json;
using UnionOf;

var exx = new InvalidDataException("error123");
var jsonex = JsonSerializer.Serialize(exx);
Console.WriteLine(jsonex);
var exx2 = JsonSerializer.Deserialize<InvalidDataException>(jsonex);
Console.WriteLine(JsonSerializer.Serialize(exx2));
Console.WriteLine("Hello, World!");




ErrOr<Cat> errOr = new Cat();
var json = JsonSerializer.Serialize(errOr);
Console.WriteLine(json);
ErrOr<Cat> errOr2 = JsonSerializer.Deserialize<ErrOr<Cat>>(json);
Console.WriteLine(JsonSerializer.Serialize(errOr2));

errOr = new Exception("error123");
json = JsonSerializer.Serialize(errOr);
Console.WriteLine(json);
errOr2 = JsonSerializer.Deserialize<ErrOr<Cat>>(json);
Console.WriteLine(JsonSerializer.Serialize(errOr2));

Cat mycat = new();
//Cat? nullcat = null;

CatDog pet = new Dog();
CatDog pet2 = mycat;
CatDog pet3 = new Cat();
//CatDog pet4 = nullcat;

EvaluatePet(pet);
//Console.WriteLine($"equal {pet.Equals(pet2)}");

//pet.Value = mycat;

//EvaluatePet(pet);
//Console.WriteLine($"equal {pet.Equals(pet2)}");
//Console.WriteLine($"equal pet3 {pet.Equals(pet3)}");

//Serialize(pet);

IntOrString value1 = new();
Console.WriteLine(">>>IntOrString null handled");
Console.WriteLine(value1);
//IntOrString value2 = 9;
//IntOrString value1a = "8";
//IntOrString value2a = "9";
//IntOrString value2c = 9;

//Console.WriteLine($"equal {value2.Equals(value2c)}");
//Console.WriteLine($"equal {value2.Equals(value2a)}");
//Console.WriteLine($"equal {value2 == value2c}");

static void EvaluatePet(CatDog pet)
{
    if (pet.Is(out Dog dogx))
    {
        Console.WriteLine($"Pet is {dogx.Type}");
    }
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
if (resulterr.Is(out Exception exxx)) Console.Write(exxx.Message);
if (resulterr.IsFail(out Exception exception)) Console.WriteLine(exception.Message);
if (resulterr.IsFail()) Console.WriteLine(resulterr.ValueException);

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
//var whentest2 = empty.WhenNot(string.IsNullOrWhiteSpace).Reduce("is empty");
//Console.WriteLine(whentest2);


Console.WriteLine(">>>optional");

Optional<string> message;
message = new();
Console.WriteLine(message);
message = "not empty";
Console.WriteLine(message.Reduce("-"));
string? nullstring = null;
message = nullstring;
Console.WriteLine(message.Reduce("-"));
Console.WriteLine("<<<end optional");
var optint = Optional.Parse<int>("9");
Console.WriteLine(optint.Reduce(-1));
var optint2 = Optional.Parse<int>("x9");
Console.WriteLine(optint2.Reduce(-1));

int? intnullable = null;
Optional<int> intoptional = 99;

Console.WriteLine($"nullable {intnullable.GetValueOrDefault()}");
Console.WriteLine($"nullable {(int)intoptional}");
Console.WriteLine($"{Optional.GetUnderlyingType(intoptional.GetType())}");


Request request = new(Guid.NewGuid(), "Infoware");

var response = ErrOr.Of(request)
    .Map(ValidateNonEmpty)
    .Map(ToUpper)
    .Bind(ToResponse)
    .Match(
        (response) => response.Result,
        (exception) => exception.Message
    );

Console.WriteLine(response);


var response2 = await ErrOr.Of(request)
    .MapAsync(ValidateNonEmptyAsync).Result
    .Map(ToUpper)
    .Bind(ToResponse)
    .MatchAsync(
        async (response) =>
        {
            await Task.Delay(1);
            return response.Result;
        },
        async (exception) =>
        {
            await Task.Delay(1);
            return exception.Message;
        }
    );

Console.WriteLine(response2);


Operation operation1 = new Operation.Initialized(DateTime.Now);
Console.WriteLine(operation1.Log());
operation1 = new Operation.Started(operation1.ValueOperation_Initialized, DateTime.Now, -1212);
Console.WriteLine(operation1.Log());
operation1 = new Operation.Completed(operation1.ValueOperation_Started, "successful");
Console.WriteLine(operation1.Log());

ErrOr<Request> ValidateNonEmpty(Request request)
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return new Exception("Name is empty");
    }
    return request;
}

async Task<ErrOr<Request>> ValidateNonEmptyAsync(Request request)
{
    await Task.Delay(1);
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return new Exception("Name is empty");
    }
    return request;
}

ErrOr<Request> ToUpper(Request request) =>
    request with
    {
        Name = request.Name.ToUpperInvariant()
    };

ErrOr<Response> ToResponse(Request request) =>
    new Response(request.Name);

record Persona(string FirstName, Optional<string> LastName);