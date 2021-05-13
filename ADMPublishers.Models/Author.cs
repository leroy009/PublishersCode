using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMPublishers.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name ="Author ID")]
        [Required]
        [MaxLength(11)]
        public string AuthorId { get; set; }
        [MaxLength(250)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [MaxLength(250)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(12)]
        public string PhoneNumber { get; set; } //I will not use the Contact Object to store contact details. Time not on my side.
        [MaxLength(250)]
        [Required]
        public string Address { get; set; } //I will not use the Address Object to store contact details. Time not on my side.
        [MaxLength(250)]
        [Required]
        public string City { get; set; } //I will not use the Address Object to store contact details. Time not on my side.
        [MaxLength(2)]
        [MinLength(2)]
        [Required]
        public string State { get; set; } //I will not use the Address Object to store contact details. Time not on my side.
        [MaxLength(5)]
        [Required]
        public string Zip { get; set; } //I will not use the Address Object to store contact details. Time not on my side.

        /*
         * I will skip this which was meant to show contracts between author and publisher
        public int? PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }*/
        public int Contract { get; set; }

    }
}
