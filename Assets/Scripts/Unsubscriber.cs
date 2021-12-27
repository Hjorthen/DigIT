using System;
using System.Collections.Generic;

public class Unsubscriber<T> : IDisposable
{
    private ICollection<IObserver<T>> observers;
    private IObserver<T> @this;

    public Unsubscriber(ICollection<IObserver<T>> observers, IObserver<T> @this) {
        this.observers = observers;
        this.@this = @this;
    }

    public void Dispose()
    {
        if(observers != null && @this != null && observers.Contains(@this)) {
            observers.Remove(@this);
        }
        @this = null;
        observers = null;
    }
}
