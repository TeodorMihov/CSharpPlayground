namespace Multithreading.Queues
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ThreadQueue
    {
        public class ThreadsQueue
        {
            private readonly object _lock = new object();
            private readonly Queue<Action<int>> _actions = new Queue<Action<int>>();
            private Thread[] _workers;

            public ThreadsQueue(int workerCount)
            {
                _workers = new Thread[workerCount];

                // Create and start a separate thread for each worker
                for (int i = 0; i < workerCount; i++)
                {
                    _workers[i] = new Thread(() => Consume(i));
                    _workers[i].Start();
                }
            }

            public void Wait(bool waitForWorkers = true)
            {
                // Enqueue one null item per worker to make each exit.
                foreach (Thread worker in _workers)
                {
                    Enqueue(null);
                }

                // Wait for workers to finish
                if (waitForWorkers)
                {
                    foreach (Thread worker in _workers)
                    {
                        worker.Join();
                    }
                }
            }

            public void Enqueue(Action<int> item)
            {
                lock (_lock)
                {
                    _actions.Enqueue(item);
                    Monitor.Pulse(_lock);
                }
            }

            private void Consume(int i)
            {
                while (true)
                {
                    Action<int> item;
                    lock (_lock)
                    {
                        while (_actions.Count == 0)
                        {
                            Monitor.Wait(_lock);
                        }

                        item = _actions.Dequeue();
                    }

                    //This signals our exit.
                    if (item == null) return;

                    item(i);
                }
            }
        }
    }
}
