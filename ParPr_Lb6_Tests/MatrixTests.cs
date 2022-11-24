using ParPr_Lb6;
using System.Security.Cryptography;

namespace ParPr_Lb6_Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Constructor_WhenLengthLessThanZero_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Matrix<int>(-10));
        }

        [TestMethod]
        public void Constructor_WhenLengthAreCorrect_ShouldCreateCorrectInstance()
        {
            const int length = 10;
            var sm = new Matrix<int>(length);
            int actualLengthX = 0;

            for (int i = 0; i < sm.Length; i++)
            {
                int actualLengthY = 0;
                for (int j = 0; j < sm.Length; j++)
                {
                    actualLengthY++;
                }
                if (actualLengthY != length)
                {
                    Assert.Fail("Matrix not the same width");
                }
                actualLengthX++;
            }

            Assert.AreEqual(length, actualLengthX);
        }

        [TestMethod]
        public void Constructor_WhenParametersAreCorrect_ShouldCreateCorrectInstance()
        {
            const int length = 10;
            int[,] m = Utils.GetMatrix(length);

            var sm = new Matrix<int>(m);
            Assert.AreEqual(sm.Length, length);

            for (int i = 0; i < sm.Length; i++)
            {
                for (int j = 0; j < sm.Length; j++)
                {
                    Assert.AreEqual(m[i, j], sm[i, j]);
                }
            }
        }

        [TestMethod]
        public void Constructor_WhenParametersNotSquare_ShouldThrowArgumentAxception()
        {
            int[,] m1 = new int[4, 5];
            int[,] m2 = new int[5, 4];

            Assert.ThrowsException<ArgumentException>(() => { new Matrix<int>(m1); });
            Assert.ThrowsException<ArgumentException>(() => { new Matrix<int>(m2); });
        }

        [TestMethod]
        public void Indexator_WhenOutOfBounds_ShouldThrowIndexOutOfRangeException()
        {
            const int length = 10;
            int[,] m = Utils.GetMatrix(length);
            var sm = new Matrix<int>(m);

            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[-1, 0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[-1, -1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[0, -1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[length + length, 0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[length + length, length + length]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[0, length + length]);
        }

        [TestMethod]
        public void Indexator_WhenCorrectParameters_ShouldGiveCorrectInstanceOfT()
        {
            const int length = 10;
            int[,] m = Utils.GetMatrix(length);
            var sm = new Matrix<int>(m);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(m[i, j], sm[i, j]);
                }
            }
        }

        [TestMethod]
        public void SequentalAdd_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.SequentalAdd(sm2));
            Assert.ThrowsException<ArgumentException>(() => sm2.SequentalAdd(sm1));
        }

        [TestMethod]
        public void SequentalAdd_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Add(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.SequentalAdd(sm2);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void ParallelAdd_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.ParallelAdd(sm2));
            Assert.ThrowsException<ArgumentException>(() => sm2.ParallelAdd(sm1));
        }

        [TestMethod]
        public void ParallelAdd_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Add(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.ParallelAdd(sm2);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void ParallelAddFixedThreads_WhenNotSameLength_ShouldThrowArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.ParallelAdd(sm2, 2));
            Assert.ThrowsException<ArgumentException>(() => sm2.ParallelAdd(sm1, 2));
        }

        [TestMethod]
        public void ParallelAddFixedThreads_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Add(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.ParallelAdd(sm2, 3);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void SequentalMultiply_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.SequentalMultiply(sm2));
            Assert.ThrowsException<ArgumentException>(() => sm2.SequentalMultiply(sm1));
        }

        [TestMethod]
        public void SequentalMultiply_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Multiply(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.SequentalMultiply(sm2);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void ParallelMultiply_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.ParallelMultiply(sm2));
            Assert.ThrowsException<ArgumentException>(() => sm2.ParallelMultiply(sm1));
        }

        [TestMethod]
        public void ParallelMultiply_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Multiply(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.ParallelMultiply(sm2);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void ParallelMultiplyFixedThreads_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length - 1);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.ParallelMultiply(sm2, 3));
            Assert.ThrowsException<ArgumentException>(() => sm2.ParallelMultiply(sm1, 3));
        }

        [TestMethod]
        public void ParallelMultiplyFixedThreads_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            int[,] m1 = Utils.GetMatrix(length);
            int[,] m2 = Utils.GetMatrix(length);
            int[,] expected = Utils.Multiply(m1, m2);
            var sm1 = new Matrix<int>(m1);
            var sm2 = new Matrix<int>(m2);
            var actual = sm1.ParallelMultiply(sm2, 3);

            Assert.AreEqual(sm1.Length, actual.Length);
            Assert.AreEqual(sm2.Length, actual.Length);
            Assert.AreEqual(actual.Length, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}