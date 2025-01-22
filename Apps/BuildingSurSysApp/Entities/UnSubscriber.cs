namespace BuildingSurSysApp.Entities;

public class UnSubscriber<ExternalVisitor>(List<IObserver<ExternalVisitor>> observers,
                          IObserver<ExternalVisitor> observer) : IDisposable
{
    private List<IObserver<ExternalVisitor>> _observers = observers;
    private IObserver<ExternalVisitor> _observer = observer;


    public void Dispose()
    {
        if (_observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}
