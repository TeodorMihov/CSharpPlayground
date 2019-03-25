using System;
using System.Runtime.CompilerServices;

namespace UnderTheHood.AsyncAwait
{
    public class MyAwaiter : INotifyCompletion
    {
        public void GetResult()
        {
        }

        public bool IsCompleted
        {
            get { return false; }
        }

        //From INotifyCompletion
        public void OnCompleted(Action continuation)
        {
        }
    }
}
