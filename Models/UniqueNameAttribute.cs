using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        public string error { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            MyContext context = new MyContext();
            string name = value.ToString();
            Course course=context.Courses.FirstOrDefault(x => x.Name == name);
            if (course == null)
            {
                return ValidationResult.Success;
            }
            return base.IsValid(value, validationContext);
        }
        
    }
}
