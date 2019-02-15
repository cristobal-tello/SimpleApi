using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.EventProcessor.Location
{
    public class LocationCache : ILocationCache
    {
        public IList<MemberLocation> GetMemberLocations(Guid teamId)
        {
            return new List<MemberLocation>
            {
                new MemberLocation()
                {
                    MemberID = Guid.NewGuid(),
                    Location = new GpsCoordinate() { Latitude = 1.0F, Longitude = 1.0f }
                },

                new MemberLocation()
                {
                    MemberID = Guid.NewGuid(),
                    Location = new GpsCoordinate() { Latitude = 2.0F, Longitude = 2.0f }
                },
                new MemberLocation()
                {
                    MemberID = Guid.NewGuid(),
                    Location = new GpsCoordinate() { Latitude = 3.0F, Longitude = 3.0f }
                }
            };
        }

        public void Put(Guid teamId, MemberLocation memberLocation)
        {
            throw new NotImplementedException();
        }
    }
}
