using ToothCareAPI.Model;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IProceduresRepository : IRepository<Procedures>
    {
        Task<Procedures> UpdateAsync(Procedures entity);

    }
}
