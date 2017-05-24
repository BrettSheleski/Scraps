using System;
using System.Linq.Expressions;

namespace Testing
{
    public static class TestContext
    {
        public static DoNothingTest<T> Create<T>()
        {
            return new DoNothingTest<T>();
        }

        
    }   
}