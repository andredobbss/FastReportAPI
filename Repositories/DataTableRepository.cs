using FastReportAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FastReportAPI.Repositories;

public class DataTableRepository : IDataTableRepository
{
    private readonly IConfiguration _configuration;

    public DataTableRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DataTable FillDataTable(string sql)
    {
        string connection = _configuration.GetConnectionString("DefaultConnection")!;

        using SqlConnection con = new(connection);

        using SqlDataAdapter da = new();

        da.SelectCommand = new SqlCommand(sql, con);

        DataTable dt = new();

        da.Fill(dt);

        return dt;
    }
}
