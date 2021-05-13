using ADMPublishers.DataAccess.Data;
using ADMPublishers.DataAccess.Repository.IRepository;
using ADMPublishers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMPublishers.DataAccess.Repository
{
    public class AuthorRepository :  Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Author author)
        {
            var objFromDb = _db.Authors.FirstOrDefault(s => s.Id == author.Id);
            if (objFromDb != null)
            {
                objFromDb.FirstName = author.FirstName;
                objFromDb.LastName = author.LastName;
                objFromDb.AuthorId = author.AuthorId;
                objFromDb.PhoneNumber = author.PhoneNumber;
                objFromDb.Address = author.Address;
                objFromDb.City = author.City;
                objFromDb.State = author.State;
                objFromDb.Zip = author.Zip;
                objFromDb.Contract = author.Contract;

                //_db.SaveChanges();
            }
        }
    }
}
