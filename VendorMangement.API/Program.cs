using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;
using VendorMangement.API.Services;
using VendorMangement.API.Services.Authentication;
using VendorMnagement.DBclient.Data;
using IAuthenticationService = VendorMangement.API.Services.Authentication.IAuthenticationService;
using AuthenticationService = VendorMangement.API.Services.Authentication.AuthenticationService;
using System.Runtime.CompilerServices;
using VendorManagement.DBclient.DBProvider;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "JWTToken_Auth_API",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });
    builder.Services.AddAuth(builder.Configuration);
    builder.Services.AddScoped<IVendorDbOperator,VendorDBOperator>();
    builder.Services.AddScoped<IQueryExecutor, QueryExecutor>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<IPartnerTypeService, PartnerTypeService>();
    builder.Services.AddScoped<IPartnerService, PartnerService>();
    builder.Services.AddScoped<ICommissionMethodService, CommissionMethodService>();
    builder.Services.AddScoped<IContractTypeService, ContractTypeService>();
    builder.Services.AddScoped<IContractStatusService, ContractStatusService>();
    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddScoped<ISalesSevice, SalesService>();
    builder.Services.AddScoped<IContractService, ContractService>();
    builder.Services.AddScoped<IStatementService, StatementService>();
    builder.Services.AddDbContext<VendorManagementDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("VendorMgmtConnectionString")));
}


var app = builder.Build();
{
    //Configure the HTTP request pipeline.
    app.UseExceptionHandler("/error");
   
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}



