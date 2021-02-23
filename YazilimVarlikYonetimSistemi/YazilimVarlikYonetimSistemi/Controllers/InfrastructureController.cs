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
    public class InfrastructureController : Controller
    {
         YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Infrastructure
        public ActionResult Index(Software software)
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
        public ActionResult Save(Infrastructure infra)
        {
            var software = db.Software.Where(x => x.S_ID == infra.Software.S_ID).FirstOrDefault();
            infra.Software = software;

            try
            {
                if (infra.I_ID == 0)
                {
                    SqlParameter param1 = new SqlParameter("@os", infra.OS);
                    SqlParameter param2 = new SqlParameter("@minRam", infra.Min_RAM);
                    SqlParameter param3 = new SqlParameter("@minStroage", infra.Min_Storeage);
                    SqlParameter param4 = new SqlParameter("@sID", infra.Software.S_ID);

                    db.Database.ExecuteSqlCommand("CreateInfra @os, @minRam, @minStroage, @sID", param1, param2, param3, param4);
                }
                else
                {
                    SqlParameter param1 = new SqlParameter("@os", infra.OS);
                    SqlParameter param2 = new SqlParameter("@minRam", infra.Min_RAM);
                    SqlParameter param3 = new SqlParameter("@minStroage", infra.Min_Storeage);
                    SqlParameter param4 = new SqlParameter("@sID", infra.Software.S_ID);
                    SqlParameter param5 = new SqlParameter("@id", infra.I_ID);

                    db.Database.ExecuteSqlCommand("UpdateInfra @id, @os, @minRam, @minStroage, @sID", param5, param1, param2, param3, param4);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
            var reservation = new Infrastructure { I_ID = id };
            try
            {
                SqlParameter param = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand("DeleteInfra @id", param);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}