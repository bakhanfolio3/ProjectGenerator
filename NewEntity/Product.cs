using MyNewApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewApp.Domain.Entities.Product
{
   public class Product : TrackableEntity, IEntity, ITrackable, ISoftDeletable, INameValueEntity<int>
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int quantity { get; set; }
    public bool? isAvailable { get; set; }
    public string? Manufacturer { get; set; }

    public bool IsDeleted { get; set; }

    public int Value => Name.ToString();
}
}