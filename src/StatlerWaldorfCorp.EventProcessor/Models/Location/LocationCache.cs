using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.EventProcessor.Models.Location
{
    public class LocationCache : ILocationCache
    {
        private readonly IDictionary<Guid, List<MemberLocation>> membersLocation;
        public LocationCache()
        {
            this.membersLocation = new Dictionary<Guid, List<MemberLocation>>();
        }
        public IList<MemberLocation> GetMemberLocations(Guid teamId)
        {
            return membersLocation.ContainsKey(teamId) ? membersLocation[teamId] : new List<MemberLocation>();
        }

        public void Put(Guid teamId, MemberLocation memberLocation)
        {
            if (!membersLocation.ContainsKey(teamId))
            {
                this.membersLocation.Add(teamId, new List<MemberLocation>());
            }
            
            this.membersLocation[teamId].Add(memberLocation);
        }
    }
}
