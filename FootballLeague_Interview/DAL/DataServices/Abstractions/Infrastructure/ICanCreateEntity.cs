using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Abstractions.Infrastructure
{
    public interface ICanCreateEntity<TAddRequest, TDto>
    {
        Task<(string url, TDto createdDto)> AddAsync(TAddRequest toAdd);
    }
}
