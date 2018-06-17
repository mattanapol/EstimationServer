using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.WebApi
{
    public class ExceptionDto
    {
        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDto"/> class.
        /// </summary>
        public ExceptionDto()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDto"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExceptionDto(Exception exception)
        {
            StackTrace = exception.StackTrace;
            Source = exception.Source;
        }
    }
}
