using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCoreBackgroundTaskDemo.Services
{
    public class TimerTaskService : IHostedService, IDisposable
    {
        Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("定时任务开始");
            _timer = new Timer(Work, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        void Work(object state)
        {
            var token = (CancellationToken)state;
            if (token.IsCancellationRequested) return;
            Console.WriteLine($"定时任务运行{DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("定时任务结束");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
