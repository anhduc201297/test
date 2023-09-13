## Concept of Attribute

Một thuộc tính chú thích (Attribute) tác động vào một thành phần nào đó của chương trình (lớp, phương thức, thuộc tính) nó là một phần của siêu dữ liệu (metadata - loại dữ liệu cung cấp thêm thông tin về đối tượng nào đó). Attribute giúp thêm thông tin vào lớp, phương thức, thuộc tính những đoạn code mở rộng.

Các thuộc tính chú thích có thể được truy xuất tra cứu ở thời điểm thực thi bằng kỹ thuật gọi là reflection, truy xuất ngược từ đối tượng biết được nguồn gốc mà đối tượng đó sinh ra (lớp).

Trong C# có định nghĩa sẵn vô số các Attribute, để sử dụng nó chỉ cần viết tên Attribute trong dấu [] trước đối tượng áp dụng như lớp, phương thức, thuộc tính lớp (có tham số như hàm, nếu Attribute đó yêu cầu).

```csharp
[AttributeName(param1, param2 ...)]
```

Example:

```csharp
[Serializable]
public class SampleClass
{
    // Objects of this type can be serialized.
}
```

## Create custom attributes

Để tạo custom attributes, chỉ cần tạo một lớp kế thừa từ System.Attribute.

```csharp
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public class AuthorAttribute : System.Attribute
{
    private string Name;
    public double Version;

    public AuthorAttribute(string name)
    {
        Name = name;
        Version = 1.0;
    }
}
```

Trong định nghĩa trên, [AttributeUsage( ... )] là để thiết lập thuộc tính cho Author, cho biết thuộc tính này áp dụng được cho những thành phần nào

- AttributeTargets.Property: áp dụng được cho thuộc tính của lớp
- AttributeTargets.Class: áp dụng được cho lớp
- AttributeTargets.Method: áp dụng được cho phương thức

Áp dụng Attribute Author

```csharp
[Author("P. Ackerman", Version = 1.1)]
class SampleClass
{
    // P. Ackerman's code goes here...
}
```

## Access attributes using reflection

Thực tế là khi định nghĩa các thuộc tính tùy chỉnh và sử dụng chúng trong code của mình sẽ có rất ít giá trị nếu không có cách nào đó để truy xuất thông tin và xử lý nó. Bằng cách sử dụng reflection, bạn có thể truy xuất thông tin đã được định nghĩa bằng custom attributes. Phương thức sử dụng là GetCustomAttributes, trả về một mảng các thuộc tính trong thời gian chạy.

Ví dụ về truy xuất thông tin 1 custom attribute thông qua reflection

```csharp
// Multiuse attribute.
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = true)]
public class AuthorAttribute : System.Attribute
{
    string Name;
    public double Version;

    public AuthorAttribute(string name)
    {
        Name = name;

        // Default value.
        Version = 1.0;
    }

    public string GetName() => Name;
}

// Class with the Author attribute.
[Author("P. Ackerman")]
public class FirstClass
{
    // ...
}

// Class with multiple Author attributes.
[Author("P. Ackerman"), Author("R. Koch", Version = 2.0)]
public class SecondClass
{
    // ...
}

class TestAuthorAttribute
{
    public static void Test()
    {
        PrintAuthorInfo(typeof(FirstClass));
        PrintAuthorInfo(typeof(SecondClass));
    }

    private static void PrintAuthorInfo(System.Type t)
    {
        System.Console.WriteLine($"Author information for {t}");

        // Using reflection.
        System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // Reflection.

        // Displaying output.
        foreach (System.Attribute attr in attrs)
        {
            if (attr is AuthorAttribute a)
            {
                System.Console.WriteLine($"   {a.GetName()}, version {a.Version:f}");
            }
        }
    }
}
/* Output:
    Author information for FirstClass
       P. Ackerman, version 1.00
    Author information for SecondClass
       R. Koch, version 2.00
       P. Ackerman, version 1.00
*/
```
