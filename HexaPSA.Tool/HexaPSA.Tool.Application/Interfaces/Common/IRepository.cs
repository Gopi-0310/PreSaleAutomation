namespace HexaPSA.Tool.Application.Interfaces.Common
{
    public interface IReadRepository<T,TId>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(TId id);
    }
    public interface IWriteRepository<T, TId>
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(TId id);
    }

}
