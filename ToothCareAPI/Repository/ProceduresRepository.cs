using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Repository
{
    public class ProceduresRepository : Repository<Procedures>, IProceduresRepository
    {
        private readonly ApplicationDbContext _db;

        public ProceduresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Procedures> UpdateAsync(Procedures entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
