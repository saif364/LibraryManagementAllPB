using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementModels.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public string? BookDescription { get; set; }
        public string? BookType { get; set; }
        public string? BookAuthor {  get; set; } 


    }
}
