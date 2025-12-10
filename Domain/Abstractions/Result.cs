using Domain.Errors;

namespace Domain.Abstractions
{


    public record Result
    {
        public bool IsSuccess { get; init; }

        public Error Error { get; init; }


        protected Result(Error error,bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result Success() => new( Error.None, true);

        public static Result Failure(Error error) => new( error, false);

    }

    public record Result<T> : Result
    {
        private readonly T? _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("there is no value for failure");

                return _value;
            }

            private init => _value = value;
        }

        private Result(T value, Error error,bool isSuccess) : base(error,isSuccess)
        {
           
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value, Error.None, true);

        public static Result<T> Failure (Error error) => new Result<T>(default!, error, false);

    }
}
