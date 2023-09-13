using aspnetcore;
using Microsoft.AspNetCore.Connections;

#region asp.net generate
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/// <summary>
/// Thêm Service vào bên trong Container
/// Bắt đầu từ đây tôi có thể thêm vào các dịch vụ và phục thuộc vào
/// Container DI của ứng dụng bằng cách sử dụng: "builder.Services".
/// Giúp tôi có thể sử dụng service này ở toàn bộ ứng dụng
/// </summary>

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); // controller HTTP request
builder.Services.AddEndpointsApiExplorer(); // tạo tài liệu API tự động và kiểm tra API
builder.Services.AddSwaggerGen(); // Dòng này đăng ký dịch vụ SwaggerGen để tạo tài liệu Swagger/OpenAPI dựa trên các điều khiển và tài nguyên API.
#endregion

#region customize
// đăng ký dịch vụ IBDConnectionService với implementation là DBConnectionService
// trên toàn bộ global apps
string connectionString = "Server=127.0.0.1;port=3307;Database=misa.employee;Uid=root;pwd=";


// một kết nối cơ sở dữ liệu sẽ được tạo khi ứng dụng khởi động và sẽ được sử dụng cho tất
// cả các yêu cầu HTTP. Điều này có thể gây ra các vấn đề về đồng thời hóa và không an toàn
// khi sử dụng kết nối cơ sở dữ liệu trong môi trường đa luồng, đặc biệt trong ứng dụng web.
//builder.Services.AddSingleton<IDBConnectionService>(new DBConnectionService(connectionString));


// tạo một phiên làm việc (scope) mới cho mỗi yêu cầu HTTP và sẽ tạo một kết nối cơ sở dữ
// liệu mới cho mỗi scope, đảm bảo tính riêng biệt và phạm vi của kết nối cơ sở dữ liệu
// trong mỗi yêu cầu HTTP.
// AddScoped/AddTransient cần một factody method để tạo đối tượng mới cho mỗi phạm vi
builder.Services.AddTransient<IDBConnectionService>(provider => new DBConnectionService(connectionString));

#endregion

#region asp.net generate
var app = builder.Build();
// Configure the HTTP request pipeline.
/// <summary>
/// Cấu hình pipeline của HTTP Request, xác định cách mà các yêu cầu
/// HTTP sẽ được xử lý khi chúng đến APP/WEB
/// </summary>
if (app.Environment.IsDevelopment()) // đabg chạy trong môi trường phát triển
{
    app.UseSwagger(); // sử dụng Swagger để tạo API
    app.UseSwaggerUI();
}

// Middleware để tự động chuyển hướng yêu cầu HTTP sang HTTPS nếu truy cập từ giao thức không an toàn (HTTP).
app.UseHttpsRedirection(); 
// Middleware cho xác thực và quyền hạn. Middleware này đảm bảo rằng các yêu cầu HTTP được xử lý theo cách an toàn và quyền hạn đã được xác định.
app.UseAuthorization(); 

// Dòng này cấu hình middleware để định tuyến các yêu cầu HTTP đến các điều khiển (controllers) trong ứng dụng của bạn để xử lý chúng.
app.MapControllers(); 

app.Run();
#endregion