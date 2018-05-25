using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext databaseContext = null;

        public virtual void Add(T entity)
        {
            databaseContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            databaseContext.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return databaseContext.Set<T>();
        }

        public virtual T GetById(int identifier)
        {
            return databaseContext.Set<T>().Find(identifier);
        }

        public virtual void Save()
        {
            databaseContext.SaveChanges();
        }

        public virtual void Update( T entity)
        {            
            databaseContext.Update(entity);
        }
    }
}
