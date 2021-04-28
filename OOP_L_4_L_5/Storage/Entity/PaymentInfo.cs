using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage.Entity
{
    [Table("tblPaymentInfo")]
    public class PaymentInfo : IUniqueIdenifiableEntity
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
        [Column("szCardNumber")]
        [MaxLength(16)]
        public string CardNumber { get; set; }
        [Required]
        [Column("szCvc")]
        [MaxLength(3)]
        public string Cvc { get; set; }
        [Required]
        [Column("gClientId")]
        public Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }








    }
}
