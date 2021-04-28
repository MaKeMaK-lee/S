using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblBook")]
    public class Book : IUniqueIdenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("szName")]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        [Column("szAuthor")]
        [MaxLength(100)]
        public string Author { get; set; }
        [Required]
        [Column("dtDate")]
        public DateTime Date { get; set; }
        [Required]
        [Column("enBookType")]
        public BookTypes BookType { get; set; }
        [Required]
        [Column("intPageCount")]
        public int PageCount { get; set; }
        [Required]
        [Column("enGenres")]
        public ArtGenres Genre { get; set; }
        
    }
}