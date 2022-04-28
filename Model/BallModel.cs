using Logic;

namespace Model
{
    public class BallModel
    {
        private Ball ball;

        public BallModel(Ball ball)
        {
            this.ball = ball;
        }


        public double getPositionX
        {
            get { return ball.Position.X; }
        }

        public double getPositionY
        {
            get { return ball.Position.Y; }
        }

    }
}
