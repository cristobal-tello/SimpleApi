using Models = StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Repositories
{
    public interface ILocationRepository
    {
        Models.Location Add(Models.Location locationRecord);
        Models.Location Update(Models.Location locationRecord);
        Models.Location Get(Guid memberId, Guid recordId);
        Models.Location Delete(Guid memberId, Guid recordId);
        Models.Location GetLatestForMember(Guid memberId);
        ICollection<Models.Location> AllForMember(Guid memberId);
    }
}
