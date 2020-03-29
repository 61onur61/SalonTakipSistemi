using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Drawing;
using Gazi_Salon_Takip.Models.EntityFramework;
using System.Linq;
using System;

namespace Gazi_Salon_Takip.Controllers
{
    public class HomeController : Controller
    {
        private SalonTakipEntities1 db = new SalonTakipEntities1();

        public ActionResult Index()
        {
            
            return View();
        }
    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}