﻿using FootballLeague_Interview.DAL.DataServices.Abstractions.Infrastructure;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Abstractions
{
    public interface ILeagueService : ICanFindEntity<LeagueDTO, FindLeagueParams>, 
        ICanCreateEntity<PostLeagueRequest, LeagueDTO>, ICanUpdateEntity<(string leagueName, UpdateLeagueRequest updateLeagueRequest)>, 
        ICanDeleteEntity<string>
    {
    }
}
