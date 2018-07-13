using System;
using System.Collections.Generic;

namespace Math
{
    public struct Complex : IEquatable<Complex>
    {
        public Complex(double real, double imaginary)
        {
            this.Real = real;
            this.Imaginary = imaginary;
            
            _magnitude = null;
        }

        public double Real { get; }
        public double Imaginary { get; }
        double? _magnitude;

        public double Magnitude
        {
            get
            {
                if (_magnitude == null)
                {
                    _magnitude = System.Math.Sqrt(Real * Real + Imaginary * Imaginary);
                }

                return _magnitude.Value;
            }
        }

        public static Complex I { get; } = new Complex(0, 1);


        public Complex GetConjugate()
        {
            return new Complex(this.Real, this.Imaginary * -1);
        }

        public static Complex operator+(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            double first = c1.Real * c2.Real,  // will result in a real
                outside = c1.Real * c2.Imaginary, // will result in imaginary
                inside = c1.Imaginary * c2.Real,  // will result in imaginary
                last = c1.Imaginary * c2.Imaginary; // will result in a (negative) real

            return new Complex(first + -last, outside + inside);
        }

        public static Complex operator /(Complex c1, Complex c2)
        {
            Complex conjugate = c2.GetConjugate();

            Complex numerator = c1 * conjugate, 
                denominator = c2 * conjugate;

            // the denominator will be completely-real

            return new Complex(numerator.Real / denominator.Real, numerator.Imaginary / denominator.Real);
        }


        public static implicit operator Complex(int n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(uint n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(short n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(ushort n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(long n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(ulong n)
        {
            return new Complex(n, 0);
        }

        public static implicit operator Complex(double d)
        {
            return new Complex(d, 0);
        }

        public static implicit operator Complex(float d)
        {
            return new Complex(d, 0);
        }

        public bool Equals(Complex other)
        {
            return this.Real == other.Real && this.Imaginary == other.Imaginary;
        }

        public override int GetHashCode()
        {
            var hashCode = 423454661;
            hashCode = hashCode * -1521134295 + Real.GetHashCode();
            hashCode = hashCode * -1521134295 + Imaginary.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex z)
            {
                return this.Equals(z);
            }

            return false;
        }

        public static bool operator ==(Complex c1, Complex c2) => c1.Equals(c2);
        public static bool operator !=(Complex c1, Complex c2) => !c1.Equals(c2);
    }
}
