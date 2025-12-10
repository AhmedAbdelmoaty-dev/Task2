namespace Application.Abstractions
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value, TimeSpan? AbsoluteExpiration=null, TimeSpan? slidingExpiration = null);

        Task DeleteAsync(string key);   
    }
}
