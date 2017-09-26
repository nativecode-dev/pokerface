namespace PokerFace.Core.Reliability
{
    using System;
    using System.Collections.Generic;

    public class DisposableManager : Disposable
    {
        private readonly ICollection<IDisposable> disposables = new List<IDisposable>();

        public void EnsureDisposed(IDisposable disposable)
        {
            this.disposables.Add(disposable);
        }

        protected override void ReleaseManaged()
        {
            foreach (var disposable in this.disposables)
            {
                disposable.Dispose();
            }

            this.disposables.Clear();
        }
    }
}