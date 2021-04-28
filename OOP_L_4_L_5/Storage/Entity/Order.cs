using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblOrder")]
    public class Order : IUniqueIdenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("gClientId")]
        public Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        [Required]
        [Column("lopOrderProducts")]
        public ICollection<OrderProduct> OrderProducts { get; set; }
        [Required]
        [Column("enOrderStatus")]
        public OrderStatuses OrderStatus { get; set; }
        [Required]
        [Column("dtDateOfCompletion")]
        public DateTime DateOfCompletion { get; set; }
        public Order() 
        { 
            OrderProducts = new List<OrderProduct>(); 
        }


    }
}
