﻿using System;
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
    public class UserController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        private DataTable dataTable = new DataTable();
        // GET: User
        [Route("Kullanıcı/Bilgileri")]
        public ActionResult Index()
        {
            var model = db.Dependent.Include(x => x.Department).Include(x => x.User).ToList();
            //var model = db.Database.SqlQuery<User>("SelectUser").ToList();
            //return View(model);
            return View(model);
        }

        public ActionResult Info(int id)
        {
            SqlConnection database = new SqlConnection("data source=DAMLA\\MSSQLSERVER01;Database=YazilimVarlikYonetimSistemi;Integrated Security=True;");
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_SelectUserInfo", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);
            database.Close();
            return View(dataTable);
        }
    }
}