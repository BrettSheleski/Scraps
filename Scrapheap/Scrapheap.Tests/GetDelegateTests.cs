using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Tests
{
    [TestClass]
    public class GetDelegateTests
    {
        [TestMethod]
        public void GetDelegate_Works()
        {
            // Setup
            DummyClass dummy = new DummyClass();
            MethodInfo setter = typeof(DummyClass).GetProperty("Name").GetSetMethod();
            Action<DummyClass, object> setterAction;

            // Act
            setterAction = Scrapheap.Extensions.IDbCommandExtensions.GetDelegate<DummyClass>(setter);
            setterAction.Invoke(dummy, "bozo");

            // Verify
            Assert.AreEqual(dummy.Name, "bozo");
        }

        class DummyClass
        {
            public string Name { get; set; }
        }
    }
}
