using CyberArsenal.DataAccess.Data;
using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;

namespace CyberArsenal.DataAccess.Repository
{
    public class BuildRepository : Repository<Build>, IBuildRepository
    {
        private readonly ApplicationDbContext _db;

        public BuildRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Build build)
        {
            var obj = _db.Builds.Find(build.Id);

            if(obj != null)
            {
                obj.Name = build.Name;
                obj.Cpu = build.Cpu;
                obj.Gpu = build.Gpu;
                obj.Ram = build.Ram;
                obj.Storage = build.Storage;
                obj.StorageSecondary = build.StorageSecondary;
                obj.MotherBoard = build.MotherBoard;
                obj.PowerSupply = build.PowerSupply;
                obj.Case = build.Case;
                obj.Description = build.Description;
            }

            _db.Update(obj);
        }
    }
}
