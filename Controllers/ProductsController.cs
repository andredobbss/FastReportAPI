using FastReportAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FastReportAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsServices;

    public ProductsController(IProductsService productsServices)
    {
        _productsServices = productsServices;
    }

    [HttpGet("GetProductsReport")]
    public async Task<ActionResult<FileStream>> GetProductsReport()
    {
        try
        {
            string fileName = $"{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}_ProductsReport.pdf";

            //var ms = _productsServices.GetProductsMemoryStreamDataTable(); // executa com DataTable

            var ms = await _productsServices.GetProductsMemoryStreamIEnumerable(); // executa com EntityFrameWork

            await ms.FlushAsync();

            byte[] dataReport = ms.ToArray();

            return File(dataReport, "application/pdf", fileName);
        }
        catch
        {
            return BadRequest();
        }
       
    }
}
