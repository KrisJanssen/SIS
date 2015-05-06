using System;

namespace SIS.AppResources
{
    public abstract class Resource<T>
    {
        protected T resource;

        public T Reference
        {
            get
            {
                if (this.resource == null)
                {
                    this.resource = Load();
                }

                return this.resource;
            }
        }

        public T GetCopy()
        {
            return Load();
        }

        protected abstract T Load();

        protected Resource()
        {
            this.resource = default(T);
        }

        protected Resource(T resource)
        {
            this.resource = resource;
        }
    }
}
