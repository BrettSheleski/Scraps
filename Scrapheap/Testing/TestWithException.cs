using System;

namespace Testing
{
    internal interface ITestContextExpectingException<T, TException> where TException : Exception
    {
        ITestContextExpectingException<T, TException> Where(Func<TestResultWithException<T, TException>, bool> whereFunc);
    }


    public class DoNothingTestContextWithException<T, TException> : TestContextBase<T>, ITestContextExpectingException<T, TException> where TException : Exception
    {
        public override TestResult Execute(T sut)
        {
            DateTime now = DateTime.Now;

            TestResultWithException<T, TException> result = new TestResultWithException<T, TException>(this, default(T), null, now, now);

            return result;
        }

        public TestContextFuncWithException<T, TResult, TException> Test<TResult>(Func<T, TResult> func)
        {
            return new TestContextFuncWithException<T, TResult, TException>(func)
            {
                WhereFunc = this.WhereFunc
            };
        }

        public TestContextActionWithException<T, TException> Test(Action<T> action)
        {
            return new TestContextActionWithException<T, TException>(action)
            {
                WhereFunc = WhereFunc
            };
        }

        public Func<TestResultWithException<T, TException>, bool> WhereFunc { get; private set; } = x => true;

        public DoNothingTestContextWithException<T, TException> Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return new DoNothingTestContextWithException<T, TException>()
            {
                WhereFunc = x => this.WhereFunc(x) && whereFunc(x)
            };
        }

        ITestContextExpectingException<T, TException> ITestContextExpectingException<T, TException>.Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return this.Where(whereFunc);
        }
    }

    public class TestContextFuncWithException<T, TResult, TException> : TestContextBase<T>,
        ITestContextExpectingException<T, TException>
        where TException : Exception
    {
        public TestContextFuncWithException(Func<T, TResult> func)
        {
            this.Func = func;
        }

        public Func<T, TResult> Func { get; set; }

        public Func<TestResultWithException<T, TException>, bool> WhereFunc { get; internal set; } = x => true;

        public override TestResult Execute(T instance)
        {
            TException caughtException = null;
            TResult result = default(TResult);
            DateTime end, start = DateTime.Now;
            try
            {
                result = Func(instance);
                end = DateTime.Now;
            }
            catch (TException ex)
            {
                end = DateTime.Now;
                caughtException = ex;
            }

            TestResultWithException<T, TException> testResult = new TestResultWithException<T, TException>(this, instance, caughtException, start, end);

            return testResult;
        }

        public TestContextFuncWithException<T, TResult, TException> Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return new TestContextFuncWithException<T, TResult, TException>(this.Func)
            {
                WhereFunc = x => this.WhereFunc(x) && WhereFunc(x)
            };
        }

        ITestContextExpectingException<T, TException> ITestContextExpectingException<T, TException>.Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return this.Where(whereFunc);
        }
    }


    public class TestContextActionWithException<T, TException> : TestContextBase<T>,
        ITestContextExpectingException<T, TException>
        where TException : Exception
    {
        public TestContextActionWithException(Action<T> action)
        {
            this.Action = action;
        }

        public Action<T> Action { get; set; }

        public Func<TestResultWithException<T, TException>, bool> WhereFunc { get; internal set; } = x => true;

        public override TestResult Execute(T instance)
        {
            TException caughtException = null;
            DateTime end, start = DateTime.Now;
            try
            {
                Action(instance);
                end = DateTime.Now;
            }
            catch (TException ex)
            {
                end = DateTime.Now;
                caughtException = ex;
            }

            bool success = caughtException != null;

            TestResultWithException<T, TException> testResult = new TestResultWithException<T, TException>(this, instance, caughtException, start, end);

            return testResult;
        }

        public TestContextActionWithException<T, TException> Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return new TestContextActionWithException<T, TException>(this.Action)
            {
                WhereFunc = x => this.WhereFunc(x) && WhereFunc(x)
            };
        }

        ITestContextExpectingException<T, TException> ITestContextExpectingException<T, TException>.Where(Func<TestResultWithException<T, TException>, bool> whereFunc)
        {
            return this.Where(whereFunc);
        }
    }
}