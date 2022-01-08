using Microsoft.EntityFrameworkCore;
using RestTray.Data;
using RestTray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTray.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly RestBreakContext _dbContext;

        public SessionRepository(RestBreakContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddSessionAsync(Session session)
        {
            _dbContext.Add(session);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync(int dateFilter = 0)
        {
            return await _dbContext.Session
                .Where(x => dateFilter > 0 ? x.Date >= DateTime.UtcNow.AddDays(-dateFilter) : true)
                .ToListAsync(); ;
        }
    }
}
