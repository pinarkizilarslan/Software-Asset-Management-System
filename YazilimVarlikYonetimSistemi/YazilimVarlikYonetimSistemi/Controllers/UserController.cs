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
    public class UserController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: User
        public ActionResult Index()
        {
            var model = db.Dependent.Include(x => x.Department).Include(x => x.User).ToList();
            //var model = db.Database.SqlQuery<User>("SelectUser").ToList();
            //return View(model);
            return View(model);
        }
    }
}