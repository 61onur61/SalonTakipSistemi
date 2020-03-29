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
    public class ProgramsController : Controller
    {
        private SalonTakipEntities1 db = new SalonTakipEntities1();
        public DateTime tarih = DateTime.Now;
        // GET: Programs
        public ActionResult Index(int? Page,string a)
        {
            var program = db.Programs.Include(p => p.Salon);
            if (!string.IsNullOrEmpty(a))
            {
                program = program.Where(m => m.Program_Adı.Contains(a));
            }
            return View(program.ToList().ToPagedList(Page ?? 1,10));
        }
        public ActionResult ProgramListele()
        {
            return View(db.Programs.OrderByDescending(p => p.Program_Adı).Take(3));
        }
        public ActionResult Gun_Listesi()
        {
            var zaman = DateTime.Now.Date;




            var programs = db.Programs.Include(p => p.Salon).Where(p => p.Program_Tarih == zaman).ToList();
            ViewBag.sayi = programs.Count;
            //var programlar = db.Program.Where(p => p.Program_Tarih == DateTime.Now.Date).ToList();
            //var program = db.Program.Where(p => p.Program_Tarih == tarih).ToList();
           
            return View(programs);

        }

        // GET: Programs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // GET: Programs/Create
        public ActionResult Create()
        {
            ViewBag.SalonID = new SelectList(db.Salons, "ID", "Salon_Adi");
            return uyari();
        }

        // POST: Programs/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SalonID,Program_Tarih,Program_Baslangic,Program_Bitis,Program_Adı,Program_Sahip,Program_Aciklama,Is_Delete")] Program program)
        {
            

            if (ModelState.IsValid)
            {


                var s2 = db.Programs.Where(p => p.SalonID == program.SalonID).Where(p => p.Program_Tarih == program.Program_Tarih)
                    .Where(p => p.Program_Baslangic == program.Program_Baslangic)
                    .ToList();

                var s3 = db.Programs.Where(p => p.SalonID == program.SalonID).Where(p => p.Program_Tarih == program.Program_Tarih)
                   .Where(p => p.Program_Bitis == program.Program_Bitis)
                   .ToList();

              

                //var bas_veri = db.Programs.Where(p => p.Program_Baslangic.HasValue);
                //var bit_veri = db.Programs.Where(p => p.Program_Bitis.HasValue);

                //var d1 = program.Program_Bitis;
                //var d2 = program.Program_Baslangic;
                //TimeSpan time = d2.Value - d1.Value;

                var date = program.Program_Tarih;
                var date2 = program.Program_Baslangic;
                var date3 = program.Program_Bitis;
                

                if(s2.Count > 0 || s3.Count >0)
                {
                    return RedirectToAction("Uyari","Programs");
                    
                }
                else if(date2 >= date3)
                {
                    return RedirectToAction("Uyari", "Programs");
                }
                



            }

            ViewBag.SalonID = new SelectList(db.Salons, "ID", "Salon_Adi", program.SalonID);
            db.Programs.Add(program);
            db.SaveChanges();
            return RedirectToAction("Index","Programs");
        }

        // GET: Programs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalonID = new SelectList(db.Salons, "ID", "Salon_Adi", program.SalonID);
            return View(program);
        }

        // POST: Programs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SalonID,Program_Tarih,Program_Baslangic,Program_Bitis,Program_Adı,Program_Sahip,Program_Aciklama,Is_Delete")] Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalonID = new SelectList(db.Salons, "ID", "Salon_Adi", program.SalonID);
            return View(program);
        }

        // GET: Programs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Program program = db.Programs.Find(id);
            db.Programs.Remove(program);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult uyari()
        {
            ViewBag.tit = "Uyarı";
            ViewBag.uyari = "Baska Bir Saate Program Ekleyiniz.Cunku Bu Saat Aralıgında Ilgili Salon Dolu";
            ViewBag.icon = "warning";
            ViewBag.confirmButtonText = "Tamam";
            return View();
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
