using System.Security.Cryptography;

namespace ParPr_Lb6_Tests
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
                    m[i, j] = RandomNumberGenerator.GetInt32(0, 1024);
                }
            }
            return m;
        }

        internal static bool[,] GetBoolMatrix(int length)
        {
            bool[,] m = new bool[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    m[i, j] = RandomNumberGenerator.GetInt32(0, 2) == 1;
                }
            }
            return m;
        }

        internal static int[,] Add(int[,] first, int[,] second)
        {
            int length = first.GetLength(0);
            int[,] res = new int[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    res[i, j] = first[i, j] + second[i, j];
                }
            }

            return res;
        }

        internal static bool[,] Add(bool[,] first, bool[,] second)
        {
            int length = first.GetLength(0);
            bool[,] res = new bool[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    res[i, j] = first[i, j] | second[i, j];
                }
            }

            return res;
        }

        internal static int[,] Multiply(int[,] first, int[,] second)
        {
            int length = first.GetLength(0);
            int[,] res = new int[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        res[i, j] += first[i, k] * second[k, j];
                    }
                }
            }

            return res;
        }
    }
}
