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
    public class InfrastructureController : Controller
    {
         YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Infrastructure
        public ActionResult Index()
        {
            var model = db.Infrastructure.Include(x=>x.Software).ToList();
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
            ViewBag.value = values;
            return View("InfraForm");
        }

        [HttpPost]
        public ActionResult Kaydet(Infrastructure infra)
        {
            var software = db.Software.Where(x => x.S_ID == infra.Software.S_ID).FirstOrDefault();
            infra.Software = software;
            if (infra.I_ID == 0)
            {
                db.Infrastructure.Add(infra);
            }
            else
            {
                //var model = db.Infrastructure.Find(infra.I_ID);
                db.Entry(infra).State = EntityState.Modified;
               
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
            ViewBag.value = values;
            var model = db.Infrastructure.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("InfraForm", model);
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

           
                var reservation = new Infrastructure { I_ID = id };
                db.Infrastructure.Attach(reservation);
                db.Infrastructure.Remove(reservation);
                db.SaveChanges();
            
            return RedirectToAction("Index");
        }

    }
}