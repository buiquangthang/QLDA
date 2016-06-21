namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOAN")]
    public partial class DOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOAN()
        {
            PHIEUMUONs = new HashSet<PHIEUMUON>();
        }

        [Key]
        [StringLength(10)]
        public string MaDoAn { get; set; }

        [StringLength(70)]
        public string TenDoAn { get; set; }

        public int? SoTrang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBaoVe { get; set; }

        [StringLength(10)]
        public string MaLoai { get; set; }

        [StringLength(50)]
        public string SinhVienTH { get; set; }

        [StringLength(50)]
        public string GiangVienHD { get; set; }

        [StringLength(5)]
        public string TrangThai { get; set; }

        public virtual PHANLOAI PHANLOAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUMUON> PHIEUMUONs { get; set; }
    }
}
