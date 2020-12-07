using CyberArsenal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CyberArsenal.DataAccess.Repository.IRepository
{
    public interface IPartRepository : IRepository<Part>
    {
        public void Update(Part part);
    }
}
