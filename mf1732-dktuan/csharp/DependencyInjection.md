**Inversion of Control (IoC) / Dependency inversion**
Inversion of Control (IoC - Đảo ngược điều khiển) là một nguyên lý thiết kế trong công nghệ phần mềm trong đó các thành phần nó dựa vào để làm việc bị đảo ngược quyền điều khiển khi so sánh với lập trình hướng thủ thục truyền thống.

Thiết kế truyền thống - tham chiếu trực tiếp đến Dependency
Có lớp class A có sử dụng một chức năng (gọi hàm ào đó) của class B, lớp class B lại tham chiếu và gọi các chức năng có trong class C. Ta thấy class A dựa vào class B để hoạt động, class B dựa vào class C. Nếu vậy khi thiết kế theo cách thông thường, viết code thì class A có tham chiếu trực tiếp (cứng) đến class B và trong class B có tham chiếu đến class C


Thiết kế theo cách đảo ngược phụ thuộc Inverse Dependency
Cách viết code này, ở thời điểm thực thi thì class A vẫn gọi được hàm có class B, class B vẫn gọi hàm có class C nghĩa là kết quả không đổi. Tuy nhiên, khi thiết kế ở thời điểm viết code (trong code) class A không tham chiếu trực tiếp đến class B mà nó lại sử dụng interface (hoặc lớp abstruct) mà classB triển khai. Điều này dẫn tới sự phụ thuộc lỏng lẻo giữa classA và classB

**Các kiểu Dependency Injection**
Từ cách thức một dependency được đưa vào đối tường cần nó thì được phân chia có ba kiểu DI:

-Inject thông qua phương thức khởi tạo: cung cấp các Dependency cho đối tượng thông qua hàm khởi tạo ( như đã thực hiện ở ví dụ trên) - tập trung vào cách này vì thư viện .NET hỗ trợ sẵn
-Inject thông qua setter: tức các Dependency như là thuộc tính của lớp, sau đó inject bằng gán thuộc tính cho Depedency object.denpendency = obj;
-Inject thông qua các Interface - xây dựng Interface có chứa các phương thức Setter để thiết lập dependency, interface này sử dụng bởi các lớp triển khai, lớp triển khai phải định nghĩa các setter quy định trong interface

Trong ba kiểu Inject thì Inject qua phương thức khởi tạo rất phổ biến vì tính linh hoạt, mềm dẻo, dễ xây dựng thư viện DI...

Ví dụ:
```csharp
using System;

// Một interface cho việc ăn mặc
public interface IOutfit
{
    void Wear();
}

// Một object cấp thấp, implement của IOutfit
public class Bikini : IOutfit
{
    public void Wear()
    {
        Console.WriteLine("Đã mặc Bikini");
    }
}

// Bây giờ Girl chỉ phụ thuộc vào IOutfit. nếu muốn thay đổi đồ của cô gái, chúng ta chỉ cần cho IOutfit một thể hiện mới.
public class Girl{
    private Outfit outfit;
    public Girl(Outfit anything){
      this.outfit = anything // Tạo ra một cô gái, với một món đồ tùy biến
      // Không bị phụ thuộc quá nhiều vào thời điểm khởi tạo, hay code.
    }
}

using System;

public class Main
{
    public static void Main(string[] args)
    {
        IOutfit bikini = new Bikini(); // Tạo ra đối tượng Bikini ở ngoài đối tượng
        Girl ngocTrinh = new Girl(bikini); // Mặc nó vào cho cô gái khi tạo ra cô ấy.
    }
}

```

**DI Container**
Mục đích sử dụng DI, để tạo ra các đối tượng dịch vụ kéo theo là các Dependency của đối tượng đó. Để làm điều này ta cần sử dụng đến các thư viện, có rất nhiều thư viện DI - Container (cơ chứa chứa và quản lý các dependency) như: Windsor, Unity Ninject, DependencyInjection ...

Trong đó DependencyInjection là DI Container mặc định của ASP.NET Core, phần này tìm hiểu về DI Container này Microsoft.Extensions.DependencyInjection

Trước tiên phải đảm bảo tích hợp Package Microsoft.Extensions.DependencyInjection vào dự án

dotnet add package Microsoft.Extensions.DependencyInjection
Sau đó sử dụng namespace

using  Microsoft.Extensions.DependencyInjection;
Từ đây các đối tượng lớp, các dependency ta gọi chúng là các dịch vụ (service)!

**ServiceCollection** là lớp triển khai giao diện IServiceCollection nó có chức năng quản lý các dịch vụ (đăng ký dịch vụ - tạo dịch vụ - tự động inject - và các dependency của địch vụ ...). ServiceCollection là trung tâm của kỹ thuật DI, nó là thành phần rất quan trọng trong ứng dụng ASP.NET

Các sử dụng cơ bản như sau:

Khởi tạo đối tượng ServiceCollection, sau đó đăng ký (lớp) các dịch vụ vào ServiceCollection
Từ ServiceCollection phát sinh ra đối tượng ServiceProvider, từ đối tượng này truy vấn lấy ra các dịch vụ cụ thể khi cần.

**ServiceProvider** cung cấp cơ chế để lấy ra (tạo và inject nếu cần) các dịch vụ đăng ký trong ServiceCollection. Đối tượng ServiceProvider được tạo ra bằng cách gọi phương thức BuildServiceProvider() của ServiceCollection