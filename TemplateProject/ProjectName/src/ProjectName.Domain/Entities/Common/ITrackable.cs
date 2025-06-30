using System;

namespace ProjectName.Domain.Entities.Common;

public interface ITrackable : IEntity
{
    DateTime CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
}

