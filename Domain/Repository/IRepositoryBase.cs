
namespace Domain
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> GetAll(params string[] includes);

        TEntity GetById(int id, params string[] includes);

        bool Create(TEntity entity);

        bool Update(TEntity entity);

        bool Save(TEntity entity);

        bool Delete(int id);

        bool UpdateRange(IEnumerable<TEntity> entities);

    }
}
