namespace ShurkaWake.Matrices
{
    public interface IMatrix<T>
    {
        public abstract int Length
        {
            get;
        }

        public abstract T this[int x, int y]
        {
            get;
            set;
        }

        public abstract IMatrix<T> SequentalAdd(IMatrix<T> matrix);
        public abstract IMatrix<T> ParallelAdd(IMatrix<T> matrix);
        public abstract IMatrix<T> ParallelAdd(IMatrix<T> matrix, int threads);
        public abstract IMatrix<T> SequentalMultiply(IMatrix<T> matrix);
        public abstract IMatrix<T> ParallelMultiply(IMatrix<T> matrix);
        public abstract IMatrix<T> ParallelMultiply(IMatrix<T> matrix, int threads);
    }
}
