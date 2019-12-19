using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper 
    { 
        IOwnerRepository Owner { get; } 
        IMeterRepository Meter { get; } 
        Task SaveAsync(); 
    }
}
