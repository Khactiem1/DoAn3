namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenTien")]
    public partial class ChuyenTien
    {
        [Key]
        [StringLength(200)]
        public string MaHoaDon { get; set; }

        [StringLength(50)]
        public string TenTaiKhoanChuyen { get; set; }

        [StringLength(50)]
        public string TenTaiKhoanNhan { get; set; }

        public int SoTien { get; set; }

        [StringLength(250)]
        public string LoiNhan { get; set; }

        public DateTime NgayChuyen { get; set; }
    }
}
