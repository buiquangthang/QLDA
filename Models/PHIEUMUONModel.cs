using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using PagedList;
using Models.ViewModel;
using System.Data.SqlClient;

namespace Models
{
    public class PHIEUMUONModel
    {
        private QuanLyDoAnDbContext context = null;
        public PHIEUMUONModel()
        {
            context = new QuanLyDoAnDbContext();
        }

        public PHIEUMUON ViewDetail(String id)
        {
            return context.PHIEUMUONs.Find(id);
        }

        public IEnumerable<MUONTRAViewModel> ListAll(string searchString, int page, int pageSize)
        {
            //IQueryable<MUONTRAViewModel> model;
            updateOverdue();
            var model = from a in context.PHIEUMUONs
                        join b in context.DOANs
                        on a.MaDoAn equals b.MaDoAn
                        select new MUONTRAViewModel()
                        {
                            MaPhieu = a.MaPhieu,
                            MaDoAn = a.MaDoAn,
                            DA_TenDoAn = b.TenDoAn,
                            NgayMuon = a.NgayMuon,
                            NgayTra = a.NgayTra,
                            TrangThai = a.TrangThai,
                            HanTra = a.HanTra,
                            MaDocGia = a.MaDocGia
                        };
            IQueryable<MUONTRAViewModel> temp = model;
            if (!string.IsNullOrEmpty(searchString))
            {
                temp = temp.Where(x => x.DA_TenDoAn.Contains(searchString) || x.MaDocGia.Contains(searchString) || x.MaDoAn.Contains(searchString));
            }
            return temp.OrderBy(x => x.MaPhieu).ToPagedList(page,pageSize);
        }

        public int Create(PHIEUMUON entity)
        {
            object[] parameters ={
                new SqlParameter("@MaDocGia",entity.MaDocGia),
                new SqlParameter("@NgayMuon",entity.NgayMuon),
                new SqlParameter("@MaNhanVien",entity.MaNhanVien),
                new SqlParameter("@MaDoAn",entity.MaDoAn),
                new SqlParameter("@TrangThai",entity.TrangThai)               
            };

            PHIEUMUON temp = checkDuplicate(entity.MaDoAn);
            if (temp == null && overQuantity(entity.MaDocGia))
            {
                var res = context.Database.ExecuteSqlCommand("PhieuMuon_Insert @MaDocGia,@NgayMuon,@MaNhanVien,@MaDoAn,@TrangThai", parameters);
                return res;
            }
            else return 0;
        }
        public bool overQuantity(string idDocgia)
        {
            var model = from p in context.PHIEUMUONs
                        where p.MaDocGia == idDocgia && p.TrangThai != "Đã trả"
                        select p;
            int res = model.Count();
            if (res > 3)
                return false;
            else
                return true;
        }
        public PHIEUMUON checkDuplicate(string str)
        {
            try
            {
                var pm = from p in context.PHIEUMUONs
                         where p.MaDoAn == str && p.NgayTra == null
                         select p;
                foreach (PHIEUMUON item in pm)
                {
                    return item;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateOverdue()
        {
            DateTime date = DateTime.Now;
            var pm = from p in context.PHIEUMUONs
                         where p.NgayTra == null 
                         select p;
            foreach (PHIEUMUON item in pm)
            {
                if (item.HanTra < date)
                {
                    var phieumuon = context.PHIEUMUONs.Find(item.MaPhieu);
                    phieumuon.TrangThai = "Quá Hạn";
                }
            }
            context.SaveChanges();
        }

        public int Update(PHIEUMUON entity)
        {
            try
            {
                var pm = context.PHIEUMUONs.Find(entity.MaPhieu);
                pm.MaDocGia = entity.MaDocGia;
                pm.MaDoAn = entity.MaDoAn;
                pm.HanTra = entity.HanTra;
                pm.TrangThai = entity.TrangThai;
                pm.NgayTra = entity.NgayTra;
                context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public PHIEUMUON Print(string madoan)
        {
            var pm = from p in context.PHIEUMUONs
                     where p.MaDoAn == madoan && p.NgayTra == null
                     select p;
            foreach (PHIEUMUON item in pm)
            {
                return item;
            }
            return null;
        }
    }
}
