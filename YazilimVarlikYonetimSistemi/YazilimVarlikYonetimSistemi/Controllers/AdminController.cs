using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimVarlikYonetimSistemi.Models.DataContext;

namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class AdminController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.SoftwareCount = db.Software.Count();
            ViewBag.DepartmentCount = db.Department.Count();
            ViewBag.UserCount = db.User.Count();
            var sorgu = db.Department.ToList();
            return View(sorgu);
        }
    }
}