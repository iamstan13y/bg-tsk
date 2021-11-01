using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bg_tsk
{
    public class ScopedService : IScopedService
    {
        private readonly ILogger<ScopedService> _logger;

        public ScopedService(ILogger<ScopedService> logger)
        {
            _logger = logger;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public void Write()
        {
            _logger.LogInformation("Scoped Service {Id}", Id);
        }
    }

    public interface IScopedService
    {
        void Write();
    }
}
