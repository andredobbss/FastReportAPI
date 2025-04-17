using FastReportAPI.Context;
using FastReportAPI.Models;
using FastReportAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastReportAPI.Repositories;

public class EnumerableRepository : IEnumerableRepository
{
    private AppDbContext _context;

    public EnumerableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Products>> GetEnumerable(string sql)
    {
        var result = await _context.ProductByCategoryName.FromSqlRaw(sql).ToListAsync();

        return result;
    }
}
