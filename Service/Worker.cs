using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                Thread thr = new Thread(DownloadInThread);
                thr.Start();

                await Task.Delay(3_600_000, stoppingToken);
            }
        }

        #region DownloadThread

        private void DownloadInThread()
        {
            int tries = 3;
            for (int i = 1; i <= tries; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                _logger.LogInformation($"{DateTimeOffset.Now}: Starting file download.");
                var downloader = new Downloader(_logger);
                downloader.DownloadFinished += Downloader_DownloadFinished;
                var downloadedText = downloader.DownloadText("http://api.openweathermap.org/data/2.5/forecast?id=3068799&appid=e00cbc3727ae3a598dbb3868eeea5530&mode=xml&units=metric");


                if (!string.IsNullOrEmpty(downloadedText))
                {
                    sw.Stop();
                    _logger.LogInformation($"{DateTimeOffset.Now}: File downloaded sucessfully. {i}/{tries}, {new TimeSpan(sw.ElapsedMilliseconds)}");
                    break;
                }
                else
                {
                    sw.Stop();
                    _logger.LogInformation($"{DateTimeOffset.Now}: File not downloaded. {i}/{tries}, {new TimeSpan(sw.ElapsedMilliseconds)}");
                }
            }

        }

        private void Downloader_DownloadFinished(object source, DownloadFinishedEventArgs e)
        {
            var forecast = XmlParser.Parse(e.File);
            var image = Drawing.Draw(forecast,5);

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            var dict = new Dictionary<string, MemoryStream>();
            dict.Add("forecast.jpg", ms);
            new Email(_logger).SendEmail("pus0065@vsb.cz", dict);

            string fileName = "forecast.xml";
            new Downloader(_logger).Save(fileName, e.File);

        }

        #endregion DownloadThread

    }
}

