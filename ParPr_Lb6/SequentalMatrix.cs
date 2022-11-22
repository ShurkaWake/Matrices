using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParPr_Lb6
{
    public class SequentalMatrix<T>
    {
        T[,] _values;
        readonly int _length;

        public SequentalMatrix(int n)
        {
            if(n < 0)
            {
                throw new ArgumentException("n must be higher than zero");
            }

            _values = new T[n, n];
            _length = n;
        }

        public SequentalMatrix(T[,] values)
        {
            if(values.GetLength(0) != values.GetLength(1))
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
            if(!IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException("Invalid indeces");
            }
        }
    }
}
