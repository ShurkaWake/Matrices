using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParPr_Lb6
{
    public class BitMatrix : IMatrix<bool>
    {
        private readonly BitArray[] _values;
        private readonly int _length;

        public BitMatrix(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be higher than zero");
            }

            _values = new BitArray[n];
            for(int i = 0; i < n; i++)
            {
                _values[i] = new BitArray(n);
            }
            _length = n;
        }

        public BitMatrix(bool[,] values)
        {
            if (values.GetLength(0) != values.GetLength(1))
            {
                throw new ArgumentException("Matrix must have same dimensions");
            }

            int n = values.GetLength(0); 
            _values = new BitArray[n];
            for (int i = 0; i < n; i++)
            {
                _values[i] = new BitArray(n);
                for (int j = 0; j < n; j++)
                {
                    _values[i][j] = values[i, j];
                }
            }
            _length = n;
        }

        public bool this[int x, int y] 
        {
            get
            {
                ThrowExceptionIfNotInBounds(x, y);
                return _values[x][y];
            }
            set
            {
                ThrowExceptionIfNotInBounds(x, y);
                _values[x][y] = value;
            } 
        }

        private BitArray[] Values => _values;
        public int Length => _length;

        public IMatrix<bool> ParallelAdd(IMatrix<bool> matrix)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> ParallelAdd(IMatrix<bool> matrix, int threads)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> ParallelMultiply(IMatrix<bool> matrix)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> ParallelMultiply(IMatrix<bool> matrix, int threads)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> SequentalAdd(IMatrix<bool> matrix)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> SequentalMultiply(IMatrix<bool> matrix)
        {
            throw new NotImplementedException();
        }

        private bool IsInBounds(int index)
        {
            return index >= 0 && index < _length;
        }

        private bool IsInBounds(int x, int y)
        {
            return IsInBounds(x) && IsInBounds(y);
        }

        private void ThrowExceptionIfNotInBounds(int x, int y)
        {
            if (!IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException("Invalid indeces");
            }
        }

        private void ThrowExceptionIfNotEqualLength(IMatrix<bool> y)
        {
            if (Length != y.Length)
            {
                throw new ArgumentException("Matrices must be the same length");
            }
        }
    }
}
