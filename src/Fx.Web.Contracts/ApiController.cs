// <copyright company="Fresh Egg Limited" file="ApiController.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Web
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.JsonPatch;
    using Microsoft.AspNet.Mvc;
    using Fx.Data;
    using Fx.Security;

    /// <summary>
    /// Provides a base implementation of an API controller
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public class ApiController<T> : Controller where T : EntityBase
    {
        private readonly IWriter<T> _service;
        private readonly IHost _host;

        /// <summary>
        /// Initialises a new instance of <see cref="ApiController{T}"/>
        /// </summary>
        /// <param name="host">The host</param>
        /// <param name="service">The entity writer/reader service.</param>
        protected ApiController(IHost host, IWriter<T> service)
        {
            _host = Ensure.ArgumentNotNull(host, nameof(host));
            _service = Ensure.ArgumentNotNull(service, nameof(service));

            // Identify the dataset representing the entity.
            DataSet = _host.GetAvailableDataSets()
                .OfType<DataSet<T>>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the dataset for the entity.
        /// </summary>
        public DataSet DataSet { get; private set; }

        /// <summary>
        /// Gets the entity writer/reader service.
        /// </summary>
        protected IWriter<T> Service { get { return _service; } }

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>The action result.</returns>
        [Route("{id:int}"), HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!(await _service.ExistsAsync(id)))
            {
                // Can't find the result.
                return HttpNotFound();
            }

            await _service.DeleteAsync(id, cancellationToken: Request.HttpContext.RequestAborted);

            // Return an HTTP 204 - No Content.
            return new NoContentResult();
        }

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <returns>The action result.</returns>
        [Route(""), HttpGet]
        public virtual IActionResult Get()
        {
            return new ObjectResult(_service.GetAll());
        }

        /// <summary>
        /// Gets the entity with the given id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>The action result.</returns>
        [Route("{id:int}"), HttpGet]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _service.GetAsync(id, cancellationToken: Request.HttpContext.RequestAborted);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(entity);
        }

        /// <summary>
        /// Patches the entity using the given JSON Patch
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="patch">The patch document.</param>
        /// <returns>The action result.</returns>
        [Route("{id:int}"), HttpPatch]
        public virtual async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<T> patch)
        {
            var ct = Request.HttpContext.RequestAborted;

            var entity = await _service.GetAsync(id, cancellationToken: ct);

            if (!ModelState.IsValid)
            {
                // The model state is invalid.
                return HttpBadRequest(ModelState);
            }

            // Apply the patch.
            patch.ApplyTo(entity);

            // Update the entity.
            await _service.UpdateAsync(entity, cancellationToken: ct);

            // Return an HTTP 204 - No Content.
            return new NoContentResult();
        }

        /// <summary>
        /// Creates a new instance of the entity.
        /// </summary>
        /// <param name="entity">The name of the entity.</param>
        /// <returns>The action result.</returns>
        [Route(""), HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] T entity)
        {
            if (entity == null || !ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            // Create the entity.
            int id = await _service.CreateAsync(entity, cancellationToken: Request.HttpContext.RequestAborted);

            if (DataSet != null)
            {
                string location = Url.RouteUrl(RoutingConstants.ApiRouteName, new
                {
                    dataSet = DataSet.Name.ToLower(),
                    id = id
                });

                // Add the location header.
                Request.Headers.Add(RoutingConstants.LocationHeader, new[] { location });
            }

            // Return an HTTP 204 - No Content
            return new NoContentResult();
        }

        /// <summary>
        /// Updates an existing instance of an entity.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="entity">The entity instance.</param>
        /// <returns>The action result.</returns>
        [Route("{id:int}"), HttpPost]
        public virtual async Task<IActionResult> Post(int id, [FromBody] T entity)
        {
            if (entity != null && !ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            // Ensure the correct id is set.
            entity.Id = id;

            // Update the entity.
            await _service.UpdateAsync(entity, cancellationToken: Request.HttpContext.RequestAborted);

            // Return an HTTP 204 - No Content
            return new NoContentResult();
        }
    }

    /// <summary>
    /// Provides a base implementation of an API controller with a bound service.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <typeparam name="S">The service type</typeparam>
    public class ApiController<T, S> : ApiController<T> where T : EntityBase where S : IWriter<T>
    {
        /// <summary>
        /// Initialises a new instance of <see cref="ApiController{T, S}"/>
        /// </summary>
        /// <param name="host">The host</param>
        /// <param name="service">The bound service</param>
        protected ApiController(IHost host, S service)
            : base(host, service)
        {
            Service = service;
        }

        /// <summary>
        /// Gets the bound service.
        /// </summary>
        public new S Service { get; private set; }
    }
}