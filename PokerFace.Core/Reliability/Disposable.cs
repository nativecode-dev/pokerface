namespace PokerFace.Core.Reliability
{
    using System;

    public abstract class Disposable : IDisposable
    {
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Disposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (this.Disposed || disposing == false)
            {
                return;
            }

            this.Disposed = true;
            this.ReleaseManaged();
        }

        protected abstract void ReleaseManaged();
    }
}