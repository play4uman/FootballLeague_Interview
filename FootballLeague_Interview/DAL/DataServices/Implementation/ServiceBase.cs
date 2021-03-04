using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Implementation
{
    public class ServiceBase
    {
        public ServiceBase(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected readonly FootballLeagueDbContext _dbContext;
    }
}
