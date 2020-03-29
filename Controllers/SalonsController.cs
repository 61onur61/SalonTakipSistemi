using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gazi_Salon_Takip.Models.EntityFramework;
using PagedList;
using PagedList.Mvc;

namespace Gazi_Salon_Takip.Controllers
{
    public class SalonsController : Controller
    {
        private SalonTakipEntities1 db = new SalonTakipEntities1();

        public ActionResult SalonListele()
        {
            //son eklenen 5 salon
            return View(db.Salons.OrderByDescending(s => s.Salon_Adi).Take(3));
        }

        public ActionResult SalonDoluluk()
        {
            //En Dolu 5 Salon
            var salons = db.Salons.Include(s => s.Programs).ToList().Take(3);
            var salon = salons.OrderByDescending(s => s.Programs.Count).ToList();
            return View(salon);
        }

        public ActionResult Work(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var program = db.Programs;
            ViewBag.program_id = id;

            var sayi = program.ToList().Count;

            return View(program.ToList());
        }


        // GET: Salons
        public ActionResult Index(int? Page)
        {
            return View(db.Salons.ToList().ToPagedList(Page ?? 1,8));
        }

        // GET: Salons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // GET: Salons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salons/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Salon_Adi,Salon_Adresi,Salon_Kontenjan,Salon_Iletisim,SalonBilgisi,Is_Delete")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Salons.Add(salon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salon);
        }

        // GET: Salons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: Salons/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Salon_Adi,Salon_Adresi,Salon_Kontenjan,Salon_Iletisim,Is_Delete")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        // GET: Salons/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.sss = "Silmek istediginize eminmisiniz?";
            ViewBag.icon = "question";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nes = db.Programs.Where(p => p.SalonID == id).ToList();
            var nesne = from s in db.Programs where s.SalonID == id select s;
            if (nes.Count()>0)
            {
                ViewBag.sss = "Bu salona baglı programlar var ilk olarak bu salona baglı programları siliniz!!!";
                ViewBag.icon = "warning";
                Salon pr = db.Salons.Find(id);
                return View(pr);
            }
            Salon salon = db.Salons.Find(id);
            db.Salons.Remove(salon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
