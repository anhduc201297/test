* Attributes trong C#
- Attributes được sử dụng trong C# để truyền tải thông tin khai báo hoặc siêu dữ liệu về các thành phần mã khác nhau như methods, assemblies, properties, types... Các thuộc tính được thêm vào mã bằng cách sử dụng thẻ khai báo được đặt bằng dấu ngoặc vuông ở trên cùng của phần tử mã được yêu cầu.
- Cú pháp để xác định một thuộc tính như sau:
    [attribute(positional_parameter, name_parameter = giá_trị, ...)]
    element
  Tên của Attribute và giá trị của nó được xác định bên trong dấu ngoặc vuông, ở trước phần tử từ đó thuộc tính được áp dụng cho. positional_parameter xác định thông tin thiết yếu và name_parameter xác định thông tin tùy ý.
- Properties of Attributes
  + Attributes cũng có thể có đối số (arguments) giống như methods, properties có đối số.
  + Attributes có thể có 0 hoặc nhiều tham số (parameters).
  + Các phần tử mã khác nhau như methods, assemblies, properties, types có thể có một hoặc nhiều attributes.
  + Reflection có thể được sử dụng để lấy siêu dữ liệu của chương trình bằng cách truy cập các thuộc tính (attributes) trong thời gian chạy.
  + Attributes thường có nguồn gốc từ System.Attribute class.
- Có hai loại triển khai Attributes do .NET Framework cung cấp là:
  1. Predefined Attributes (Thuộc tính được xác định trước)
  Predefined attributes là những thuộc tính là một phần của .NET Framework Class Library và được trình biên dịch C# hỗ trợ cho một mục đích cụ thể. Một số thuộc tính được xác định trước có nguồn gốc từ không gian tên System. Các lớp cơ sở thuộc tính được đưa ra như sau:
  - AttributeUsageAttribute: Thuộc tính này chỉ định cách sử dụng một thuộc tính khác.
  - CLSCompliantAttribute: Thuộc tính này hiển thị liệu một thành phần mã cụ thể có tuân thủ Đặc tả ngôn ngữ chung (Common Language Specification) hay không.
  - ContextStaticAttribute: Thuộc tính này chỉ định rằng một trường tĩnh (static field) không được chia sẻ giữa các ngữ cảnh.
  - FlagsAttribute: FlagsAttribute chỉ định rằng một bảng liệt kê (enumeration) có thể được sử dụng như một tập hợp các cờ. Điều này được sử dụng phổ biến nhất với các toán tử bit.
  - LoaderOptimizationAttribute: Thuộc tính này đặt chính sách tối ưu hóa cho trình tải mặc định trong phương thức chính (main method).
  - NonSerializedAttribute: Thuộc tính này biểu thị rằng trường của lớp có thể tuần tự hóa không nên được tuần tự hóa.
  - ObsoleteAttribute: Thuộc tính này đánh dấu các thành phần mã đã lỗi thời, tức là không còn được sử dụng nữa.
  - SerializableAttribute: Thuộc tính này biểu thị rằng trường của lớp có thể tuần tự hóa có thể được tuần tự hóa.
  - ThreadStaticAttribute: Thuộc tính này chỉ ra rằng có một giá trị trường tĩnh duy nhất cho mỗi luồng.
  - DllImportAttribute: Thuộc tính này chỉ ra rằng phương thức này là một điểm vào tĩnh như được hiển thị bởi DLL không được quản lý.
  2. Custom Attributes (Thuộc tính tùy chỉnh)
  - Các thuộc tính tùy chỉnh có thể được tạo trong C# để đính kèm thông tin khai báo vào các phương thức, tập hợp, thuộc tính, kiểu... theo bất kỳ cách nào được yêu cầu. Điều này làm tăng khả năng mở rộng của .NET framework. Các bước để tạo Thuộc tính tùy chỉnh:
  + Xác định custom attribute class có nguồn gốc từ System.Attribute class.
  + Tên lớp thuộc tính tùy chỉnh phải có hậu tố Attribute.
  + Sử dụng thuộc tính AttributeUsage để chỉ định cách sử dụng lớp thuộc tính tùy chỉnh đã tạo.
  + Tạo hàm tạo và các thuộc tính có thể truy cập của lớp thuộc tính tùy chỉnh.