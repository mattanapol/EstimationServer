using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.WebApi
{
    public class ErrorResult<T>
    {
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
        public T ErrorObject { get; set; }

        public ErrorResult(T errorObject, string message, int? errorCode)
        {
            ErrorObject = errorObject;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
