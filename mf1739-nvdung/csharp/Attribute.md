Tìm hiểu về attributes csharp

I, Tổng quan:
    - Ngôn ngữ C# cho phép lập trình viên chỉ định thông tin khai báo về các thực thể được xác định trong chương trình. Ví dụ: như modifiers (public, private, protected, ...).
    - C# cho phép các lập trình viên phát minh ra các loại thông tin khai báo mới, được gọi là các thuộc tính (attribute). Các lập trình viên sau đó có thể đính kèm các thuộc tính (attribute) vào các thực thể chương trình khác nhau và truy xuất thông tin thuộc tính trong run-time environment.
    - Attributes được xác định thông qua việc khai báo attributes classes, có thể có các parameters. Các attributes được gắn vào các thực thể (class, method, ...) trong chương trình bằng attribute specifications, và có thể truy xuất tại run-time dưới dạng attribute instances.

II, Các lớp thuộc tính (attribute classes):
    1, General:
        - Một class bắt nguồn từ abstract class (trực tiếp hay gián tiếp) được coi là một lớp thuộc tính (attribute class).
        - Khai báo một attribute class như sau:
            public class A : System.Attribute {} hoặc
            public class A : Attribute {}
        - Lưu ý: Tạo một class chung sẽ không được sử dụng làm base class System.Attribute. Ví dụ:
            public class B : Attribute {}
            public class C<T> : B {} // Error: generic cannot be an attribute.

    2, Attribute usage:
        - AttributeUsage được sử dụng để mô tả cách dử dụng một attribute class.
        - AttributeUsage là một positional parameter cho phép một attribute class chỉ định các loại thực thể chương trình mà nó có thể sử dụng.
        - Ví dụ: Định nghĩa attribute class, sử dụng thuộc tính SimpleAttributeSimple (hoặc viết gọn Simple):
            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
            public class SimpleAttribute : Attribute { ... }
            [Simple] class Class1 {...}
            [Simple] interface Interface1 {...}

        - AttributeUsage  có một named parameter cho biết liệu thuộc tính có thể được chỉ định nhiều lần cho một thực thể nhất định hay không. Nếu đối với một attribute class là true, thì lớp thuộc tính đó là một lớp thuộc tính đa dụng (multi-use attribute class) và có thể được chỉ định nhiều lần trên một thực thể. Nếu đối với một lớp thuộc tính là false hoặc nó không được chỉ định, thì lớp thuộc tính đó là một lớp thuộc tính sử dụng một lần (single-use attribute class) và có thể được chỉ định nhiều nhất một lần trên một thực thể. Ví dụ:

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
            public class AuthorAttribute : Attribute
            {
                public string Name { get; }
                public AuthorAttribute(string name) => Name = name;
            }

            [Author("BabiPunSociu"), Author("NVDung")]
            class Class1 {...}

        - AttributeUsage có một named parameter cho biết liệu thuộc tính được chỉ định trên base class, cũng được kế thừa từ các class có nguồn gốc từ base class đó. Nếu đối với một atrribute class là true, thì thuộc tính đó được kế thừa. Nếu đối với một attribute class là false thì thuộc tính đó không được kế thừa. Nếu nó không được chỉ định, giá trị mặc định của nó là true (Inherited). Ví dụ:

            class X : Attribute { ... } tương đương với

            [AttributeUsage(
                AttributeTargets.All,
                AllowMultiple = false,
                Inherited = true)
            ]
            class X : Attribute { ... }

    3, Positional parameters, named parameters:
        - Các atrribute class có thể có positional parameters và named parameters.
        - Mỗi public instance constructor cho attribute class xác định một chuỗi positional parameter hợp lệ cho attribute class đó.
        - Mỗi property của một attribute class xác định một named parameter cho attribute class. Để một property xác định một named parameter, property đó phải có cả public get accessor và public set accessor.
        - Ví dụ: Sử dụng các property: HelpAttribute, url, Topic, Url:

            [AttributeUsage(AttributeTargets.Class)]
            public class HelpAttribute : Attribute
            {
                public HelpAttribute(string url) {...} // url is a positional parameter

                public string Topic { get; set; } // Topic is a named parameter

                public string Url { get; }
            }

            [Help("http://www.mycompany.com/xxx/Class1.htm")]
            class Class1 {...}

            [Help("http://www.mycompany.com/xxx/Misc.htm", Topic ="Class2")]
            class Class2 {...}
    4, Attribute parameter types:
        Các loại positonal parameters và named parameters của một attribute class được giới hạn ở các types đó là:
            - Các loại nguyên thủy: bool, byte, char, float, double, int, long, sbyte, short, string, uint, ulong, ushort, ...
            - Kiểu object.
            - Kiểu System.Type.
            - Kiểu Enum
            - Array một chiều.
            - Đối số (arg) hàm tạo hoặc public field.
III, Attribute specification (Đặc tả thuộc tính):
    - Attribute specification là việc áp dụng một attribute được xác định trước đó cho một thực thể chương trình. Attribute là một phần thông tin khai báo bổ sung được chỉ định cho một thực thể chương trình.
    - Attribute specification được chỉ định trong các attribute sections. Phần attribute bao gồm một cặp dấu ngoặc vuông "[]", bao quanh danh sách được phân tách bằng dấu phẩy gồm một hoặc nhiều attributes.
    - Một attribute gồm một attribute_name và một danh sách tùy chọn các đối số vị trí và được đặt tên. Các đối số vị trí (nếu có) đứng trước các đối số được đặt tên. Một đối số vị trí bao gồm một attribute_argument_expression; Một đối số được đặt tên bao gồm một tên, theo sau là một dấu bằng, theo sau là một attribute_argument_expression, cùng nhau, bị ràng buộc bởi các quy tắc tương tự như gán đơn giản.

    - attribute_name xác định một attribute class.
    - Khi một attribute được đặt ở cấp độ global, cần phải có global_attribute_target_specifier. Chỉ phép có 2 giá trị cho global_attribute_target bằng:
        + assembly: the target is the containing assembly
        + module: the target is the containing module
    - Attribute_target chỉ được sử dụng trong các ngữ cảnh sau: event, field, method, param, property, return, type, typevar.

    - Một số ngữ cảnh nhất định cho phép đặc tả một thuộc tính trên nhiều mục tiêu. Một chương trình có thể chỉ định rõ ràng mục tiêu bằng cách bao gồm một attribute_target_specifier. Nếu không có attribute_target_specifier một mặc định được áp dụng, nhưng một attribute_target_specifier có thể được sử dụng để xác nhận hoặc ghi đè mặc định.

IV, Attribute instances:
    1, General:
        - Attribute instances là instance đại diện cho một attribute tại thời gian chạy.
        - Một attribute được định nghĩa với một attribute class, positional arguments và các named arguments. Attribute instance  là một instance của attribute class được khởi tạo với các positional arguments và các named arguments.
        - Việc truy xuất attribute instance bao gồm cả quá trình xử lý thời gian biên dịch và thời gian chạy.

    2, Compilation of an attribute:
        Việc biên dịch một thuộc tính với lớp thuộc tính , positional_argument_list , named_argument_list và được chỉ định trên một thực thể chương trình được biên dịch thành một tập hợp thông qua các bước sau: T P N E A
    3, Run-time retrieval of an attribute instance:
        Attribute instance được liên kết với có thể được truy xuất tại thời gian chạy từ cụm bằng cách sử dụng các bước sau:T C P N E A.

V, Reserved attributes (Thuộc tính dành riêng):
    1, General:
        Một số ít attributes ảnh hưởng đến ngôn ngữ theo một cách nào đó. Các attributes này bao gồm:
        - System.AttributeUsageAttribute: được sử dụng để mô tả các cách thức mà một lớp thuộc tính có thể được sử dụng.
        - System.Diagnostics.ConditionalAttribute: là một lớp thuộc tính đa dụng được sử dụng để xác định các phương thức có điều kiện và các lớp thuộc tính có điều kiện. Thuộc tính này cho biết một điều kiện bằng cách kiểm tra ký hiệu biên dịch có điều kiện.
        - System.ObsoleteAttribute: được sử dụng để đánh dấu một thành viên là lỗi thời.
        - System.Runtime.CompilerServices.CallerLineNumberAttribute: được sử dụng để cung cấp thông tin về ngữ cảnh gọi cho các tham số tùy chọn:
            + System.Runtime.CompilerServices.CallerFilePathAttribute
            + System.Runtime.CompilerServices.CallerMemberNameAttribute
    2, AttributeUsage attribute:
        - AttributeUsage được sử dụng để mô tả cách thức mà lớp thuộc tính có thể được sử dụng.
        - Một clas AttributeUsages được sử dụng với một attribute được bắt nguồn từ System.Attribute. Nếu không, lỗi thời gian biên dịch xảy ra.
    3, Conditional attribute:
        - Conditional attribute cho phép định nghĩa các phương thức có điều kiện và các lớp thuộc tính có điều kiện.

        - Conditional methods:
            + Một phương pháp được trang trí với thuộc tính là một phương pháp có điều kiện. Do đó, mỗi phương thức có điều kiện được liên kết với các ký hiệu biên dịch có điều kiện được khai báo trong các thuộc tính của nó.
            Ví dụ:
                [Conditional("ALPHA")]
                [Conditional("BETA")]
                public static void M() {...}

            + Một lệnh gọi đến một phương thức có điều kiện được bao gồm nếu một hoặc nhiều ký hiệu biên dịch có điều kiện liên quan của nó được xác định tại điểm gọi, nếu không cuộc gọi sẽ bị bỏ qua.

            + Một phương pháp có điều kiện phải tuân theo các hạn chế sau:
                ~ Phương pháp có điều kiện phải là một phương pháp trong một class_declaration hoặc struct_declaration. Lỗi thời gian biên dịch xảy ra nếu thuộc tính được chỉ định trên một phương thức trong khai báo giao diện.
                ~ Phương pháp có điều kiện sẽ có kiểu trả về là void
                ~ Phương pháp có điều kiện sẽ không được đánh dấu bằng sửa đổi. Tuy nhiên, một phương pháp có điều kiện có thể được đánh dấu bằng công cụ sửa đổi. Ghi đè của một phương pháp như vậy là có điều kiện ngầm và sẽ không được đánh dấu rõ ràng bằng một thuộc tính.
                ~ Phương pháp có điều kiện sẽ không phải là một triển khai của một phương thức giao diện. Nếu không, lỗi thời gian biên dịch xảy ra.
                ~ Các tham số của phương pháp có điều kiện sẽ không có sửa đổi.
        
        - Conditional attribute class:
            + Một lớp thuộc tính (§22.2) được trang trí với một hoặc nhiều thuộc tính là một lớp thuộc tính có điều kiện. Do đó, một lớp thuộc tính có điều kiện được liên kết với các ký hiệu biên dịch có điều kiện được khai báo trong các thuộc tính của nó.
            Ví dụ:
                [Conditional("ALPHA")]
                [Conditional("BETA")]
                public class TestAttribute : Attribute {}

            + Thông số kỹ thuật thuộc tính (§22.3) của một thuộc tính có điều kiện được bao gồm nếu một hoặc nhiều ký hiệu biên dịch có điều kiện liên quan của nó được xác định tại điểm đặc tả, nếu không thì đặc tả thuộc tính sẽ bị bỏ qua.

            + Điều quan trọng cần lưu ý là việc bao gồm hoặc loại trừ một đặc tả thuộc tính của một lớp thuộc tính có điều kiện được kiểm soát bởi các ký hiệu biên dịch có điều kiện tại điểm đặc tả.
    
    4, Obsolete attribute:
        - Obsolete attribute được sử dụng để đánh dấu các loại và thành viên của các loại không nên sử dụng nữa.
        - Nếu một chương trình sử dụng một loại hoặc thành viên được trang trí với thuộc tính, trình biên dịch sẽ đưa ra cảnh báo hoặc lỗi. Cụ thể, trình biên dịch sẽ đưa ra cảnh báo nếu không có tham số lỗi nào được cung cấp hoặc nếu tham số lỗi được cung cấp và có giá trị . Trình biên dịch sẽ đưa ra lỗi nếu tham số lỗi được chỉ định và có giá trị.
        - Ví dụ:
        [Obsolete("This class is obsolete; use class B instead")]
        class A
        {
            public void F() {}
        }

        class B
        {
            public void F() {}
        }

        class Test
        {
            static void Main()
            {
                A a = new A(); // Warning
                a.F();
            }
        }

    5, Caller-info attributes:


VI, Các thuộc tính cho hoạt động tương tác (Attributes for interoperation):
    Để tương tác với các ngôn ngữ khác, một trình lập chỉ mục có thể được triển khai bằng cách sử dụng các thuộc tính được lập chỉ mục. Nếu không có thuộc tính nào cho trình lập chỉ mục, thì tên được sử dụng theo mặc định. Thuộc tính này cho phép nhà phát triển ghi đè mặc định này và chỉ định một tên khác.

    Ví dụ:
    [System.Runtime.CompilerServices.IndexerName("TheItem")]
    public int this[int index]
    {
        get { ... }
        set { ... }
    }