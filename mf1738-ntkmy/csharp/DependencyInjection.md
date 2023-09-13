# Dependency Injection

## Khái niệm

**Dependency Injection (DI)** là một design pattern được ASP.Net hỗ trợ. Các module phụ thuộc (dependency) sẽ được inject vào module cấp cao.

**Dependency Injection** là một kiểu của Inversion of Control (IoC). 

**Dependency Inversion** là nguyên lý cuối cùng của SOLID:
Các module cấp cao không nên phụ thuộc vào các modules cấp thấp. Cả 2 nên phụ thuộc vào abstraction.

**Inversion of Control (IoC - Đảo ngược điều khiển)** là một design pattern được tạo ra để code có thể tuân thủ nguyên lý Dependency Inversion. Inversion of Control là một nguyên lý thiết kế trong công nghệ phần mềm trong đó các thành phần nó dựa vào để làm việc bị đảo ngược quyền điều khiển khi so sánh với lập trình hướng thủ thục truyền thống.

**Interface (abstraction)** không nên phụ thuộc vào chi tiết, mà ngược lại (các class giao tiếp với nhau thông qua interface, không phải thông qua implementation).

Với các code thông thường, các module cấp cao sẽ gọi các module cấp thấp. Module cấp cao sẽ phụ thuộc vào module cấp thấp, điều đó tạo ra các dependency. Khi module cấp thấp thay đổi, module cấp cao phải thay đổi theo. Một thay đổi sẽ kéo theo hàng loạt thay đổi, giảm khả năng bảo trì của code.

## Các dạng Dependency Injection

Có 3 dạng Dependency Injection:

- Constructor Injection: Các dependency sẽ được container truyền vào (inject vào) 1 class thông qua constructor của class đó. Đây là cách thông dụng nhất.
- Setter Injection: Các dependency sẽ được truyền vào 1 class thông qua các hàm Setter.
- Interface Injection: Class cần inject sẽ implement 1 interface. Interface này chứa 1 hàm tên Inject. Container sẽ injection dependency vào 1 class thông qua việc gọi hàm Inject của interface đó. Đây là cách rườm rà và ít được sử dụng nhất.

## Ưu và nhược điểm

### Ưu điểm 

- Giúp viết Unit test dễ dàng hơn.
- Mở rộng dự án dễ dàng hơn.
- Giúp ích trong việc liên kết lỏng (loose coupling) giữa các thành phần trong dự án.

### Nhược điểm

- Sử dụng interface nên đôi khi sẽ khó debug, do không biết chính xác module nào được gọi.
- Các object được khởi tạo toàn bộ ngay từ đầu, có thể làm giảm performance.

## Dependency Injection trong ASP.NET Core

### Khái niệm container

**Container** có sẵn trong ASP.NET Core được biểu diễn bởi IServiceProvider interface nó hỗ trợ mặc định inject vào constructor.

Các kiểu mà chúng ta đăng ký với container được biết là các service.

```csharp
// Startup.cs

using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Đăng ký các dịch vụ vào DI Container
        services.AddScoped<IMyService, MyService>();
        services.AddScoped<IOtherService, OtherService>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}
```

### Sử dụng ependency injection

Sử dụng Dependency Injection thông qua các bước:

- Sử dụng một interface hoặc base class để trừu tượng hóa việc triển khai phụ thuộc.
- Đăng ký phần phụ thuộc trong service container. ASP.NET Core cho phép đăng ký các dịch vụ ứng dụng của mình với IoC container, trong phương thức ConfigureServices của lớp Startup. Phương thức ConfigureServices bao gồm một tham số kiểu IServiceCollection . được sử dụng để đăng ký các dịch vụ ứng dụng.
- Đưa service vào phương thức khởi tạo của lớp mà nó được sử dụng. Framework sẽ tạo một thể hiện của sự phụ thuộc và loại bỏ nó khi nó không còn cần thiết nữa.

**Tạo một interface IMyDependency**

```csharp
public interface IMyDependency
{
    void WriteMessage(string message);
}
```

**Class MyDependency triển khai IMyDependency:**

Class MyDependency triển khai interface IMyDependency.

```csharp
public class MyDependency : IMyDependency
{
    public void WriteMessage(string message)
    {
        Console.WriteLine($"MyDependency.WriteMessage Message: {message}");
    }
}
```

**Đăng ký service với scoped lifetime, lifetime của một singleton request:**


```csharp
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IMyDependency, MyDependency>();

var app = builder.Build();
```

**IMyDependency service được request và sử dụng để gọi phương thức Write:**


```csharp
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
```

**Logging:**

Trong ví dụ này, MyDependency cũng có thể sử dụng built-in logging API thông qua DI. Bạn có thể inject ILogger vào constructor của MyDependency2 để ghi logs.

```csharp
public class MyDependency2 : IMyDependency
{
    private readonly ILogger<MyDependency2> _logger;

    public MyDependency2(ILogger<MyDependency2> logger)
    {
        _logger = logger;
    }

    public void WriteMessage(string message)
    {
        _logger.LogInformation( $"MyDependency2.WriteMessage Message: {message}");
    }
}
```

**Sử dụng ConfigureServices để đăng ký implement IMyDependency:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IMyDependency, MyDependency2>();

    services.AddRazorPages();
}
```

**LoggingMessageWriter phụ thuộc vào ILogger TCategoryName - yêu cầu trong phương thức khởi tạo:**

Trong Dependency Injection, một service sẽ:

- Là một object cung cấp một service cho các đối tượng khác, chẳng hạn như IMyDependency service.
- Không liên quan đến web service, mặc dù service đó có thể sử dụng một web service.

```csharp
public class AboutModel : PageModel
{
    private readonly ILogger _logger;

    public AboutModel(ILogger<AboutModel> logger)
    {
        _logger = logger;
    }
    
    public string Message { get; set; }

    public void OnGet()
    {
        Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
        _logger.LogInformation(Message);
    }
}
```

### Lifetime Management: 

Các loại  Service Lifetime khi đăng ký Dependency Injection:

- Transient: Instance được khởi tạo mỗi lần tạo service
- Scoped: Instance được khởi tạo mỗi scope(scope ở đây chính là mỗi request gửi đến ứng dụng). Trong cùng một scope thì service sẽ được tái sử dụng.
- Singleton: Instance của service được tạo duy nhất lúc khởi chạy ứng dụng và được dùng ở mọi nơi.

## References

- [Tổng quan về Dependency Injection - Sử dụng Dependency Injection trong ASP.NET CORE](https://viblo.asia/p/tong-quan-ve-dependency-injection-su-dung-dependency-injection-trong-aspnet-core-YWOZrG67lQ0)

