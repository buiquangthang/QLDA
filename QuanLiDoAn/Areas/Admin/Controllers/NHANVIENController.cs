using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using QuanLiDoAn.Common;
using QuanLiDoAn.Areas.Admin.Code;
using Models.Framework;
using QuanLiDoAn.Areas.Admin.Models;

namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class NHANVIENController : BaseController
    {
        //
        // GET: /Admin/NHANVIEN/

        public ActionResult Index()
        {
            var nv = new AccountModel();
            UserSession temp = (UserSession)Session[CommonConstants.ADMIN_SESSION];
            var model = nv.getByID(temp.ID);
            return View(model);
        }

        public ActionResult Refused()
        {
            return View();
        }

        // GET: /Admin/NHANVIEN/Edit/

        public ActionResult Edit(String id)
        {
            var pm = new AccountModel().getByID(id);
            return View(pm);
        }

        //
        // POST: /Admin/NHANVIEN/Edit/5

        [HttpPost]
        public ActionResult Edit(NHANVIEN collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
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

        // GET /ChangePass

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(ChangePassModel pw){
            try
            {
                if (ModelState.IsValid)
                {
                    if (pw.passNew != pw.reType)
                        ModelState.AddModelError("", "Mật khẩu không khớp");
                    else
                    {
                        var nv = new AccountModel();
                        UserSession temp = (UserSession)Session[CommonConstants.ADMIN_SESSION];
                        int res = nv.ChangePass(temp.ID, pw.passOld,pw.passNew);
                        if (res > 0)
                            return RedirectToAction("Index");
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật không thành công");
                        }
                    }
                    return View(pw);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return View();
        }


        // Get /creat
        public ActionResult Create()
        {
            var model = new AccountModel();
            UserSession nv = (UserSession)Session[CommonConstants.ADMIN_SESSION];
            if (model.checkAdmin(nv.ID))
            {
                return RedirectToAction("Refused");
            }
            else return View();   
        }

        [HttpPost]
        public ActionResult Create(NHANVIEN collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    int res = model.Create(collection);
                    if (res > 0)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công");
                    }
                }
                return View(collection);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

    }
}
