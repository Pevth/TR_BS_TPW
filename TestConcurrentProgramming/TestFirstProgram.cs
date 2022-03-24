using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestConcurrentProgramming
{
    [TestClass]
    public class TestFirstProgram
    {
        [TestMethod]
        public void TestHelloWorld()
        {
            ConcurrentProgramming.FirstProgram testProgram = new ConcurrentProgramming.FirstProgram(5, 6);
            Assert.AreEqual(testProgram.HelloWorld(), "Hello World!");
        }
        [TestMethod]
        public void TestAdd()
        {
            ConcurrentProgramming.FirstProgram testProgram = new ConcurrentProgramming.FirstProgram(5, 6);
            Assert.AreEqual(testProgram.Add(), 11);
            Assert.AreNotEqual(testProgram.Add(), 10);
            testProgram.A = 7;
            Assert.AreEqual(testProgram.Add(), 13);
        }
    }
}