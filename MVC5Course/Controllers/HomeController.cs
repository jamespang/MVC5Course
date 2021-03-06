﻿using MVC5Course.ActionFilters;
using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(ArgumentException), View = "Error_ArgumentException")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [設定本控制器常用的ViewBag資料]
        public ActionResult About(int ex)
        {
            ViewBag.Message = "Your application description page.";

            if (ex == 1)
            {
                throw new Exception("ex");
            }

            return View();
        }

        [設定本控制器常用的ViewBag資料]
        [僅在本機開發測試用]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.RedirectFromLoginPage(login.Username, false);
                TempData["LoginResult"] = login;
                if (ReturnUrl.StartsWith("/"))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}