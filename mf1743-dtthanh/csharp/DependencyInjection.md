# Dependency injection (DI) trong C#
## I. Khái niệm
Dependency injection (DI) là một kỹ thuật trong lập trình, nó là một hình thức cụ thể của Dependency Inverse. DI thiết kế sao cho các dependency (phụ thuộc) của một đối tượng CÓ THỂ được đưa vào, tiêm vào đối tượng đó (Injection) khi nó cần tới (khi đối tượng khởi tạo). Cụ thể cần làm:
- Xây dựng các lớp (dịch vụ) có sự phụ thuộc nhau một cách lỏng lẻo, và dependency có thể tiêm vào đối tượng (injection) - thường qua phương thức khởi tạo constructor, property, setter
- Xây dựng được một thư viện có thể tự động tạo ra các đối tượng, các dependency tiêm vào đối tượng đó, thường là áp dụng kỹ thuật Reflection của C# (xem thêm lớp type): Thường là thư viện này quá phức tạp để tự phát triển nên có thể sử dụng các thư viện có sẵn như: Microsoft.Extensions.DependencyInjection hoặc thư viện bên thứ ba như Windsor, Unity Ninject ...

Để sử dụng được các Framework hỗ trợ DI thì ta cần phải viết code mà các dependency có thể đưa vào từ bên ngoài (chủ yếu qua phương thức khởi tạo), giúp cho các dịch vụ tương đối độc lập với nhau. 
## II. Các kiểu Dependency Injection
- __Inject thông qua phương thức khởi tạo__: cung cấp các Dependency cho đối tượng thông qua hàm khởi tạo
````
    public class Client {
         private IService _service;
         public Client(IService service) {
             this._service = service;
         }
         public ServeMethod() { 
             this._service.Serve(); 
         }
    }
````
- __Inject thông qua setter__: tức các Dependency như là thuộc tính của lớp, sau đó inject bằng gán thuộc tính cho Depedency
````
public class Client {
    private IService _service;
        public IService Service {
            set { this._service = value; }
        }
    public ServeMethod() { 
        this._service.Serve(); 
    }
}
````
- __Inject thông qua các Interface__
````
public interface IInjectable
{
    void Inject(IService service);
}
public class Client : IInjectable
{
    private IService _service;
    public void Inject(IService service)
    {
        _service = service;
    }
}
````

## III. DI Container
Mục đích sử dụng DI, để tạo ra các đối tượng dịch vụ kéo theo là các Dependency của đối tượng đó. Để làm điều này ta cần sử dụng đến các thư viện, có rất nhiều thư viện DI - Container (cơ chứa chứa và quản lý các dependency) như: ```Windsor, Unity Ninject, DependencyInjection ...```

Trong đó DependencyInjection là DI Container mặc định của ``` ASP.NET Core```, phần này tìm hiểu về DI Container này ```Microsoft.Extensions.DependencyInjection```
