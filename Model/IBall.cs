using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public interface IBall : INotifyPropertyChanged
    {
        double Top { get; }
        double Left { get; }
        int Diameter { get; }
    }
}
