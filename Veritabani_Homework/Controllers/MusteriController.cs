using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;
using Veritabani_Homework.Models;
using Veritabani_Homework.Models.Entity;

namespace Veritabani_Homework.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcStokEntities2 db=new MvcStokEntities2();
        public ActionResult Index(string gelen,int sayfa=1)
        {
            var degerler = from d in db.Tbl_Musteriler select d;
            if (!string.IsNullOrEmpty(gelen))
            {
                degerler = degerler.Where(m => m.Musteri_Ad.Contains(gelen));
            }
            return View(degerler.ToList().ToPagedList(sayfa,7));
            //var musteriler = db.Tbl_Musteriler.ToList();
            //return View(musteriler);
        }

        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriEkle(Tbl_Musteriler m1)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
            }
            db.Tbl_Musteriler.Add(m1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            return View("MusteriGetir", musteri);
        }

        public ActionResult Guncelle(Tbl_Musteriler m)
        {
            var musteri = db.Tbl_Musteriler.Find(m.Musteri_No);
            musteri.Musteri_Ad = m.Musteri_Ad;
            musteri.Musteri_Soyad = m.Musteri_Soyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}