using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class DOANController : BaseController
    {
        //
        // GET: /Admin/DOAN/

        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var iplDOAN = new DOANModel();
            var model = iplDOAN.ListAll(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        //
        // GET: /Admin/DOAN/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/DOAN/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/DOAN/Create

        [HttpPost]
        public ActionResult Create(DOAN collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new DOANModel();
                    int res = model.Create(collection.TenDoAn,collection.SoTrang,collection.NgayBaoVe,collection.MaLoai,collection.SinhVienTH,collection.GiangVienHD);
                    
                    if(res>0)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công");
                    }
                }
                return View(collection);
                
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/DOAN/Edit/5

        public ActionResult Edit(String id)
        {
            var doan = new DOANModel().ViewDetail(id);
            return View(doan);
        }

        //
        // POST: /Admin/DOAN/Edit/5

        [HttpPost]
        public ActionResult Edit(DOAN collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new DOANModel();
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

        //
        // GET: /Admin/DOAN/Delete/5

        public ActionResult Delete(String id,int i=1)
        {
            var doan = new DOANModel().ViewDetail(id);
            return View(doan);
        }

        //
        // POST: /Admin/DOAN/Delete/5

        [HttpPost]
        public ActionResult Delete(String id)
        {
            try
            {
                // TODO: Add delete logic here
                new DOANModel().Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
