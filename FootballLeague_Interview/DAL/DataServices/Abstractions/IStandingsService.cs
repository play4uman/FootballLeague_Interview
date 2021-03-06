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
    public interface IStandingsService : ICanFindEntity<StandingsDTO, FindStandingsParams>, 
        ICanUpdateEntity<StandingsRowDTO>, ICanDeleteEntity<(string leagueName, string season)>
    {
        Task<(string url, StandingsDTO createdDto)> InitiateAsync(InitiateStandingsRequest initiateStandingsRequest);
        Task<(string url, StandingsDTO updatedDto)> UpdateMatchAsync(ResultDTO resultDTO, bool rollback);

    }
}
