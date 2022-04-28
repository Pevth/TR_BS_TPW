namespace Data
{
    public abstract class DataAPI
    {
        public static DataAPI CreateDataBall()
        {
            return new DataBall();
        }

        private class DataBall : DataAPI
        {
            public DataBall()
            {

            }
        }
    }
}