**Định nghĩa:**
Attribute là một loại metadata được sử dụng để cung cấp thông tin bổ sung về các thành phần của chương trình, chẳng hạn như lớp (class), phương thức (method), hoặc các thành phần khác trong mã nguồn C#. Attribute giúp mở rộng thông tin về các thành phần này mà không cần phải sửa đổi mã nguồn chính.

**Sử dụng:**
Các attribute được sử dụng bằng cách đặt phía trên các thành phần (lớp, phương thức, property…) với cặp ngoặc vuông [] bao lại. Nhiều attribute có thể được sử dụng trong cùng một cặp ngoặc vuông  này và được ngăn cách nhau bởi dấu phẩy “,”.
[AttributeName(param1, param2 ...)]
Ví dụ:
```csharp
public class MyClass {

    [Obsolete ("Phương thức này lỗi thời, hãy  dùng phương thức Abc")]
        public static void Method1 () {
            Console.WriteLine ("Phương thức chạy");
        }
}
```
**Các attribute thông dụng:**
[Obsolete]: Đánh dấu một phương thức, lớp hoặc thuộc tính đã bị lỗi thời và không nên được sử dụng trong mã nguồn mới.
[Serializable]: Đánh dấu một lớp có thể được serialize và deserialize thành các đối tượng, để lưu trữ hoặc truyền tải giữa các ứng dụng khác nhau.
[DllImport]: Đánh dấu một phương thức tĩnh mà được triển khai bởi một thư viện động bên ngoài.
[Conditional]: Đánh dấu một phương thức để chỉ định điều kiện biên dịch, khi được đánh dấu, phương thức chỉ được biên dịch khi điều kiện được đưa ra.
[DebuggerDisplay]: Cho phép bạn chỉ ra các thông tin cần thiết sẽ hiện thị trên đối tượng khi debug.
[Description]: Thêm mô tả cho các property hoặc event. Bạn có thể lấy được các thông tin này trong quá trình runtime.
[AttributeUsage]: Đánh dấu một attribute để chỉ định cách sử dụng attribute, bao gồm cách sử dụng, số lần sử dụng, và các đối tượng được áp dụng attribute.

**Attribute tự tạo**:
Để tạo Attribute riêng cần tạo một lớp kế thừ từ System.Attribute. Tuy nhiên để sử dụng được trước tiên cần thành thạo kỹ thuật Reflection trong C#.
Ví dụ:
```csharp
// áp dụng được cho lớp, thuộc tính của lớp và phương thức của lớp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
public class MyCustomAttribute : Attribute
{
    public string Description { get; set; }
    public MyCustomAttribute(string description)
    {
        Description = description;
    }
}
```
Truy xuất thông tin của custom attribute bằng cách sử dụng reflection
```csharp
// Đọc các Attribute của lớp
    var a = new User ();
    foreach (Attribute attr in a.GetType ().GetCustomAttributes (false)) {
    MyCustomAttribute myAttr = attr as MyCustomAttribute;
    if (myAttr != null) {
        Console.WriteLine ($"{a.GetType().Name,10} : {myAttr.Description}");
    }
    }
//       User : Lớp biểu diễn người dùng
//        age : Thuộc tính lưu tuổi
//    ShowAge : Phương thức này hiện thị age
```
