using NowEvent.Models;

namespace NowEvent.Data.Repositories.RequestsRepository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public RequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void SaveRequest(Request request)
        {
            _appDbContext.Requests.Add(request);
                _appDbContext.SaveChanges();
        }
        public IQueryable GetRequestsByUserId(string id)
        {
            var requests = _appDbContext.Requests.Where(x => x.ClientId == id);
            return requests;
        }
        public async Task<Request> GetRequestById(int id)
        {
            return _appDbContext.Requests.Find(id);
        }
    }
}
