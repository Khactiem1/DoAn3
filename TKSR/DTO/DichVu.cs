namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichVu")]
    public partial class DichVu
    {
        [Key]
        [StringLength(50)]
        public string MaDichVu { get; set; }

        [StringLength(50)]
        public string TenKhachHang { get; set; }

        public int? TienThanhToan { get; set; }

        [StringLength(50)]
        public string TenDichVu { get; set; }

        [StringLength(50)]
        public string DoiTac { get; set; }

        public DateTime NgayThem { get; set; }
    }
}
