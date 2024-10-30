using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain;

namespace Infra
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly IUnitOfWork context;

        public RepositoryBase(IUnitOfWork context)
        {
            this.context = context;
        }
        public bool Create(T entity)
        {
            context.Set<T>().Add(entity);
            var result = context.SaveChanges();
            return result > 0;
        }

        public bool Delete(int id)
        {
            var entity = context.Set<T>().Find(id);
            if (entity == null)
                return false;

            context.Set<T>().Remove(entity);
            var result = context.SaveChanges();
            return result > 0;
        }

        public IQueryable<T> GetAll(params string[] includes)
        {
            if (includes == null || includes.Length == 0)
                return context.Set<T>().AsNoTracking();

            var content = context.Set<T>().AsQueryable();

            foreach (string param in includes)
                content = content.Include(param);

            return content.AsQueryable<T>();
        }

        public T GetById(int id, params string[] includes)
        {
            if (includes == null || includes.Length == 0)
                return context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);

            var content = context.Set<T>().AsQueryable();

            foreach (string param in includes)
                content = content.Include(param);

            return content.AsQueryable().FirstOrDefault(e => e.Id == id);
        }

        public bool Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result > 0;
        }

        public bool UpdateRange(IEnumerable<T> entities)
        {
            context.UpdateRange(entities);
            var result = context.SaveChanges();
            return result > 0;
        }

        public bool Save(T entity)
        {
            if (entity.IsNew())
                return Create(entity);
            return Update(entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }
    }
}
