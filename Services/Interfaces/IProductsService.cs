using System.Data;

namespace FastReportAPI.Services.Interfaces;

public interface IProductsService
{
    MemoryStream GetProductsMemoryStreamDataTable();

    Task<MemoryStream> GetProductsMemoryStreamIEnumerable();

}
