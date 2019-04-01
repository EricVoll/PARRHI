using PARRHI.Objects.BaseElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Points
{
    public class Point : Element, IPoint
    {
        public Point(string id)
        {
            this.id = id;
        }

        public Point(string id, double x, double y, double z) : this(x, y, z)
        {
            this.id = id;
        }
        public Point(double x, double y, double z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public void SetCoords(Point point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Point operator -(Point a)
        {
            return new Point(-a.X, -a.Y, -a.Z);
        }
        public static Point operator *(double a, Point b)
        {
            return new Point(a * b.X, a * b.Y, a * b.Z);
        }
        public static double operator *(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public double this[int index]
        {

            // The get accessor.
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default:
                        throw new IndexOutOfRangeException();
                        break;
                }
            }

            // The set accessor.
            set
            {
                // set the value specified by index
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                    break;
                }
            }
        }
    }
}
