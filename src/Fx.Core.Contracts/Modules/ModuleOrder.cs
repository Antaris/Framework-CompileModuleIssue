namespace Fx
{
    /// <summary>
    /// Gets the module order.
    /// </summary>
    public static class ModuleOrder
    {
        /// <summary>
        /// Core libraries
        /// </summary>
        public static readonly int Level0 = 0;

        /// <summary>
        /// Database libraries and other critical services
        /// </summary>
        public static readonly int Level1 = 100;

        /// <summary>
        /// Default services
        /// </summary>
        public static readonly int Level2 = 200;

        /// <summary>
        /// Utility services
        /// </summary>
        public static readonly int Level3 = 300;

        /// <summary>
        /// Client libraries
        /// </summary>
        public static readonly int Level4 = 400;

        /// <summary>
        /// Client hosts
        /// </summary>
        public static readonly int Level5 = 500;
    }
}
