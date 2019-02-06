using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService
{
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController : Controller
    {
        readonly ITeamRepository repository;
        readonly ILocationClient locationClient;

        public MembersController(ITeamRepository repository, ILocationClient locationClient)
        {
            this.repository = repository;
            this.locationClient = locationClient;
        }

        [HttpGet]
        public virtual IActionResult GetMembers(Guid teamID)
        {
            Team team = repository.Get(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team.Members);
            }
        }


        [HttpGet]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual async System.Threading.Tasks.Task<IActionResult> GetMemberAsync(Guid teamID, Guid memberId)
        {
            Team team = repository.Get(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                var q = team.Members.Where(m => m.ID == memberId);

                if (q.Any())
                {
                    return this.NotFound();
                }
                else
                {
                    Member member = q.First();
                    return this.Ok(
                            new LocatedMember {
                                ID = member.ID,
                                FirstName = member.FirstName,
                                LastName = member.LastName,
                                LastLocation = await this.locationClient.GetLatestForMember(member.ID) });
                }
            }
        }

        [HttpPut]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamID, Guid memberId)
        {
            Team team = repository.Get(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                var q = team.Members.Where(m => m.ID == memberId);

                if (q.Any())
                {
                    return this.NotFound();
                }
                else
                {
                    team.Members.Remove(q.First());
                    team.Members.Add(updatedMember);
                    return this.Ok();
                }
            }
        }

        [HttpPost]
        public virtual IActionResult CreateMember([FromBody]Member newMember, Guid teamID)
        {
            Team team = repository.Get(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                team.Members.Add(newMember);
                var teamMember = new { TeamID = team.ID, MemberID = newMember.ID };
                return this.Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
            }
        }

        [HttpGet]
        [Route("/members/{memberId}/team")]
        public IActionResult GetTeamForMember(Guid memberId)
        {
            var teamId = GetTeamIdForMember(memberId);
            if (teamId != Guid.Empty)
            {
                return this.Ok(new
                {
                    TeamID = teamId
                });
            }
            else
            {
                return this.NotFound();
            }
        }

        private Guid GetTeamIdForMember(Guid memberId)
        {
            foreach (var team in repository.List())
            {
                var member = team.Members.FirstOrDefault(m => m.ID == memberId);
                if (member != null)
                {
                    return team.ID;
                }
            }
            return Guid.Empty;
        }
    }
}