namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [StringLength(50)]
        public string MaHoaDon { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        public DateTime NgayGD { get; set; }

        public virtual TaiKhoan TaiKhoan1 { get; set; }
    }
}
