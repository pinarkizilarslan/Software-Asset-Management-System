using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimVarlikYonetimSistemi.Models.DataContext;
using YazilimVarlikYonetimSistemi.Models.Model;

namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class DepartmentController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Department
        public ActionResult Index(int id)
        {
            var model = db.Department.Include(x => x.Usage).Include(x => x.Dependents).Where(x => x.D_ID==id).ToList();
            return View(model);
        }

        public ActionResult Info(int id)
        {
            var model = db.Usage.Include(a=>a.Software).Include(b=>b.Department).Where(x => x.D_ID == id).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> values = (from x in db.Software.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.S_Name,
                                               Value = x.S_ID.ToString()
                                           }).ToList();

            List<SelectListItem> valued = (from x in db.Department.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.D_Name,
                                               Value = x.D_ID.ToString()
                                           }).ToList();
            ViewBag.values = values;
            ViewBag.valued = valued;
            return View("UsageForm");
        }

        [HttpPost]
        public ActionResult Kaydet(Usage usage)
        {
            var software = db.Software.Where(x => x.S_ID == usage.Software.S_ID).FirstOrDefault();
            var department = db.Department.Where(x => x.D_ID == usage.Department.D_ID).FirstOrDefault();
            usage.Software = software;
            usage.Department = department;
            if (usage.Usage_ID == 0)
            {
                db.Usage.Add(usage);
            }
            else
            {

            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}