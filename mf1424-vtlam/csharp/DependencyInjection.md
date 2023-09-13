# Khái niệm 
Dependency Injection là một design pattern được phát triển nhằm để giảm thiểu tối đa sự phụ thuộc giữa các Class với nhau. Điều này sẽ giúp cho việc tổ chức code trở nên “clean” hơn, dễ hiểu và trực quan hơn.

# Các loại Dependency Injection

1. Constructor Injection: các Dependency sẽ được các container truyền vào một Class thông qua Constructor của Class đó.

2. Setter Injection: các Dependency sẽ được truyền vào một Class thông qua hàm Setter.

3. Interface Injection: Khi thực hiện, Class cần inject sẽ phải truyền vào một Interface. Trong đó, Interface sẽ phải chứa 1 hàm với tên là inject. Tiếp theo, Container sẽ thực hiện truyền Dependency vào một Class bằng cách gọi tên hàm inject đó.

# Ưu, khuyết điểm của Dependency Injection
1. Ưu điểm
- Giảm sự kết dính giữa các module
- Code dễ bảo trì, dễ thay thế module
- Rất dễ test và viết Unit Test
- Dễ dàng thấy quan hệ giữa các module (Vì các dependency đều được inject vào constructor)
2. Khuyết điểm
- Khái niệm DI khá “khó tiêu”, các developer mới sẽ gặp khó khăn khi học
- Sử dụng interface nên đôi khi sẽ khó debug, do không biết chính xác module nào được gọi
- Các object được khởi tạo toàn bộ ngay từ đầu, có thể làm giảm performance
- Làm tăng độ phức tạp của code

# Sử dụng DI trong .NET CORE
Sử dụng Dependency Injection thông qua các bước:

- Sử dụng một interface hoặc base class để trừu tượng hóa việc triển khai phụ thuộc.
- Đăng ký phần phụ thuộc trong service container. ASP.NET Core cho phép chúng ta đăng ký các dịch vụ ứng dụng của mình với IoC container, trong phương thức ConfigureServices của lớp Startup. Phương thức ConfigureServices bao gồm một tham số kiểu IServiceCollection . được sử dụng để đăng ký các dịch vụ ứng dụng.
- Đưa service vào phương thức khởi tạo của lớp mà nó được sử dụng. Framework sẽ tạo một thể hiện của sự phụ thuộc và loại bỏ nó khi nó không còn cần thiết nữa.
Ví dụ:

IMyDependency interface xác định phương thức Write
public interface IMyDependency
{
    void WriteMessage(string message);
}

Interface này được MyDependency triển khai
public class MyDependency : IMyDependency
{
    public void WriteMessage(string message)
    {
        Console.WriteLine($"MyDependency.WriteMessage Message: {message}");
    }
}

Phương thức AddScoped đăng ký service với scoped lifetime, lifetime của một singleton request
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IMyDependency, MyDependency>();

var app = builder.Build();

IMyDependency service được request và sử dụng để gọi phương thức Write
public class Index2Model : PageModel
{
    private readonly IMyDependency _myDependency;

    public Index2Model(IMyDependency myDependency)
    {
        _myDependency = myDependency;            
    }

    public void OnGet()
    {
        _myDependency.WriteMessage("Index2Model.OnGet");
    }
}