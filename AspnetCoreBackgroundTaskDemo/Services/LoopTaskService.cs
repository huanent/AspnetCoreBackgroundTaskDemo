using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCoreBackgroundTaskDemo.Services
{
    public class LoopTaskService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("循环任务开始");

            while (!cancellationToken.IsCancellationRequested)
            {
                Work(cancellationToken);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                }

            }
        }

        void Work(CancellationToken cancellationToken)
        {
            Console.WriteLine($"循环任务{DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("循环任务结束");
            return Task.CompletedTask;
        }
    }
}
