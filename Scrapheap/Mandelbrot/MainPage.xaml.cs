using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mandelbrot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TheCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs e)
        {
            /*
             * 
             * 
             * FROM https://en.wikipedia.org/wiki/Mandelbrot_set#Computer_drawings
For each pixel (Px, Py) on the screen, do:
{
  x0 = scaled x coordinate of pixel (scaled to lie in the Mandelbrot X scale (-2.5, 1))
  y0 = scaled y coordinate of pixel (scaled to lie in the Mandelbrot Y scale (-1, 1))
  x = 0.0
  y = 0.0
  iteration = 0
  max_iteration = 1000
  while (x*x + y*y < 2*2  AND  iteration < max_iteration) {
    xtemp = x*x - y*y + x0
    y = 2*x*y + y0
    x = xtemp
    iteration = iteration + 1
  }
  color = palette[iteration]
  plot(Px, Py, color)
} 

             */


            double paletteMin = 30, palletteMax = 330;

            Rect rect;
            Color color;

            List<Color> pallette = new List<Color>();

            Rect bounds;

            bounds = new Rect(new Point(-2, 0.5), new Point(-1, -0.5));

            bounds = GetBounds(-0.7463, 0.1102, 0.005);

            bounds = GetBounds(-0.7453, 0.1127, 0.00065);

            int iterations = 10000;

            double palletteStep = (palletteMax - paletteMin) / (double)iterations;
            double hue;
            for (int i = 0; i < iterations; ++i)
            {
                hue = paletteMin + (i * palletteStep);

                while (hue > 360)
                {
                    hue -= 360.0;
                }

                pallette.Add(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(hue, 1, 1));
            }

            double deltaX = bounds.Width / this.ActualWidth;
            double deltaY = bounds.Height / this.ActualHeight;

            Point logicalPoint;

            double x, y;

            for (double pX = 0.0; pX < this.ActualWidth; ++pX)
            {
                x = (pX * deltaX) + bounds.Left;
                for (double pY = 0; pY < this.ActualHeight; ++pY)
                {
                    y = (pY * deltaY) + bounds.Top;
                    logicalPoint = new Point(x, y);
                    color = GetColorForPoint(pallette, logicalPoint);

                    rect = new Rect(new Point(pX, pY), new Size(1, 1));

                    e.DrawingSession.DrawRectangle(rect, color);
                }
            }

        }

        private Rect GetBounds(double centerX, double centerY, double size)
        {
            double halfSize = size / 2.0;

            return new Rect(centerX - halfSize, centerY - halfSize, size, size);
        }

        private static Color GetColorForPoint(List<Color> palette, Point point)
        {
            int iteration = 0;
            double x = 0, y = 0;
            double xtemp;
            for (; x * x + y * y < 2 * 2 && iteration < palette.Count; ++iteration)
            {
                xtemp = x * x - y * y + point.X;
                y = 2 * x * y + point.Y;
                x = xtemp;
            }

            if (iteration < palette.Count)
            {
                return palette[iteration];
            }
            else
            {
                return Colors.Black;
            }
        }
    }
}
