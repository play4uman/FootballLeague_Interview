using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
using FootballLeague_Interview.Shared.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Abstractions
{
    public interface ITeamService : IDataService<Team, FindTeamParams, PostTeamRequest, Team>
    {
    }
}
