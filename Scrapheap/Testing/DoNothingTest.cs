using System;

namespace Testing
{
    public class TestAssertionFailedException : Exception
    {
        
        public TestAssertionFailedException(string message, Exception innerException):base(message, innerException)
        {

        }

        public TestAssertionFailedException(string message, TestResult result) : base(message)
        {
            this.TestResult = result;
        }

        public TestAssertionFailedException(Exception innerException) : base()
        {

        }

        public TestResult TestResult { get; }
    }

    public class TestContextBase
    {

    }

    public abstract class TestContextBase<T> : TestContextBase
    {
        public abstract TestResult Execute(T instance);

        public virtual void Assert(T instance)
        {
            TestResult testResult;
            try
            {
                testResult = this.Execute(instance);
            }
#if !DEBUG
            catch(Exception ex)
            {
                throw new TestAssertionFailedException("Assertion failed, see inner exception.", ex);
            }
#endif
            finally { }
            
            if (!testResult.Succeeded)
                throw new TestAssertionFailedException("Assertion failed.", testResult);
        }
    }
    
    public class DoNothingTest<T> : TestContextBase<T>
    {
        public override TestResult Execute(T instance)
        {
            // maybe this should throw an exception instead since there's really nothing being tested??

            var now = DateTime.Now;
            
            var result = new TestResult(this, true, now, now);

            return result;
        }

        public DoNothingTestContextWithException<T, TException> ExpectException<TException>() where TException : Exception
        {
            DoNothingTestContextWithException<T, TException> exceptionTest = new DoNothingTestContextWithException<T, TException>();

            return exceptionTest;
        }

        public TestContextWithAction<T> Test(Action<T> action)
        {
            return new TestContextWithAction<T>(action);
        }

        public TestContextWithFunc<T, TResult> Test<TResult>(Func<T, TResult> func)
        {
            return new TestContextWithFunc<T, TResult>(func);
        }
    }

    public class TestContextWithFunc<T, TResult> : TestContextBase<T>
    {
        public TestContextWithFunc(Func<T, TResult> func)
        {
            this.Func = func;
        }

        public Func<TestResultWithValue<T, TResult>, bool> WhereFunc { get; internal set; } = x => true;

        public Func<T, TResult> Func { get; }

        public override TestResult Execute(T instance)
        {
            DateTime startTime = DateTime.Now;
            TResult result = Func(instance);
            DateTime endTime = DateTime.Now;

            return new TestResultWithValue<T, TResult>(this, instance, result, true, startTime, endTime);
        }

        public TestContextWithFunc<T, TResult> Where(Func<TestResultWithValue<T, TResult>, bool> whereFunc)
        {
            return new TestContextWithFunc<T, TResult>(this.Func)
            {
                WhereFunc = x => WhereFunc(x) && whereFunc(x)
            };
        }
    }

    public class TestContextWithAction<T> : TestContextBase<T>
    {
        public TestContextWithAction(Action<T> action)
        {
            this.Action = action;
        }

        public Action<T> Action { get; }

        public override TestResult Execute(T sut)
        {
            throw new NotImplementedException();
        }


    }

    public class ActionTest<T> : TestContextBase<T>
    {
        public ActionTest(Action<T> action)
        {
            this.Action = action;
        }

        public Action<T> Action { get; private set; }

        public override TestResult Execute(T sut)
        {
            DateTime startTime = DateTime.Now;
            Action(sut);
            DateTime endTime = DateTime.Now;

            return new TestResult(this, true, startTime, endTime);
        }
    }

    public class ActionTestWithException<T, TException> : ActionTest<T>
    {
        public ActionTestWithException(Action<T> action) : base(action)
        {
        }
    }
}