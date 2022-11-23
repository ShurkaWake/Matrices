using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParPr_Lb6
{
    public class BitMatrix
    {
        private readonly BitArray[] _valuesVertical;
        private readonly BitArray[] _valuesHorizontal;
        private readonly int _length;

        public BitMatrix(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be higher than zero");
            }

            _valuesVertical = new BitArray[n];
            _valuesHorizontal = new BitArray[n];
            for (int i = 0; i < n; i++)
            {
                _valuesVertical[i] = new BitArray(n);
                _valuesHorizontal[i] = new BitArray(n);
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
            _valuesHorizontal = new BitArray[n];
            _valuesVertical = new BitArray[n];

            for (int i = 0; i < n; i++)
            {
                _valuesHorizontal[i] = new BitArray(n);
                _valuesVertical[i] = new BitArray(n);

                for (int j = 0; j < n; j++)
                {
                    _valuesHorizontal[i][j] = values[i, j];
                    _valuesVertical[i][j] = values[j, i];
                }
            }
            _length = n;
        }

        public bool this[int x, int y] 
        {
            get
            {
                ThrowExceptionIfNotInBounds(x, y);
                return _valuesHorizontal[x][y];
            }
            set
            {
                ThrowExceptionIfNotInBounds(x, y);
                _valuesVertical[x][y] = value;
                _valuesHorizontal[y][x] = value;
            } 
        }

        public int Length => _length;

        public BitMatrix ParallelAdd(BitMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public BitMatrix ParallelAdd(BitMatrix matrix, int threads)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            BitMatrix result = new BitMatrix(Length);
            Parallel.For(0, threads, (threadId) =>
            {
                for (int i = threadId; i < Length; i += threads)
                {
                    result._valuesHorizontal[i] = _valuesHorizontal[i].Or(matrix._valuesHorizontal[i]);
                    result._valuesVertical[i] = _valuesVertical[i].Or(matrix._valuesVertical[i]);
                }
            });
            return result;
        }

        public BitMatrix ParallelMultiply(BitMatrix matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            BitMatrix result = new BitMatrix(Length);
            Parallel.For(0, Length, (i) =>
            {
                result._valuesHorizontal[i] = _valuesHorizontal[i].Or(matrix._valuesHorizontal[i]);
                result._valuesVertical[i] = _valuesVertical[i].Or(matrix._valuesVertical[i]);
            });
            return result;
        }

        public BitMatrix ParallelMultiply(BitMatrix matrix, int threads)
        {
            throw new NotImplementedException();
        }

        public BitMatrix SequentalAdd(BitMatrix matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            BitMatrix result = new BitMatrix(Length);
            for(int i = 0; i < Length; i++)
            {
                result._valuesHorizontal[i] = _valuesHorizontal[i].Or(matrix._valuesHorizontal[i]);
                result._valuesVertical[i] = _valuesVertical[i].Or(matrix._valuesVertical[i]);
            }
            return result;
        }

        public BitMatrix SequentalMultiply(BitMatrix matrix)
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

        private void ThrowExceptionIfNotEqualLength(BitMatrix y)
        {
            if (Length != y.Length)
            {
                throw new ArgumentException("Matrices must be the same length");
            }
        }
    }
}
