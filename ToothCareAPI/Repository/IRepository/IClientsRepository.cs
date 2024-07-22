using ToothCareAPI.Model;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IClientsRepository : IRepository<Clients>
    {
        Task<Clients> UpdateAsync(Clients entity);
    }
}
