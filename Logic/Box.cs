using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Box
    {
        public int wall { get; private set; }
        public List<Ball> ListOfBalls { get; private set; }

        public Task moving;

        public Box(int wallSize)
        {
            this.wall = wallSize;
            this.ListOfBalls = new List<Ball>();
        }

        public void controlMovingBalls()
        {
            moving = new Task(moveAllBalls);
        }

        private void moveAllBalls()
        {
            while (true)
            {
                foreach (Ball ball in ListOfBalls)
                {
                    ball.moveBall(wall);
                }
                Thread.Sleep(1);
            }

        }

        public List<Ball> getAllBalls()
        {
            return ListOfBalls;
        }


    }
}
