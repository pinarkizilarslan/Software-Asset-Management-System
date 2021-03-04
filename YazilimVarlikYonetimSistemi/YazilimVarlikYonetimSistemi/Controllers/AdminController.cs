using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimVarlikYonetimSistemi.Models.DataContext;
using YazilimVarlikYonetimSistemi.Models.Model;

namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class AdminController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.UsageExpired = db.Database.SqlQuery<Usage>("spt_UsageExpired").ToString();
            ViewBag.SoftwareCount = db.Software.Count();
            ViewBag.DepartmentCount = db.Department.Count();
            ViewBag.UserCount = db.User.Count();
            var sorgu = db.Department.ToList();
            return View(sorgu);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login _login)
        {
            var login = db.Login.Where(x => x.L_Email == _login.L_Email).SingleOrDefault();
            if(login.L_Email== _login.L_Email && login.L_Password == _login.L_Password)
            {
                Session["adminid"] = login.Login_ID;
                Session["email"] = login.L_Email;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Warning = "Email ya da parola yanlış!";
            return View(_login);
        }

        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}