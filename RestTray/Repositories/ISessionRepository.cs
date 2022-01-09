using RestTray.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTray.Repositories
{
    public interface ISessionRepository
    {
        Task<int> AddSessionAsync(Session session);
        Task<IEnumerable<Session>> GetSessionsAsync(int dateFilter = 0);

#if DEBUG
        Task RemoveAll();
#endif
    }
}
