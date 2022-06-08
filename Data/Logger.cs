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

        private void endlessLoop()
        {
            Monitor.Enter(_lock);
            sw = new StreamWriter("Ball.log", append: true);
            try
            {
                foreach (string i in fifo.GetConsumingEnumerable())
                {
                    sw.WriteLine(i);                   
                }
            }
            finally
            {
                Monitor.Exit(_lock);
                sw.Dispose();
            }
            
        }

        public Logger()
        {
            fifo = new BlockingCollection<string>();
            
            Task.Run(endlessLoop);
        }

        public void log(string t) => fifo.Add(DateTime.Now.ToString("HH:mm:ss ") + t);

        public void Dispose()
        {
            sw.Dispose();
        }
    }
}
