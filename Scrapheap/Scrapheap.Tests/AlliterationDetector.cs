using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrapheap.Tests
{
    [TestClass]
    public class AlliterationDetector
    {
        [TestMethod]
        public void AlliterationDetector_IsAlliteration()
        {
            // Setup
            string phrase = "Peter Piper Picked a Peck of Pickled Peppers";

            // Act
            bool isAlliteration = Scrapheap.AlliterationDetector.IsAlliteration(phrase);

            // Verify
            Assert.IsTrue(isAlliteration);
        }

        [TestMethod]
        public void AlliterationDetector_IsNotAlliteration()
        {
            // Setup
            string phrase = "This is not alliteration";

            // Act
            bool isAlliteration = Scrapheap.AlliterationDetector.IsAlliteration(phrase);

            // Verify
            Assert.IsFalse(isAlliteration);
        }
    }
}
