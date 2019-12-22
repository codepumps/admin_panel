using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Veritabani_Homework.Models.Entity;//Projenin içindeki modele ulaştık (veritabanına)
using PagedList;//PagedList için gerekli kütüphaneleri ekledim. Sayfalama işlemi için gerekli
using PagedList.Mvc;
using System.Net;
using Veritabani_Homework.Models;
using Veritabani_Homework.Models.Entity;
using System.Data.Entity;

namespace Veritabani_Homework.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcStokEntities2 db =new MvcStokEntities2();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(sayfa,4);//Her sayfada 4 ürün listelemesini istiyorum.
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Tbl_Kategoriler image,HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;

            string extension = Path.GetExtension(file.FileName);

            string path = Path.Combine(Server.MapPath("~/Image/"), _filename);

            image.ImagePath = "~/Image/" + _filename;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength <= 1000000)
                {
                    db.Tbl_Kategoriler.Add(image);

                    if (db.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Image Added";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.msg = "File Size must be Equal or less than 1mb";
                }
            }
            else
            {
                ViewBag.msg = "Inavlid File Type";
            }
            return View();
            //string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            //string extension = Path.GetExtension(image.ImageFile.FileName);
            //fileName = fileName + DateTime.Now.ToString("yy-MM-dd") + extension;
            //image.ImagePath = "~/Image/" + fileName;
            //fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            //image.ImageFile.SaveAs(fileName);
            //using (db)
            //{
            //    db.Tbl_Kategoriler.Add(image);
            //    db.SaveChanges();
            //}
            //if (!ModelState.IsValid)
            //{
            //    return View("KategoriEkle");
            //}
            //ModelState.Clear();
            //return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var kategori=db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir", kategori);
        }
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kategori = db.Tbl_Kategoriler.Find(id);
            Session["ImagePath"] = kategori.ImagePath;

            if (kategori == null)
            {
                return HttpNotFound();
            }

            return View(kategori);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Kategoriler k, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                if (file!= null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;

                    string extension = Path.GetExtension(file.FileName);

                    string path = Path.Combine(Server.MapPath("~/Image/"), _filename);

                    k.ImagePath = "~/Image/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            db.Entry(k).State = EntityState.Modified;
                            string oldImgPath = Request.MapPath(Session["ImagePath"].ToString());

                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }
                                TempData["msg"] = "Güncellendi";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "File Size must be Equal or less than 1mb";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Geçersiz dosya tipi";
                    }
                }
                else
                {
                    k.ImagePath = Session["ImagePath"].ToString();
                    db.Entry(k).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg"] = "Güncellendi.";
                        return RedirectToAction("index");
                    }
                }
            }
                return View();
            }
            
            //var kategori = db.Tbl_Kategoriler.Find(k.Kategori_Id);
            //kategori.Kategori_Ad = k.Kategori_Ad;
            //kategori.ImageFile = k.ImageFile;
            //db.SaveChanges();
            //return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    var model = db.Tbl_Kategoriler.Find(k.Kategori_Id);
            //    string extension = model.ImagePath;
            //    if (image != null && image.ContentLength > 0)
            //    {
            //        var fileName = Path.GetFileName(image.FileName);
            //        string path = System.IO.Path.Combine(
            //            Server.MapPath("~/Image/"), fileName);
            //        image.SaveAs(path);
            //        model.ImagePath = "/Image/" + image.FileName;
            //        string fullPath = Request.MapPath("~" + extension);
            //        if (System.IO.File.Exists(fullPath))
            //        {
            //            System.IO.File.Delete(fullPath);
            //        }
            //    }

            //    model.ImagePath = k.ImagePath;
            //    db.SaveChanges();
            //}


        //new

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Kategoriler kategori = db.Tbl_Kategoriler.Find(id);
            Session["ImagePath"] = kategori.ImagePath;
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }
        
    }
}