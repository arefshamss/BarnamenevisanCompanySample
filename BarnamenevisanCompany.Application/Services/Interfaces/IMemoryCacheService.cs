using BarnamenevisanCompany.Application.Cache;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IMemoryCacheService
{
    Task<T> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> factory);
    Task<T> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> factory, TimeSpan expiration);
    Task SetAsync<T>(CacheKey key, Func<Task<T>> factory);
    Task SetAsync<T>(CacheKey key, Func<Task<T>> factory, TimeSpan expiration);
    Task RemoveAsync(CacheKey key);
    Task RemoveByPrefixAsync(CacheKey prefix);
    Task RemoveByPatternAsync(CacheKey pattern);
    Task FlushAsync();
}