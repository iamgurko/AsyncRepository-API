using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private RepositoryContext _repoContext; 
        private IOwnerRepository _owner; 
        private IMeterRepository _meter; 
        public IOwnerRepository Owner 
        { 
            get 
            { 
                if (_owner == null) 
                { 
                    _owner = new OwnerRepository(_repoContext); 
                } 
                return _owner; 
            } 
        } 
        
        public IMeterRepository Meter 
        { 
            get 
            { 
                if (_meter == null) 
                {
                    _meter = new MeterRepository(_repoContext); 
                } 
                return _meter; 
            } 
        } 
        
        public RepositoryWrapper(RepositoryContext repositoryContext) 
        { 
            _repoContext = repositoryContext; 
        } 
        
        public async Task SaveAsync() 
        {
            await _repoContext.SaveChangesAsync();
        } 
    }
}
