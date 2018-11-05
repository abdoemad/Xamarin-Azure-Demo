using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    [Table("User")]
    public class User
    {
        public User() {
            Orders = new HashSet<Order>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}