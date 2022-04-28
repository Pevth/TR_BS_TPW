using Logic;
using System.Collections.Generic;

namespace Model
{
    public abstract class ModelAPI
    {
        public abstract List<BallModel> ListOfBallModel { get; }

        public abstract void createBalls(int numberOfBalls);

        public abstract void moveBalls();

        public static ModelAPI CreateApi()
        {
            return new Model();
        }


        internal class Model : ModelAPI
        {
            internal LogicAPI LogicAPI;

            public BoxModel boxX;
            public override List<BallModel> ListOfBallModel => CopyBallFromBox();

            public Model()
            {
                LogicAPI = LogicAPI ?? LogicAPI.CreateApi();
                boxX = new BoxModel(LogicAPI.GenerateBox());
            }

            public override void createBalls(int numberOfBalls)
            {
                LogicAPI.CreateBalls(boxX.box, numberOfBalls);
            }

            public override void moveBalls()
            {
                LogicAPI.MoveBalls(boxX.box);
            }

            public List<BallModel> CopyBallFromBox()
            {
                List<BallModel> copiesOfBall = new List<BallModel>();

                foreach (Ball ball in LogicAPI.GetBalls(boxX.box))
                {
                    copiesOfBall.Add(new BallModel(ball));
                }

                return copiesOfBall;
            }

        }
    }
}