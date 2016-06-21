using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class MUONTRAViewModel
    {
        public string MaPhieu { get; set; }
        public string MaDoAn { get; set; }
        public string DA_TenDoAn { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public DateTime? HanTra { get; set; }
        public string TrangThai { get; set; }
        public string MaDocGia { get; set; }
    }
}
