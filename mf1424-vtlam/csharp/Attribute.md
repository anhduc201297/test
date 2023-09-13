# Khái niệm
Attribute trong C# được sử dụng để truyền thông tin tới runtime về các hành vi của các phần tử khác nhau như các lớp, phương thức, cấu trúc, enum, assembly… trong chương trình của bạn. Bạn có thể thêm thông tin khai báo vào chương trình bằng cách sử dụng attribute. Một thẻ khai báo được mô tả bởi các dấu ngoặc vuông ([]) đặt bên trên phần tử mà nó được sử dụng cho.

Cú pháp:

[attribute(positional_parameter, name_parameter = giá_trị, ...)]
element
Tên của attribute và giá trị của nó được xác định bên trong dấu ngoặc vuông, ở trước phần tử mà attribute được áp dụng cho positional_parameter xác định thông tin thiết yếu và name_parameter xác định thông tin tùy chọn.

# Attribute được định nghĩa trước trong C#
- AttributeUsage: mô tả cách một lớp attribute tùy chỉnh có thể được sử dụng. Nó xác định kiểu của các item, mà từ đó attribute có thể áp dụng cho.

Cú pháp:

[AttributeUsage(
   validon,
   AllowMultiple=allowmultiple,
   Inherited=inherited
)]
Trong đó:

validon: xác định các phần tử ngôn ngữ mà attribute có thể được đặt. Nó là tổ hợp giá trị của một AttributeTargets enumerator. Giá trị mặc định là AttributeTargets.All.
allowmultiple (tùy ý): cung cấp giá trị cho đặc tính AllowMultiple của attribute này, một giá trị Boolean. Nếu điều này là true, attribute là multiuse (đa dụng). Giá trị mặc định là false (tức là single-use - chỉ dùng một lần).
inherited (tùy ý): cung cấp giá trị cho đặc tính Inherited của attribute này, một giá trị Boolean. Nếu nó là true, attribute được kế thừa bởi các lớp dẫn xuất. Giá trị mặc định là false (không được kế thừa).

Ví dụ:

[AttributeUsage(
   AttributeTargets.Class |
   AttributeTargets.Constructor |
   AttributeTargets.Field |
   AttributeTargets.Method |
   AttributeTargets.Property, 
   AllowMultiple = true)]

- Conditional: đánh dấu một phương thức có điều kiện mà sự thực thi của nó phụ thuộc vào một định danh tiền xử lý được chỉ định.

Cú pháp:

[Conditional(
   conditionalSymbol
)]

Ví dụ:

[Conditional("DEBUG")]

- Obsolete: đánh dấu một thực thể chương trình mà không nên được sử dụng. Nó cho bạn thông báo với trình biên dịch để loại bỏ một phần tử target cụ thể.

Cú pháp:

[Obsolete(message)]
[Obsolete(message,iserror)]
Trong đó:

message là một chuỗi mô tả lý do tại sao item là obsolete và cái gì được sử dụng thay cho nó.

iserror là một giá trị Boolean. Nếu giá trị của nó là true, trình biên dịch sẽ coi việc sử dụng item này là một lỗi. Giá trị mặc định là false (tức là trình biên dịch sẽ tạo ra cảnh báo).

Ví dụ:

[Obsolete("Phương thức này lỗi thời, hãy dùng phương thức Abc")]

# Tạo Custom Attribute trong C#
Còn gọi là attribute tùy biến hay attribute do người dùng tự định nghĩa.

Để tạo Attribute riêng khá đơn giản, bạn chỉ việc tạo một lớp kế thừ từ System.Attribute

Ví dụ:

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class MotaAttribute : Attribute // có thể đặt tên Mota thay cho MotaAttribute
    {
        // Phương thức khởi tạo
        public MotaAttribute(string v) => Description = v;

        public string Description {set; get;}
    }

Áp dụng attribute:

[Mota("Lớp biểu diễn người dùng")]                  // thêm Attribute cho lớp
    public class User
    {
        [Mota("Thuộc tính lưu tuổi")]                   // thêm Attribute cho thuộc tính lớp
        public int age {set; get;}

        [Mota("Phương thức này hiện thị age")]          // thêm Attribute cho phương thức
        public void ShowAge() {}
    }