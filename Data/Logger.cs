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
    
    public class Logger: IDisposable
    {
        BlockingCollection<string> fifo;
        StreamWriter sw;
        static object _lock = new object();
        string filename = "Ball.log";
        private void endlessLoop()
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

        public Logger()
        {
            fifo = new BlockingCollection<string>();        
            sw = new StreamWriter(filename, true);            
            Task.Run(endlessLoop);                     
        }

        public void log(Ball ball) => fifo.Add(DateTime.Now.ToString("HH:mm:ss ") + " ID: " + ball.Id + " Position.X: " + Math.Round(ball.Position.X, 4) + " Position.Y: " + Math.Round(ball.Position.Y, 4) + " Velocity.X: " + Math.Round(ball.Velocity.X, 4) + " Velocity.Y: " + Math.Round(ball.Velocity.Y, 4));

        public void Dispose()
        {
            sw.Dispose();
            fifo.Dispose();
        }
    }
}
