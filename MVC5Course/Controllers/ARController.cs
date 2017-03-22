using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View("123");
        }

        public ActionResult View2()
        {
            return PartialView("Index");
        }

        public ActionResult View3()
        {
            return View();
        }

        public ActionResult Content1()
        {
            return Content("<script>alert('新增成功');location.href='/';</script>");
        }

        public ActionResult File1()
        {
            //return File(@"C:\Users\James Pang\Downloads\狂新聞.png", "image/png");
            return File(Server.MapPath("~/Content/狂新聞.png"), "image/png");
        }

        public ActionResult File2()
        {
            //return File(@"C:\Users\James Pang\Downloads\狂新聞.png", "image/png");
            return File(Server.MapPath("~/Content/狂新聞.png"), "image/png", "圖片下載.png");
        }

        public ActionResult Json1()
        {
            return Json(new LoginVM() { Username = "will", Password = "111222"}, JsonRequestBehavior.AllowGet);
        }
    }
}