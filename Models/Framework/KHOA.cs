namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHOA")]
    public partial class KHOA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHOA()
        {
            DOCGIAs = new HashSet<DOCGIA>();
        }

        [Key]
        [StringLength(10)]
        public string MaKhoa { get; set; }

        [StringLength(50)]
        public string TenKhoa { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCGIA> DOCGIAs { get; set; }
    }
}
