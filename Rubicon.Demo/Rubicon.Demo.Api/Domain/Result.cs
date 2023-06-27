using Rubicon.Demo.Api.Domain.Enums;

namespace Rubicon.Demo.Api.Domain
{
    public class Result
    {
        public Result(ResultStatus status)
        {
            Status = status;
        }

        public Result(ResultStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public ResultStatus Status { get; private set; }

        public string Message { get; private set; }

        public bool IsSuccess => Status == ResultStatus.Success;

    }

    public class Result<T> : Result
    {
        public Result(ResultStatus status) : base(status)
        { }

        public Result(ResultStatus status, string message) : base(status, message)
        { }

        public Result(T value) : base(ResultStatus.Success)
        {
            Value = value;
        }

        public T Value { get; private set; }

    }
}
