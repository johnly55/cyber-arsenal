using CyberArsenal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CyberArsenal.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        public IBuildRepository Build { get; }

        public IPartRepository Part { get; }

        public IApplicationUserRepository User { get; }

        public void Save();
    }
}
