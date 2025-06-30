using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.DTOs.Settings;
public class JWTSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; } = "ProjectName";
    public string Audience { get; set; } = "ProjectName";
    public double DurationInMinutes { get; set; } = 1440;
}
