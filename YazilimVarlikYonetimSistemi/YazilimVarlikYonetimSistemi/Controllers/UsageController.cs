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
        public ActionResult Save(Usage usage)
        {
            var software = db.Software.Where(x => x.S_ID == usage.Software.S_ID).FirstOrDefault();
            var department = db.Department.Where(x => x.D_ID == usage.Department.D_ID).FirstOrDefault();
            usage.Software = software;
            usage.Department = department;
            
            try
            {
                if(usage.Usage_ID==0)
                {
                    SqlParameter param1 = new SqlParameter("@SoftwareKey", usage.Software_Key);
                    SqlParameter param3 = new SqlParameter("@Expirydate", usage.ExpiryDate);
                    SqlParameter param4 = new SqlParameter("@AcquisitionDate", usage.Acquisition_Date);

                    SqlParameter param5 = new SqlParameter("@UpdateStartDate", usage.Update_Start_Date);
                    SqlParameter param6 = new SqlParameter("@UpdateFinishDate", usage.Update_Finish_Date);
                    SqlParameter param7 = new SqlParameter("@DID", usage.Department.D_ID);
                    SqlParameter param8 = new SqlParameter("@SID", usage.Software.S_ID);

                    db.Database.ExecuteSqlCommand("stp_CreateUsage @SoftwareKey, @Expirydate, @AcquisitionDate, @UpdateStartDate, @UpdateFinishDate, @DID, @SID", param1, param3, param4, param5, param6, param7, param8);
                }
                else
                {
                    SqlParameter param1 = new SqlParameter("@SoftwareKey", usage.Software_Key);
                    SqlParameter param3 = new SqlParameter("@Expirydate", usage.ExpiryDate);
                    SqlParameter param4 = new SqlParameter("@AcquisitionDate", usage.Acquisition_Date);

                    SqlParameter param5 = new SqlParameter("@UpdateStartDate", usage.Update_Start_Date);
                    SqlParameter param6 = new SqlParameter("@UpdateFinishDate", usage.Update_Finish_Date);
                    SqlParameter param7 = new SqlParameter("@DID", usage.Department.D_ID);
                    SqlParameter param8 = new SqlParameter("@SID", usage.Software.S_ID);
                    SqlParameter param9 = new SqlParameter("@id", usage.Usage_ID);

                    db.Database.ExecuteSqlCommand("stp_UpdateUsage @id, @SoftwareKey, @Expirydate, @AcquisitionDate, @UpdateStartDate, @UpdateFinishDate, @DID, @SID", param9, param1, param3, param4, param5, param6, param7, param8);
                
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


            var reservation = new Usage { Usage_ID = id };
            try
            {
                SqlParameter param = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand("stp_DeleteUsage @id", param);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}