namespace BuildingSurSysApp.Entities;

public class SecuritySurvillanceHub : IObservable<ExternalVisitor>
{
    private List<ExternalVisitor> _extenalVisitors;
    private List<IObserver<ExternalVisitor>> _observers;


    public IDisposable Subscribe(IObserver<ExternalVisitor> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        foreach (var externalVisitor in _extenalVisitors)
        {
            observer.OnNext(externalVisitor);
        }

        return new UnSubscriber<ExternalVisitor>(_observers, observer);
    }

    public void ConfirmExternalVisitorEntersBuilding(ExternalVisitor externalVisitor)
    {
        // ExternalVisitor externalVisitor = new ExternalVisitor
        // {
        //     Id = externalVisitor.Id,
        //     FullName = externalVisitor.FullName,
        //     CompanyName = externalVisitor.CompanyName,
        //     EntryDateTime = externalVisitor.EntryDateTime,
        //     JobTitle = externalVisitor.JobTitle,
        //     EmployeeContactId = externalVisitor.EmployeeContactId
        // };

        _extenalVisitors.Add(externalVisitor);

        foreach (var obs in _observers)
            obs.OnNext(externalVisitor);
    }

    public void ConfirmExternalVisitorExitsBuilding(int externalVisitorId, DateTime exitDateTime)
    {
        var externalVisitor = _extenalVisitors.FirstOrDefault(e => e.Id == externalVisitorId);

        if (externalVisitor != null)
        {
            externalVisitor.ExitDateTime = exitDateTime;
            externalVisitor.InBuilding = false;

            foreach (var obs in _observers)
                obs.OnNext(externalVisitor);
        }
    }

    public void BuildingEntryCutOffTimeReached()
    {
        if (_extenalVisitors.Where(e => e.InBuilding == true).ToList().Count() == 0)
        {
            foreach (var obs in _observers)
            {
                obs.OnCompleted();
            }
        }
    }

}
