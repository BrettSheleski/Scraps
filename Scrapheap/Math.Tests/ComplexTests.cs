using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math.Tests
{
    [TestClass]
    public class ComplexTests
    {
        [TestMethod]
        public void Complex_Magnitude_345()
        {
            // Setup
            Complex z = new Complex(3, 4);
            const int expectedMagnitude = 5;

            // Act
            var magnitude = z.Magnitude;

            // Verify
            Assert.IsTrue(magnitude == expectedMagnitude);
        }

        [TestMethod]
        public void Complex_Addition()
        {
            // Setup
            var z = new Complex(3, 0);
            var w = new Complex(0, 4);

            // Act
            var zPlusW = z + w;

            // Verify
            Assert.IsTrue(zPlusW.Real == (z.Real + w.Real));
            Assert.IsTrue(zPlusW.Imaginary == (z.Imaginary + w.Imaginary));
        }

        [TestMethod]
        public void Complex_Casting()
        {
            // Setup
            double real = 3;
            Complex z;

            double d = real;
            float f = (float)d;

            int i = (int)d;
            uint ui = (uint)i;

            short s = (short)i;
            ushort us = (ushort)s;

            long l = i;
            ulong ul = (ulong)l;

            // Act
            z = i;
            z = ui;
            z = s;
            z = us;
            z = l;
            z = ul;

            // Verify
            // nothing, just making sure the above casts work without exception
        }

        [TestMethod]
        public void Complex_Multiplication_ByComplex()
        {
            // setup
            Complex z1 = new Complex(2, 0);
            Complex z2 = new Complex(0, 2);
            Complex zProduct;

            // act
            zProduct = z1 * z2;

            // Verify
            Assert.IsTrue(zProduct.Real == 0 && zProduct.Imaginary == 4);

        }

        [TestMethod]
        public void Complex_Equality()
        {
            // setup
            Complex z1 = new Complex(3, 4);
            Complex z2 = new Complex(3, 4);

            // act

            // verify
            Assert.IsTrue(z1.Equals(z2));
            Assert.IsTrue(z2.Equals(z1));
            Assert.IsTrue(z1 == z2);
            Assert.IsFalse(z1 != z2);
        }

        [TestMethod]
        public void Complex_Division_Example1()
        {
            // see http://www.mesacc.edu/~scotz47781/mat120/notes/complex/dividing/dividing_complex.html

            // Setup
            Complex c1 = new Complex(3, 2);
            Complex c2 = new Complex(4, -3);
            Complex result;
            double expectedReal = 6 / 25.0, 
                expectedImaginary = 17 / 25.0;

            // Act
            result = c1 / c2;

            // Verify
            Assert.IsTrue(result.Real == expectedReal);
            Assert.IsTrue(result.Imaginary == expectedImaginary);
        }

        [TestMethod]
        public void Complex_Division_Example2()
        {
            // see http://www.mesacc.edu/~scotz47781/mat120/notes/complex/dividing/dividing_complex.html

            // Setup
            Complex c1 = new Complex(4, 5);
            Complex c2 = new Complex(2, 6);
            Complex result;
            double expectedReal = 19 / 20.0,
                expectedImaginary = -7 / 20.0;

            // Act
            result = c1 / c2;

            // Verify
            Assert.IsTrue(result.Real == expectedReal);
            Assert.IsTrue(result.Imaginary == expectedImaginary);
        }

        [TestMethod]
        public void Complex_Division_Example3()
        {
            // see http://www.mesacc.edu/~scotz47781/mat120/notes/complex/dividing/dividing_complex.html

            // Setup
            Complex c1 = new Complex(2, -1);
            Complex c2 = new Complex(-3, 6);
            Complex result;
            double expectedReal = -4 / 15.0,
                expectedImaginary = -1 / 5.0;

            // Act
            result = c1 / c2;

            // Verify
            Assert.IsTrue(result.Real == expectedReal);
            Assert.IsTrue(result.Imaginary == expectedImaginary);
        }

        [TestMethod]
        public void Complex_Division_Example4()
        {
            // see http://www.mesacc.edu/~scotz47781/mat120/notes/complex/dividing/dividing_complex.html

            // Setup
            Complex c1 = new Complex(-6, -3);
            Complex c2 = new Complex(4, 6);
            Complex result;
            double expectedReal = -21 / 26.0,
                expectedImaginary = 6 / 13.0;

            // Act
            result = c1 / c2;

            // Verify
            Assert.IsTrue(result.Real == expectedReal);
            Assert.IsTrue(result.Imaginary == expectedImaginary);
        }

        [TestMethod]
        public void Complex_Multiplication_ByScalars()
        {
            // Setup
            Complex z = new Complex(3, 4);
            short shortScalar = 5;
            ushort ushortScalar = (ushort)shortScalar;
            int intScalar = ushortScalar;
            uint uintScalar = ushortScalar;
            long longScalar = ushortScalar;
            ulong ulongScalar = ushortScalar;
            double doubleScalar = ushortScalar;
            float floatScalar = ushortScalar;
            Complex expectedScaledZ = new Complex(z.Real * shortScalar, z.Imaginary * shortScalar);

            Complex shortZ, ushortZ, intZ, uintZ, longZ, ulongZ, doubleZ, floatZ;

            // Act
            shortZ = shortScalar * z;
            ushortZ = ushortScalar * z;
            intZ = intScalar * z;
            uintZ = uintScalar * z;
            longZ = longScalar * z;
            ulongZ = ulongScalar * z;
            doubleZ = doubleScalar * z;
            floatZ = floatScalar * z;

            // Verify
            Assert.IsTrue(shortZ == expectedScaledZ);
            Assert.IsTrue(ushortZ == expectedScaledZ);
            Assert.IsTrue(intZ == expectedScaledZ);
            Assert.IsTrue(uintZ == expectedScaledZ);
            Assert.IsTrue(longZ == expectedScaledZ);
            Assert.IsTrue(ulongZ == expectedScaledZ);
            Assert.IsTrue(doubleZ == expectedScaledZ);
            Assert.IsTrue(floatZ == expectedScaledZ);
        }

    }
}
