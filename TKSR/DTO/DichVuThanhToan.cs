namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichVuThanhToan")]
    public partial class DichVuThanhToan
    {
        [StringLength(50)]
        public string MaDichVu { get; set; }

        [Key]
        [StringLength(50)]
        public string MaHoaDon { get; set; }

        public int? DonGia { get; set; }

        [StringLength(50)]
        public string KhuVuc { get; set; }

        [StringLength(50)]
        public string DacDiemKhac { get; set; }
    }
}
