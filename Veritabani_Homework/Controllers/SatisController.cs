using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritabani_Homework.Models;
using Veritabani_Homework.Models.Entity;

namespace Veritabani_Homework.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        MvcStokEntities2 db=new MvcStokEntities2();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> degerler = (from k in db.Tbl_Urunler.ToList()
                                             select new SelectListItem()
                                             {
                                                 Text = k.UrunAd,
                                                 Value = k.UrunId.ToString()
                                             }).ToList();
            ViewBag.degr = degerler;
            return View(degerler.ToList());
        }

        [HttpPost]
        public ActionResult YeniSatis(Tbl_Satislar s)
        {
            db.Tbl_Satislar.Add(s);
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult Listele()
        {
            var liste = db.Tbl_Satislar.ToList();

            return View(liste);
        }
            
    }
}