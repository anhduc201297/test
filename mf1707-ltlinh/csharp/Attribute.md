1. Attribute trong C# dùng để gắn metadata(dữ liệu mô tả ) (cung cấp thông tin) vào các thành phần của mã nguồn chẳng hạn như class, method, property hay cả object -> mục đích dùng để cung cấp thông tin hay điều chỉnh cách hoạt động
2. Phân loại
-Attribute gồm có loại sẵn có như [Obsolete], [Serializable], [DllImport], [Conditional]
[Obsolete]: Đánh dấu một phương thức, lớp hoặc thuộc tính đã bị lỗ thời không nên được sử dụng
[Serializable]: Đánh dấu 1 lớp có thể được serialize và deserialize thành các đối tượng, để lưu trữ hoặc truyền tải ứng dụng khác nhau
[DIIimport]: đánh dấu một phương thức tĩnh mà được triển khai bởi 1 thư viện bên ngoài
[Conditional]: Đánh dấu một phuogw thức để chỉ định điều kiện biên dịch, khi được đánh dấu, phương thức chỉ được biên dịch khi điền kiện được đưa ra
[AttributeUsage]: đánh dấu một attribute để chỉ định cách sử dụng Attribute, bao gồm cách, số lần sử dụng và các đối tượng được áp dụng 
[MethodImpl]: Đánh dấu phương thức để chỉ định cách triển khai của nó, bao gồm phương thức nội tại, đồng bộ hóa và các tùy chọn triển khai khác
-Custom Atributes dùng theo cách tùy chỉnh riêng của mỗi người qua kế thừa từ System.Attribute
3. Cách dùng
Ta sử dụng [] và đặt tên của attribute bên trong nó
Ví dụ qua thuộc tính Obsolete
[Obsolete ("Phương thức này lỗi thời, hãy  dùng phương thức Abc")]

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class MotaAttribute : Attribute // có thể đặt tên Mota thay cho MotaAttribute
    {
        // Phương thức khởi tạo
        public MotaAttribute(string v) => Description = v;

        public string Description {set; get;}
    }