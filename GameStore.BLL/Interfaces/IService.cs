﻿namespace GameStore.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        void Create(T entity);
        void Edit(T entity);
        void Delete(int id);
        T Get(int id);
    }
}
