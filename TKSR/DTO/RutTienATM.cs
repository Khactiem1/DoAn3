namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RutTienATM")]
    public partial class RutTienATM
    {
        [Key]
        [StringLength(200)]
        public string MaHoaDon { get; set; }

        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [StringLength(50)]
        public string NganHang { get; set; }

        [StringLength(50)]
        public string STK { get; set; }

        [StringLength(50)]
        public string ChuTK { get; set; }

        public int SoTien { get; set; }

        public DateTime NgayRut { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
    }
}
