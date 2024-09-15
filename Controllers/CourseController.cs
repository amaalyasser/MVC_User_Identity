using Microsoft.AspNetCore.Mvc;
using Identity.Models;

namespace Identity.Controllers
{
    public class CourseController : Controller
    {
        MyContext _context = new MyContext();
        public IActionResult Index()
        {
            var Courses = _context.Courses.ToList();
            return View(Courses);
        }

        public IActionResult GetCoursebyId(int id)
        {
            var CourseDetails = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View("CourseDetails", CourseDetails);
        }

        public IActionResult Add(int Id)
        {
            var CourseDetails = _context.Courses.Where(x => x.Id == Id).FirstOrDefault();
            return View(CourseDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCourse(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(course.Id==0)
                    {
                        _context.Courses.Add(course);
                        _context.SaveChanges();
                    }else
                    {
                        _context.Courses.Update(course);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("Index");

                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("msg", ex.Message);
                
            }
            return View("Add", course);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Course crs = _context.Courses.Find(id);
            if (crs != null)
            {
                _context.Courses.Remove(crs);
                _context.SaveChanges();
            }
            return Content("Related Data Found");
        }


        public IActionResult CheckMinDegree(int MinDegree)
        {
            if(MinDegree>=60)
            {
                return Json(true);
            }else
            {
                return Json(false);
            }
        }

        public IActionResult CheckMaxDegree(int Degree)
        {
            if (Degree >= 60 && Degree<100)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
