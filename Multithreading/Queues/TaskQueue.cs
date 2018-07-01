namespace Multithreading.Queues
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskQueue
    {
        private readonly Task[] _workers;
        private readonly ConcurrentQueue<Action> _testsQueue = new ConcurrentQueue<Action>();
        private readonly CancellationTokenSource _cancelationToken = new CancellationTokenSource();

        public TaskQueue(int workersCount)
        {
            _workers = new Task[workersCount];
        }

        public void Wait()
        {
            for (int i = 0; i < _workers.Length; i++)
            {
                _workers[i] = Task.Factory.StartNew(Consume, _cancelationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }

            try
            {
                Task.WaitAll(_workers);
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
            _testsQueue.Enqueue(action);
        }

        private void Consume()
        {
            Action action;
            while (_testsQueue.TryDequeue(out action))
            {
                action();
            }
        }
    }
}