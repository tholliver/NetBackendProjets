
using LinQMax.Data;

namespace LinQMax.Contract;
public interface IDatabaseService : IDisposable
{
    LinQMaxContext GetContext();
    Task<bool> TestConnection();
}