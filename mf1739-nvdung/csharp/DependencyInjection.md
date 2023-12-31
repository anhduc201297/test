# Tìm hiểu về Dependency Injection

1, Mở đầu:
    Những dự án với độ phức tạp cao ngoài việc thiết kế tính năng cho ứng dụng, tổ chức code luôn luôn là vấn đề được đặt lên hàng đầu. Tổ chức tốt giúp lập trình viên dễ dàng bảo trì, cũng như mở rộng code về sau.

    Để có thể tiết kiệm chi phí và thời gian cho công đoạn này nhưng vẫn đem lại hiệu quả cao, việc nắm vững về các design pattern sẽ giúp ích rất nhiều. Dependency Injection là một dạng design pattern được thiết kế với mục đích ngăn chặn sự phụ thuộc giữa các class, để khiến cho code dễ hiểu hơn, trực quan hơn, nhằm phục vụ cho mục đích bảo trì và nâng cấp code.

2, Khái niệm:
    - Để hiểu được Dependency Injection, cần phải hiểu Dependency Inversion và Inversion of Control Pattern trước.
    - Dependency Inversion:
        + Dependency Inversion: là nguyên lý trong nguyên lý SOLID, trong đó:
            ~ Các module cấp cao không nên phụ thuộc vào các modules cấp thấp. Cả 2 nên phụ thuộc vào abstraction.
            ~ Interface (abstraction) không nên phụ thuộc vào chi tiết, mà ngược lại. ( Các class giao tiếp với nhau thông qua interface, không phải thông qua implementation.)
        + Với cách code thông thường, các module cấp cao sẽ gọi các module cấp thấp. Module cấp cao sẽ phụ thuộc vào module cấp thấp, điều đó tạo ra các dependency. Khi module cấp thấp thay đổi, module cấp cao phải thay đổi theo. Một thay đổi sẽ kéo theo hàng loạt thay đổi, giảm khả năng bảo trì của code.
        + Nếu tuân theo Dependency Inversion principle, các module cùng phụ thuộc vào 1 interface không đổi. Ta có thể dễ dàng thay thế, sửa đổi module cấp thấp mà không ảnh hưởng gì tới module cấp cao.
    - Inversion of Control:
        Đây là một design pattern được tạo ra để code có thể tuân thủ nguyên lý Dependency Inversion. Có nhiều cách hiện thực pattern này: ServiceLocator, Event, Delegate, … Dependency Injection là một trong các cách đó.
    - Dependency Injection (DI):
        Là một design pattern được ASP.Net hỗ trợ. Đây là một kỹ thuật để hiện thực hóa Inversion of Control Pattern (có thể coi nó là một design pattern riêng cũng được). Các module phụ thuộc (dependency) sẽ được inject vào module cấp cao. Có thể hiểu 1 cách đơn giản như sau:
        + Các module không giao tiếp trực tiếp với nhau, mà thông qua interface. Module cấp thấp sẽ implement interface, module cấp cao sẽ gọi module cấp thấp thông qua interface.
        + Để giao tiếp với database, ta có interface IDatabase, các module cấp thấp là XMLDatabase, SQLDatabase. Module cấp cao là CustomerBusiness sẽ chỉ sử dụng interface IDatabase.
        + Việc khởi tạo các module cấp thấp sẽ do DI Container thực hiện. Ví dụ: Trong module CustomerBusiness, ta sẽ không khởi tạo IDatabase db = new XMLDatabase(), việc này sẽ do DI Container thực hiện. Module CustomerBusiness sẽ không biết gì về module XMLDatabase hay SQLDatabase.
        + Việc Module nào gắn với interface nào sẽ được config trong code hoặc trong file XML.
        + DI được dùng để làm giảm sự phụ thuộc giữa các module, dễ dàng hơn trong việc thay đổi module, bảo trì code và testing.

3, Các loại Dependency Injection:
    - Constructor injection: Các dependency (biến phụ thuộc) được cung cấp thông qua constructor (hàm tạo lớp).
    - Setter injection: Các dependency sẽ được truyền vào 1 class thông qua các setter method (hàm setter).
    - Interface injection: Dependency sẽ cung cấp một Interface, trong đó có chứa hàm có tên là Inject. Các client phải triển khai một Interface mà có một setter method dành cho việc nhận dependency và truyền nó vào class thông qua việc gọi hàm Inject của Interface đó.
    * Lưu ý: Trong ba kiểu Inject thì Inject qua phương thức khởi tạo rất phổ biến vì tính linh hoạt, mềm dẻo, dễ xây dựng thư viện DI...

4, Ưu điểm, khuyết điểm của Dependency Injection:
    - Ưu điểm:
        + Giảm sự kết dính giữa các module.
        + Code dễ bảo trì, dễ thay thế module.
        + Rất dễ test và viết Unit Test.
        + Dễ dàng thấy quan hệ giữa các module (Vì các dependency đều được inject vào constructor).
    - Khuyết điểm:
        + Khái niệm DI khá “khó tiêu”, các developer mới sẽ gặp khó khăn khi học.
        + Sử dụng interface nên đôi khi sẽ khó debug, do không biết chính xác module nào được gọi.
        + Các object được khởi tạo toàn bộ ngay từ đầu, có thể làm giảm performance.
        + Làm tăng độ phức tạp của code.

5, Sử dụng DI trong .NET CORE:
    - Sử dụng Dependency Injection thông qua các bước:
        + Sử dụng một interface hoặc base class để trừu tượng hóa việc triển khai phụ thuộc.
        + Đăng ký phần phụ thuộc trong service container. ASP.NET Core cho phép chúng ta đăng ký các dịch vụ ứng dụng của mình với IoC container, trong phương thức ConfigureServices của lớp Startup. Phương thức ConfigureServices bao gồm một tham số kiểu IServiceCollection . được sử dụng để đăng ký các dịch vụ ứng dụng.
        + Đưa service vào phương thức khởi tạo của lớp mà nó được sử dụng. Framework sẽ tạo một thể hiện của sự phụ thuộc và loại bỏ nó khi nó không còn cần thiết nữa.
    Ví dụ:

    // IMyDependency interface xác định phương thức Write
    public interface IMyDependency
    {
        void WriteMessage(string message);
    }

    //Interface này được MyDependency triển khai
    public class MyDependency : IMyDependency
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"MyDependency.WriteMessage Message: {message}");
        }
    }

    //Phương thức AddScoped đăng ký service với scoped lifetime, lifetime của một singleton request
    using DependencyInjectionSample.Interfaces;
    using DependencyInjectionSample.Services;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorPages();

    builder.Services.AddScoped<IMyDependency, MyDependency>();

    var app = builder.Build();

    // IMyDependency service được request và sử dụng để gọi phương thức Write
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

    Bằng cách sử dụng DI pattern, service sẽ: Không sử dụng MyDependency, chỉ sử dụng IMyDependency interface thực hiện nó. Điều đó giúp bạn dễ dàng thay đổi việc thực thi của Controller mà không cần sửa đổi Controller. Không tạo một instance của MessageWriter, nó được tạo bởi DI container.

    // Việc triển khai IMyDependency interface có thể được cải thiện bằng cách sử dụng built-in logging API
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

    // Sử dụng ConfigureServices để đăng ký implement IMyDependency
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IMyDependency, MyDependency2>();

        services.AddRazorPages();
    }

    // LoggingMessageWriter phụ thuộc vào ILogger TCategoryName - yêu cầu trong phương thức khởi tạo.

    Trong Dependency Injection, một service:
        - Là một object cung cấp một service cho các đối tượng khác, chẳng hạn như IMyDependency service.
        - Không liên quan đến web service, mặc dù service đó có thể sử dụng một web service
    
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

6, Các loại Service Lifetime khi đăng ký Dependency Injection:
    - Bất cứ khi nào chúng ta yêu cầu Service, DI Container sẽ quyết định xem có tạo mới một thể hiện (instance) hay sử dụng lại thể hiện đã tạo từ trước đó. Vòng đời của Service phụ thuộc vào khi khởi tạo thể hiện và nó tồn tại bao lâu.
    - Chúng ta định nghĩa vòng đời khi đăng ký Service. Có 3 mức độ vòng đời: addTransient, addScoped, addSingleton.
        + addTransient: Instance được khởi tạo mỗi lần tạo service
        + addScoped: Instance được khởi tạo mỗi scope. ( Scope ở đây chính là mỗi request gửi đến ứng dụng). Trong cùng một scope thì service sẽ được tái sử dụng.
        + addSingleton: Instance của service được tạo duy nhất lúc khởi chạy ứng dụng và được dùng ở mọi nơi.