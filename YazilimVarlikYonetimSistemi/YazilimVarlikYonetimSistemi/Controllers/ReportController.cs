using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimVarlikYonetimSistemi.Models.DataContext;
using YazilimVarlikYonetimSistemi.Models.Model;

namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();

        public ActionResult Index()
        {
            var model = db.Department.ToList();
            return View(model);
        }

        public ActionResult Report(int id)
        {
            SqlParameter param1 = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand("ReportYear @id", param1);
            ViewBag.saat = DateTime.Now;
            return RedirectToAction("Index");
        }
    }
}