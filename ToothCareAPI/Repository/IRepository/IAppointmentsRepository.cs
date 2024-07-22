using ToothCareAPI.Model;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IAppointmentsRepository : IRepository<Appointments>
    {
        Task<Appointments> UpdateAsync(Appointments entity);
    }
}
