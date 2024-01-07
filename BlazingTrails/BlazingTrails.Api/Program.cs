using BlazingTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container:
{
	//Db configuration:
	builder.Services.AddDbContext<BlazingTrailsContext>(options =>
		{
			options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"));
		});
	builder.Services.AddControllers();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//Позволяет отлаживать код проекта Blazor.Client WebAssembly:
	app.UseWebAssemblyDebugging();
}
app.UseHttpsRedirection();
//Позволяет API проекту отдавать файлы приложения Blazor.Client:
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//app.UseAuthorization();
app.UseRouting();
app.MapControllers();

//Если запрос не соответствует контролеру, отдавать файл из Blazor.Client:
app.MapFallbackToFile("index.html");

app.Run();
