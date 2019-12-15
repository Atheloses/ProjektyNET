using Shared;
using System;
using System.Diagnostics;
using System.Timers;

namespace Plugins
{
    public class DebugTrace : IService
    {
        public DebugTrace()
        {
            Trace.Listeners.Add(new ListenerToFile());

            Debugger.Launch();
        }

        public Timer Interval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ServiceName => "Debug and Trace";

        public void Pause()
        {
            Debug.WriteLine("Debug - aplikace pozastavena");
            Trace.WriteLine("Trace - aplikace pozastavena");
        }

        public void Start()
        {
            Debug.WriteLineIf(true, "Debug - aplikace spuštěna");
        }

        public void Stop()
        {
            Trace.Indent();
            Debug.WriteLine("Debug - indent");
            int a = 5;
            Debug.Assert(a > 10, "Text hlášky");
        }
    }
}
