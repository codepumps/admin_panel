# Admin Panel

<div align="center">
I had a homework about database. I wanted to make a different project. That's why I decided to make a web project but I didn't know about Asp.Net MVC.
I have tried to make a admin panel which add catagory, products, customers and sales control. When I started this project, I didn't know anything about backend. In addition, I used HTML, CSS, JAVASCRIPT and Bootstrap 4 for frontend.
Anyway Sum up, when I made this project , I learned many componentes and That's got me very happy.
Generally for backend I used Asp.NET MVC with Entity framework database first approches because this way was easy for connect MSSQL.
</div>


## View of the project

![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/admin.png)

![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/catagory.png)
![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/add_catagory.png)

![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/protuct.png)
![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/update.png)

![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/database.png)
![Markdown Logo](https://github.com/codepumps/admin_panel/blob/master/share_img/customer.png)


__I want to share this code which showing you How can you add image and How can you update image ? Because when I was coding, It was taking my time.__

__Add image__

```
[HttpPost]
public ActionResult CatagoryAdd(Tbl_Kategoriler image,HttpPostedFileBase file){
        
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
        }
```
__Update Image__

```
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
```
            
           

