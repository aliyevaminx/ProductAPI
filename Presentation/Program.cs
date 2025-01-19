using Data.Contexts;
using Data.Repositories.Product.Read;
using Data.Repositories.Product.Write;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;
using Business.Extensions;
using Business.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
	x.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "MyProductAPI"
	});

	x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));
});


builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("Data")));

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddAutoMapper(x =>
{
	x.AddProfile<ProductMappingProfile>();
});

#region Data

builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<CustomExceptionMiddleware>();

app.Run();
