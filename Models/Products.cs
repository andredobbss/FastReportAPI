using Microsoft.EntityFrameworkCore;

namespace FastReportAPI.Models;

[Keyless]
public class Products
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string QuantityPerUnit { get; set; } = null!;
    public decimal UnitPrice { get; set; }

}
