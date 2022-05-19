using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class Box
    {
        public int Wall { get; private set; }
        public List<Ball> ListOfBalls { get; private set; }

        public Box()
        {
            this.Wall = 370;
            this.ListOfBalls = new List<Ball>();
        }

        public void CreateBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                Ball ball = new Ball();
                ball.Id = i;
                ListOfBalls.Add(ball);
            }
        }

        public List<Ball> GetAllBalls()
        {
            return ListOfBalls;
        }

        public Ball GetBall(int index)
        {
            return ListOfBalls[index];
        }


    }
}
