using ToothCareAPI.Model;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IDoctorsRepository : IRepository<Doctors>
    {
        Task<Doctors> UpdateAsync(Doctors entity);
    }
}
