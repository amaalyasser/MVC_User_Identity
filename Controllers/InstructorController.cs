using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Models;
using Identity.Models;
using System.Net;

namespace Identity.Controllers
{
    public class InstructorController : Controller
    {
        MyContext _context=new MyContext();
        public IActionResult Index()
        {
            var Instructors = _context.Instructors.Include(x => x.Department).ToList();
            ViewBag.Flag = true;
            return View(Instructors);
        }
        public IActionResult GetInstructorbyId(int id)
        {          
            var InstructorDetails = _context.Instructors.Where(x=>x.Id==id).Include(x => x.Department).FirstOrDefault();
            ViewBag.Flag = false;
            return View("InstructorDetails", InstructorDetails);
        }
        public IActionResult Add(int Id)
        {
            Instructor InstructorObj = new Instructor();
            ViewBag.departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            if (Id!=0)
            {
                InstructorObj = _context.Instructors.Where(x => x.Id == Id).FirstOrDefault();
            }
           
           
            return View(InstructorObj);
        }
        [HttpPost]
        public IActionResult Submit(Instructor instructor)
        {
            if (instructor.Id==0) //Add
            {
                _context.Instructors.Add(instructor);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                _context.Instructors.Update(instructor);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Update(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
             _context.SaveChanges();

             return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Instructor instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
            }
            return Content("Related Data Found");
        }
    }
}

