namespace Fx.Security.Users
{
    using Microsoft.AspNet.Mvc;
    using Fx.Web;

    /// <summary>
    /// Provides API access to users and supporting models.
    /// </summary>
    [Route(SecurityRoutingConstants.UsersApiRoute)]
    public class UsersApiController : ApiController<User, IUserService>
    {
        /// <summary>
        /// Initialises a new instance of <see cref="UsersApiController"/>
        /// </summary>
        /// <param name="host">The host</param>
        /// <param name="userService">The user service.</param>
        public UsersApiController(IHost host, IUserService userService)
            : base(host, userService)
        {
            
        }
    }
}