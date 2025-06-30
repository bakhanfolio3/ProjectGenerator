using ProjectName.Application.DTOs;
using ProjectName.Application.DTOs.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;

public interface ILogRepository
{
    Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId);

    Task AddLogAsync(string action, string userId);
}
