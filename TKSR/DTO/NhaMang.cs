namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaMang")]
    public partial class NhaMang
    {
        [Key]
        [StringLength(50)]
        public string TenNhaMang { get; set; }

        [StringLength(50)]
        public string Logo { get; set; }
    }
}
