using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Data
{

    public static class Time
    {
        public static DateTime dateTime;

        public static string getData()
        {
            dateTime = DateTime.Now;
            string sData = dateTime.ToString();
            sData = sData.Replace(" ", "_");
            sData = sData.Replace(":", "-");
            return sData;
        }
    }

    public class Logger: IDisposable
    {
        public static string filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}/Logs/";
        BlockingCollection<string> fifo;
        StreamWriter sw;
        string filename = $"{filePath}Ball_{Time.getData()}.log";
        private void fifoControllLoop()
        {           
            try
            {               
                foreach (string i in fifo.GetConsumingEnumerable())
                {                   
                    sw.WriteLine(i);
                }
            }
            finally
            {               
                Dispose();               
            }
                                    
        }

        public void isFolderExist()
        {
            if (!Directory.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}/Logs"))
            {
                Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}/Logs");
            }
        }

        public Logger()
        {
            isFolderExist();
            fifo = new BlockingCollection<string>();        
            sw = new StreamWriter(filename, true);            
            Task.Run(fifoControllLoop);                     
        }

        public void log(Ball ball) => fifo.Add(DateTime.Now.ToString("HH:mm:ss ") + " ID: " + ball.Id + " Position.X: " + Math.Round(ball.Position.X, 4) + " Position.Y: " + Math.Round(ball.Position.Y, 4) + " Velocity.X: " + Math.Round(ball.Velocity.X, 4) + " Velocity.Y: " + Math.Round(ball.Velocity.Y, 4));

        public void Dispose()
        {
            sw.Dispose();
            fifo.Dispose();
        }
    }
}
