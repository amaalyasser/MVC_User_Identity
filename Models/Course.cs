using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Field is required")]
        [MinLength(5, ErrorMessage = "The length must be at least 5 chars")]
        [MaxLength(200, ErrorMessage = "The length cannot more than 200 chars.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Characters are allowed")]
        [UniqueName(error ="Change your Name")]
        public string Name { get; set; }
        [Display(Name = "Max Degree")]
        [Remote("CheckMaxDegree,Course", ErrorMessage = "Max Degree is not valid")]
        public int Degree { get; set; }
        [Remote("CheckMinDegree,Course", ErrorMessage = "Min Degree is not valid")]
        public int MinDegree { get; set; }
        public virtual ICollection<Instructor> Instructors { get;set; }
        public virtual ICollection<CourseResult> CourseResults { get; set; }
    }
}
