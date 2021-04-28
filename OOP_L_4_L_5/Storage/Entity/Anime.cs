using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblAnime")]
    public class Anime : IUniqueIdenifiableEntity
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
        [Column("szStudio")]
        [MaxLength(100)]
        public string Studio { get; set; }
        [Required]
        [Column("dtDate")]
        public DateTime Date { get; set; }
        [Required]
        [Column("enAnimeType")]
        public AnimeTypes AnimeType { get; set; }
        [Required]
        [Column("intEpisodesCount")]
        public int EpisodesCount { get; set; }
        [Required]
        [Column("enGenre")]
        public ArtGenres Genre { get; set; }

    }
}
