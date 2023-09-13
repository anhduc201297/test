using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;
using MISA.SME.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add application
    builder.Services.AddApplication();

    // Add infrastructure
    builder.Services.AddInfrastructure();

    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add cors
    builder.Services.AddCors();

    // Config database connection
    DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("Mysql");

    // Add SuppressModelStateInvalidFilter
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
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

    // Enable cors
    app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

    app.Run();
}

