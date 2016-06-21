using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using System.Data.SqlClient;
using PagedList;
using Models.ViewModel;

namespace Models
{
    public class DOANModel
    {
        private QuanLyDoAnDbContext context = null;
        public DOANModel()
        {
            context = new QuanLyDoAnDbContext();
        }

        public DOAN ViewDetail(String id)
        {
            return context.DOANs.Find(id);
        }

        public IEnumerable<DOAN> ListAll(string searchString, int page, int pageSize)
        {
            
            IQueryable<DOAN> model = context.DOANs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenDoAn.Contains(searchString) || x.SinhVienTH.Contains(searchString) || x.MaDoAn.Contains(searchString));
            }
            updateStatus();
            return model.OrderBy(x => x.MaDoAn).ToPagedList(page, pageSize);
        }

        public int Create(string tenDa, int? sotrang, DateTime? ngaybv, string maloai, string sv, string gv)
        {
            object[] parameters ={
                new SqlParameter("@TenDoAn",tenDa),
                new SqlParameter("@SoTrang",sotrang),
                new SqlParameter("@NgayBaoVe",ngaybv),
                new SqlParameter("@MaLoai",maloai),
                new SqlParameter("@SinhVien",sv),
                new SqlParameter("@GiangVien",gv)
            };

            var res = context.Database.ExecuteSqlCommand("DoAn_Insert @TenDoAn,@SoTrang,@NgayBaoVe,@MaLoai,@SinhVien,@GiangVien", parameters);
            return res;
        }

        public int Update(DOAN entity)
        {
            try
            {
                var doan = context.DOANs.Find(entity.MaDoAn);
                doan.TenDoAn = entity.TenDoAn;
                doan.SoTrang = entity.SoTrang;
                doan.NgayBaoVe = entity.NgayBaoVe;
                doan.SinhVienTH = entity.SinhVienTH;
                doan.GiangVienHD = entity.GiangVienHD;
                doan.TrangThai = entity.TrangThai;
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                //logging
                return 0;
            }
        }

        public bool Delete(String id)
        {
            try
            {
                var doan = context.DOANs.Find(id);
                context.DOANs.Remove(doan);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void updateStatus()
        {
            var model = from a in context.PHIEUMUONs
                        where a.TrangThai == "N/A" || a.TrangThai == "Gia Hạn"
                        select a;
            foreach (PHIEUMUON item in model)
            {
                var da = context.DOANs.Find(item.MaDoAn);
                da.TrangThai = "NA";
            }
            context.SaveChanges();
        }
    }
}
