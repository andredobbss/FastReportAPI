using FastReport.Export.PdfSimple;
using FastReport.Web;
using FastReportAPI.Repositories.Interfaces;
using FastReportAPI.Services.Interfaces;

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

       
        WebReport webReport = new WebReport();

        webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath,"wwwroot/reports", "ProductsReport.frx"));

        var dt = _dataTableRepository.FillDataTable(sqlDataTable);

        webReport.Report.RegisterData(dt, "Products");

        webReport.Report.Prepare();

        using MemoryStream ms = new MemoryStream();

        webReport.Report.Export(new PDFSimpleExport(), ms);

        ms.Position = 0;

        return ms;
    }

    public async Task<MemoryStream> GetProductsMemoryStreamIEnumerable()
    {
        string sqlIEnumerable = "SELECT * FROM ProductByCategoryName"; //view criada

        WebReport webReport = new WebReport();

        webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "ProductsReport.frx"));

        var ie = await _ienumerableRepository.GetEnumerable(sqlIEnumerable);

        webReport.Report.RegisterData(ie, "Products");

        webReport.Report.Prepare();

        using MemoryStream ms = new MemoryStream();

        webReport.Report.Export(new PDFSimpleExport(), ms);

        ms.Position = 0;

        return ms;
    }
}
