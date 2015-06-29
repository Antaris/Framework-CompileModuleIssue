// <copyright company="Fresh Egg Limited" file="MiddlewareBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Web
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;


    /// <summary>
    /// Represents a middleware that forms part of the application pipleline.
    /// </summary>
    public abstract class MiddlewareBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref=""/>
        /// </summary>
        /// <param name="next"></param>
        protected MiddlewareBase(RequestDelegate next)
        {
            Next = next;
        }

        /// <summary>
        /// Gets the next middleware to pass through the request.
        /// </summary>
        public RequestDelegate Next { get; private set; }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current <see cref="HttpContext"/>.</param>
        /// <returns>The result task.</returns>
        public virtual async Task Invoke(HttpContext context)
        {
            await Next(context);
        }
    }
}