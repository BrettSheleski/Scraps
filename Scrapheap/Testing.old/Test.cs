using System;
using System.Linq.Expressions;

namespace Testing
{
    public class Test<T>
    {

        public Test(T sut)
        {
            this.Sut = sut;
        }

        protected Test(T sut, Test<T> parent) : this(sut)
        {
            this.Parent = parent;
        }

        public T Sut { get;  }

        public Test<T> Parent { get; }

        public Func<T, object> Func { get; private set; }
        public Action<T> Action { get; private set; }

        private Func<T, bool> assertion;
        
        public Type ReturnType { get; private set; }


        public Test<T> Assert(Func<T, bool> func)
        {
            Test<T> newTest = new Test<T>(this.Sut, this);
            newTest.assertion = func;

            return newTest;
        }
        
        public Test<T> Act<TResult>(Func<T, TResult> func)
        {
            this.ReturnType = typeof(TResult);
            Func = delegate (T sut) { return (object)func(sut); };
            
            return this;
        }

        public Test<T> Act(Action<T> action)
        {
            this.Action = action;

            return this;
        }


        public ITestResult Run()
        {
            throw new NotImplementedException();
        }
        
    }
}
