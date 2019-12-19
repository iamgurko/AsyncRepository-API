using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IMeterRepository : IRepositoryBase<Meter>
    {
        IEnumerable<Meter> MetersByOwner(Guid ownerId);
    }
}
