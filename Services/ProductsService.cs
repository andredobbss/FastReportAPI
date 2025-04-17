using FastReportAPI.Repositories.Interfaces;
using FastReportAPI.Services.Interfaces;
using FastReportAPI.Utils;

namespace FastReportAPI.Services;

public class ProductsService : IProductsService
{
    private readonly IDataTableRepository _dataTableRepository;

    private readonly IEnumerableRepository _ienumerableRepository;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsService(IDataTableRepository dataTableRepository, 
                           IEnumerableRepository ienumerableRepository, 
                           IWebHostEnvironment webHostEnvironment)
    {
        _dataTableRepository = dataTableRepository;
        _ienumerableRepository = ienumerableRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public MemoryStream GetProductsMemoryStreamDataTable()
    {
        string sqlDataTable = 
                             @"
                             SELECT 
                             	P.[ProductID],
                             	P.[ProductName],
                             	C.[CategoryName],
                             	P.[QuantityPerUnit],
                             	P.[UnitPrice]
                             FROM 
                             	[dbo].[Products] P
                             INNER JOIN 
                             	[dbo].[Categories] C
                             ON
                             	P.CategoryID = C.CategoryID";
                          

        var dt = _dataTableRepository.FillDataTable(sqlDataTable);

        var ms =  GenerateReport.GetMemoryReportStream("ProductsReport.frx", "Products", _webHostEnvironment, dt);

        return ms;
    }

    public async Task<MemoryStream> GetProductsMemoryStreamIEnumerable()
    {
        string sqlIEnumerable = "SELECT * FROM ProductByCategoryName"; //view criada no banco de dados

        var ie = await _ienumerableRepository.GetEnumerable(sqlIEnumerable);

        var ms = GenerateReport.GetMemoryReportStream("ProductsReport.frx", "Products", _webHostEnvironment, ie);

        return ms;
    }
}
