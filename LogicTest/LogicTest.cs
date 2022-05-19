using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {
        
        [TestMethod]
        public void CollideWallTest()
        {
            CollisionLogic collisionLogic = new CollisionLogic();
            Ball ball = new Ball();
            ball.Position.X = 369;
            ball.Position.Y = 369;
            ball.Velocity.X = 1;
            ball.Velocity.Y = 1;

            ball.MoveBall();
            collisionLogic.CollideWall(ball);

            Assert.AreEqual(ball.Velocity.X, -1);
            Assert.AreEqual(ball.Velocity.Y, -1);
        }

        [TestMethod]
        public void DetectCollisionTest()
        {
            CollisionLogic collisionLogic = new CollisionLogic();
            Ball ball1 = new Ball();
            ball1.Position.X = 235;
            ball1.Position.Y = 85;
            ball1.Center = ball1.Position + new Vector(ball1.diameter / 2, ball1.diameter / 2);
            ball1.Velocity.X = 10;
            ball1.Velocity.Y = 10;

            Ball ball2 = new Ball();
            ball2.Position.X = 250;
            ball2.Position.Y = 100;
            ball2.Center = ball2.Position + new Vector(ball2.diameter / 2, ball2.diameter / 2);
            ball2.Velocity.X = 0;
            ball2.Velocity.Y = 0;

            ball1.MoveBall();

            Assert.IsTrue(collisionLogic.DetectCollision(ball1, ball2));
        }

        [TestMethod]

        public void ChangeVelocitiesTest()
        {
            CollisionLogic collisionLogic = new CollisionLogic();
            Ball ball1 = new Ball();
            ball1.Position.X = 235;
            ball1.Position.Y = 85;
            ball1.Center = ball1.Position + new Vector(ball1.diameter / 2, ball1.diameter / 2);
            ball1.Velocity.X = 10;
            ball1.Velocity.Y = 10;

            Ball ball2 = new Ball();
            ball2.Position.X = 250;
            ball2.Position.Y = 100;
            ball2.Center = ball2.Position + new Vector(ball2.diameter / 2, ball2.diameter / 2);
            ball2.Velocity.X = 0;
            ball2.Velocity.Y = 0;

            Vector tempVelocity = ball1.Velocity;

            collisionLogic.ChangeVelocities(ball1,ball2);

            Assert.AreNotEqual(collisionLogic.ChangeVelocities(ball1, ball2), tempVelocity);
        }




    }
}