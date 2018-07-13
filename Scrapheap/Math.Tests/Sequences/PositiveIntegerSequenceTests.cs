using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Tests.Sequences
{
    [TestClass]
    public class PositiveIntegerSequenceTests
    {
        [TestMethod]
        public void PositiveIntegers_First100()
        {
            // setup
            var first100 = Math.Sequences.PositiveIntegers.Take(100);

            // act
            uint[] first100Array = first100.ToArray();

            // verify
            Assert.IsTrue(first100Array.Length == 100);

            for(uint i = 0; i < 100; ++i)
            {
                Assert.AreEqual(first100Array[i], i + 1);
            }
        }

        [TestMethod]
        public void PositiveIntegers_First100_64()
        {
            // setup
            var first100 = Math.Sequences.PositiveIntegers64.Take(100);

            // act
            ulong[] first100Array = first100.ToArray();

            // verify
            Assert.IsTrue(first100Array.Length == 100);

            for (ulong i = 0; i < 100; ++i)
            {
                Assert.AreEqual(first100Array[i], i + 1);
            }
        }
    }
}
