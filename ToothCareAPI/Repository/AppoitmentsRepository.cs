using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Repository
{
    public class AppoitmentsRepository : Repository<Appointments>, IAppointmentsRepository
    {
        private readonly ApplicationDbContext _db;
        public AppoitmentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Appointments> UpdateAsync(Appointments entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
