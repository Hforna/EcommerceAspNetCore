using EcommerceAspNet.Api.BackgroundServices;
using EcommerceAspNet.Api.Filters;
using EcommerceAspNet.Application;
using EcommerceAspNet.Domain.Entitie.Identity;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Filters;
using EcommerceAspNet.Infrastructure;
using EcommerceAspNet.Infrastructure.DataEntity;
using EcommerceAspNet.Infrastructure.Migration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterException)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<FilterBindId>();

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("sqlserverconnection");

builder.Services.AddDbContext<ProjectDbContext>(d => d.UseSqlServer(connectionString));


builder.Services.AddIdentity<UserEntitie, RoleEntitie>()
    .AddEntityFrameworkStores<ProjectDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddInstrastructure(builder.Configuration);

builder.Services.AddScoped<IGetUserLoggedToken, GetUserLoggedToken>();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHostedService<DeleteProductService>();

AddAuthentication();

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("settings:token:signKey"))!),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParams;
});

builder.Services.AddAuthorization(d =>
{
    d.AddPolicy("AdminOnly", p => p.RequireRole("admin"));
    d.AddPolicy("CustomerOnly", p => p.RequireRole("customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("settings:stripe:secretKey");

app.UseAuthorization();

app.MapControllers();

var dd = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

DatabaseMigration.Migrate(builder.Configuration.GetConnectionString("sqlserverconnection")!, dd.ServiceProvider);

void AddAuthentication()
{
    var clientId = builder.Configuration.GetValue<string>("settings:google:clientId")!;
    var clientSecret = builder.Configuration.GetValue<string>("settings:google:clientSecret")!;

    builder.Services.AddAuthentication(d =>
    {
        d.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie()
    .AddGoogle(d =>
    {
        d.ClientId = clientId;
        d.ClientSecret = clientSecret;
    });
}

app.Run();

public partial class Program
{

}