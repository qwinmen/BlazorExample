using BlazingTrails.Api;
using BlazingTrails.Api.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container:
{
	//Db configuration:
	builder.Services.AddDbContext<BlazingTrailsContext>(options =>
		{
			options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"));
		});
	builder.Services.AddControllers();

	builder.Services.AddFluentValidationAutoValidation();
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
//Конфига, которая позволит отдавать статические файлы из указанного каталога:
app.UseStaticFiles(new StaticFileOptions
	{
		FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), BlazingTrailsApiConsts.ImageStaticDirectory)),
		RequestPath = new PathString(BlazingTrailsApiConsts.ImageStaticDirectory.EnsureStartsWith('/')),
	});

//app.UseAuthorization();
app.UseRouting();
app.MapControllers();

//Если запрос не соответствует контролеру, отдавать файл из Blazor.Client:
app.MapFallbackToFile("index.html");

app.Run();
