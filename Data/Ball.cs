using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : IObservable<int>
    {
        public int Id { get; set; }
        public Vector Position { get; set; }
        public Vector Center { get; set; }
        public Vector Velocity { get; set; }
        public int diameter = 20;
        public double mass = 10;
        private Task BallTask;
        private Stopwatch Timer = new Stopwatch();
        private Logger log;

        internal readonly IList<IObserver<int>> observers;
        public Ball()
        {
            Random rnd = new Random();
            Vector position = new Vector();
            Vector velocity = new Vector();
            Logger Log = new Logger();

            observers = new List<IObserver<int>>();

            //position X i Y jest zaczytana z Vectora

            position.X = GetRandomNumber(30, 340);
            position.Y = GetRandomNumber(30, 340);

            //-----------------------------

            velocity.X = GetRandomNumber(-0.2, 0.2);
            velocity.Y = GetRandomNumber(-0.2, 0.2);

            //-----------------------------

            this.Position = position;
            this.Velocity = velocity;
            this.Center = position + new Vector(this.diameter / 2, this.diameter / 2);
            this.log = Log;
        }

        public void MoveBallTask()
        {
            this.BallTask = new Task(MoveBallInLoop);
            BallTask.Start();
        }

        public void MoveBallInLoop()
        {
            while (true)
            {
                Timer.Restart();
                Timer.Start();
                MoveBall(Timer.ElapsedMilliseconds);
                BallLog(ToString());
                foreach (var observer in observers.ToList())
                {
                    if (observer != null)
                    {
                        observer.OnNext(Id);
                    }
                }
                Timer.Stop();
                //System.Threading.Thread.Sleep(1);

            }
        }


        public void MoveBall(long time)
        {
            if (time > 0)
            {

                Position += Velocity * time;
                Center += Velocity * time;
                /*
                Position.X += Velocity.X * time;
                Position.Y += Velocity.Y * time;
                Center.X += Velocity.X  * time;
                Center.Y += Velocity.Y  * time;
                */
            }
            else
            {
                Position += Velocity;
                Center += Velocity;
                /*
                Position.X += Velocity.X;
                Position.Y += Velocity.Y;
                Center.X += Velocity.X;
                Center.Y += Velocity.Y;
                */
            }
        }

        public void BallLog(string stats)
        {
            log.log(stats);
        }

        public override string ToString()
        {
            return "ID: " + Id + "Position.X: " + Position.X + "Position.Y: " + Position.Y + "Velocity.X: " + Velocity.X + "Velocity.Y: " + Velocity.Y;
        }


        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }



        #region provider

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<int>> _observers;
            private IObserver<int> _observer;

            public Unsubscriber
            (IList<IObserver<int>> observers, IObserver<int> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        #endregion
    }
}
