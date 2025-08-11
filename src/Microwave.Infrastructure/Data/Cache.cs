using Microsoft.Extensions.Caching.Memory;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models;

namespace Microwave.Infrastructure.Data;

public class Cache(IMemoryCache memoryCache) : ICache
{
    public T? TryGetCachedValue<T>(int id) where T : BaseModel
    {
        memoryCache.TryGetValue(id, out var model);
        
        return model as T;
    }

    public void SetValue<T>(int id, T model) where T : BaseModel
    {
        memoryCache.Set(id, model);
    }

    public void Remove(int id)
    {
        memoryCache.Remove(id);
    }
}