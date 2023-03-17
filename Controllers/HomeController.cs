using Project.Database;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            var res = obj.Employees.ToList();
            List<Emp> objN = new List<Emp>();
            foreach(var item in res)
            {
                objN.Add(new Emp
                {
                    Id = item.Id,
                    Name = item.Name,
                    Gmail = item.Gmail,
                    Department = item.Department,
                    Salary = item.Salary,


                });
            }
            return View(objN);
        }

        public ActionResult Delete(int id)
        {
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            var delete = obj.Employees.Where(m => m.Id == id).First();
            obj.Employees.Remove(delete);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmp(Emp emp)
        {
            Employee empl = new Employee();
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            empl.Id = emp.Id;
            empl.Name = emp.Name;
            empl.Gmail = emp.Gmail;
            empl.Department = emp.Department;
            empl.Salary = emp.Salary;
            if (emp.Id == 0)
            {
                obj.Employees.Add(empl);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(empl).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            var edititem = obj.Employees.Where(m => m.Id == id).First();
            Emp emp = new Emp();
            emp.Id = edititem.Id;
            emp.Name = edititem.Name;
            emp.Gmail = edititem.Gmail;
            emp.Department = edititem.Department;
            emp.Salary = edititem.Salary;
            ViewBag.Id = edititem.Id;
            return View("AddEmp", emp);
        }
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(Log log)
        {
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            var login = obj.LoginInfoes.Where(m => m.UserId == log.UserId).FirstOrDefault();
            if(login == null)
            {
                TempData["mail"] = "Invalid Username";
            }
            else
            {
                if(log.UserId==login.UserId && log.Password == login.Password)
                {
                    FormsAuthentication.SetAuthCookie(login.UserId, false);
                    Session["mail"] = log.UserId;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Invalid password";
                }
            }
            return View();

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(Log log)
        {
            ZuhaibBaigEntities obj = new ZuhaibBaigEntities();
            LoginInfo loginInfo = new LoginInfo();
            loginInfo.ID = log.ID;
            loginInfo.UserId = log.UserId;
            loginInfo.Password = log.Password;
            obj.LoginInfoes.Add(loginInfo);
            obj.SaveChanges();
            return RedirectToAction("LogIn");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}