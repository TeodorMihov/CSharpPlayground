namespace UnderTheHood.AsyncAwait
{
    public class MyAwaitableClass
    {
        public MyAwaiter GetAwaiter()
        {
            return new MyAwaiter();
        }
    }
}
