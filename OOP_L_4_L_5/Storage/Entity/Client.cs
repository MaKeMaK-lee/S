using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblClient")]
    public class Client : IUniqueIdenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("szName")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Column("szTelephone")]
        [MaxLength(12)]
        public string Telephone { get; set; }
        [Required]
        [Column("szEmail")]
        [MaxLength(100)]
        public string Email { get; set; }
        public ICollection<PaymentInfo> PaymentInfos { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Client()
        {
            PaymentInfos = new List<PaymentInfo>();
        }


    }
}
