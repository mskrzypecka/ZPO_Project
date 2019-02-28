using System.Diagnostics;

namespace ZPO_Projekt.Helpers
{
    public class DebugLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Debug.WriteLine($"{level}:\n\t{message}");
        }
    }
}