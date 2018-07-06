using System.Collections.Generic;

namespace GameStore.BLL.Filtering.Factory
{
    abstract class Pipeline<T>
    {
        protected readonly List<IFilter<T>> filters = new List<IFilter<T>>();

        public Pipeline<T> Register(IFilter<T> filter)
        {
            filters.Add(filter);
            return this;
        }

        public abstract T Process(T input);
    }
}
