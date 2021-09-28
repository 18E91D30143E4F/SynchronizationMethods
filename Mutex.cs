using System.Threading;

namespace SynchronizationMethods
{
    public class Mutex
    {
        private int _curId = -1;

        public int Lock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            while (Interlocked.CompareExchange(ref this._curId,
                id, -1) != -1)
            {
            }

            return id;
        }

        public void Unlock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref this._curId,
                -1, id);
        }
    }
}