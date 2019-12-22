using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritabani_Homework.Models.Entity;

namespace Veritabani_Homework.Controllers
{
    public class AdminLoginController : Controller
    {
        //MvcStokEntities2 db=new MvcStokEntities2();
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Authorize()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Tbl_AdminLogin login)
        {
            
            using (MvcStokEntities2 db = new MvcStokEntities2())
            {
                var admin = db.Tbl_AdminLogin.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();
                if (admin==null)
                {
                    //ViewBag.msg = "Hatalı giriş yaptınız...";
                    return View("Index",login);//Yanlış girildiğinde
                }
                else
                {
                    Session["UserId"] = admin.AdminNo;
                    Session["name"] = admin.Name;
                    Session["surname"] = admin.Surname;
                    return RedirectToAction("Index","Kategori");
                }
                
            }              
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}