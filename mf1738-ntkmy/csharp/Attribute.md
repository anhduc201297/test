# Attribute trong C#

## Tổng quan

**Attribute (Thuộc tính)** trong C# là những đoạn mã được gắn vào các phần tử trong mã nguồn (ví dụ: lớp, phương thức, biến, ...) được sử dụng để cung cấp thông tin bổ sung (metadata) cho các phần tử trong mã nguồn như: class, method, property,..

Trong C# có định nghĩa sẵn vô số các Attribute, để sử dụng nó bạn chỉ cần viết tên Attribute trong dấu [] trước đối tượng áp dụng như lớp, phương thức, thuộc tính lớp (có tham số như hàm, nếu Attribute đó yêu cầu).

```` [AttributeName(param1, param2 ...)] ````

## Các loại Attribute trong C# 

### Một số attribute tích hợp sẵn trong C#:

**[Obsolete]:** Đánh dấu một phương thức, lớp hoặc thuộc tính đã bị lỗi thời và không nên được sử dụng trong mã nguồn mới.

```csharp
[Obsolete("Phương thức này lỗi thời, hãy dùng phương thức khác thay thế")]
public void DeprecatedMethod()
{

}
```

**[Serializable]:** Đánh dấu một lớp có thể được serialize và deserialize thành các đối tượng, để lưu trữ hoặc truyền tải giữa các ứng dụng khác nhau.

```csharp
[Serializable]
public class SerializableData
{
    public int Value { get; set; }
    public string Text { get; set; }
}
```

**[DllImport]:** Đánh dấu một phương thức tĩnh mà được triển khai bởi một thư viện động bên ngoài.

```csharp
using System.Runtime.InteropServices;

public static class ExternalLibrary
{
    [DllImport("external.dll")]
    public static extern void SomeMethod();
}
```

**[Conditional]:** Đánh dấu một phương thức để chỉ định điều kiện biên dịch, khi được đánh dấu, phương thức chỉ được biên dịch khi điều kiện được đưa ra.

```csharp
public class Example
{
    [Conditional("DEBUG")]
    public void DebugMethod()
    {
  
    }
}
```

**[AttributeUsage]:** Đánh dấu một attribute để chỉ định cách sử dụng attribute, bao gồm cách sử dụng, số lần sử dụng, và các đối tượng được áp dụng attribute.

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class MyCustomAttribute : Attribute
{
 
}
```

**[MethodImpl]:** Đánh dấu một phương thức để chỉ định cách thức triển khai của nó, bao gồm phương thức nội tại, đồng bộ hóa và các tùy chọn triển khai khác.

```csharp
using System.Runtime.CompilerServices;

public class Example
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void SynchronizedMethod()
    {
  
    }
}
```

### Attribute tự tạo:

Thực hiện các bước sau: 

1. Tạo một lớp mới và kế thừa từ lớp System.Attribute

2. Định nghĩa các thuộc tính và/hoặc phương thức trong lớp attribute của bạn để lưu trữ thông tin mà bạn muốn đính kèm vào các phần tử trong mã nguồn.

3. Sử dụng attribute tùy chỉnh bằng cách gắn nó vào các phần tử trong mã nguồn, chẳng hạn như lớp, phương thức hoặc thuộc tính.

Trong ví dụ này: Định nghĩa lớp MyCustomAttribute, kế thừa từ System.Attribute và thêm một thuộc tính Description để lưu trữ mô tả của attribute.

```csharp
using System;

// Bước 1: Tạo một lớp attribute tùy chỉnh và kế thừa từ System.Attribute
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MyCustomAttribute : Attribute
{
    // Bước 2: Định nghĩa các thuộc tính và/hoặc phương thức cho attribute
    public string Description { get; set; }

    public MyCustomAttribute(string description)
    {
        Description = description;
    }
}

// Bước 3: Sử dụng attribute tùy chỉnh trên lớp và phương thức
[MyCustom("This is a custom attribute.")]
public class MyClass
{
    [MyCustom("This method has a custom attribute.")]
    public void MyMethod()
    {
        // ...
    }
}
```

## Data Annotation/Attribute 

Các Data Annotation/Attribute trong C# định nghĩa trong namespace System.ComponentModel.DataAnnotations, có các loại gồm:

- Các Attribute để kiểm tra dữ liệu (Validation Attribute)
- Các Attribute hiện thị (Display Attribute), điều khiển dữ liệu trong lớp hiện thị thế nào trong UI
- Modelling Attribute

**Ví dụ:**

**[Required]:** Xác định rằng một trường không được để trống.

**[StringLength]:** Xác định độ dài tối đa và tối thiểu của một chuỗi.

**[Range]:** Xác định một khoảng giá trị.

**[EmailAddress]:** Xác định rằng một chuỗi phải là một địa chỉ email hợp lệ.

**[Compare]:** So sánh hai trường dữ liệu trong một lớp.

**[DataType]:** Xác định kiểu dữ liệu của một trường, chẳng hạn như kiểu ngày tháng, số nguyên, số thực, v.v.

**[Display]:** Định nghĩa tên hiển thị tùy chỉnh cho một trường.

**[Key]:** Xác định trường là khóa chính trong một đối tượng được ánh xạ vào cơ sở dữ liệu

## Attribute targets

Để xác định rõ mục tiêu của thuộc tính, sử dụng cú pháp:

````[target : attribute-list]````

# References

- Tự học IT(tuhocict.com)
- [ChatGPT](https://chat.openai.com/)