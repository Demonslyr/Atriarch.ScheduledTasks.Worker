using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Atriarch.ScheduledTasks.Worker.Worker
{
    public class MicrocenterStockChecker : BackgroundService
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _clientfactory;
        private readonly ISmtpClient _smtpClient;

        public MicrocenterStockChecker(ILogger<MicrocenterStockChecker> logger, IHttpClientFactory clientfactory, ISmtpClient smtpClient)
        {
            _logger = logger;
            _clientfactory = clientfactory;
            _smtpClient = smtpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var client = _clientfactory.CreateClient();
                using var response = await client.GetAsync();
            }

            if (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError("Unknown cancellation, left loop");
            }
            _logger.LogInformation("Stopping by request. CancellationToken requested cancellation.");

        }
    }
}
