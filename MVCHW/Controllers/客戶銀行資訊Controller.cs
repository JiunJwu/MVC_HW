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
    public class 客戶銀行資訊Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository ClientData;
        客戶聯絡人Repository ClientContact;
        客戶銀行資訊Repository ClientBank;

        public 客戶銀行資訊Controller()
        {
            ClientData = RepositoryHelper.Get客戶資料Repository();
            ClientContact = RepositoryHelper.Get客戶聯絡人Repository(ClientData.UnitOfWork);
            ClientBank = RepositoryHelper.Get客戶銀行資訊Repository(ClientContact.UnitOfWork);
        }

        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            return View(ClientBank.All());
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CB = ClientBank.Get單一筆客戶銀行資料(id.Value);
            if (CB == null)
            {
                return HttpNotFound();
            }
            return View(CB);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(ClientData.All().OrderBy(p=>p.客戶名稱), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                ClientBank.Add(客戶銀行資訊);
                ClientBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(ClientData.All().OrderBy(p=>p.客戶名稱), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CB =  ClientBank.Get單一筆客戶銀行資料(id.Value);
            if (CB == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(ClientData.All().OrderBy(p => p.客戶名稱), "Id", "客戶名稱", CB.客戶Id);
            return View(CB);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, 客戶銀行資訊Edit 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var CB = ClientBank.Get單一筆客戶銀行資料(ID);
                CB.InjectFrom(客戶銀行資訊);
                ClientBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(ClientData.All().OrderBy(p => p.客戶名稱), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CB = ClientBank.Get單一筆客戶銀行資料(id.Value);
            if (CB == null)
            {
                return HttpNotFound();
            }
            return View(CB);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var CB = ClientBank.Get單一筆客戶銀行資料(id);
            ClientBank.Delete(CB);
            ClientBank.UnitOfWork.Commit();
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
