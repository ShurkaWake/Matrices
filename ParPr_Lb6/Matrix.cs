using System.Numerics;

namespace ParPr_Lb6
{
    public class Matrix<T> : IMatrix<T> where T : INumber<T>
    {
        readonly T[,] _values;
        readonly int _length;

        public Matrix(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be higher than zero");
            }

            _values = new T[n, n];
            _length = n;
        }

        public Matrix(T[,] values)
        {
            if (values.GetLength(0) != values.GetLength(1))
            {
                throw new ArgumentException("Matrix must have same dimensions");
            }

            _values = values;
            _length = Values.GetLength(0);
        }

        private T[,] Values => _values;
        public int Length => _length;

        public T this[int x, int y]
        {
            get
            {
                ThrowExceptionIfNotInBounds(x, y);
                return _values[x, y];
            }
            set
            {
                ThrowExceptionIfNotInBounds(x, y);
                _values[x, y] = value;
            }
        }

        public IMatrix<T> SequentalAdd(IMatrix<T> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            Matrix<T> result = new Matrix<T>(this.Length);
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    result[i, j] = this[i, j] + matrix[i, j];
                }
            }
            return result;
        }

        public IMatrix<T> ParallelAdd(IMatrix<T> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            Matrix<T> result = new Matrix<T>(Length);
            Parallel.For(0, Length, (i) =>
            {
                for (int j = 0; j < Length; j++)
                {
                    result[i, j] = this[i, j] + matrix[i, j];
                }
            });

            return result;
        }


        public IMatrix<T> ParallelAdd(IMatrix<T> matrix, int threads)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = threads;

            Matrix<T> result = new Matrix<T>(Length);
            Parallel.For(0, Length, parallelOptions, (i) =>
            {
                for (int j = 0; j < Length; j++)
                {
                    result[i, j] = this[i, j] + matrix[i, j];
                }
            });

            return result;
        }

        public IMatrix<T> SequentalMultiply(IMatrix<T> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            Matrix<T> result = new Matrix<T>(Length);
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    for (int k = 0; k < Length; k++)
                    {
                        result[i, j] += this[i, k] * matrix[k, j];
                    }
                }
            }
            return result;
        }

        public IMatrix<T> ParallelMultiply(IMatrix<T> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);

            Matrix<T> result = new Matrix<T>(Length);
            Parallel.For(0, Length, (i) =>
            {
                for (int j = 0; j < Length; j++)
                {
                    for (int k = 0; k < Length; k++)
                    {
                        result[i, j] += this[i, k] * matrix[k, j];
                    }
                }
            });

            return result;
        }

        public IMatrix<T> ParallelMultiply(IMatrix<T> matrix, int threads)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = threads;

            Matrix<T> result = new Matrix<T>(Length);
            Parallel.For(0, Length, parallelOptions, (i) =>
            {
                for (int j = 0; j < Length; j++)
                {
                    for (int k = 0; k < Length; k++)
                    {
                        result[i, j] += this[i, k] * matrix[k, j];
                    }
                }
            });

            return result;
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
