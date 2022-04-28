using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Box
    {
        public int Wall { get; private set; }
        public List<Ball> ListOfBalls { get; private set; }

        public Task moving;

        public Box(int wallSize)
        {
            this.Wall = wallSize;
            this.ListOfBalls = new List<Ball>();
        }

        public void CreateBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                ListOfBalls.Add(new Ball());
            }
        }


        public void ControlMovingBalls()
        {
            moving = new Task(MoveAllBalls);
            moving.Start();
        }

        public void MoveAllBalls()
        {
            while (true)
            {
                foreach (Ball ball in ListOfBalls)
                {
                    ball.MoveBall(Wall);
                }
                Thread.Sleep(1);
            }

        }

        public List<Ball> GetAllBalls()
        {
            return ListOfBalls;
        }


    }
}
