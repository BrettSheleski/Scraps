using System;

namespace Testing
{
    public class TestResult
    {
        public TestResult(TestContextBase context, bool succeeded, DateTime startTime, DateTime endTime) 
        {
            this.Succeeded = succeeded;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Context = context;
        }
        
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public bool Succeeded { get; }
        public TestContextBase Context { get; }
    }

    public class TestResult<T> : TestResult
    {
        public TestResult(TestContextBase context, T instance, bool succeeded, DateTime startTime, DateTime endTime) : base(context, succeeded, startTime, endTime)
        {
            this.ObjectInstance = instance;
        }

        public T ObjectInstance { get; }
    }

    public class TestResultWithException<T, TException> : TestResult<T> where TException : Exception
    {
        public TestResultWithException(TestContextBase context, T instance, TException exception, DateTime startTime, DateTime endTime) : base(context, instance, exception != null, startTime, endTime)
        {
            this.Exception = exception;
        }

        public TException Exception { get; }

        private TimeSpan? _duration;

        public TimeSpan Duration
        {
            get
            {
                return _duration ?? (_duration = EndTime.Subtract(StartTime)).Value;
            }
        }
    }

    public class TestResultWithValue<T, TValue> : TestResult<T>
    {
        public TestResultWithValue(TestContextBase context, T instance, TValue value, bool isSuccess, DateTime startTime, DateTime endTime) : base(context, instance, isSuccess, startTime, endTime)
        {
            this.Value = value;
        }

        public TValue Value { get; }
    }
}