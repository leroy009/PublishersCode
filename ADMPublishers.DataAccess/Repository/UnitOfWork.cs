using ADMPublishers.DataAccess.Data;
using ADMPublishers.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMPublishers.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) //Constructor to retrieve applicationDBContext from dependency Injection and we will inject this into all our repositories
        {
            _db = db;
            Author = new AuthorRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IAuthorRepository Author { get; private set; }
        public ISP_Call SP_Call { get; private set; }

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
