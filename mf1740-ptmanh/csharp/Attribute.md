- Attribute là một khái niệm quan trọng trong C#, được sử dụng để thêm thông tin bổ sung cho các thành phần của chương trình, chẳng hạn như lớp, phương thức, thuộc tính, trường, tham số, v.v. Các attribute có thể được sử dụng để cung cấp thông tin cho trình biên dịch, trình dịch ngược, trình đọc mã, các công cụ phân tích mã, hoặc các công cụ khác trong quá trình phát triển và triển khai phần mềm.
- Cú pháp để xác định một attribute trong C# như sau:
[attribute(positional_parameter, name_parameter = giá_trị, ...)] element
    + attribute là tên của attribute.
    + positional_parameter là các tham số vị trí của attribute.
    + name_parameter là các tham số tên của attribute.
    + element là phần tử mà attribute được áp dụng cho.

- Các attribute được định nghĩa sẵn trong .NET Framework bao gồm:
    + Obsolete: Đánh dấu một phần tử là lỗi thời và không nên sử dụng nữa.
    + Conditional: Chỉ định điều kiện mà một phần tử có thể được sử dụng.
    + AttributeUsage: Xác định cách một attribute có thể được sử dụng.
    + MetadataAttribute: Thêm metadata vào một phần tử.

- Tự tạo một attribute:
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    + AttributeTargets: chỉ ra rằng attribute có thể áp dụng cho đối tượng nào: class, property, method,...
    + Inherited: chỉ ra attribute có thể được kế thừa bởi các lớp dẫn xuất hay không, mặc định là true
    + AllowMultiple: chỉ ra attribute có thể được xuất hiện nhiều lần trên 1 đối tượng hay không, mặc định là không
    