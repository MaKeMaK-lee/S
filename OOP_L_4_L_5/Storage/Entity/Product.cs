using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblProduct")]
    public class Product : IUniqueIdenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("gMatterId")]
        public Guid MatterId { get; set; }
        [Required]
        [Column("enProductType")]
        public ProductTypes ProductType { get; set; }
        [Required]
        [Column("intPrice")]
        public int Price { get; set; }
        [Required]
        [Column("intCountInStock")]
        public int CountInStock { get; set; }
        [Required]
        [Column("lopOrderProducts")]
        public List<OrderProduct> OrderProducts { get; set; }
        public Anime Anime { get; set; }
        public Book Book { get; set; }
        public Figure Figure { get; set; }
        public Product()
        {
            OrderProducts = new List<OrderProduct>();
        }
    }
}
