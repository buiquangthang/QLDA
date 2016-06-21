using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using System.Data.SqlClient;

namespace Models
{
    public class AccountModel
    {
        private QuanLyDoAnDbContext context = null;
        public AccountModel()
        {
            context = new QuanLyDoAnDbContext();
        }
        public bool Login(string userName, string password)
        {
            if (userName == null || password == null)
                return false;
            object[] sqlParams= {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Pass",password),
            };
            var res = context.Database.SqlQuery<bool>("Login_Admin @UserName,@Pass",sqlParams).SingleOrDefault();
            return res;
        }

        public int Create(NHANVIEN entity)
        {
            
            object[] parameters = {
                                      new SqlParameter("@HoTen",entity.HoTen),
                                      new SqlParameter("@DiaChi",entity.DiaChi),
                                      new SqlParameter("@TenDangNhap",entity.TenDangNhap),
                                      new SqlParameter("@QuyenHan",entity.QuyenHan)
                                  };
            NHANVIEN temp = checkDuplicate(entity.TenDangNhap);
            if (temp == null)
            {
                var res = context.Database.ExecuteSqlCommand("NhanVien_Insert @HoTen,@DiaChi,@TenDangNhap,@QuyenHan", parameters);
                return res;
            }
            return 0;
        }

        public NHANVIEN getByName(string userName)
        {
            return context.NHANVIENs.SingleOrDefault(x => x.TenDangNhap == userName);
        }

        public NHANVIEN getByID(string id)
        {
            return context.NHANVIENs.SingleOrDefault(x => x.MaNhanVien == id);
        }

        public int Update(NHANVIEN entity)
        {
            try
            {
                var nv = context.NHANVIENs.Find(entity.MaNhanVien);
                if (entity.MatKhau != nv.MatKhau)
                    return 0;
                if (checkDuplicate(entity.TenDangNhap) != null)
                    return 0;
                nv.HoTen = entity.HoTen;
                nv.DiaChi = entity.DiaChi;
                nv.TenDangNhap = entity.TenDangNhap;
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public int ChangePass(string id, string passOld, string passNew)
        {
            try
            {
                var nv = context.NHANVIENs.Find(id);
                string temp = nv.MatKhau;
                temp = "1";
                if (nv.MatKhau != passOld)
                    return 0;
                nv.MatKhau = passNew;
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public NHANVIEN checkDuplicate(string name){
            try{
                var nv = from p in context.NHANVIENs
                         where p.TenDangNhap == name
                         select p;
                foreach (NHANVIEN item in nv)
                {
                    return item;
                }
                return null;
            }catch(Exception){
                throw;
            }
        }

        public bool checkAdmin(string idNhanVien)
        {
            var nv = context.NHANVIENs.Find(idNhanVien);
            if (nv.QuyenHan.ToString() != "Nhân Viên" && nv.QuyenHan.ToString() != "Nhân viên" && nv.QuyenHan.ToString() != "nhân viên")
            {
                return false;
            }
            else return true;
        }
    }
}
