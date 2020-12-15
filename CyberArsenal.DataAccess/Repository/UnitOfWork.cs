using CyberArsenal.DataAccess.Data;
using CyberArsenal.DataAccess.Repository.IRepository;
using System.Threading.Tasks;

namespace CyberArsenal.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Build = new BuildRepository(db);
            Part = new PartRepository(db);
            User = new ApplicationUserRepository(db);
        }

        public IBuildRepository Build { get; private set; }

        public IPartRepository Part{ get; private set; }

        public IApplicationUserRepository User { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
