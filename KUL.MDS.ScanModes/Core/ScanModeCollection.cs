using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIS.ScanModes
{
    public sealed class ScanModeCollection
    {
        private Assembly[] assemblies;
        private List<Type> scanmodes;

        public ScanModeCollection(List<Assembly> assemblies)
        {
            this.assemblies = assemblies.ToArray();
            this.scanmodes = null;
        }

        public ScanModeCollection(List<Type> scanmodes)
        {
            this.assemblies = null;
            this.scanmodes = new List<Type>(scanmodes);
        }

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

        private static List<Type> GetScanModesFromAssemblies(Assembly[] assemblies)
        {
            List<Type> effects = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                GetScanModesFromAssembly(effects, assembly);
            }

            return effects;
        }

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
    }
}
