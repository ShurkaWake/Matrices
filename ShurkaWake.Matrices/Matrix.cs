using System.Numerics;

namespace ShurkaWake.Matrices
{
    public class Matrix<T> : IMatrix<T> where T : struct, INumber<T>
    {
        readonly int chunkSize = Vector<T>.Count;
        readonly Vector<T>[,] _values;
        readonly int _length;

        public Matrix(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be higher than zero");
            }

            _length = n;
            _values = new Vector<T>[n, (int) Math.Ceiling(n / (double) chunkSize)];

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < _values.GetLength(1); j++)
                {
                    _values[i, j] = new Vector<T>(new T[chunkSize]);
                }
            }
        }

        public Matrix(T[,] values)
        {
            if (values.GetLength(0) != values.GetLength(1))
            {
                throw new ArgumentException("Matrix must have same dimensions");
            }

            _length = values.GetLength(0);
            _values = new Vector<T>[Length, (int)Math.Ceiling(Length / (double)chunkSize)];

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < _values.GetLength(1); j++)
                {
                    var arr = new T[chunkSize];
                    for (int k = 0; k < chunkSize; k++)
                    {
                        if(IsInBounds(i, j * chunkSize + k))
                        {
                            arr[k] = values[i, j * chunkSize + k];
                        }
                        else
                        {
                            arr[k] = default(T);
                        }
                    }
                    _values[i, j] = new Vector<T>(arr);
                }
            }
        }

        private Vector<T>[,] Values => _values;
        public int Length => _length;

        public T this[int x, int y]
        {
            get
            {
                ThrowExceptionIfNotInBounds(x, y);
                return Values[x, y / chunkSize][y % chunkSize];
            }
            set
            {
                ThrowExceptionIfNotInBounds(x, y);
                T[] temp = new T[chunkSize];
                for (int i = 0; i < chunkSize; i++)
                {
                    temp[i] = this[x, y / chunkSize + i];
                }
                temp[y % chunkSize] = value;
                _values[x, y / chunkSize] = new Vector<T>(temp);
            }
        }

        public IMatrix<T> SequentalAdd(IMatrix<T> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            Matrix<T> result = new Matrix<T>(Length);

            if (matrix is Matrix<T> second)
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Values.GetLength(1); j++)
                    {
                        result.Values[i, j] = Values[i, j] + second.Values[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        result[i, j] = this[i, j] + matrix[i, j];
                    }
                }
            }

            return result;
        }

        public IMatrix<T> ParallelAdd(IMatrix<T> matrix)
        {
            throw new NotImplementedException();

        }


        public IMatrix<T> ParallelAdd(IMatrix<T> matrix, int threads)
        {
            throw new NotImplementedException();

        }

        public IMatrix<T> SequentalMultiply(IMatrix<T> matrix)
        {
            throw new NotImplementedException();

        }

        public IMatrix<T> ParallelMultiply(IMatrix<T> matrix)
        {
            throw new NotImplementedException();

        }

        public IMatrix<T> ParallelMultiply(IMatrix<T> matrix, int threads)
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

        private void ThrowExceptionIfNotEqualLength(IMatrix<T> y)
        {
            if (Length != y.Length)
            {
                throw new ArgumentException("Matrices must be the same length");
            }
        }
    }
}
