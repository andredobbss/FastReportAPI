using FastReportAPI.Models;

namespace FastReportAPI.Repositories.Interfaces;

public interface IEnumerableRepository
{
    Task<IEnumerable<Products>> GetEnumerable(string sql);
}
