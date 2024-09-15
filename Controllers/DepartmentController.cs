using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Models;

namespace Identity.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext _context = new MyContext();
        public IActionResult Index()
        {
            var Departments = _context.Departments.ToList();
            return View(Departments);
        }
        public IActionResult GetDepartmentbyId(int id)
        {
            var DepartmentDetails = _context.Departments.Where(x => x.Id == id).FirstOrDefault();
            return View("DepartmentDetails", DepartmentDetails);
        }
        public IActionResult Add(int Id)
        {
            Department depObj = new Department();
            if (Id != 0)
            {
                depObj = _context.Departments.Where(x => x.Id == Id).FirstOrDefault();
            }


            return View(depObj);
        }
        [HttpPost]
        public IActionResult Submit(Department department)
        {
            if (department.Id == 0) //Add
            {
                _context.Departments.Add(department);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                _context.Departments.Update(department);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Department dep = _context.Departments.Find(id);
            if (dep != null)
            {
                _context.Departments.Remove(dep);
                _context.SaveChanges();
            }
            return Content("Related Data Found");
        }

    }
}
