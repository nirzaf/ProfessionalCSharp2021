﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using WebSampleApp.Services;

namespace WebSampleApp
{
    public class CustomReadyCheck : IHealthCheck
    {
        private readonly HealthSample _healthSample;
        public CustomReadyCheck(HealthSample healthSample) => _healthSample = healthSample;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_healthSample.IsReady) return Task.FromResult(HealthCheckResult.Healthy("healthy"));
            else return Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
        }
    }
}
