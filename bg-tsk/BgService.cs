using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bg_tsk
{
    public class BgService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BgService> _logger;
        public BgService(IServiceProvider serviceProvider, ILogger<BgService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                _logger.LogInformation("From my BgService: ExecuteAsync {dateTime}", DateTime.Now);
                var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
                scopedService.Write();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("From my BgService: StopAsync {dateTime}", DateTime.Now);
            return base.StopAsync(cancellationToken);
        }
    }
}
