using ParPr_Lb6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParPr_Lb6_Tests
{
    [TestClass]
    public class BitMatrixTests
    {
        [TestMethod]
        public void Constructor_WhenLengthLessThanZero_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new BitMatrix(-10));
        }

        [TestMethod]
        public void Constructor_WhenLengthAreCorrect_ShouldCreateCorrectInstance()
        {
            const int length = 10;
            var sm = new BitMatrix(length);
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
            bool[,] m = Utils.GetBoolMatrix(length);

            var sm = new BitMatrix(m);
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
            bool[,] m1 = new bool[4, 5];
            bool[,] m2 = new bool[5, 4];

            Assert.ThrowsException<ArgumentException>(() => { new BitMatrix(m1); });
            Assert.ThrowsException<ArgumentException>(() => { new BitMatrix(m2); });
        }

        [TestMethod]
        public void Indexator_WhenOutOfBounds_ShouldThrowIndexOutOfRangeException()
        {
            const int length = 10;
            bool[,] m = Utils.GetBoolMatrix(length);
            var sm = new BitMatrix(m);

            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[-1, 0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[-1, -1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[0, -1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[length + length, 0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[length + length, length + length]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => sm[0, length + length]);
        }

        [TestMethod]
        public void Indexator_WhenCorrectParameters_ShouldGiveCorrectInstance()
        {
            const int length = 10;
            bool[,] m = Utils.GetBoolMatrix(length);
            var sm = new BitMatrix(m);
            
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    Assert.AreEqual(m[i, j], sm[i, j]);
                }
            }
        }

        [TestMethod]
        public void Indexator_WhenCorrectParameters_ShouldCorrectChangeValue()
        {
            const int length = 10;
            bool[,] m = Utils.GetBoolMatrix(length);
            var sm = new BitMatrix(m);

            sm[length / 2, length / 2] = !sm[length / 2, length / 2];
            Assert.AreNotEqual(m[length / 2, length / 2], sm[length / 2, length / 2]);
        }



        [TestMethod]
        public void SequentalAdd_WhenNotSameMatrixLength_ThrowsArgumentException()
        {
            const int length = 10;
            bool[,] m1 = Utils.GetBoolMatrix(length);
            bool[,] m2 = Utils.GetBoolMatrix(length - 1);
            var sm1 = new BitMatrix(m1);
            var sm2 = new BitMatrix(m2);

            Assert.ThrowsException<ArgumentException>(() => sm1.SequentalAdd(sm2));
            Assert.ThrowsException<ArgumentException>(() => sm2.SequentalAdd(sm1));
        }

        [TestMethod]
        public void SequentalAdd_WhenCorrectParameter_ShouldGiveCorrectResult()
        {
            const int length = 10;
            bool[,] m1 = Utils.GetBoolMatrix(length);
            bool[,] m2 = Utils.GetBoolMatrix(length);
            bool[,] expected = Utils.Add(m1, m2);
            var sm1 = new BitMatrix(m1);
            var sm2 = new BitMatrix(m2);
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
    }
}
