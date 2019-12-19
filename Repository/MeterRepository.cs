using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class MeterRepository : RepositoryBase<Meter>, IMeterRepository 
    { 
        public MeterRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Meter> MetersByOwner(Guid ownerId)
        { 
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList(); 
        }
    }
}
