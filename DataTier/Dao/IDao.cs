using System.Collections.Generic;

namespace DataTier.Dao
{
    public interface IDao<T> where T : class
    {
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(T obj);

        List<T> GetAll();
        T GetById(int id);
    }
}