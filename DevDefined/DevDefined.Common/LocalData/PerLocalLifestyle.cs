using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace DevDefined.Common.LocalData
{
    public class PerLocalLifestyleManager : AbstractLifestyleManager
    {
        private readonly string PerLocalObjectID = "PerLocalLifestyleManager_" + Guid.NewGuid();

        public override object Resolve(CreationContext context)
        {
            object instance = Local.Data[PerLocalObjectID];

            if (instance == null)
            {
                instance = base.Resolve(context);
                Local.Data[PerLocalObjectID] = instance;
            }

            return instance;
        }

        public override void Dispose()
        {
        }
    }
}