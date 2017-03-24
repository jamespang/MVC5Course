using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Web.UI;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
    public class ProductsController : BaseController
    {
        // GET: Products
        public ActionResult Index(string keyword, string sortBy, /*int? page*/int pageNo = 1)
        {
            DoSearchOnIndex(keyword, sortBy, pageNo);
            return View();
        }

        [HttpPost]
        public ActionResult Index(Product[] data, string keyword, string sortBy, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var prod = repoProduct.Find(item.ProductId);
                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                    prod.Active = item.Active;
                }
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            DoSearchOnIndex(keyword, sortBy, pageNo);
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repoProduct.Find(id);
            if (TryUpdateModel(product, new string[] { "ProductName", "Stock" }))
            {

            }
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
            //return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = repoProduct.UnitOfWork.Context;
                db.Dispose();   
            }
            base.Dispose(disposing);
        }

        private void DoSearchOnIndex(string keyword, string sortBy, int pageNo)
        {
            ViewBag.keyword = keyword;

            var data = repoProduct.All().AsQueryable();

            if (!String.IsNullOrEmpty(keyword))
            {
                data = repoProduct.Where(p => p.ProductName.Contains(keyword));
            }
            if (sortBy == "+Price")
            {
                data = data.OrderBy(p => p.Price);
            }
            else if (sortBy == "-Price")
            {
                data = data.OrderByDescending(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.ProductId);
            }

            ViewData.Model = data.ToPagedList(pageNo, 10);
        }
    }
}
