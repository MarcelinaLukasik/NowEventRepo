namespace NowEvent.Services.ProgressService
{
    public interface IProgressService
    {
        Task<bool> CheckEventStatus(int id);
        Task<int> GetChecklistCount(int id);
    }
}
