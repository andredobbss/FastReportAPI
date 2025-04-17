using System.Data;

namespace FastReportAPI.Repositories.Interfaces;

public interface IDataTableRepository
{
    DataTable FillDataTable(string sql);
}
