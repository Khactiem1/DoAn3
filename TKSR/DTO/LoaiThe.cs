namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiThe")]
    public partial class LoaiThe
    {
        [StringLength(50)]
        public string MaDichVu { get; set; }

        [Key]
        [StringLength(50)]
        public string LoaiTheNap { get; set; }

        public int? MenhGia { get; set; }

        public int? SoLuong { get; set; }
    }
}
