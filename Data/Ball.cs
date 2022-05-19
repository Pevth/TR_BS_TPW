using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball
    {
        public Vector Position { get; private set; }
        public Vector Velocity { get; private set; }

        public Ball()
        {
            Random rnd = new Random();
            Vector position = new Vector();
            Vector velocity = new Vector();

            //position X i Y jest zaczytana z Vectora

            position.X = rnd.Next(100);
            position.Y = rnd.Next(100);

            //-----------------------------

            velocity.X = GetRandomNumber(1.0, 2.0);
            velocity.Y = GetRandomNumber(1.0, 2.0);

            //-----------------------------

            this.Position = position;
            this.Velocity = velocity;
        }

        public void MoveBall(int wall)
        {
            CollideWall(this.Position, wall);
            Position += Velocity;
        }
        private void CollideWall(Vector position, int wall)
        {

            if (this.Position.X >= wall || position.X < 0)
                this.Velocity.X *= -1;

            if (this.Position.Y >= wall || position.Y < 0)
                this.Velocity.Y *= -1;

        }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}