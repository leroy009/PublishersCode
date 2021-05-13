using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMPublishers.DataAccess.Repository.IRepository
{
    //This is my wrapper for all
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Author { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}
