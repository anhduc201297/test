Là một design pattern được ASP.Net hỗ trợ. Đây là một kỹ thuật để hiện thực hóa Inversion of Control Pattern
- Các module không giao tiếp trực tiếp với nhau, mà thông qua interface. Module cấp thấp sẽ implement interface, module cấp cao sẽ gọi module cấp thấp thông qua interface.
- Việc Module nào gắn với interface nào sẽ được config trong code hoặc trong file XML.
DI được dùng để làm giảm sự phụ thuộc giữa các module, dễ dàng hơn trong việc thay đổi module, bảo trì code và testing.

2. Các loại DI
- Constructor injection: Các dependency (biến phụ thuộc) được cung cấp thông qua constructor (hàm tạo lớp).
- Setter injection: Các dependency sẽ được truyền vào 1 class thông qua các setter method (hàm setter).
- Interface injection: Dependency sẽ cung cấp một Interface, trong đó có chứa hàm có tên là Inject. Các client phải triển khai một Interface mà có một setter method dành cho việc nhận dependency và truyền nó vào class thông qua việc gọi hàm Inject của Interface đó.

3. Ưu điểm, khuyết điểm của Dependency Injection:
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


4. Ví dụ
Khi thực thi, classB có thể được thay thế bởi bất kỳ lớp nào triển khai từ giao điện interface B, classB cụ thể mà classA sử dụng được quyết định và điểu khiển bởi interface B (điều này có nghĩa tại sao gọi là đảo ngược phụ thuộc)
interface IClassB {
    public void ActionB();
}
interface IClassC {
    public void ActionC();
}

class ClassC : IClassC {
    public ClassC() => Console.WriteLine ("ClassC is created");
    public void ActionC() => Console.WriteLine("Action in ClassC");
}

class ClassB : IClassB {
    IClassC c_dependency;
    public ClassB(IClassC classc)
    {
        c_dependency = classc;
        Console.WriteLine("ClassB is created");
    }
    public void ActionB()
    {
        Console.WriteLine("Action in ClassB");
        c_dependency.ActionC();
    }
}


class ClassA {
    IClassB b_dependency;
    public ClassA(IClassB classb)
    {
        b_dependency = classb;
        Console.WriteLine("ClassA is created");
    }
    public void ActionA()
    {
        Console.WriteLine("Action in ClassA");
        b_dependency.ActionB();
    }
}