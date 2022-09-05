namespace CustomerDatalayer.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        TEntity Read(int entityCode);
        int Update(TEntity entity);
        int Delete(int entityCode);
        void DeleteAll();
    }
}
