using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Shared
{
    public interface IService
    {
        Timer Interval { get; set; }
        string ServiceName { get; }
        void Start();
        void Stop();
        void Pause();
    }
}
