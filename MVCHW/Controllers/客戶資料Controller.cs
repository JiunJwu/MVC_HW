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
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository ClientData;
        客戶聯絡人Repository ClientContact;
        客戶銀行資訊Repository ClientBank;

        public 客戶資料Controller()
        {
            ClientData = RepositoryHelper.Get客戶資料Repository();
            ClientContact = RepositoryHelper.Get客戶聯絡人Repository(ClientData.UnitOfWork);
            ClientBank = RepositoryHelper.Get客戶銀行資訊Repository(ClientContact.UnitOfWork);
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            return View(ClientData.All());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CD = ClientData.Get單一筆客戶資料(id.Value);
            if (CD == null)
            {
                return HttpNotFound();
            }
            return View(CD);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ClientData.Add(客戶資料);
                ClientData.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CD = ClientData.Get單一筆客戶資料(id.Value);
            if (CD == null)
            {
                return HttpNotFound();
            }
            return View(CD);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID , 客戶資料Edit 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var CD = ClientData.Get單一筆客戶資料(ID);
                CD.InjectFrom(客戶資料);
                ClientData.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CD = ClientData.Get單一筆客戶資料(id.Value);
            if (CD == null)
            {
                return HttpNotFound();
            }
            return View(CD);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var CD = ClientData.Get單一筆客戶資料(id);
            ClientData.Delete(CD);
            ClientData.UnitOfWork.Commit();
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
