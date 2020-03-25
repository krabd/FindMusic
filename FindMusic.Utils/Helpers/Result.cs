using System;

namespace FindMusic.Utils.Helpers
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
