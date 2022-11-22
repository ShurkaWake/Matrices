using System.Diagnostics;
using System.Security.Cryptography;

namespace ParPr_Lb6
{
    internal class Utils
    {
        internal static int[,] GetMatrix(int length)
        {
            int[,] m = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    m[i, j] = RandomNumberGenerator.GetInt32(0, 10);
                }
            }
            return m;
        }

        internal static double GetExecutionTakenTime(TimeFormat format, Action a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a.Invoke();
            sw.Stop();

            long ticks = sw.ElapsedTicks;
            switch (format)
            {
                case TimeFormat.Miliseconds:
                    return ticks / TimeSpan.TicksPerMillisecond;
                case TimeFormat.Seconds:
                    return ticks / TimeSpan.TicksPerSecond;
                case TimeFormat.Ticks:
                    return ticks;
                default:
                    return ticks;
            }
        }
    }

    enum TimeFormat
    {
        Miliseconds,
        Ticks,
        Seconds
    }
}
