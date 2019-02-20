using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.LocationService.Repositories.Memory
{
    public class MemoryLocationRepository : ILocationRepository
    {
        readonly private Dictionary<Guid, SortedList<long, Location>> locationRecords;

        public MemoryLocationRepository()
        {
            if (locationRecords == null)
            {
                locationRecords = new Dictionary<Guid, SortedList<long, Location>>();
            }
        }

        public Location Add(Location locationRecord)
        {
            var memberRecords = getMemberRecords(locationRecord.MemberID);

            memberRecords.Add(locationRecord.Timestamp, locationRecord);
            return locationRecord;
        }

        public ICollection<Location> AllForMember(Guid memberId)
        {
            SortedList<long, Location> memberRecords = getMemberRecords(memberId);
            return memberRecords.Values.Where(l => l.MemberID == memberId).ToList();
        }

        public Location Delete(Guid memberId, Guid recordId)
        {
            var memberRecords = getMemberRecords(memberId);
            Location lr = memberRecords.Values.FirstOrDefault(l => l.ID == recordId);

            if (lr != null)
            {
                memberRecords.Remove(lr.Timestamp);
            }

            return lr;
        }

        public Location Get(Guid memberId, Guid recordId)
        {
            var memberRecords = getMemberRecords(memberId);

            Location lr = memberRecords.Values.FirstOrDefault(l => l.ID == recordId);
            return lr;
        }

        public Location Update(Location locationRecord)
        {
            return Delete(locationRecord.MemberID, locationRecord.ID);
        }

        public Location GetLatestForMember(Guid memberId)
        {
            var memberRecords = getMemberRecords(memberId);

            Location lr = memberRecords.Values.LastOrDefault();
            return lr;
        }

        private SortedList<long, Location> getMemberRecords(Guid memberId)
        {
            if (!locationRecords.ContainsKey(memberId))
            {
                locationRecords.Add(memberId, new SortedList<long, Location>());
            }

            foreach(var i in locationRecords)
            {
                System.Diagnostics.Debug.WriteLine("K={0}", i.Key);
                System.Diagnostics.Debug.WriteLine("Value={0}", locationRecords[i.Key]);
            }

            var list = locationRecords[memberId];
            return list;
        }
    }
}