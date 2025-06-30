using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectName.Domain.Entities.Common;
public abstract class TrackableEntity : ITrackable
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
    [StringLength(64)]
    public string? CreatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }
    [StringLength(64)]
    public string? UpdatedBy { get; set; }

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
