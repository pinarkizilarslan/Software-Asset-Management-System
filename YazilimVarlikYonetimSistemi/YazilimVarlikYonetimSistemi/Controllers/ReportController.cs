using System;
using System.Collections.Generic;
using System.Data;
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
        private DataTable dataTable = new DataTable();


        [Route("Raporlama")]
        public ActionResult Index()
        {
            var model = db.Department.ToList();
            return View(model);
        }
        
        public ActionResult TimeReport(int id)
        {
            SqlConnection database = new SqlConnection("data source=DAMLA\\MSSQLSERVER01;Database=YazilimVarlikYonetimSistemi;Integrated Security=True;");
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_TimeReport", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);
            database.Close();
            return View(dataTable);
        }
        
        public ActionResult CostReport(int id)
        {
            SqlConnection database = new SqlConnection("data source=DAMLA\\MSSQLSERVER01;Database=YazilimVarlikYonetimSistemi;Integrated Security=True;");
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_CostReport", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);
            database.Close();
            return View(dataTable);

        }
    }
}