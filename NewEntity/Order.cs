using MyNewApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewApp.Domain.Entities.Order
{
   public class Order : TrackableEntity, IEntity, ITrackable, ISoftDeletable, INameValueEntity<int>
{
    public int Id { get; set; }

    // Foreign key to Product
    public int ProductId { get; set; }


    // Additional properties
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }

    public bool IsDeleted { get; set; }

    public int Value => Id;
}
}