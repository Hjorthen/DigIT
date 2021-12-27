using System;
using System.Collections.Generic;

public class ConsoleModel  : IObservable<ConsoleModel>
{
    public IReadOnlyList<string> LogEntries => logEntries;
    private List<string> logEntries = new List<string>();
    private List<IObserver<ConsoleModel>> observers = new List<IObserver<ConsoleModel>>();
    public void AddEntry(string logEntry) {
        logEntries.Add(logEntry);
        OnUpdated();
    }

    private void OnUpdated() {
        foreach(var observer in observers) {
            observer.OnNext(this);
        }
    }

    public IDisposable Subscribe(IObserver<ConsoleModel> observer)
    {
        observers.Add(observer);
        return new Unsubscriber<ConsoleModel>(observers, observer);
    }
}
