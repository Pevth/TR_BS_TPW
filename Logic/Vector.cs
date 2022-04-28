using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
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

        
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

    }
}
