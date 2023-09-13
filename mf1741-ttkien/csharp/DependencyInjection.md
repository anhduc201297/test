* Dependency Injection (DI)
1. Khái niệm
- Là một design pattern được ASP.Net hỗ trợ. Đây là một kỹ thuật để hiện thực hóa Inversion of Control Pattern. Các module phụ thuộc (dependency) sẽ được inject vào module cấp cao. Có thể hiểu 1 cách đơn giản như sau:
  + Các module không giao tiếp trực tiếp với nhau, mà thông qua interface. Module cấp thấp sẽ implement interface, module cấp cao sẽ gọi module cấp thấp thông qua interface.
  Ví dụ: Để giao tiếp với database, ta có interface IDatabase, các module cấp thấp là XMLDatabase, SQLDatabase. Module cấp cao là CustomerBusiness sẽ chỉ sử dụng interface IDatabase.
  + Việc khởi tạo các module cấp thấp sẽ do DI Container thực hiện. 
  Ví dụ: Trong module CustomerBusiness, ta sẽ không khởi tạo IDatabase db = new XMLDatabase(), việc này sẽ do DI Container thực hiện. Module CustomerBusiness sẽ không biết gì về module XMLDatabase hay SQLDatabase.
  + Việc Module nào gắn với interface nào sẽ được config trong code hoặc trong file XML.
  + DI được dùng để làm giảm sự phụ thuộc giữa các module, dễ dàng hơn trong việc thay đổi module, bảo trì code và testing.
=> Đó là lý do tại sao cần sử dụng Dependency Injection

2 Các loại Dependency Injection
- Constructor injection: Các dependency (biến phụ thuộc) được cung cấp thông qua constructor (hàm tạo lớp).
- Setter injection: Các dependency sẽ được truyền vào 1 class thông qua các setter method (hàm setter).
- Interface injection: Dependency sẽ cung cấp một Interface, trong đó có chứa hàm có tên là Inject. Các client phải triển khai một Interface mà có một setter method dành cho việc nhận dependency và truyền nó vào class thông qua việc gọi hàm Inject của Interface đó.

  Trong ba kiểu Inject thì Inject qua phương thức khởi tạo rất phổ biến vì tính linh hoạt, mềm dẻo, dễ xây dựng thư viện DI...

3. Ưu, khuyết điểm của Dependency Injection
- Ưu điểm:
  + Giảm sự kết dính giữa các module.
  + Code dễ bảo trì, dễ thay thế module.
  + Rất dễ test và viết Unit Test.
  + Dễ dàng thấy quan hệ giữa các module (Vì các dependency đều được inject vào constructor).
- Khuyết điểm:
  + Khái niệm DI khá khó hiểu đặc biệt khi mới tìm hiểu sẽ chưa thể nắm bắt được.
  + Sử dụng interface nên đôi khi sẽ khó debug, do không biết chính xác module nào được gọi.
  + Các object được khởi tạo toàn bộ ngay từ đầu, có thể làm giảm performance.
  + Làm tăng độ phức tạp của code.

4. Sử dụng DI trong .NET CORE
- Sử dụng Dependency Injection thông qua các bước:

  + Sử dụng một interface hoặc base class để trừu tượng hóa việc triển khai phụ thuộc.
  + Đăng ký phần phụ thuộc trong service container. ASP.NET Core cho phép chúng ta đăng ký các dịch vụ ứng dụng của mình với IoC container, trong phương thức ConfigureServices của lớp Startup. Phương thức ConfigureServices bao gồm một tham số kiểu IServiceCollection . được sử dụng để đăng ký các dịch vụ ứng dụng.
  + Đưa service vào phương thức khởi tạo của lớp mà nó được sử dụng. Framework sẽ tạo một thể hiện của sự phụ thuộc và loại bỏ nó khi nó không còn cần thiết nữa.