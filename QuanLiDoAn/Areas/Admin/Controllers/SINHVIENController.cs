using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;
namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class SINHVIENController : Controller
    {
        //
        // GET: /Admin/SINHVIEN/

        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var sv = new SINHVIENModel();
            var model = sv.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Details(String madocgia)
        {
            var dg = new DOCGIAModel();
            var model = dg.Borrow(madocgia);
            var sv = dg.getByID(madocgia);
            ViewBag.TenDocGia = sv.HoTen;
            ViewBag.MaDocGia = sv.MaDocGia;
            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult Create(DOCGIA collection)
        {
            var sv = new SINHVIENModel();
            int res = sv.Create(collection);
            if (res > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Thêm mới không thành công");
            }
            return View(collection);
        }

        public ActionResult Extend(string id)
        {
            var sv = new SINHVIENModel();
            int res = sv.Extend(id);
            if (res > 0)
            {
                string madocgia = sv.getByMSSV(id);
                return RedirectToAction("Details", "SINHVIEN", new { madocgia });
            }
            else
            {
                ModelState.AddModelError("","Không thành công");
                return View();
            }
        }

        public ActionResult Check(string id)
        {
            var sv = new SINHVIENModel();
            int res = sv.Check(id);
            if (res > 0)
            {
                string madocgia = sv.getByMSSV(id);
                return RedirectToAction("Details", "SINHVIEN", new { madocgia });
            }
            else
            {
                ModelState.AddModelError("", "Không thành công");
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            var sv = new SINHVIENModel();
            var model = sv.ViewDetails(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DOCGIA collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new SINHVIENModel();
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

        public ActionResult AboutMe()
        {
            return View();
        }
    }
}
