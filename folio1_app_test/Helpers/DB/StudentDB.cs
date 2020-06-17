using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Helpers.DB
{
    public class StudentDB
    {
        public int Id { get; set; }
        public int FolioClassId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(60, ErrorMessage = "First Name can't be longer than 60 characters")]
        public string FirstName { get; set; }
       
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(60, ErrorMessage = "Last Name can't be longer than 60 characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 55, ErrorMessage = "Can only be between 18 .. 55")]
        public int Age { get; set; }

        [Required(ErrorMessage = "GPA is required")]
        [Range(0, 10, ErrorMessage = "Please enter valid GPA")]
        public double GPA { get; set; }
    }
}
