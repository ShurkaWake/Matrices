using ParPr_Lb6;
using System.Security.Cryptography;

namespace ParPr_Lb6_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Constructor_WhenLengthLessThanZero_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new SequentalMatrix<int>(-10));
        }

        [TestMethod]
        public void Constructor_WhenLengthAreCorrect_ShouldCreateCorrectInstance() 
        {
            const int length = 10;
            var sm = new SequentalMatrix<int>(length);
            int actualLengthX = 0;

            for(int i = 0; i < sm.Length; i++)
            {
                int actualLengthY = 0;
                for(int j = 0; j < sm.Length; j++)
                {
                    actualLengthY++;
                }
                if(actualLengthY != length)
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
            int[,] m = new int[length, length];
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    m[i, j] = RandomNumberGenerator.GetInt32(0, 10);
                }
            }

            var sm = new SequentalMatrix<int>(m);
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

            Assert.ThrowsException<ArgumentException>(() => { new SequentalMatrix<int>(m1); });
            Assert.ThrowsException<ArgumentException>(() => { new SequentalMatrix<int>(m2); });
        }
    }
}