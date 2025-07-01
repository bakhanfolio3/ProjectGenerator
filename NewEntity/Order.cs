using MyNewApp.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewApp.Domain.Entities.Order
{
    public class Order : TrackableEntity, IEntity, ITrackable, ISoftDeletable, INameValueEntity<int>
    {
        public int Id { get; set; }


        // Additional properties
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail.OrderDetail> OrderDetails { get; set; } = new List<OrderDetail.OrderDetail>();

        public bool IsDeleted { get; set; }

        public int Value => Id;
        public string Name => $"Id{OrderDate.ToShortDateString()}";
}
}