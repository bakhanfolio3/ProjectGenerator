using MyNewApp.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewApp.Domain.Entities.OrderDetail
{
    public class OrderDetail : TrackableEntity, IEntity, ITrackable, ISoftDeletable, INameValueEntity<int>
    {
        public int Id { get; set; }

        // Foreign key to Order
        public int OrderId { get; set; }
        public virtual Order.Order Order { get; set; }

        // Foreign key to Product
        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

        // Additional detail fields
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public bool IsDeleted { get; set; }

        [NotMapped]
        public int Value => Id;

        [NotMapped]
        public string Name => $"OrderDetail #{Id}";
    }
}
