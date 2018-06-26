using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        void Create(T entity);
        void Edit(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
