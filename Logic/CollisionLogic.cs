using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class CollisionLogic
    {
        public List<Ball> ListOfBalls { get; set; }
        
        public CollisionLogic(List <Ball> BallList)
        {
            ListOfBalls = BallList;
        }

        public CollisionLogic()
        {
            
        }

        public void CollideWall(Ball ball1)
        {
            if (ball1.Position.X >= 370 || ball1.Position.X < 0)
                ball1.Velocity.X *= -1;

            if (ball1.Position.Y >= 370 || ball1.Position.Y < 0)
                ball1.Velocity.Y *= -1;
        }

        public bool DetectCollision(Ball ball1, Ball ball2)
        {
            Vector vec = ball1.Center - ball2.Center;
            double len = vec.Length();
            if (len < (ball1.diameter / 2 + ball2.diameter / 2))
            {
                return true;
            }
            else return false;
        }

        public void CheckCollision(Ball ball1, Ball ball2)
        {
            if (DetectCollision(ball1, ball2))
            {
                var tempVelocity = ChangeVelocities(ball1, ball2);
                ball2.Velocity = ChangeVelocities(ball2, ball1);
                ball1.Velocity = tempVelocity;
            }
        }

        public Vector ChangeVelocities(Ball ball1, Ball ball2)
        {
            Vector centresVector = ball2.Center - ball1.Center;
            Vector ballOnePerpendicular = centresVector.PerpendicularComponent(ball1.Velocity);
            Vector ballTwoPerpendicular = centresVector.PerpendicularComponent(ball2.Velocity);

            Vector ballTwoPara = centresVector.ParralelComponent(ball2.Velocity);

            Vector ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; 

            return ballOneNewVelocity;
        }

        public void CollideWallChecking(int index)
        {
            CollideWall(ListOfBalls[index]);
        }

        public void CollisionBallChecking(int index)
        {
            for (int j = index; j < ListOfBalls.Count; j++)
            {              
                CheckCollision(ListOfBalls[index], ListOfBalls[j]);          
            }
        }




    }
}
