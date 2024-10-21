using Microsoft.AspNetCore.Http; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementModels.Entities
{
    public class Library
    {

        public Library()
        {
            SelectedStudents = new List<string>();
            
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name charecter should not be more then 50")]
        public string Name { get; set; }

        public string Address { get; set; }
        public string Thana { get; set; }

        public bool IsSubscribed { get; set; }

        public string Gender { get; set; }
        public string SelectedCountry { get; set; }
        [NotMapped]
        public IFormFile UploadedFile { get; set; }
        public string FileName { get; set; }
        // Additional properties
        public DateTime DateOfBirth { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public int Age { get; set; }
        public int SatisfactionLevel { get; set; } // For range input
        public string FavoriteColor { get; set; } // For color picker

        public string? Students { get; set; } // multiple checkbox
         
        [NotMapped]
        public List<string>? SelectedStudents { get; set; }

    }
}
