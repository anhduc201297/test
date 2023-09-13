using MISA.Web07.MF1732_DKTUAN;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

// Th�m MySqlConnectionFactory v�o DI
builder.Services.AddScoped<MySqlConnectionFactory>();
// Th�m EmployeeServices v�o DI
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
