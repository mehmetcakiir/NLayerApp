using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.UnitOfWorks;
using NLayer.Repository.Repositories;
using System.Reflection;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.API.Filters;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Middlewares;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayer.API.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Filter lar� kullan
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x =>
x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


//Fremwork �n kendi response unu bask�lar (Kendi filter �m�z�n aktif hale gelebilmesi i�in kullan�ld�)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));


//Generic de�il
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();



builder.Services.AddDbContext<AppDbContext>(x =>
{
    // AppDbContext s�n�f�n�n oldu�u katman al�n�r
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly("NLayer.Repository");
        //Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


//RepoServiceModule � implement et
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomExeption();


//Yaz�lan Middleware i g�rmesi i�in
app.UseAuthorization();

app.MapControllers();

app.Run();
