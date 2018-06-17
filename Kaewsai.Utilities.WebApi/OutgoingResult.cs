using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.WebApi
{
    public class OutgoingResult<T>
    {
        /// <summary>
        /// Gets or sets the status.
        /// Success or Fail
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public ErrorResult<T> Error { get; set; }

        /// <summary>
        /// Create successful response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="result">Result.</param>
        public static OutgoingResult<T> SuccessResponse(T result) => new OutgoingResult<T>() { Result = result, Status = "Successful" };

        /// <summary>
        /// Create fail response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="errorObject">Error object.</param>
        public static OutgoingResult<T> FailResponse(T errorObject, string message, int? errorCode)
        {
            return new OutgoingResult<T>()
            {
                Status = "Fail",
                Error = new ErrorResult<T>(errorObject, message, errorCode)
            };
        }

        /// <summary>
        /// Create fail response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="errorObject">Error object.</param>
        /// <param name="message">Message.</param>
        public static OutgoingResult<T> FailResponse(T errorObject, string message = null)
        {
            return FailResponse(errorObject, message, null);
        }

        /// <summary>
        /// Create exception response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="exceptionObject">Exception object.</param>
        /// <param name="errorCode">Error code.</param>
        public static OutgoingResult<ExceptionDto> ExceptionResponse(Exception exceptionObject, int? errorCode)
        {
            var exceptionDto = new ExceptionDto(exceptionObject);
            return new OutgoingResult<ExceptionDto>()
            {
                Status = "Fail",
                Error = new ErrorResult<ExceptionDto>(exceptionDto, exceptionObject.Message, errorCode)
            };
        }

        /// <summary>
        /// Create exception response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="exceptionObject">Exception object.</param>
        public static OutgoingResult<ExceptionDto> ExceptionResponse(Exception exceptionObject)
        {
            return ExceptionResponse(exceptionObject, null);
        }
    }
}
