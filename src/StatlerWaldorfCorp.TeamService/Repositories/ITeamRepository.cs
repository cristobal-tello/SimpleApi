using System;
using System.Collections.Generic;
using Models = StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Repositories
{
    public interface ITeamRepository
    {
        IEnumerable<Models.Team> List();
        Models.Team Get(Guid id);
        Models.Team Add(Models.Team team);
        Models.Team Update(Models.Team team);
        Models.Team Delete(Guid id);
    }
}