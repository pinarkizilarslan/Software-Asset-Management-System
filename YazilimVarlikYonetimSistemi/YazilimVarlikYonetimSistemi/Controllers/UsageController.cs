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
    public class UsageController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Usage
        public ActionResult Index()
        {
            var model = db.Usage.Include(x => x.Software).Include(a=>a.Department).ToList();
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
                var x = db.Usage.Find(usage.Usage_ID);
                x.Software_Key = usage.Software_Key;
                x.Update_Start_Date = usage.Update_Start_Date;
                x.Update_Finish_Date = usage.Update_Finish_Date;
                x.Usage_Time = usage.Usage_Time;
                x.ExpiryDate = usage.ExpiryDate;
                x.Acquisition_Date = usage.Acquisition_Date;
                x.Software = software;
                x.Department = department;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
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
            var model = db.Usage.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("UsageForm", model);
        }

        public ActionResult Delete(int id)
        {
            //var ınfrastructure = db.Infrastructure.Find(id);
            //if (ınfrastructure == null)
            //{
            //    return HttpNotFound();
            //}
            //db.Software.Remove(ınfrastructure);
            //db.SaveChanges();


            var reservation = new Usage { Usage_ID = id };
            db.Usage.Attach(reservation);
            db.Usage.Remove(reservation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}