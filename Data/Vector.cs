using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }


        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        // Mnożenie przez skalar

        public Vector Scale(double scale)
        {
            return new Vector(scale * X, scale * Y);
        }

        public static Vector operator *(double k, Vector v1)
        {
            return new Vector(k * v1.X, k * v1.Y);
        }

        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(k * v1.X, k * v1.Y);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        // Mnożenie przez skalar

        public double DotProduct(Vector v2)
        {
            return (X * v2.X + Y * v2.Y);
        }

        // Normalizacja wektora

        public Vector Unit()
        {
            double length = this.Length();
            return new Vector(X / length, Y / length);
        }

        // rzutowanie wektora v2 na ten wektor
        public Vector ParralelComponent(Vector v2)
        {
            double lengthSquared, dotProduct, scale;
            lengthSquared = Math.Pow(Length(), 2);
            dotProduct = DotProduct(v2);
            if (lengthSquared != 0)
                scale = dotProduct / lengthSquared;
            else
                return new Vector();
            return new Vector(this.Scale(scale));
        }

        public Vector PerpendicularComponent(Vector v2)
        {
            return new Vector(v2 - this.ParralelComponent(v2)); 
        }

    }

}


