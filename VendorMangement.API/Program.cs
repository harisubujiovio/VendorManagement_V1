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

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

    //builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    builder.Services.AddAuth(builder.Configuration);
    builder.Services.AddScoped<IVendorDbOperator,VendorDBOperator>();
    builder.Services.AddScoped<IQueryExecutor, QueryExecutor>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<IPartnerTypeService, PartnerTypeService>();
    builder.Services.AddScoped<IPartnerService, PartnerService>();
    builder.Services.AddScoped<ICommissionMethodService, CommissionMethodService>();
    builder.Services.AddScoped<IContractTypeService, ContractTypeService>();
    builder.Services.AddScoped<IContractStatusService, ContractStatusService>();

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



