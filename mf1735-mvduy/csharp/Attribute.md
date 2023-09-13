# Attribute

## I. Định nghĩa

- Attribute là một loại thẻ có thể được thêm vào các thành phần khác nhau trong mã nguồn C# để cung cấp thông tin mô tả bổ sung về các thành phần đó.
- Attribute giúp bạn thêm các thông tin phụ trợ cho các lớp, phương thức, thuộc tính, hoặc trường trong mã nguồn của bạn.
- Attribute được dùng bằng cách sử dụng dấu [] và được đặt trực tiếp trước thành phần mà bạn muốn.

## II. Cách tạo một Attribute

- Bạn có thể tạo các attribute tùy chỉnh bằng cách định nghĩa lớp và thừa kế từ lớp Attribute. Sau đó, bạn có thể sử dụng attribute tùy chỉnh này trên các thành phần của mã nguồn của bạn.

```csharp
public class MyCustomAttribute : Attribute
{
    public string Description { get; }

    public MyCustomAttribute(string description)
    {
        Description = description;
    }
}

/// Sử dụng
[MyCustom("This is a custom attribute.")]
public class MyClass
{
    // ...
}
```