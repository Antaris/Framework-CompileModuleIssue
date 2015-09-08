// <copyright company="Fresh Egg Limited" file="EntityBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using Fx.Security;

    /// <summary>
    /// Provides a base implementation of an entity.
    /// </summary>
    public abstract class EntityBase
    {
        private Lazy<IIdentity> _createdIdentityThunk = new Lazy<IIdentity>(() => null);
        private Lazy<IIdentity> _updatedIdentityThunk = new Lazy<IIdentity>(() => null);

        /// <summary>
        /// Initialises a new instance of <see cref="EntityBase"/>.
        /// </summary>
        protected EntityBase() {  }

        /// <summary>
        /// Gets the identity of the user that created the entity.
        /// </summary>
        public IIdentity Created
        {
            get { return _createdIdentityThunk.Value; }
        }

        /// <summary>
        /// Gets or sets the date/time the entity was created.
        /// </summary>
        public DateTimeOffset CreatedDateTimeOffset { get; set; }

        /// <summary>
        /// Gets or sets the id of the user that created the entity.
        /// </summary>
        public int CreatedUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the entity has been deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the entity is hidden.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets the id of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the entity is readonly.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets the identity of the user that last updated the entity.
        /// </summary>
        public IIdentity Updated
        {
            get { return _updatedIdentityThunk.Value; }
        }

        /// <summary>
        /// Gets or sets the date/time the entity was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedDateTimeOffset { get; set; }

        /// <summary>
        /// Gets or sets the id of the user that updated the entity.
        /// </summary>
        public int? UpdatedUserId { get; set; }

        /// <summary>
        /// Sets the method used to resolve <see cref="Identity"/> instances.
        /// </summary>
        /// <param name="func">The delegate used to resolve <see cref="Identity"/> instances.</param>
        public void SetUpdatedIdentityResolver(Func<int, IIdentity> func)
        {
            Ensure.ArgumentNotNull(func, nameof(func));

            _createdIdentityThunk = new Lazy<IIdentity>(() => func(CreatedUserId));

            _updatedIdentityThunk = new Lazy<IIdentity>(() =>
            {
                if (UpdatedUserId != null)
                {
                    return func(UpdatedUserId.Value);
                }
                return null;
            });
        }
    }
}