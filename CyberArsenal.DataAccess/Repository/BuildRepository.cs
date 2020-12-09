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
                obj.CpuId = build.CpuId;
                obj.GpuId = build.GpuId;
                obj.RamId = build.RamId;
                obj.Storage = build.Storage;
                obj.CpuName = build.CpuName;
                obj.GpuName = build.GpuName;
                obj.RamName = build.RamName;
                obj.StorageName = build.StorageName;
                obj.StorageSecondary = build.StorageSecondary;
                obj.MotherBoard = build.MotherBoard;
                obj.PowerSupply = build.PowerSupply;
                obj.Case = build.Case;
                obj.Description = build.Description;
                obj.Score = build.Score;
                obj.Date = build.Date;
                obj.Private = build.Private;
            }

            _db.Update(obj);
        }
    }
}
