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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcStokEntities2 db=new MvcStokEntities2();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.Tbl_Urunler.ToList().ToPagedList(sayfa, 6);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from k in db.Tbl_Kategoriler.ToList()
                select new SelectListItem()
                {
                    Text = k.Kategori_Ad,
                    Value = k.Kategori_Id.ToString()
                }).ToList();
            ViewBag.dgr = degerler;//Ürün ekle kısmına taşırken kullanıyoruz.
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler u1)
        {
            var ktg = db.Tbl_Kategoriler.Where(k => k.Kategori_Id == u1.Tbl_Kategoriler.Kategori_Id).FirstOrDefault();
            u1.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            List<SelectListItem> degerler = (from k in db.Tbl_Kategoriler.ToList()//Kategori dropdown için gerekli
                select new SelectListItem()
                {
                    Text = k.Kategori_Ad,
                    Value = k.Kategori_Id.ToString()
                }).ToList();
            ViewBag.dgr = degerler;//Ürün ekle kısmına taşırken kullanıyoruz.
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(Tbl_Urunler u)
        {
            var urun = db.Tbl_Urunler.Find(u.UrunId);
            urun.UrunAd = u.UrunAd;
            urun.Fiyat = u.Fiyat;
            urun.Marka = u.Marka;
            urun.Stok = u.Stok;
            //İlişkili tablolar olduğundan dolayı kategoriyi ayrı alacağız.
            var kategori = db.Tbl_Kategoriler.Where(k => k.Kategori_Id == u.Tbl_Kategoriler.Kategori_Id)
                .FirstOrDefault();
            urun.UrunKategori = kategori.Kategori_Id;
            db.SaveChanges();
           // urun.UrunKategori = u.UrunKategori;

           return RedirectToAction("Index");
        }
    }
}