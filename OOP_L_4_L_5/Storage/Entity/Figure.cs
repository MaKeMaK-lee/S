using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblFigure")]
    public class Figure
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("szName")]
        [MaxLength(150)]
        public string Name { get; set; }
        [Column("szSource")]
        [MaxLength(500)]
        public string Source { get; set; }
        [Required]
        [Column("intScale")]
        public int Scale { get; set; }
        [Required]
        [Column("intWeight")]
        public int Weight { get; set; }

    }
}
