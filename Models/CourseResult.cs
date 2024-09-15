using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class CourseResult
    {
        public int Id { get; set; }
        public int degree { get; set; }
        [Display(Name = "Courses")]
        [ForeignKey("Course")]
        public int courseId { get; set; }   
        public virtual Course Course { get; set; }
    }
}
