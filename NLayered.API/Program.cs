using Autofac.Extensions.DependencyInjection;
using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayered.API.Filters;
using NLayered.API.Middlewares;
using NLayered.Core.Repositories;
using NLayered.Core.Services;
using NLayered.Core.UnitOfWorks;
using NLayered.Repository;
using NLayered.Repository.Repositories;
using NLayered.Repository.UnitOfWorks;
using NLayered.Service.Mapping;
using NLayered.Service.Services;
using NLayered.Service.Validations;
using System.Reflection;
using NLayered.API.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>  //API'in dönmüş olduğu Model Filtresini baskılıyoruz, Fluent Validation'un dönüşünü istemiyoruz, bizim modelimiz custom olan dönsün.
{
    options.SuppressModelStateInvalidFilter = true;

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();//in-memory caching ekliyoruz


builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        //options.MigrationsAssembly("NLayered.Repository"); //bu static bir yaklaşım oldu, ileride assembley name ini değiştirisek, burayı da güncellemek gerekecek. Bunun yerine aşağıdaki giib git AppDbContext'in bulunduğu Assmbly'nin ismini al demeliyiz
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });

});

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();


//aşağıdakileri autofac ile Modules klasörü içine taşıdık
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //generic olduğu için typeof kullan. Eğer birden fazle T alsaydı iki T için <,>, üç T için <,,> kullanacaktık. 
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();

//.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
