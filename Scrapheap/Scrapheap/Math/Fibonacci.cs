using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Math
{
    public class Fibonacci : IEnumerable<ulong>
    {
        public Fibonacci() : this(1, 1)
        {
        }

        public Fibonacci(ulong first, ulong second)
        {
            this.First = first;
            this.Second = second;
        }

        public ulong First { get; }

        public ulong Second { get; }

        public IEnumerator<ulong> GetEnumerator()
        {
            return new Enumerator(First, Second);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class Enumerator : IEnumerator<ulong>
        {
            private readonly ulong _start1, _start2;

            public Enumerator(ulong start1, ulong start2)
            {
                _start1 = start1;
                _start2 = start2;
            }


            private ulong _current, _previous;

            bool _isStarted;

            public ulong Current
            {
                get
                {
                    if (!_isStarted)
                        throw new InvalidOperationException();

                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (_isStarted)
                {
                    _current = _start2;
                    _previous = _start1;

                    _isStarted = true;
                }else
                {
                    ulong last = _current;

                    _current += _previous;

                    _previous = last;
                }

                return true;
            }

            public void Reset()
            {
                _isStarted = false;
            }
        }
    }
}
