using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract void CreateBalls(int numberOfBalls);
        public abstract void MoveBalls();
        public abstract List<Ball> GetBalls();

        public static LogicAPI CreateLayer(DataAPI data = default) //?
        {
            return new Logic(data ?? DataAPI.CreateDataBall()); //?
        }

        private class Logic : LogicAPI
        {
            private Box box;
            private readonly DataAPI dataLayer;

            public Logic(DataAPI dataLayerAPI)
            {
                this.dataLayer = dataLayerAPI;
                box = new Box(400);

            }

            public override void CreateBalls(int numberOfBalls)
            {
                if(box != null)
                {
                    box.CreateBalls(numberOfBalls);
                }
            }

            public override void MoveBalls()
            {
                if (box != null)
                {
                    box.ControlMovingBalls();
                }
            }

            public override List<Ball> GetBalls()
            {
                if (box != null)
                {
                    return box.GetAllBalls();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
