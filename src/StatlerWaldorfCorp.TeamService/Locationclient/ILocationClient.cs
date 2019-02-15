using StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public interface ILocationClient
    {
        Task<Location> GetLatestForMember(Guid memberId);
        Task<Location> AddLocation(Guid memberId, Location locationRecord);
    }
}