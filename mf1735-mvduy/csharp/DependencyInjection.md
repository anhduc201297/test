# Dependency Injection

## I. Định nghĩa

- Dependency Injection (DI) là một nguyên tắc trong lập trình và phát triển phần mềm được sử dụng để quản lý các phụ thuộc (dependencies) giữa các thành phần trong một ứng dụng. Mục tiêu chính của Dependency Injection là giảm sự liên kết giữa các thành phần của ứng dụng, làm cho ứng dụng dễ dàng bảo trì, mở rộng và kiểm thử hơn.


## II. Các loại Dependency Injection

- Constructor injection: Các dependency (biến phụ thuộc) được cung cấp thông qua constructor (hàm tạo lớp).
- Setter injection: Các dependency (biến phụ thuộc) sẽ được truyền vào 1 class thông qua các setter method (hàm setter).
- Interface injection: Dependency sẽ cung cấp một Interface, trong đó có chứa hàm có tên là Inject.  Các client phải triển khai một Interface mà có một setter method dành cho việc nhận dependency và truyền nó vào class thông qua việc gọi hàm Inject của Interface đó.


## III. Ví dụ

- Giả sử bạn có một ứng dụng ASP.NET Core đơn giản để quản lý danh sách sản phẩm. Bạn muốn sử dụng DI để cung cấp một service để thao tác với danh sách sản phẩm.

```csharp
// Interface cho ProductService
public interface IProductService
{
    List<Product> GetAllProducts();
}

// Implement ProductService
public class ProductService : IProductService
{
    public List<Product> GetAllProducts()
    {
        // Lấy danh sách sản phẩm từ cơ sở dữ liệu hoặc nơi khác
        // Trong ví dụ này, chúng ta sẽ trả về danh sách giả định.
        return new List<Product>
        {
            new Product { Id = 1, Name = "Sản phẩm 1" },
            new Product { Id = 2, Name = "Sản phẩm 2" },
            new Product { Id = 3, Name = "Sản phẩm 3" }
        };
    }
}
```