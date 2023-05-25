using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Clients
{
    public interface IClient<T>
    {
        Task<IReadOnlyCollection<T>> GetAsync();
    }
}