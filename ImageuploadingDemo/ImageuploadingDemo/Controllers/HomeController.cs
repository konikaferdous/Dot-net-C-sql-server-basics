using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageuploadingDemo.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                Student1Entities db = new Student1Entities();
                //string ImageName = System.IO.Path.GetFileName(file.FileName); // fILE er name passo
                //// ekhane arekta line hobe j file copy korte hbe

                //string physicalPath = Server.MapPath("~/Image/" + ImageName);
                //// jodi ekhan theke path nite chao then image ei folder e copy kore age rakho
              
                int fileSizeInBytes = file.ContentLength;
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                tblStudent student = new tblStudent();
                student.FirstName = Request.Form["firstname"];
                student.LastName = Request.Form["lastname"];
                student.imagefile = data;
                db.tblStudents.Add(student);
                db.SaveChanges();

            }
            return RedirectToAction("../home/DisplayImage");
        }
        public ActionResult DisplayImage()
        {
            return View();
        }

    }
}