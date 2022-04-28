using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Threading.Tasks;
using System.Threading;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void BallMovingTest()
        {

            Box box = new Box(370);
            Ball ball = new Ball();

            double startingPositionX = ball.Position.X;
            double startingPositionY = ball.Position.Y;

            ball.MoveBall(box.Wall);

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
        }

        [TestMethod]
        public void CreatingBallsTest()
        {
            Box box = new Box(370);
            int numberOfBalls = 5;

            box.CreateBalls(numberOfBalls);

            Assert.AreEqual(box.ListOfBalls.Count, numberOfBalls);     

        }


        [TestMethod]
        public void BoxConstructorTest()
        {
            int wall = 400;
            Box box = new Box(wall);

            Assert.AreEqual(wall, box.Wall);
        }


    }
}