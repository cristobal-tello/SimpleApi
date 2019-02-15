using Microsoft.EntityFrameworkCore;
using Models = StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Repositories.Location.PostgreSQL
{
    public class LocationRepository : ILocationRepository
    {
        readonly private LocationDbContext dbContext;

        public LocationRepository(LocationDbContext context)
        {
            this.dbContext = context;
        }

        public Models.Location Add(Models.Location locationRecord)
        {
            this.dbContext.Add(locationRecord);
            this.dbContext.SaveChanges();
            return locationRecord;
        }

        public ICollection<Models.Location> AllForMember(Guid memberId)
        {
            return this.dbContext.Locations.Where(lr => lr.MemberID == memberId).OrderBy(lr => lr.Timestamp).ToList();
        }

        public Models.Location Delete(Guid memberId, Guid recordId)
        {
            var locationRecord = this.Get(memberId, recordId);
            this.dbContext.Remove(locationRecord);
            this.dbContext.SaveChanges();
            return locationRecord;
        }

        public Models.Location Get(Guid memberId, Guid recordId)
        {
            return this.dbContext.Locations.Single(lr => lr.MemberID == memberId && lr.ID == recordId);
        }

        public Models.Location GetLatestForMember(Guid memberId)
        {
            return this.dbContext.Locations.Where(lr => lr.MemberID == memberId).OrderBy(lr => lr.Timestamp).Last();
        }

        public Models.Location Update(Models.Location locationRecord)
        {
            this.dbContext.Entry(locationRecord).State = EntityState.Modified;
            this.dbContext.SaveChanges();
            return locationRecord;
        }
    }
}
