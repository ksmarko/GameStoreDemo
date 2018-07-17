using System;

namespace GameStore.BLL.Interfaces
{
    public interface IService<T> : IDisposable where T : class
    {
        void Create(T entity);
        void Edit(T entity);
        void Delete(int id);
        T Get(int id);
    }
}
