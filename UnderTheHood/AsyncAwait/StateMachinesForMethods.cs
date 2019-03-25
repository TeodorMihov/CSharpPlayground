using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnderTheHood.AsyncAwait
{
    [CompilerGenerated]
    [StructLayout(LayoutKind.Auto)]
    public struct d__1<FooAsync> : IAsyncStateMachine
    {
        public int Stack__1;
        public AsyncTaskMethodBuilder t__builder;
        public int state__1;

        void IAsyncStateMachine.MoveNext()
        {
            try
            {
                Console.WriteLine("Async method that doesn't have await");
            }
            catch (Exception ex)
            {
                this.state__1 = -2;
                this.t__builder.SetException(ex);
                return;
            }
            this.state__1 = -2;
            this.t__builder.SetResult();
        }

        [DebuggerHidden]
        void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.t__builder.SetStateMachine(stateMachine);
        }
    }

    [CompilerGenerated]
    [StructLayout(LayoutKind.Auto)]
    public struct d__2<BarAsync> : IAsyncStateMachine
    {
        private int state__1;
        private AsyncTaskMethodBuilder t__builder;
        private TaskAwaiter<int> u__1;

        void IAsyncStateMachine.MoveNext()
        {
            int num1 = this.state__1;
            try
            {
                TaskAwaiter<int> awaiter;
                int num2;
                if (num1 != 0)
                {
                    Console.WriteLine("This happens before await");
                    awaiter = Program.QuxAsync().GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        this.state__1 = num2 = 0;
                        this.u__1 = awaiter;
                        this.t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, d__2<BarAsync>> (ref awaiter, ref this);
                        return;
                    }
                }
                else
                {
                    awaiter = this.u__1;
                    this.u__1 = new TaskAwaiter<int>();
                    this.state__1 = num2 = -1;
                }
                Console.WriteLine("This happens after await. The result of await is " + (object)awaiter.GetResult());
            }
            catch (Exception ex)
            {
                this.state__1 = -2;
                this.t__builder.SetException(ex);
                return;
            }
            this.state__1 = -2;
            this.t__builder.SetResult();
        }

        [DebuggerHidden]
        void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.t__builder.SetStateMachine(stateMachine);
        }
    }
}
