// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationFallbacFinder.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This is an implementation of SerializationBinder that tries to find a match
//   for a type even if a direct match doesn't exist. This gets around versioning
//   mismatches, and allows you to move data types between assemblies.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// This is an implementation of SerializationBinder that tries to find a match
    /// for a type even if a direct match doesn't exist. This gets around versioning
    /// mismatches, and allows you to move data types between assemblies.
    /// </summary>
    /// <remarks>
    /// This class is in SystemLayer because there is code in this assembly that must
    /// make use of it. This class does not otherwise need to be here, and can be
    /// ignored by implementors.
    /// </remarks>
    public sealed class SerializationFallbackBinder : SerializationBinder
    {
        #region Fields

        /// <summary>
        /// The assemblies.
        /// </summary>
        private List<Assembly> assemblies;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializationFallbackBinder"/> class.
        /// </summary>
        public SerializationFallbackBinder()
        {
            this.assemblies = new List<Assembly>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void AddAssembly(Assembly assembly)
        {
            this.assemblies.Add(assembly);
        }

        /// <summary>
        /// The bind to type.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <param name="typeName">
        /// The type name.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type = null;

            foreach (Assembly tryAssembly in this.assemblies)
            {
                type = this.TryBindToType(tryAssembly, typeName);

                if (type != null)
                {
                    break;
                }
            }

            if (type == null)
            {
                string fullTypeName = typeName + ", " + assemblyName;

                try
                {
                    type = System.Type.GetType(fullTypeName, false, true);
                }
                catch (FileLoadException)
                {
                    type = null;
                }
            }

            return type;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The try bind to type.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="typeName">
        /// The type name.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        private Type TryBindToType(Assembly assembly, string typeName)
        {
            Type type = assembly.GetType(typeName, false, true);
            return type;
        }

        #endregion
    }
}