using Microsoft.EntityFrameworkCore;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Data;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IPartnerTypeService, PartnerTypeService>();
    builder.Services.AddScoped<IPartnerService, PartnerService>();

    builder.Services.AddDbContext<VendorManagementDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("VendorMgmtConnectionString")));
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}



