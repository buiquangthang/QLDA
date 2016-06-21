using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using Models.ViewModel;

namespace Models
{
    public class DOCGIAModel
    {
        private QuanLyDoAnDbContext context = null;

        public DOCGIAModel()
        {
            context = new QuanLyDoAnDbContext();
        }

        public DOCGIA getByID(string id)
        {
            return context.DOCGIAs.Find(id);
        }

        public int Login(string id, string pass)
        {
            var dg = context.DOCGIAs.Find(id);
            if (dg == null) return 0;
            if (dg.MatKhau != pass) return 0;
            return 1;
        }

        public List<MUONTRAViewModel> Borrow(string idDocGia)
        {
            var model = from a in context.PHIEUMUONs
                        join b in context.DOANs
                        on a.MaDoAn equals b.MaDoAn
                        where a.MaDocGia == idDocGia && a.TrangThai != "Đã trả"
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
            return model.ToList();
        }
    }
}
