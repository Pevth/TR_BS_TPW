using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class BallModel : IBall
    {
        public int Diameter { get; }
        public double Top { get; }
        public double Left { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BallModel(double top, double left, int diameter)
        {
            Top = top;
            Left = left;
            Diameter = diameter;
        }
    }
}
