using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{

    public interface ITestResult
    {
        bool IsPassed { get; }

        DateTime StartTime { get;}

        DateTime? EndTime { get; }

        Exception Exception { get; }
    }

    public interface ITestResultWithValue : ITestResult
    {
        object Value { get; }
    }
    
}
