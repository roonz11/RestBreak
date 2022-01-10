using RestTray.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTray.Repositories
{
    public interface ISessionRepository
    {
        Task<int> AddSessionAsync(Session session);
        Task<IList<Session>> GetSessionsAsync(int dateFilter = -1);

#if DEBUG
        Task RemoveAll();
#endif
    }
}
