using NowEvent.Models;

namespace NowEvent.Data.Repositories.RequestsRepository
{
    public interface IRequestRepository
    {
        void SaveRequest(Request request);
        IQueryable GetRequestsByUserId(string id);
        Task<Request> GetRequestById(int id);
    }
}
