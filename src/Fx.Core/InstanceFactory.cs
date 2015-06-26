// <copyright company="Fresh Egg Limited" file="InstanceFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Provides services for creating object instances.
    /// </summary>
    public class InstanceFactory : IInstanceFactory
    {
        private readonly ConcurrentDictionary<int, Delegate> _cache = new ConcurrentDictionary<int, Delegate>();

        /// <inheritdoc />
        public T Create<T, P>(P arg)
        {
            var func = (Func<P, T>)GetConstructor(typeof(T), typeof(P));

            return func(arg);
        }

        /// <inheritdoc />
        public T Create<T, P1, P2>(P1 arg1, P2 arg2)
        {
            var func = (Func<P1, P2, T>)GetConstructor(typeof(T), typeof(P1), typeof(P2));

            return func(arg1, arg2);
        }

        /// <summary>
        /// Gets the constructor delegate used to create an instance of the type.
        /// </summary>
        /// <param name="instanceType">The instance type.</param>
        /// <param name="args">The constructor arguments.</param>
        /// <returns>The constructor delegate.</returns>
        private Delegate GetConstructor(Type instanceType, params Type[] args)
        {
            int hash = 0;
            unchecked
            {
                hash = instanceType.GetHashCode();
                hash = args.Aggregate(hash, (current, type) => current * 31 + type.GetHashCode());
            }

            return _cache.GetOrAdd(hash, h => GetConstructorCore(instanceType, args));
        }

        /// <summary>
        /// Gets a constructor by building a dynamic expression used to compile the constructor delegate.
        /// </summary>
        /// <param name="instanceType">The instance type.</param>
        /// <param name="args">The constructor arguments.</param>
        /// <returns>The constructor delegate.</returns>
        private static Delegate GetConstructorCore(Type instanceType, params Type[] args)
        {
            var parameters = args.Select(Expression.Parameter).ToArray();
            var method = instanceType.GetConstructor(args);
            if (method == null)
            {
                throw new InvalidOperationException("The type '" + instanceType.ToString() + "' does not have an appropriate constructor.");
            }

            var constructor = Expression.New(method, parameters);
            var lambda = Expression.Lambda(constructor, parameters);

            return lambda.Compile();
        }
    }
}
