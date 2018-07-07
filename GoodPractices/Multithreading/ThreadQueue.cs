using System;
using System.Collections.Generic;
using System.Text;

namespace GoodPractices.Multithreading
{

    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ThreadQueue
    {
        public class ThreadsQueue
        {
            private readonly object lockObj = new object();
            private readonly Queue<Action<int>> actions = new Queue<Action<int>>();
            private Thread[] workers;

            public ThreadsQueue(int workerCount)
            {
                this.workers = new Thread[workerCount];

                // Create and start a separate thread for each worker
                for (var i = 0; i < workerCount; i++)
                {
                    this.workers[i] = new Thread(() => Consume(i));
                    this.workers[i].Start();
                }
            }

            public void Wait(bool waitForWorkers = true)
            {
                // Enqueue one null item per worker to make each exit.
                foreach (var worker in this.workers)
                {
                    Enqueue(null);
                }

                // Wait for workers to finish
                if (waitForWorkers)
                {
                    foreach (var worker in this.workers)
                    {
                        worker.Join();
                    }
                }
            }

            public void Enqueue(Action<int> item)
            {
                lock (this.lockObj)
                {
                    this.actions.Enqueue(item);
                    Monitor.Pulse(this.lockObj);
                }
            }

            private void Consume(int i)
            {
                while (true)
                {
                    Action<int> item;
                    lock (this.lockObj)
                    {
                        while (this.actions.Count == 0)
                        {
                            Monitor.Wait(this.lockObj);
                        }

                        item = this.actions.Dequeue();
                    }

                    //This signals our exit.
                    if (item == null) return;

                    item(i);
                }
            }
        }
    }
}
