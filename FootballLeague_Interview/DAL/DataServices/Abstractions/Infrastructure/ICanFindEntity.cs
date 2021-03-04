using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Abstractions.Infrastructure
{
    public interface ICanFindEntity<TDto, TFindRequest>
    {
        Task<IEnumerable<TDto>> FindAsync(TFindRequest findTeamParams);
    }
}
