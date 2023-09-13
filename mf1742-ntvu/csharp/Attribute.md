Thuộc tính (attribute) trong C#, là một thẻ khai báo, được sử dụng để truyền thông tin tới runtime về các hành vi của các phần tử khác nhau như các lớp, phương thức, cấu trúc, enum, assembly… trong chương trình

----- Xác định một Attribute trong C#
[attribute(positional_parameter, name_parameter = giá_trị, ...)]
element

Attribute được định nghĩa trước trong C#

.Net Framework cung cấp 3 Attribute được định nghĩa trước:

    AttributeUsage
    Conditional
    Obsolete

---- AttributeUsage trong C#

-   Mô tả cách một lớp attribute tùy chỉnh có thể được sử dụng.
-   Xác định kiểu của các item, mà từ đó attribute có thể áp dụng cho
-   Cú pháp để xác định attribute này trong C# như sau:

            [AttributeUsage(
            validon,
            AllowMultiple=allowmultiple,
            Inherited=inherited
            )]

    Trong đó:
    +, validon: tổ hợp giá trị của một AttributeTargets enumerator. Mặc định là AttributeTargets.All
    +, allowmultiple (tùy ý) : boolean, true: đa dụng, false: chỉ dùng một lần
    +, inherited (tùy ý) : boolean, true: được kế thừa bởi các lớp dẫn xuất, false: không được kế thừa

---- Conditional trong C#

-   Đánh dấu một phương thức có điều kiện, gây ra việc biên dịch có điều kiện của các lời gọi phương thức,

-   Cú pháp để xác định attribute này trong C# như sau:

[Conditional(
conditionalSymbol
)]

---- Obsolete trong C#

-   Đánh dấu 1 phương thức ko nên sử dụng
-   Cú pháp để xác định Attribute này trong C# là như sau:

[Obsolete(
message // chuỗi mô tả lý do do tại sao item là obsolete và cái gì được sử dụng thay cho nó.
)]
[Obsolete(
message,
iserror // {boolean} true: complier coi việc sử dụng item này là 1 lỗi
)]

---- Tạo Custom Attribute trong C#

-   Tạo và sử dụng Custom Attribute trong C# bao gồm 4 bước sau:

*   Bước 1: Khai báo một Custom Attribute trong C#

-   Một Custom Attribute mới nên được dẫn xuất từ lớp System.Attribute trong C#

*   Bước 2: Xây dựng Custom Attribute trong C#

*   Bước 3: Áp dụng Custom Attribute trong C#

-   Custom Attribute trong C# được áp dụng bằng việc đặt nó ngay trước target của nó

*   Bước 4: Truy cập các Attribute thông qua reflection

-   Reflection: Reflection có các ứng dụng sau:

    Cho phép quan sát thông tin attribute tại runtime.
    Cho phép kiểm tra các kiểu khác nhau trong một Assembly và khởi tạo các kiểu này.
    Cho phép Late Binding tới các phương thức và các thuộc tính.
    Cho phép tạo các kiểu mới tại runtime và sau đó thực hiện một số tác vụ sử dụng những kiểu này.

-   Xem Metadata trong C#
