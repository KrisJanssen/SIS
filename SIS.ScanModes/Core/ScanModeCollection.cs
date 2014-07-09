// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanModeCollection.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan mode collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.ScanModes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The scan mode collection.
    /// </summary>
    public sealed class ScanModeCollection
    {
        #region Fields

        /// <summary>
        /// The assemblies.
        /// </summary>
        private Assembly[] assemblies;

        /// <summary>
        /// The scanmodes.
        /// </summary>
        private List<Type> scanmodes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanModeCollection"/> class.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        public ScanModeCollection(List<Assembly> assemblies)
        {
            this.assemblies = assemblies.ToArray();
            this.scanmodes = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanModeCollection"/> class.
        /// </summary>
        /// <param name="scanmodes">
        /// The scanmodes.
        /// </param>
        public ScanModeCollection(List<Type> scanmodes)
        {
            this.assemblies = null;
            this.scanmodes = new List<Type>(scanmodes);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the scanmodes.
        /// </summary>
        public Type[] Scanmodes
        {
            get
            {
                if (this.scanmodes == null)
                {
                    this.scanmodes = GetScanModesFromAssemblies(this.assemblies);
                }

                return this.scanmodes.ToArray();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get scan modes from assemblies.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private static List<Type> GetScanModesFromAssemblies(Assembly[] assemblies)
        {
            List<Type> effects = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                GetScanModesFromAssembly(effects, assembly);
            }

            return effects;
        }

        /// <summary>
        /// The get scan modes from assembly.
        /// </summary>
        /// <param name="scanModesAccumulator">
        /// The scan modes accumulator.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        private static void GetScanModesFromAssembly(List<Type> scanModesAccumulator, Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Scanmode)) && !type.IsAbstract)
                    {
                        scanModesAccumulator.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
            }
        }

        #endregion
    }
}