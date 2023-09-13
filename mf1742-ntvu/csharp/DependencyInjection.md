-   Là một kỹ thuật để đạt được Đảo ngược điều khiển (IoC) giữa các lớp và các phần phụ thuộc của chúng
-   Giải quyết vấn đề quá nhiều phụ thuộc -> nhiều đối tượng rác trong một dự án lớn

-   DI giải quyết các vấn đề này thông qua:

*   Sử dụng 1 interface or base class để trừa tượng hóa việc triển khai phần phụ thuộc.
*   Đăng kí phần phụ thuộc trong vùng services, IServiceProvider. Các dịch vụ thường đc đăng kí khi khởi động ứng dụng và được thêm vào IServiceCollection. Sau tất cả, BuildServiceProvider tạo vùng chứa dịch vụ.
*   Đưa dịch vụ vào constructor của lớp khi nó được sử dụng. Framework .NET đảm nhận trách nhiệm tạo 1 phiên bản phụ thuộc và bỏ đi nếu ko cần thiết nữa

-   Sử dụng DI có thể được cải thiện với ILogger

-   Khi có nhiều hơn 1 constructor, constructor có nhiều params có thể phân giải DI hơn sẽ được chọn
    +, VD: public class ExampleService
    {
    public ExampleService()
    {
    }

        public ExampleService(ILogger<ExampleService> logger) -> Hàm được chọn, do không thể phân giải DI cho FooService fooService và BarService barService ở constructor dưới
        {
            // omitted for brevity
        }

        public ExampleService(FooService fooService, BarService barService)
        {
            // omitted for brevity
        }

    }

-   Nếu cả 2 constructor đều có params có thể phân giải DI như nhau -> exception được tung ra

-   Services lifetimes:
    +, Transient: Với mỗi yêu cầu -> tạo ra một service mới, disposed at the end of the request - use: AddTransient
    +, Scoped: Chỉ tạo một service với mỗi yêu cầu, disposed at the end of the request - use: AddScoped
    +, Singleton: Chỉ tạo ra một service khi khởi chạy, dispose automatically - use: AddSingleton

*   TryAdd{LIFETIME}: thêm một dịch vụ nếu kiểu dịch vụ này chưa được thực thi

---- Constructor injection behavior

-   IServiceProvider
-   ActivatorUtilities

*   Khi dịch vụ được cung cấp bởi IServiceProvider or ActivatorUtilities, constructor cần public
*   Khi dịch vụ được cung cấp bởi ActivatorUtilities, chỉ có 1 constructor được tồn tại, có thể overload nhưng chỉ 1 constructor được tiêm phụ thuộc.

---- Scope validation

-   Khi app được khởi chạy tại dev E và gọi CreateApplicationBuilder to build host, dịch vụ mặc định sẽ check để xác minh:
    +, Scoped services ko được tiêm từ root service
    +, Scoped services ko bị tiêm vào singletons.

---- Scope scenarios

-   IServiceScopeFactory luôn được đăng ký dưới dạng singleton, nhưng IServiceProvider có thể thay đổi tùy theo thời gian tồn tại của lớp chứa
