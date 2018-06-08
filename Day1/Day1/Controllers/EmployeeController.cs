using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Day1.Models;

namespace Day1.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeDBEntities db = new EmployeeDBEntities();
        // GET: Employee
        public ActionResult Index()
        {
            var data = (from konika in db.Employees select konika).ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string FirstName, string LastName, string Address)
        {
            Employee emp = new Employee();
            emp.FirstName = FirstName;
            emp.LastName = LastName;
            emp.Address = Address;
            db.Employees.Add(emp);
            db.SaveChanges();

            return View();
        }


        public ActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string FirstName, string LastName, string Address)
        {
            return View();
        }

        public ActionResult Delete(int Id)
        {
            return View();
        }

        public ActionResult Details(int Id)
        {
            var data = (from konika in db.Employees where konika.Id==Id select konika).SingleOrDefault();
            return View(data);           
        }
    }
}