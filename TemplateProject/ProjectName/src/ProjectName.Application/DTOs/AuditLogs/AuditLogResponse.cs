using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.DTOs.Logs;
public class AuditLogResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Type { get; set; }
    public required string TableName { get; set; }
    public required DateTime DateTime { get; set; }
    public string? OldValues { get; set; }
    public required string NewValues { get; set; }
    public required string AffectedColumns { get; set; }
    public required string PrimaryKey { get; set; }
}
