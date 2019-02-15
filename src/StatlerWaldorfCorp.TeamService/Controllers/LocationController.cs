using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Repositories;
using System;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    [Route("locations/{memberId}")]
    public class LocationController : Controller
    {
        readonly private ILocationRepository locationRepository;
        public LocationController(ILocationRepository repository)
        {
            locationRepository = repository;
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId, [FromBody] Location locationRecord)
        {
            locationRepository.Add(locationRecord);
            return this.Created($"/locations/{memberId}/{locationRecord.ID}", locationRecord);
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId)
        {
            return this.Ok(locationRepository.AllForMember(memberId));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberId)
        {
            return this.Ok(locationRepository.GetLatestForMember(memberId));
        }
    }
}