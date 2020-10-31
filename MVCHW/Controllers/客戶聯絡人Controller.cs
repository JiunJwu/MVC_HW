using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHW.Models;
using Omu.ValueInjecter;

namespace MVCHW.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶資料Repository ClientData;
        客戶聯絡人Repository ClientContact;
        客戶銀行資訊Repository ClientBank;

        public 客戶聯絡人Controller()
        {
            ClientData = RepositoryHelper.Get客戶資料Repository();
            ClientContact = RepositoryHelper.Get客戶聯絡人Repository(ClientData.UnitOfWork);
            ClientBank = RepositoryHelper.Get客戶銀行資訊Repository(ClientContact.UnitOfWork);
        }


        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            return View(ClientContact.All());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CC= ClientContact.Get單一筆客戶聯絡人資料(id.Value);

            if (CC == null)
            {
                return HttpNotFound();
            }
            return View(CC);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(ClientData.All().OrderBy(p => p.客戶名稱), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                if (ClientContact.確認信箱不重複(客戶聯絡人))
                {
                    ClientContact.Add(客戶聯絡人);
                    ClientContact.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "信箱重複";
            }

            ViewBag.客戶Id = new SelectList(ClientData.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CC = ClientContact.Get單一筆客戶聯絡人資料(id.Value);
            if (CC == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(ClientData.All(), "Id", "客戶名稱", CC.客戶Id);
            return View(CC);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID,客戶聯絡人Edit 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var CC = ClientContact.Get單一筆客戶聯絡人資料(ID);
                CC.InjectFrom(客戶聯絡人);
                ClientBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(ClientData.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CC = ClientContact.Get單一筆客戶聯絡人資料(id.Value);
            if (CC == null)
            {
                return HttpNotFound();
            }
            return View(CC);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var CC = ClientContact.Get單一筆客戶聯絡人資料(id);
            ClientContact.Delete(CC);
            ClientContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
