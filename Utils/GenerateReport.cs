using FastReport.Export.PdfSimple;
using FastReport.Web;
using System.Data;

namespace FastReportAPI.Utils;

public static class GenerateReport
{
    public static MemoryStream GetMemoryReportStream<T>(string reportName, string dataSetName, IWebHostEnvironment webHostEnvironment, T data)
    {
        WebReport webReport = new();

        webReport.Report.Load(Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/reports", reportName));

        if (data is DataTable dt)
            webReport.Report.RegisterData(dt, dataSetName);

        if (data is IEnumerable<object> ie)
            webReport.Report.RegisterData(ie, dataSetName);

        webReport.Report.Prepare();

        using MemoryStream ms = new();

        webReport.Report.Export(new PDFSimpleExport(), ms);

        ms.Position = 0;

        return ms;
    }

}
