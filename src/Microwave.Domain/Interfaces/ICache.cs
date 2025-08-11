using Microwave.Domain.Models;

namespace Microwave.Domain.Interfaces;

public interface ICache
{
    public T? TryGetCachedValue<T>(int id) where T : BaseModel;

    public void SetValue<T>(int id, T model) where T : BaseModel;
    public void Remove(int id);
}