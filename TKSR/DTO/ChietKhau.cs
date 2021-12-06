namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChietKhau")]
    public partial class ChietKhau
    {
        [Key]
        [StringLength(50)]
        public string MaChietKhau { get; set; }

        [StringLength(50)]
        public string NhaMang { get; set; }

        [StringLength(50)]
        public string MenhGia { get; set; }

        public double ChietKhauNap { get; set; }

        public double ChietKhauBan { get; set; }
    }
}
