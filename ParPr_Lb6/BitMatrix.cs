using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParPr_Lb6
{
    public class BitMatrix : IMatrix<bool>, ICloneable
    {
        private BitArray[] _valuesVertical;
        private BitArray[] _valuesHorizontal;
        private int _length;

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

        public IMatrix<bool> SequentalAdd(IMatrix<bool> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            BitMatrix result = this.Clone() as BitMatrix; 

            if (matrix is BitMatrix bitMatrix)
            {
                for (int i = 0; i < Length; i++)
                {
                    result._valuesHorizontal[i].Or(bitMatrix._valuesHorizontal[i]);
                    result._valuesVertical[i].Or(bitMatrix._valuesVertical[i]);
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        result._valuesHorizontal[i][j] |= matrix[i, j];
                        result._valuesVertical[j][i] |= matrix[i, j];
                    }
                }
            }
            
            return result;
        }

        public IMatrix<bool> ParallelAdd(IMatrix<bool> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            BitMatrix result = this.Clone() as BitMatrix;

            if(matrix is BitMatrix bitMatrix)
            {
                Parallel.For(0, Length, (i) =>
                {
                    result._valuesHorizontal[i].Or(bitMatrix._valuesHorizontal[i]);
                    result._valuesVertical[i].Or(bitMatrix._valuesVertical[i]);
                });
            }
            else
            {
                Parallel.For(0, Length, (i) =>
                {
                    for (int j = 0; j < Length; j++)
                    {
                        result._valuesHorizontal[i][j] |= matrix[i, j];
                        result._valuesVertical[j][i] |= matrix[i, j];
                    }
                });
            }
            
            return result;
        }

        public IMatrix<bool> ParallelAdd(IMatrix<bool> matrix, int threads)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = threads;
            BitMatrix result = this.Clone() as BitMatrix;

            if (matrix is BitMatrix bitMatrix)
            {
                Parallel.For(0, Length, parallelOptions, (i) =>
                {
                    result._valuesHorizontal[i].Or(bitMatrix._valuesHorizontal[i]);
                    result._valuesVertical[i].Or(bitMatrix._valuesVertical[i]);
                });
            }
            else
            {
                Parallel.For(0, Length, parallelOptions, (i) =>
                {
                    for (int j = 0; j < Length; j++)
                    {
                        result._valuesHorizontal[i][j] |= matrix[i, j];
                        result._valuesVertical[j][i] |= matrix[i, j];
                    }
                });
            }
            
            return result;
        }

        public IMatrix<bool> SequentalMultiply(IMatrix<bool> matrix)
        {
            ThrowExceptionIfNotEqualLength(matrix);
            BitMatrix result = new BitMatrix(Length);

            if (matrix is BitMatrix bitMatrix)
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        BitArray curr = _valuesHorizontal[i].Clone() as BitArray;
                        curr.And(bitMatrix._valuesVertical[j]);

                        bool res = false;
                        foreach (bool elem in curr)
                        {
                            res ^= elem;
                        }

                        result._valuesHorizontal[i][j] = res;
                        result._valuesVertical[j][i] = res;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        bool sum = false;
                        for (int r = 0; r < Length; r++)
                        {
                            result[i, j] |= this[i, j] & matrix[i, j];
                        }
                    }
                }
            }

            return result;
        }

        public IMatrix<bool> ParallelMultiply(IMatrix<bool> matrix)
        {
            throw new NotImplementedException();
        }

        public IMatrix<bool> ParallelMultiply(IMatrix<bool> matrix, int threads)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            var result = new BitMatrix(Length);
            result._valuesHorizontal = _valuesHorizontal.Clone() as BitArray[];
            result._valuesVertical = _valuesVertical.Clone() as BitArray[];
            result._length = _length;
            return result;
        }
    }
}
