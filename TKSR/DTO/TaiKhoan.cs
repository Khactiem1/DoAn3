namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [StringLength(50)]
        public string tenTK { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string MatKhauC2 { get; set; }

        [StringLength(10)]
        public string LoaiTK { get; set; }

        [StringLength(20)]
        public string sdt { get; set; }

        public DateTime ngayLap { get; set; }

        [StringLength(100)]
        public string DacDiem { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public double SoDu { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int NumberLock { get; set; }

        [StringLength(250)]
        public string IP { get; set; }
    }
}
