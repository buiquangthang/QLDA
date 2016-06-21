namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUMUON")]
    public partial class PHIEUMUON
    {
        [Key]
        [StringLength(10)]
        public string MaPhieu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDocGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayMuon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTra { get; set; }

        
        [StringLength(10)]
        public string MaNhanVien { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDoAn { get; set; }

        [StringLength(10)]
        public string TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HanTra { get; set; }

        public virtual DOAN DOAN { get; set; }

        public virtual DOCGIA DOCGIA { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
