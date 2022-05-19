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
        public void CreatingBallsTest()
        {
            Box box = new Box();
            int numberOfBalls = 6;

            box.CreateBalls(numberOfBalls);

            Assert.AreEqual(box.ListOfBalls.Count, numberOfBalls);
            Assert.AreEqual(box.ListOfBalls[0].Id, 0);
            Assert.AreEqual(box.ListOfBalls[1].Id, 1);
            Assert.AreEqual(box.ListOfBalls[2].Id, 2);
            Assert.AreEqual(box.ListOfBalls[3].Id, 3);
            Assert.AreEqual(box.ListOfBalls[4].Id, 4);
            Assert.AreEqual(box.ListOfBalls[5].Id, 5);
        }


        [TestMethod]
        public void BoxConstructorTest()
        {

            Box box = new Box();

            Assert.AreEqual(370, box.Wall);
        }
    }
}
