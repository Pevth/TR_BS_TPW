using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;

namespace Model
{
    public class BallChaneEventArgs : EventArgs
    {
        public IBall Ball { get; set; }
    }

    public abstract class ModelAPI : IObservable<IBall>
    {
        public static ModelAPI CreateApi()
        {
            return new ModelBall();
        }

        public abstract void OKLetsGo(int ballsAmount);

        #region IObservable

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        #endregion IObservable

        internal class ModelBall : ModelAPI
        {
            private LogicAPI logicApi;
            public event EventHandler<BallChaneEventArgs> BallChanged;

            private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
            private List<BallModel> Balls = new List<BallModel>();

            public ModelBall()
            {
                logicApi = logicApi ?? LogicAPI.CreateLayer();
                IDisposable observer = logicApi.Subscribe<int>(x => Balls[x].Move(logicApi.GetBallPositionX(x), logicApi.GetBallPositionY(x)));
                eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
            }

            public override void OKLetsGo(int numberofballs)
            {
                logicApi.OKLetsGo(numberofballs);
                for (int i = 0; i < numberofballs; i++)
                {
                    BallModel newBall = new BallModel(logicApi.GetBallPositionX(i), logicApi.GetBallPositionY(i), logicApi.GetBallDiameter(i));
                    Balls.Add(newBall);
                }

                foreach (BallModel ball in Balls)
                {
                    BallChanged?.Invoke(this, new BallChaneEventArgs() { Ball = ball });
                }

            }

            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
            }
        }
    }
}