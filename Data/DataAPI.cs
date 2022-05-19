using System;
using System.Collections.Generic;
using System.Threading;

namespace Data
{
    public abstract class DataBall : IObserver<int>, IObservable<int>
    {

        public abstract double GetBallPositionX(int ballId);
        public abstract double GetBallPositionY(int ballId);
        public abstract int GetBallDiameter(int ballId);

        public abstract void CreateBalls(int numberofballs);
        public abstract int GetNumberOfBalls();

        public abstract List<Ball> GetListOfBalls();
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public abstract IDisposable Subscribe(IObserver<int> observer);

        public static DataBall CreateDataAPI()
        {
            return new DataAPI();
        }

        private class DataAPI : DataBall
        {
            private Box Box;
            private IDisposable unsubscriber;
            static object _lock = new object();
            private IList<IObserver<int>> observers;
            private Barrier barrier;
            public DataAPI()
            {
                this.Box = new Box();

                observers = new List<IObserver<int>>();
            }

            public override double GetBallPositionX(int ballId)
            {
                return this.Box.GetBall(ballId).Position.X;
            }

            public override double GetBallPositionY(int ballId)
            {
                return this.Box.GetBall(ballId).Position.Y;
            }

            public override int GetBallDiameter(int ballId)
            {
                return this.Box.GetBall(ballId).diameter;
            }

            public override List<Ball> GetListOfBalls()
            {
                return Box.GetAllBalls();
            }

            public override int GetNumberOfBalls()
            {
                return Box.ListOfBalls.Count;
            }

            public override void CreateBalls(int numberofballs)
            {
                barrier = new Barrier(numberofballs);
                Box.CreateBalls(numberofballs);

                foreach (var ball in Box.ListOfBalls)
                {
                    Subscribe(ball);
                    ball.MoveBallTask();
                }
            }

            #region observer

            public virtual void Subscribe(IObservable<int> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public override void OnCompleted()
            {
                Unsubscribe();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public override void OnNext(int value)
            {
                barrier.SignalAndWait();

                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }

            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region provider

            public override IDisposable Subscribe(IObserver<int> observer)
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
}