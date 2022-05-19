using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Data;

namespace Logic
{
    public abstract class LogicAPI : IObserver<int>, IObservable<int>
    {
        public abstract void OKLetsGo(int numberofballs);
        public abstract double GetBallPositionX(int ballId);
        public abstract double GetBallPositionY(int ballId);
        public abstract int GetBallDiameter(int ballId);
        public abstract IDisposable Subscribe(IObserver<int> observer);
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public static LogicAPI CreateLayer(DataBall data = default(DataBall))
        {
            return new Logic(data == null ? DataBall.CreateDataAPI() : data);
        }

        public class BallChaneEventArgs : EventArgs
        {
            public int ballId { get; set; }     ///nie wiem o co chodzi ale na razie wzialem 
        }

        private class Logic : LogicAPI, IObservable<int>
        {
            private readonly DataBall DataAPI;
            private IDisposable unsubscriber;
            static object _lock = new object();
            private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
            public event EventHandler<BallChaneEventArgs> BallChanged;

            public Logic(DataBall DataAPI)
            {
                eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
                this.DataAPI = DataAPI;
                Subscribe(DataAPI);
            }

            public override void OKLetsGo(int numberofballs)
            {
                DataAPI.CreateBalls(numberofballs);
            }

            public override double GetBallPositionX(int ballId)
            {
                return this.DataAPI.GetBallPositionX(ballId);
            }

            public override double GetBallPositionY(int ballId)
            {
                return this.DataAPI.GetBallPositionY(ballId);
            }

            public override int GetBallDiameter(int ballId)
            {
                return this.DataAPI.GetBallDiameter(ballId);
            }

            #region observer

            public virtual void Subscribe(IObservable<int> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public override void OnNext(int value)
            {
                Monitor.Enter(_lock);
                try
                {
                    
                    CollisionLogic CollisionLogic = new CollisionLogic(DataAPI.GetListOfBalls());

                    for (int i = 0; i < CollisionLogic.ListOfBalls.Count; i++)
                    {

                        if(CollisionLogic.ListOfBalls.Count % 2 == 0)
                        {

                        }

                        if(value != i)
                        {
                            CollisionLogic.wall(i);
                            CollisionLogic.kolizja(i);
                        }              
                    }

                    
                    BallChanged?.Invoke(this, new BallChaneEventArgs() { ballId = value });
                }
                catch (SynchronizationLockException exception)
                {
                    throw new Exception("Checking collision synchronization lock not working", exception);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }

            public override void OnCompleted()
            {
                Unsubscribe();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region observable
            public override IDisposable Subscribe(IObserver<int> observer)
            {
                return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.ballId), ex => observer.OnError(ex), () => observer.OnCompleted());
            }
            #endregion
        }

    }
}
