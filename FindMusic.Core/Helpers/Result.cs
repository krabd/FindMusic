using System;

namespace FindMusic.Core.Helpers
{
    public struct Result<TResult, TModel> where TResult : Enum where TModel : class
    {
        public TResult Value { get; }

        public TModel Model { get; }

        public string Message { get; }

        public Result(TResult value, TModel model = null, string message = "")
        {
            Value = value;
            Message = message;
            Model = model;
        }
    }

    public enum Status
    {
        Ok,
        Fail
    }
}
