using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParPr_Lb6_Tests
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

        internal static int[,] Add(int[,] first, int[,] second)
        {
            int length = first.GetLength(0);
            int[,] res = new int[length, length];

            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    res[i, j] = first[i, j] + second[i, j];
                }
            }

            return res;
        }
    }
}
