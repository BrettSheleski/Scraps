using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Math
{
    public class NumberHelpers
    {

        public static bool IsPrime(int value)
        {
            if (IsEven(value))
                return false;

            throw new NotImplementedException();
        }

        public static bool IsPrime(Int64 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPrime(Int16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPrime(uint value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPrime(UInt64 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPrime(UInt16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOdd(int value)
        {
            return !IsEven(value);
        }

        public static bool IsOdd(Int64 value)
        {
            return !IsEven(value);
        }

        public static bool IsOdd(Int16 value)
        {
            return !IsEven(value);
        }

        public static bool IsOdd(uint value)
        {
            return !IsEven(value);
        }

        public static bool IsOdd(UInt64 value)
        {
            return !IsEven(value);
        }

        public static bool IsOdd(UInt16 value)
        {
            return !IsEven(value);
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        public static bool IsEven(Int64 value)
        {
            return value % 2L == 0;
        }

        public static bool IsEven(Int16 value)
        {
            return value % 2 == 0;
        }

        public static bool IsEven(uint value)
        {
            return value % 2U == 0;
        }

        public static bool IsEven(UInt64 value)
        {
            return value % 2UL == 0;
        }

        public static bool IsEven(UInt16 value)
        {
            return value % 2 == 0;
        }

    }
}
