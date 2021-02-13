using System;
using System.Collections.Generic;
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
            var model = db.Infrastructure.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("InfrastructureForm");
        }

        [HttpPost]
        public ActionResult Save(Infrastructure ınfrastructure)
        {
            if (ınfrastructure.S_ID == 0)
            {
                db.Infrastructure.Add(ınfrastructure);
            }
            else
            {
                db.Entry(ınfrastructure).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var model = db.Infrastructure.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("InfrastructureForm", model);
        }
    }
}