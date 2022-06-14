using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallMovingTest()
        {

            Box box = new Box();
            Ball ball = new Ball();

            double startingPositionX = ball.Position.X;
            double startingPositionY = ball.Position.Y;

            ball.MoveBall();

            Assert.AreEqual(ball.Position.X, startingPositionX + ball.Velocity.X);
            Assert.AreEqual(ball.Position.Y, startingPositionY + ball.Velocity.Y);

        }


        [TestMethod]
        public void BallConstructorTest()
        {
            Ball ball = new Ball();

            Assert.IsNotNull(ball.Position.X);
            Assert.IsNotNull(ball.Position.Y);
            Assert.IsNotNull(ball.Velocity.X);
            Assert.IsNotNull(ball.Velocity.Y);
            Assert.IsNotNull(ball.diameter);
            Assert.IsNotNull(ball.mass);
        }
       

        [TestMethod]
        public void BoxConstructorTest()
        {

            Box box = new Box();

            Assert.AreEqual(370, box.Wall);
        }
    }
}
