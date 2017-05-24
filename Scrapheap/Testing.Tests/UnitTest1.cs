using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Tests
{
    

    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestShit()
        {
            var testContext = TestContext.Create<Person>()
                                        .Test(x => x.DisplayName)
                                        .Where(x => x.Value == x.ObjectInstance.FirstName + " " + x.ObjectInstance.LastName)
                                        ;

            Person p = new Person
            {
                FirstName = "Brett",
                LastName = "Sheleski"
            };

            testContext.Assert(p);
        }

        class Person
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }

            public string DisplayName
            {
                get
                {
                    return $"{FirstName} {MiddleName} {LastName}".Trim();
                }
            }

            public void DoSomething()
            {

            }
        }
    }
}
