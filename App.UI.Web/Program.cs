using App.Infrastructure;
using App.Infrastructure.Persistance;
using App.UI.Web.Authentification.OptionsSetup;
using App.UI.Web.Authentification;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Repositories;
using App.ApplicationCore.Services;
using App.ApplicationCore.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure();

    });

    options.AddInterceptors(new TimeStampInterceptor());

    // options.UseModelBuilder(modelBuilder =>
    ///{
    //    modelBuilder.ConfigureEnumConversions();
    // });
});


   builder.Services.AddHttpClient<NexusService>((serviceProvider, client) =>
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var nexusUrl = configuration["Nexus:Url"];
        var nexusApiKey = configuration["Nexus:ApiKey"];

        client.BaseAddress = new Uri(nexusUrl);
    });






builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAdresseRepository, AddresseRepository>();




builder.Services.AddScoped<ISanitizerService, SanitizerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAdresseService, AdresseService>();
builder.Services.AddScoped<IStripeService,StripeService>();
builder.Services.AddScoped<IFactureService, FactureService>();
builder.Services.AddScoped<StripeService>();
builder.Services.AddScoped<FactureService>();



builder.Services.AddScoped<PasswordService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtManager, JwtManager>();


builder.Services.AddHttpContextAccessor();

// Configure JwtOptions
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

// Configure JwtBearereOptions
JwtConfiguration.ConfigureJwt(builder.Services, builder.Configuration);


//configuration swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Application E-commerce", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authentication",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        
    });
});


//automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application E-commerce"); });

app.UseCors();

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();