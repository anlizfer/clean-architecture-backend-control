using System;
using System.Collections.Generic;

namespace CodeFirst.Core.DTOs.Health
{
    public class HealthCheckDtoReponse
    {
        public string Status { get; set; }
        public IEnumerable<StatusHealthsDtoResponse> HealthChecks { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}