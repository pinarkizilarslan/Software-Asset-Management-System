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
    public class SoftwareInfoController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: SoftwareInfo
        public ActionResult Index()
        {
            var model = db.Database.SqlQuery<Software>("stp_SelectSoftware").ToList();
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

            try
            {
                SqlParameter param1 = new SqlParameter("@sName", software.S_Name);
                SqlParameter param2 = new SqlParameter("@agreementID", software.Agreement_ID);
                SqlParameter param3 = new SqlParameter("@licenseType", software.License_Type);
                SqlParameter param4 = new SqlParameter("@licenseOption", software.License_Option);
                SqlParameter param5 = new SqlParameter("@softwareCost", software.Software_Cost);
                SqlParameter param6 = new SqlParameter("@maintaCost", software.Maintenance_Cost);
                SqlParameter param7 = new SqlParameter("@inHouse", software.In_House);
                SqlParameter param8 = new SqlParameter("@openSourceCode", software.Open_Source_Code);
                SqlParameter param9 = new SqlParameter("@sVersion", software.S_Version);

                db.Database.ExecuteSqlCommand("stp_CreateSoftware @sName, @agreementID, @licenseType, @licenseOption, @softwareCost, @maintaCost,  @inHouse, @openSourceCode, @sVersion", param1, param2, param3, param4, param5, param6, param7, param8, param9);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = db.Software.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("Update", model);
        }

        [HttpPost]
        public ActionResult Update(int id, Software software)
        {
            var model = db.Software.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            try
            {
                SqlParameter param10 = new SqlParameter("@S_ID", id);
                SqlParameter param1 = new SqlParameter("@sName", software.S_Name);
                SqlParameter param2 = new SqlParameter("@agreementID", software.Agreement_ID);
                SqlParameter param3 = new SqlParameter("@licenseType", software.License_Type);
                SqlParameter param4 = new SqlParameter("@licenseOption", software.License_Option);
                SqlParameter param5 = new SqlParameter("@softwareCost", software.Software_Cost);
                SqlParameter param6 = new SqlParameter("@maintaCost", software.Maintenance_Cost);
                SqlParameter param7 = new SqlParameter("@inHouse", software.In_House);
                SqlParameter param8 = new SqlParameter("@openSourceCode", software.Open_Source_Code);
                SqlParameter param9 = new SqlParameter("@sVersion", software.S_Version);

                db.Database.ExecuteSqlCommand("stp_UpdateSoftware @S_ID, @sName, @agreementID, @licenseType, @licenseOption, @softwareCost, @maintaCost,  @inHouse, @openSourceCode, @sVersion", param10, param1, param2, param3, param4, param5, param6, param7, param8, param9);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        

        public ActionResult Delete(int id)
        {
            var deleteSoftware = db.Software.Find(id);
            if (deleteSoftware == null)
            {
                return HttpNotFound();
            }

            try
            {
                SqlParameter param = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand("stp_DeleteSoftware @id", param);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
           
        }


        public ActionResult Info(int id)
        {
            var model = db.Infrastructure.Where(x => x.S_ID == id).ToList();
            return View(model);
        }
    }
}