using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App.Helpers.Service
{
    public class ServiceResult<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError { get; private set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; private set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{T}"/> class.
        /// </summary>
        /// <param name="haserror">if set to <c>true</c> [haserror].</param>
        public ServiceResult()
        {
            this.HasError = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{T}" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ServiceResult(string message = "")
        {
            this.HasError = true;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ServiceResult(T result)
        {
            this.HasError = false;
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="message">The message.</param>
        public ServiceResult(T result, string message)
        {
            this.HasError = false;
            this.Result = result;
            this.Message = message;
        }
    }
}
