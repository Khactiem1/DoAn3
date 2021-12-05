namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheNap")]
    public partial class TheNap
    {
        [Key]
        [StringLength(50)]
        public string MaThe { get; set; }

        [StringLength(50)]
        public string MenhGia { get; set; }

        [StringLength(50)]
        public string Seri { get; set; }

        [StringLength(50)]
        public string NhaMang { get; set; }

        public DateTime NgayThem { get; set; }
    }
}
