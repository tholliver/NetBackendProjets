using BuildingSurSysApp.Contracts;

namespace BuildingSurSysApp.Entities;

public class ExternalVisitor : IExternalVisitor

{
    public int Id { get; set; }
    public int FullName { get; set; }
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public DateTime? EntryDateTime { get; set; }
    public DateTime? ExitDateTime { get; set; }
    public bool InBuilding { get; set; }
    public int EmployeeContactId { get; set; }
}
