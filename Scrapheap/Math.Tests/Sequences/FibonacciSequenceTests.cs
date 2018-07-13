using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Tests.Sequences
{
    [TestClass]
    public class FibonacciSequenceTests
    {
        [TestMethod]
        public void FibonacciSequence_First10AreCorrect()
        {
            // setup
            uint[] expectedValues = new uint[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55};
            uint[] actualValues;
            var first10FibonacciValues = Math.Sequences.Fibonacci.Take(10);

            // act
            actualValues = first10FibonacciValues.ToArray();

            // verify
            Assert.IsTrue(actualValues.Length == 10);
            Assert.IsTrue(expectedValues.Length == actualValues.Length);

            for(int i = 0; i < expectedValues.Length; ++i)
            {
                Assert.AreEqual(expectedValues[i], actualValues[i]);
            }
        }

        [TestMethod]
        public void FibonacciSequence_First10AreCorrect_64()
        {
            // setup
            ulong[] expectedValues = new ulong[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
            ulong[] actualValues;
            var first10FibonacciValues = Math.Sequences.Fibonacci64.Take(10);

            // act
            actualValues = first10FibonacciValues.ToArray();

            // verify
            Assert.IsTrue(actualValues.Length == 10);
            Assert.IsTrue(expectedValues.Length == actualValues.Length);

            for (int i = 0; i < expectedValues.Length; ++i)
            {
                Assert.AreEqual(expectedValues[i], actualValues[i]);
            }
        }
    }
}
