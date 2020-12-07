using CyberArsenal.DataAccess.Data;
using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;

namespace CyberArsenal.DataAccess.Repository
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        private readonly ApplicationDbContext _db;

        public PartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Part part)
        {
            var obj = _db.Parts.Find(part.Id);

            if(obj != null)
            {
                obj.Type = part.Type;
                obj.Name = part.Name;
                obj.Benchmark = part.Benchmark;
                obj.Score = part.Score;
                obj.Price = part.Price;
                obj.ReleaseDate = part.ReleaseDate;
            }

            _db.Update(obj);
        }
    }
}
