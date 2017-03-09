using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Math.Geometry
{
    struct LineSegment : IEquatable<LineSegment>
    {
        public LineSegment(PointF p1, PointF p2)
        {
            this.Point1 = p1;
            this.Point2 = p2;
        }

        public PointF Point1 { get; }
        public PointF Point2 { get; }

        public bool Equals(LineSegment other)
        {
            return this.Point1.X == other.Point1.X && this.Point1.Y == other.Point1.Y && this.Point2.X == other.Point2.X && this.Point2.Y == other.Point2.Y;
        }
        
        public override bool Equals(object other)
        {
            if (!(other is LineSegment))
                return false;

            return  this.Equals((LineSegment)other);
        }

        public override int GetHashCode()
        {
            return Point1.GetHashCode() ^ Point2.GetHashCode();
        }

        public double Length
        {
            get
            {
                return System.Math.Sqrt(System.Math.Pow((Point1.X - Point2.X), 2) + System.Math.Pow((Point1.Y - Point2.Y), 2));
            }
        }

        public PointF MidPoint
        {
            get
            {
                return new PointF(
                    (Point2.X - Point1.X) / 2f,
                    (Point2.Y - Point1.Y) / 2f
                    );
            }
        }
    }

    struct Polygon
    {
        public Polygon(IEnumerable<PointF> points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            else if (points.Count() < 3)
                throw new ArgumentException("must specify at least 3 points", nameof(points));

            this.Points = points.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<PointF> Points { get; }

        public Polygon Scale(float scale)
        {
            return new Polygon(Points.Select(p => new PointF(p.X * scale, p.Y * scale)));
        }

        public bool Contains(PointF point)
        {
            bool polygonContainsPoint = false;

            int index1 = Points.Count - 1;

            // start with index1 being the last index and index2 being the first
            // this consider the edge of the polygon with edges between the last point and first point

            for (int index2 = 0; index2 < Points.Count; index2++)
            {
                if (Points[index2].Y < point.Y && Points[index1].Y >= point.Y || Points[index1].Y < point.Y && Points[index2].Y >= point.Y)
                {
                    if (Points[index2].X + (point.Y - Points[index2].Y) / (Points[index1].Y - Points[index2].Y) * (Points[index1].X - Points[index2].X) < point.X)
                    {
                        polygonContainsPoint = !polygonContainsPoint;
                    }
                }

                index1 = index2;
            }
            return polygonContainsPoint;
        }
    }
}
