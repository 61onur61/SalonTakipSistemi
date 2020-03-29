using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gazi_Salon_Takip.Models.EntityFramework;
using Gazi_Salon_Takip.Models.ValidationRules.FluentValidation;

namespace Gazi_Salon_Takip.Controllers
{
    public class UyeController : Controller
    {
        private SalonTakipEntities1 db = new SalonTakipEntities1();
        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Uye uye)
        {
            var validator = new UyeValidator();

            var result = validator.Validate(uye);

            if (result.Errors.Count > 0)
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                return View(uye);
            }

            var login = db.Uyes.Where(u => u.KullaniciAd == uye.KullaniciAd).SingleOrDefault();
            if(login.KullaniciAd == uye.KullaniciAd && login.KullaniciSifre == uye.KullaniciSifre)
            {
                Session["uyeID"] = login.uyeID;
                Session["KullaniciAd"] = login.KullaniciAd;
                Session["YetkiID"] = login.YetkiID;
                return RedirectToAction("Index", "Salons");
            }
            else
            {
                return View("hata");
            }
            
        }

        public ActionResult Logout()
        {
            Session["uyeID"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Salons");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Uye uye)
        {
            var validator = new UyeValidator();

            var result = validator.Validate(uye);

            if (result.Errors.Count > 0)
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                return View(uye);
            }

            if (ModelState.IsValid)
            {
                uye.YetkiID = 2;
                db.Uyes.Add(uye);
                db.SaveChanges();
                Session["uyeID"] = uye.uyeID;
                Session["KullaniciAd"] = uye.KullaniciAd;
                return RedirectToAction("Index", "Salons");
            }
            return View(uye);
        }
    }
}