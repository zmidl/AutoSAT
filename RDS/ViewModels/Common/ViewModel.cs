using RDS.Models.RuntimeData.Base;
using System;
using System.Windows;

namespace RDS.ViewModels.Common
{
    public abstract class ViewModel : Notify
    {
        public event EventHandler<EventArgs> ViewChanged;
        protected virtual void OnViewChanged(EventArgs args)
        {
            this.ViewChanged?.Invoke(this, args);
        }

        public void AddHandler(EventHandler<EventArgs> handler)
        {
            WeakEventManager<ViewModel, EventArgs>.AddHandler(this, nameof(this.ViewChanged),handler);
        }
    }
}