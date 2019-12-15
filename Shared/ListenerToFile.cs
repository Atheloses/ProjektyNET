using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Shared
{
    public class ListenerToFile : TraceListener
    {

        public override void Write(string message)
        {
            //this.IndentLevel
            message = DateTime.Now.ToString("dd. MM. yyy HH:mm:ss.fff") + ": " + message;
            File.AppendAllText(Path.GetFullPath("./DebugTraceOutput.txt"), message);
        }

        public override void WriteLine(string message)
        {
            this.Write(message + '\n');
        }
    }
}
