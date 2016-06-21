using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models;
using QuanLiDoAn.Common;
using QuanLiDoAn.Areas.Admin.Code;

namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class PHIEUMUONController : BaseController
    {
        //
        // GET: /Admin/PHIEUMUON/

        public ActionResult Index(string searchString, int page=1, int pageSize = 2)
        {
            var pm = new PHIEUMUONModel();
            var model = pm.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PHIEUMUON collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new PHIEUMUONModel();
                    collection.NgayMuon = (DateTime?)DateTime.Now;
                    collection.TrangThai = "N/A";
                    UserSession nv = (UserSession)Session[CommonConstants.ADMIN_SESSION];
                    collection.MaNhanVien = nv.ID;
                    int res = model.Create(collection);

                    if (res > 0){
                        return RedirectToAction("Print", collection);
                    }       
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công");
                    }
                }
                return View(collection);

            }
            catch
            {
                ModelState.AddModelError("", "Thêm mới không thành công");
                return View();
            }
        }

        public ActionResult Print(PHIEUMUON collection)
        {
            return RedirectToAction("Index");
        }
        //
        // GET: /Admin/DOAN/Edit/5

        public ActionResult Edit(String id)
        {
            var pm = new PHIEUMUONModel().ViewDetail(id);
            return View(pm);
        }

        //
        // POST: /Admin/DOAN/Edit/5

        [HttpPost]
        public ActionResult Edit(PHIEUMUON collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new PHIEUMUONModel();
                    if (collection.TrangThai == "Gia Hạn")
                    {
                        collection.HanTra = collection.HanTra.Value.AddMonths(3);
                    }

                    if (collection.TrangThai == "Đã trả")
                    {
                        collection.NgayTra = DateTime.Now;
                    }
                    int res = model.Update(collection);

                    if (res > 0)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật không thành công");
                    }
                }
                return View(collection);

            }
            catch
            {
                return View();
            }
        }
    }
}
