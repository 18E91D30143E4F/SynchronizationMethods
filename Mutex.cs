using System.Threading;

namespace SynchronizationMethods
{
    public class Mutex
    {
        private int curId = -1;

        public int Lock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            while (Interlocked.CompareExchange(ref this.curId,
                id, -1) != -1)
            {
            }

            return id;
        }

        public void Unlock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref this.curId,
                -1, id);
        }
    }
}