using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball
    {
        public Vector position { get; private set; }
        public Vector velocity { get; private set; }

        internal Ball()
        {
            Random rnd = new Random();
            Vector position = new Vector();
            Vector velocity = new Vector();

            //position X i Y jest zaczytana z Vectora

            position.X = rnd.Next(100);
            position.Y = rnd.Next(100);

            //-----------------------------

            velocity.X = GetRandomNumber(1.0, 3.0);
            velocity.Y = GetRandomNumber(1.0, 3.0);

            //-----------------------------

            this.position = position;
            this.velocity = velocity;
        }

        internal void moveBall(int wall)
        {
            collideWall(this.position, this.velocity, wall);
            position += velocity;
        }
        private void collideWall(Vector position, Vector velocity, int wall)
        {

            if (this.position.X >= wall || position.X < 0)
                this.velocity.X *= -1;

            if (this.position.Y >= wall || position.Y < 0)
                this.velocity.Y *= -1;

        }
        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
