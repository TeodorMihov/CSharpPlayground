namespace GoodPractices.Multithreading
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskQueue
    {
        private readonly Task[] workers;
        private readonly ConcurrentQueue<Action> testsQueue = new ConcurrentQueue<Action>();
        private readonly CancellationTokenSource cancelationToken = new CancellationTokenSource();

        public TaskQueue(int workersCount)
        {
            this.workers = new Task[workersCount];
        }

        public void Wait()
        {
            for (var i = 0; i < this.workers.Length; i++)
            {
                this.workers[i] = Task.Factory.StartNew(this.Consume, this.cancelationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }

            try
            {
                Task.WaitAll(this.workers);
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    return e is OperationCanceledException;
                });
            }
        }

        public void Enqueue(Action action)
        {
            this.testsQueue.Enqueue(action);
        }

        private void Consume()
        {
            while (this.testsQueue.TryDequeue(out var action))
            {
                action();
            }
        }
    }
}
