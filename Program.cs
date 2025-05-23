using FastReport.Data;
using FastReportAPI.Context;
using FastReportAPI.Repositories;
using FastReportAPI.Repositories.Interfaces;
using FastReportAPI.Services;
using FastReportAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddFastReport();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FastReportAPI", Version = "v1" });
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder .Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

builder.Services.AddScoped<IDataTableRepository, DataTableRepository>();

builder.Services.AddScoped<IEnumerableRepository, EnumerableRepository>();

builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFastReport();

app.UseAuthorization();

app.MapControllers();

app.Run();
