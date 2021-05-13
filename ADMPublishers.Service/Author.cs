using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMPublishers.Service
{
    //Dot Net 4.7 cannot use Dot Net 5.0 Strange things I know
    public class Author
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Contract { get; set; }
    }
}
