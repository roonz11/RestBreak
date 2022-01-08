using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SessionRepository> _logger;

        public SessionRepository(RestBreakContext dbContext,
            ILogger<SessionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<int> AddSessionAsync(Session session)
        {
            try
            {
                _dbContext.Add(session);
                var result = await _dbContext.SaveChangesAsync();
                if (result <= 0)
                    _logger.LogInformation("Failed to Add Session");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync(int dateFilter = 0)
        {
            return await _dbContext.Session
                .Where(x => dateFilter > 0 ? x.Date >= DateTime.UtcNow.AddDays(-dateFilter) : true)
                .ToListAsync(); ;
        }
    }
}
