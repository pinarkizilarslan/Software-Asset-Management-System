using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimVarlikYonetimSistemi.Models.DataContext;

namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class InfrastructureController : Controller
    {
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        // GET: Infrastructure
        public ActionResult Index()
        {
            
            return View();
        }
    }
}