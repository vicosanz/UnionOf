using UnionOf;

namespace ConsoleApp2
{

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
}
