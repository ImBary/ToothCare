using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Repository
{
    public class DoctorsRepository : Repository<Doctors>, IDoctorsRepository
    {
        private readonly ApplicationDbContext _db;
        public DoctorsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Doctors> UpdateAsync(Doctors entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
