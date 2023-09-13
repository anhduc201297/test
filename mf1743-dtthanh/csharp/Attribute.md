# Attribute trong C#
## I. Tổng quan
Attribute cung cấp một phương pháp mạnh mẽ để liên kết siêu dữ liệu hoặc thông tin khai báo với code (lớp, phương thức, thuộc tính). Sau khi một thuộc tính được liên kết với một thực thể chương trình, thuộc tính đó có thể được truy vấn trong runtime chạy bằng cách sử dụng một kỹ thuật gọi là reflection ( lấy thông tin kiểu dữ liệu tại thời điểm thực thi).
Trong C# có định nghĩa sẵn vô số các Attribute, để sử dụng nó bạn chỉ cần viết tên Attribute trong dấu [] trước đối tượng áp dụng như lớp, phương thức, thuộc tính lớp (có tham số như hàm, nếu Attribute đó yêu cầu).
```` [AttributeName(param1, param2 ...)] ````
__Ví dụ__: Attribute Obsolete, để đánh dấu một phương thức, lớp... nào đó là lạc hậu.
    
    public class MyClass {
        [Obsolete ("Phương thức này lỗi thời, hãy  dùng phương thức Abc")]
        public static void Method1 () {
            Console.WriteLine ("Phương thức chạy");
        }
    }
Ở đây đã đánh dấu Method1() bị lạc hậu, khi sử dụng Method1() sẽ có cảnh báo hoặc sẽ hiển thị ErrorMessage khi run/build code.
Attribute không chỉ là thêm thông tin sử dụng bởi trình biên dịch, mà nó thực sự thêm các đặc tính mà code sẽ đọc được ở thời điểm thực thi.
## II. Attribute targets
Target của Attribute là thực thể mà thuộc tính đó áp dụng. Một thuộc tính có thể áp dụng cho một lớp, một phương thức cụ thể hoặc toàn bộ tập hợp. Theo mặc định, một thuộc tính sẽ áp dụng cho phần tử theo sau nó. Nhưng bạn cũng có thể xác định rõ ràng, ví dụ: liệu một thuộc tính có được áp dụng cho một phương thức hay cho tham số của nó hay cho giá trị trả về của nó hay không.
Các target của attribute:
````assembly, module, field, event, method, method, property, return, type````
## III. Attribute tự tạo
Custom Attribute trong C# .NET là một cách để bạn có thể định nghĩa thêm các thông tin met-data cho một lớp, một thuộc tính, một phương thức hay một tham số. Các attribute này có thể được sử dụng để cung cấp thông tin cho các công cụ như trình biên dịch, trình giải mã, hoặc để đánh dấu các vị trí quan trọng trong mã nguồn.
__Ví dụ__: Ở đây để tạo một Attribute ta cần kế thừa từ System.Attribute và sử dụng ````[AttributeUsage(...)]```` để thiết lập thuộc tính cho Attribute

    using System;
    namespace CSAttribute {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
        public class MotaAttribute : Attribute // có thể đặt tên Mota thay cho MotaAttribute
        {
            // Phương thức khởi tạo
            public MotaAttribute(string v) => Description = v;
            public string Description {set; get;}
        }
    }
## IV. Data Annotation/Attribute trong C#
Các Data Annotation/Attribute trong C# định nghĩa trong namespace ````System.ComponentModel.DataAnnotations````, có các loại gồm:
- Các Attribute để kiểm tra dữ liệu (Validation Attribute)
- Các Attribute hiện thị (Display Attribute), điều khiển dữ liệu trong lớp hiện thị thế nào trong UI
- Modelling Attribute

Một vài Data Annotation thường dùng để validate dữ liệu:
| Attribute       | Mô tả |
|:----------------|:------------------------------------|
| ````Require```` | Dữ liệu phải được thiết lập (!=null)|
| ````StringLength```` | Thiết lập độ dài trường dữ liệu|
| ````DataType```` |Chỉ ra dữ liệu phải liên kết phù hợp với một kiểu nào đó|
| ````Range```` |Chỉ ra dữ liệu phải trong khoảng nào đó|
|````Phone````|[Phone] dữ liệu phải là dạng số điện thoại|
|````EmailAddress````|[EmailAddress] dữ liệu phải là dạng email|
## V. Tổng kết
Attribute rất quan trọng và được sử dụng nhiều trong các ứng dụng Web .NET CORE
