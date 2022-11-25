using System.Diagnostics;
using System.Security.Cryptography;
using ShurkaWake.Matrices;

namespace ParPr_Lb6
{
    internal static class Utils
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
                    return ticks / (double)TimeSpan.TicksPerMillisecond;
                case TimeFormat.Seconds:
                    return ticks / (double)TimeSpan.TicksPerSecond;
                case TimeFormat.Ticks:
                    return ticks;
                default:
                    return ticks / (double)TimeSpan.TicksPerMicrosecond;
            }
        }

        internal static bool[,] GetBoolMatrix(int length)
        {
            bool[,] m = new bool[length, length];
            Parallel.For(0, length, (i) =>
            {
                bool curr = (System.Diagnostics.Stopwatch.GetTimestamp() & 1) == 1;
                for (int j = 0; j < length; j++)
                {
                    m[i, j] = curr;
                }
            });
            return m;
        }

        internal static void NumberMatrixAddTest(int length, int threads)
        {
            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~ Number Matrix Add ~#~#~#~#~#~");
            Console.WriteLine();

            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            IMatrix<int> resSeq = new Matrix<int>(1);
            IMatrix<int> resPar = new Matrix<int>(1);

            double elapsedSeq = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resSeq = sm1.SequentalAdd(sm2));
            double elapsedPar = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelAdd(sm2));
            double elapsedParFixed = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelAdd(sm2, threads));

            Console.WriteLine($"Sequental add [length = {length}] execution time: {elapsedSeq:F4} s");
            Console.WriteLine($"Parallel add [length = {length}] execution time: {elapsedPar:F4} s");
            Console.WriteLine($"Parallel [threads = {threads}] add [length = {length}] execution time: {elapsedParFixed:F4} s");

            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~");
            Console.WriteLine();
        }

        internal static void BoolMatrixAddTest(int length, int threads)
        {
            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~ Bool Matrix Add ~#~#~#~#~#~");
            Console.WriteLine();

            var sm1 = new BitMatrix(GetBoolMatrix(length));
            var sm2 = new BitMatrix(GetBoolMatrix(length));

            IMatrix<bool> resSeq = new BitMatrix(1);
            IMatrix<bool> resPar = new BitMatrix(1);

            double elapsedSeq = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
                () => resSeq = sm1.SequentalAdd(sm2));
            double elapsedPar = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
                () => resPar = sm1.ParallelAdd(sm2));
            double elapsedParFixed = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
                () => resPar = sm1.ParallelAdd(sm2, threads));

            Console.WriteLine($"Sequental add [length = {length}] execution time: {elapsedSeq:F4} ms");
            Console.WriteLine($"Parallel add [length = {length}] execution time: {elapsedPar:F4} ms");
            Console.WriteLine($"Parallel [threads = {threads}] add [length = {length}] execution time: {elapsedParFixed:F4} ms");

            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~");
            Console.WriteLine();
        }

        internal static void NumberMatrixMultiplyTest(int length, int threads)
        {
            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~ Number Matrix Multiply ~#~#~#~#~#~");
            Console.WriteLine();

            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            IMatrix<int> resSeq = new Matrix<int>(1);
            IMatrix<int> resPar = new Matrix<int>(1);

            double elapsedSeq = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resSeq = sm1.SequentalMultiply(sm2));
            double elapsedPar = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelMultiply(sm2));
            double elapsedParFixed = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelMultiply(sm2, threads));

            Console.WriteLine($"Sequental multiply [length = {length}] execution time: {elapsedSeq:F4} s");
            Console.WriteLine($"Parallel multiply [length = {length}] execution time: {elapsedPar:F4} s");
            Console.WriteLine($"Parallel [threads = {threads}] multiply [length = {length}] execution time: {elapsedParFixed:F4} s");

            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~");
            Console.WriteLine();
        }

        internal static void BoolMatrixMultiplyTest(int length, int threads)
        {
            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~ Bool Matrix Muliply ~#~#~#~#~#~");
            Console.WriteLine();

            var sm1 = new BitMatrix(GetBoolMatrix(length));
            var sm2 = new BitMatrix(GetBoolMatrix(length));

            IMatrix<bool> resSeq = new BitMatrix(1);
            IMatrix<bool> resPar = new BitMatrix(1);

            double elapsedSeq = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resSeq = sm1.SequentalMultiply(sm2));
            double elapsedPar = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelMultiply(sm2));
            double elapsedParFixed = Utils.GetExecutionTakenTime(TimeFormat.Seconds,
                () => resPar = sm1.ParallelMultiply(sm2, threads));

            Console.WriteLine($"Sequental multiply [length = {length}] execution time: {elapsedSeq:F4} s");
            Console.WriteLine($"Parallel multiply [length = {length}] execution time: {elapsedPar:F4} s");
            Console.WriteLine($"Parallel [threads = {threads}] multiply [length = {length}] execution time: {elapsedParFixed:F4} s");

            Console.WriteLine();
            Console.WriteLine("~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~#~");
            Console.WriteLine();
        }
    }

    enum TimeFormat
    {
        Miliseconds,
        Ticks,
        Seconds,
        Microseconds
    }
}
