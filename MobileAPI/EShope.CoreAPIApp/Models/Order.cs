using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShope.CoreAPIApp.Models
{
    //[Table("Order")]
    public class Order //: ITableData ////: EntityData //
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        [Key]
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CheckoutDateTime { get; set; }
        //public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        //#region ITableData

        ////public string Id { get; set; }
        //public byte[] Version { get; set; }
        //public DateTimeOffset? CreatedAt { get; set; }
        //public DateTimeOffset? UpdatedAt { get; set; }
        //public bool Deleted { get; set; }
        //#endregion
    }
}
