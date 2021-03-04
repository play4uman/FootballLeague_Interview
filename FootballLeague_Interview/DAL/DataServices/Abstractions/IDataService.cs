using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Abstractions
{
    public interface IDataService<TEntity, TFindRequest, TAddRequest, TUpdateRequest>
    {
        Task<IEnumerable<TEntity>> FindAsync(TFindRequest findTeamParams);
        Task<string> AddAsync(TAddRequest toAdd);
        Task<string> UpdateAsync(TUpdateRequest toUpdate);
        Task DeleteAsync(object id);
    }
}
