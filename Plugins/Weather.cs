using Shared;
using System;
using System.IO;
using System.Timers;

namespace Plugins
{
    public class Weather : IService
    {
        public string ServiceName => "DownloadWeather";

        private Timer _Interval;
        public Timer Interval { get => _Interval; set => _Interval = value; }

        public void Pause()
        {
            WriteDebugToFile("pause");
        }

        public void Start()
        {
            WriteDebugToFile("start");
        }

        public void Stop()
        {
            WriteDebugToFile("stop");
        }

        private void WriteDebugToFile(string p_Line)
        {

            string weatherPath = Path.GetFullPath("./Weather");
            if (!Directory.Exists(weatherPath))
                Directory.CreateDirectory(weatherPath);

            string text = DateTime.Now.ToString("dd. MM. yyy HH:mm:ss.fff") + ": " + p_Line + "\n";
            File.AppendAllText(Path.Combine(weatherPath, "Debug.txt"), text);
        }
    }
}
