using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    public static class Sequences
    {
        public static IEnumerable<uint> PositiveIntegers
        {
            get
            {
                for(uint i = 1; ; ++i)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<ulong> PositiveIntegers64
        {
            get
            {
                for (ulong i = 1; ; ++i)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<uint> Fibonacci
        {
            get
            {
                uint last = 1, current = 1, next;

                yield return last;
                
                while (true)
                {
                    yield return current;

                    next = last + current;
                    last = current;
                    current = next;
                }
                
            }
        }

        public static IEnumerable<ulong> Fibonacci64
        {
            get
            {
                ulong last = 1, current = 1, next;

                yield return last;

                while (true)
                {
                    yield return current;

                    next = last + current;
                    last = current;
                    current = next;
                }

            }
        }

    }
}
