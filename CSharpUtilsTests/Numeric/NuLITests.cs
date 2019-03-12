namespace CSharpUtilsTests.Numeric
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using CSharpUtils.Numeric;

    [TestClass]
    public class NuLITests
    {
        #region Initialization

        [TestMethod]
        public void TrueInInterval()
        {
            var nuli = new NuLI(0, 10, 8, true);

            Assert.IsTrue((int)nuli == 8, $"{(int)nuli} obtained, but must be 8");
        }

        [TestMethod]
        public void FalseInInterval()
        {
            var nuli = new NuLI(0, 10, 8, false);

            Assert.IsTrue((int)nuli == 8, $"{(int)nuli} obtained, but must be 8");
        }

        [TestMethod]
        public void TrueOutOfInterval()
        {
            var nuli = new NuLI(0, 10, 16, true);

            Assert.IsTrue((int)nuli == 10, $"{(int)nuli} obtained, but must be 10");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "A userId of null was inappropriately allowed.")]
        public void FalseOutOfInterval()
        {
            var nuli = new NuLI(0, 10, 16, false);
        }

        #endregion

        [TestMethod]
        public void DifferenceIntervals()
        {
            var nuli    = new NuLI(0, 10, 5);
            var nuli2   = new NuLI(-5, 5, 2);

            Assert.IsFalse(nuli.IsWithinLimits(nuli2), "intervals are different, but its checking return true");
        }

        #region Addition

        [TestMethod]
        public void NuLIPlusNumber()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            int other   = 6;                        // int 6

            var result  = nuli + other;             // 11

            Assert.IsTrue((int)result == 11, $"{(int)result} obtained, but must be 11");
        }

        [TestMethod]
        public void NumberPlusNuLI()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            int other   = 6;                        // int 6

            var result  = other + nuli;             // 11

            Assert.IsTrue((int)result == 11, $"{(int)result} obtained, but must be 11");
        }

        [TestMethod]
        public void NuLIPlusNuLI()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            var nuli2   = new NuLI(10, 15, 12);     // nuli 12

            var result  = nuli + nuli2;             // 12

            Assert.IsTrue((int)result == 12, $"{(int)result} obtained, but must be 12");
        }

        #endregion

        #region Subtraction

        [TestMethod]
        public void NuLIMinusNumber()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            int other   = 6;                        // int 6

            var result  = nuli - other;             // 14

            Assert.IsTrue((int)result == 14, $"{(int)result} obtained, but must be 14");
        }

        [TestMethod]
        public void NumberMinusNuLI()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            int other   = 6;                        // int 6

            var result  = other - nuli;             // 11

            Assert.IsTrue((int)result == 11, $"{(int)result} obtained, but must be 11");
        }

        [TestMethod]
        public void NuLIMinusNuLI()
        {
            var nuli    = new NuLI(10, 15, 100);    // nuli 15
            var nuli2   = new NuLI(10, 15, 12);     // nuli 12

            var result  = nuli - nuli2;             // 13

            Assert.IsTrue((int)result == 13, $"{(int)result} obtained, but must be 13");
        }

        #endregion

        #region Multiplication

        [TestMethod]
        public void NuLIMultiplyNumber()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            int other   = 6;                    // int 6

            var result  = nuli * other;         // 9

            Assert.IsTrue((int)result == 9, $"{(int)result} obtained, but must be 9");
        }

        [TestMethod]
        public void NumberMultiplyNuLI()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            int other   = 6;                    // int 6

            var result  = other * nuli;         // 9

            Assert.IsTrue((int)result == 9, $"{(int)result} obtained, but must be 9");
        }

        [TestMethod]
        public void NuLIMultiplyNuLI()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            var nuli2   = new NuLI(5, 12, 9);   // nuli 9

            var result  = nuli * nuli2;         // 10

            Assert.IsTrue((int)result == 10, $"{(int)result} obtained, but must be 10");
        }

        #endregion

        #region Division

        [TestMethod]
        public void NuLIDivideNumber()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            int other   = 6;                    // int 6

            var result  = nuli / other;         // 9

            Assert.IsTrue((int)result == 9, $"{(int)result} obtained, but must be 9");
        }

        [TestMethod]
        public void NumberDivideNuLI()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            int other   = 6;                    // int 6

            var result  = other / nuli;         // 7

            Assert.IsTrue((int)result == 7, $"{(int)result} obtained, but must be 7");
        }

        [TestMethod]
        public void NuLIDivideNuLI()
        {
            var nuli    = new NuLI(5, 12, 100); // nuli 12
            var nuli2   = new NuLI(5, 12, 9);   // nuli 9

            var result  = nuli / nuli2;         // 8

            Assert.IsTrue((int)result == 8, $"{(int)result} obtained, but must be 8");
        }

        #endregion
    }
}