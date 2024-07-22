using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Repository
{
    public class ClientsRepository : Repository<Clients>, IClientsRepository
    {
        private readonly ApplicationDbContext _db;
        public ClientsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Clients> UpdateAsync(Clients entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
