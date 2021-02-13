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
    public class SoftwareInfoController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: SoftwareInfo
        public ActionResult Index()
        {
            var model = db.Software.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("SoftwareForm");
        }

        [HttpPost]
        public ActionResult Save(Software software)
        {
            if(software.S_ID==0)
            {
                db.Software.Add(software);
            }
            else
            {
                //var updateSoftware = db.Software.Find(software.S_ID);
                //if(updateSoftware==null)
                //{
                //    return HttpNotFound();
                //}
                //db.Entry(updateSoftware).State = EntityState.Modified;
                db.Entry(software).State = System.Data.Entity.EntityState.Modified;
            }
         
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var model = db.Software.Find(id);
            if(model==null)
            {
                return HttpNotFound();
            }
            return View("SoftwareForm",model);
        }

        public  ActionResult Delete(int id)
        {
            var deleteSoftware = db.Software.Find(id);
            if(deleteSoftware==null)
            {
                return HttpNotFound();
            }
            db.Software.Remove(deleteSoftware);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}