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
            var sorgu = db.Department.ToList();
            return View(sorgu);
        }
    }
}