
using LinQMax.Data;

namespace LinQMax.Contract;
public interface IDatabaseService
{
    LinQMaxContext GetContext();
    Task<bool> TestConnection();
}