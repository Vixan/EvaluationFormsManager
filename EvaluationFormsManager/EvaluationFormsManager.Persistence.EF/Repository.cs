using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext databaseContext = null;

        public void Add(T entity)
        {
            databaseContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            databaseContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return databaseContext.Set<T>();
        }

        public T GetById(int identifier)
        {
            return databaseContext.Set<T>().Find(identifier);
        }

        public void Save()
        {
            databaseContext.SaveChanges();
        }
    }
}
