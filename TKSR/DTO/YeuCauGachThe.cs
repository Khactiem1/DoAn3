namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YeuCauGachThe")]
    public partial class YeuCauGachThe
    {
        [Key]
        [StringLength(200)]
        public string MaHoaDon { get; set; }

        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [StringLength(50)]
        public string NhaMang { get; set; }

        [StringLength(50)]
        public string MaThe { get; set; }

        [StringLength(50)]
        public string Seri { get; set; }

        public int MenhGia { get; set; }

        [StringLength(50)]
        public string TienThucNhan { get; set; }

        public DateTime NgayNap { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
    }
}
