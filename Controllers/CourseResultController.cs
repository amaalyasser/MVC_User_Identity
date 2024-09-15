using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Models;

namespace Identity.Controllers
{
    public class CourseResultController : Controller
    {
        MyContext _context = new MyContext();
        public IActionResult Index()
        {
            var crs_Result = _context.CourseResults.ToList();
            return View(crs_Result);
        }
        public IActionResult GetCourseResultbyId(int id)
        {
            var crs_Result = _context.CourseResults.Include(x => x.Course).Where(x => x.Id == id).FirstOrDefault();
            return View("CourseResultDetails", crs_Result);
        }

        public IActionResult Add(int Id)
        {
            var CrsResult = _context.CourseResults.Where(x => x.Id == Id).FirstOrDefault();
            var courses = _context.Courses.ToList();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");

            return View(CrsResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCourseResult(CourseResult CrsResult)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (CrsResult.Id == 0)
                    {
                        _context.CourseResults.Add(CrsResult);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.CourseResults.Update(CrsResult);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("msg", ex.Message);

            }
            return View("Add", CrsResult);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            CourseResult crsResult = _context.CourseResults.Find(id);
            if (crsResult != null)
            {
                _context.CourseResults.Remove(crsResult);
                _context.SaveChanges();
            }
            return Content("Related Data Found");
        }
    }
}
