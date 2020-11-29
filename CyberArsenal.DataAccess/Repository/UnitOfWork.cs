using CyberArsenal.DataAccess.Data;
using CyberArsenal.DataAccess.Repository.IRepository;

namespace CyberArsenal.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Build = new BuildRepository(db);
        }

        public IBuildRepository Build { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
