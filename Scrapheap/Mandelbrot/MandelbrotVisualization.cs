using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Mandelbrot
{
    public class MandelbrotVisualization : FrameworkElement
    {

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(nameof(Zoom), typeof(double), typeof(MandelbrotVisualization), new PropertyMetadata(1.0, ZoomPropertyChangedCallback));
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(nameof(Center), typeof(double), typeof(MandelbrotVisualization), new PropertyMetadata(0.0, CenterPropertyChangedCallback));

        public double Center { get => (double)GetValue(CenterProperty); set => SetValue(CenterProperty, value); }
        public double Zoom { get => (double)GetValue(ZoomProperty); set => SetValue(ZoomProperty, value); }

        private static void CenterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MandelbrotVisualization)d).OnCenterChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void ZoomPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MandelbrotVisualization)d).OnZoomChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnCenterChanged(double oldValue, double newValue)
        {
            
        }

        protected virtual void OnZoomChanged(double oldValue, double newValue)
        {
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);

            
        }
    }
}
