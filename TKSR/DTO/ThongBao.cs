namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        [StringLength(250)]
        public string MaThongBao { get; set; }

        public string NoiDung { get; set; }

        public DateTime NgayThem { get; set; }
    }
}
