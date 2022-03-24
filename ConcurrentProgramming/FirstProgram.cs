namespace ConcurrentProgramming
{
    public class FirstProgram
    {
        private double a;
        private double b;

        public FirstProgram(double x, double y)
        {
            this.a = x;
            this.b = y;
        }

        public double A
        {
            get { return a; }   
            set { a = value; }
        }
        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public String HelloWorld()
        {
            return "Hello World!";
        }

        public double Add()
        {
            return a + b;
        }

    }
}