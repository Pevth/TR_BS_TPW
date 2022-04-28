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
        public abstract void CreateBalls(Box box, int numberOfBalls);
        public abstract void MoveBalls(Box box);
        public abstract List<Ball> GetBalls(Box box);

        public abstract Ball GenerateBall();

        public abstract Box GenerateBox();

        public static LogicAPI CreateApi(DataAPI data = default) 
        {
            return new Logic(data ?? DataAPI.CreateDataBall()); 
        }

        private class Logic : LogicAPI
        {
            private readonly DataAPI dataLayer;

            public Logic(DataAPI dataLayerAPI)
            {
                this.dataLayer = dataLayerAPI;

            }

            public override Ball GenerateBall()
            {
                return new Ball();
            }

            public override Box GenerateBox()
            {
                return new Box(370);
            }

            public override void CreateBalls(Box box, int numberOfBalls)
            {
                if (box != null)
                {
                    box.CreateBalls(numberOfBalls);
                }
            }

            public override void MoveBalls(Box box)
            {
                if (box != null)
                {
                    box.ControlMovingBalls();
                }
            }

            public override List<Ball> GetBalls(Box box)
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
