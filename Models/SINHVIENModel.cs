using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using Models.ViewModel;
using PagedList;

namespace Models
{
    public class SINHVIENModel
    {
        private QuanLyDoAnDbContext context;
        public SINHVIENModel()
        {
            context = new QuanLyDoAnDbContext();
        }
        public IEnumerable<SINHVIEN_KHOA_ViewModel> ListAll(string searchString, int page, int pageSize)
        {
            var model = from a in context.DOCGIAs
                        join b in context.KHOAs
                        on a.MaKhoa equals b.MaKhoa
                        select new SINHVIEN_KHOA_ViewModel()
                        {
                            MaDocGia = a.MaDocGia,
                            HoTen = a.HoTen,
                            NgaySinh = a.NgaySinh,
                            MaKhoa = a.MaKhoa,
                            ChucVu = a.ChucVu,
                            DiaChi = a.DiaChi,
                            KHOA_TenKhoa = b.TenKhoa
                        };
            IQueryable<SINHVIEN_KHOA_ViewModel> temp = model;
            if (!string.IsNullOrEmpty(searchString))
            {
                temp = temp.Where(x => x.MaDocGia.Contains(searchString) || x.DiaChi.Contains(searchString) || x.KHOA_TenKhoa.Contains(searchString));
            }
            return temp.OrderBy(x => x.HoTen).ToPagedList(page, pageSize);

        }

        public int Create(DOCGIA entity)
        {
            try
            {
                context.DOCGIAs.Add(entity);
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public string getByMSSV(string id)
        {
            return context.PHIEUMUONs.Find(id).MaDocGia;
        }
        public int Extend(string id) 
        {
            try
            {
                var pm = context.PHIEUMUONs.Find(id);
                pm.HanTra = pm.HanTra.Value.AddMonths(3);
                pm.TrangThai = "Gia Hạn";
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }           
        }

        public int Check(string id)
        {
            try
            {
                var pm = context.PHIEUMUONs.Find(id);
                pm.NgayTra = DateTime.Now;
                pm.TrangThai = "Đã trả";
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DOCGIA ViewDetails(string id)
        {
            return context.DOCGIAs.Find(id);
        }

        public int Update(DOCGIA entity)
        {
            try
            {
                var sv = context.DOCGIAs.Find(entity.MaDocGia);
                sv.HoTen = entity.HoTen;
                sv.DiaChi = entity.DiaChi;
                sv.NgaySinh = entity.NgaySinh;
                sv.MatKhau = entity.MatKhau;
                sv.ChucVu = entity.ChucVu;
                sv.MaKhoa = entity.MaKhoa;
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
