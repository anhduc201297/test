

_______Transaction_______
- ACID: 
    ```
    Atomicity: Tính nguyên tử: Đảm bảo tất cả lệnh trong 1 nhóm lệnh được thực thi thành công, nếu ko thì sẽ hủy bỏ transaction
    - Thêm dữ liệu vào 2 bảng: Transaction - thêm data vào 1 trong 2 bảng lỗi -> undo all

    Consistency: Tính nhất quán: Đảm bảo dữ liệu được thay đổi chính xác khi transaction thành công
    - Ràng buộc: Constrains

    Isolation: Đảm bảo các transaction luôn độc lập và minh bạch với nhau
    - Tương tranh dữ liệu

    - Khoá: Khoá bảng, Khoá Service: Distributed Lock
    - ConcurrencyStamp: Dấu thời gian

    Durability
    - bền vững: Backup, đảm bảo dữ liệu trong DB luôn được persist trước mọi thay đổi
    ```
- Xử lí trong Transaction:

    ```
    COMMIT: Lưu các thay đổi

    ROLLBACK: trở về trạng thái trước

    SAVEPOINT: tạo các point bên trong cách nhóm Transaction để ROLLBACK

    SET TRANSACTION: đặt tên cho 1 Transaction

    Các lệnh khác: INSERT, UPDATE, DELETE
    ```

**Cú pháp sử dụng lệnh COMMIT SQL:**
    
    COMMIT;
 
**Lệnh ROLLBACK**

    ROLLBACK;

**Lệnh SAVEPOINT**

    SAVEPOINT TEN_SAVEPOINT;

**Lệnh RELEASE SAVEPOINT**

    RELEASE SAVEPOINT TEN_SAVEPOINT;

**Lệnh SET TRANSACTION**

    SET TRANSACTION [ READ WRITE | READ ONLY ];



_______________DAPPER_______________

JS: Promise, async, await
C#: Task, async, await: Thêm Async vào cuối hàm.

Dapper ExecuteScalar: 
- Thường dùng cho những SQL queries trả về 1 dòng 1 cột:
VD:
    ```
    using (var connection = new SqlConnection(connectionString))
    {
        var sql = "SELECT COUNT(*) FROM Products";
        var count = connection.ExecuteScalar(sql);
    	
        Console.WriteLine($"Total products: {count}");
    }
    ```
- Hoặc có thể thêm dữ liệu trả về với:  `var count = connection.ExecuteScalar<int>(sql);`

QueryFirstOrDefault: mặc định là null.

QueryFirst: Nếu ko có bản ghi -> throw exception.

QuerySingle: 0: exception, 1: Ok, >2: exception.

QuerySingle: 0: null, 1: OK, >2: exception.

Query.

Execute: thêm, cập nhật, xóa (thay đổi dữ liệu)

Querying Multiple Results 
- Cho phép chọn nhiều kết quả từ DB query, giảm đc những round trips ko cần thiết.
- Cách map kết quả: Read<T>, ReadFirst<T>, ReadSingle<T>...
- VD:
    ```
    string sql = @"
    SELECT * FROM Invoices WHERE InvoiceID = @InvoiceID;
    SELECT * FROM InvoiceItems WHERE InvoiceID = @InvoiceID;
    ";
    using (var connection = new SqlConnection(connectionString))
    {
        using (var multi = connection.QueryMultiple(sql, new {InvoiceID = 1}))
        {
            var invoice = multi.First<Invoice>();
            var invoiceItems = multi.Read<InvoiceItem>().ToList();
        }
    }
    ```

 Relationship
- Dapper cung cấp khả năng query trên những object lồng nhau mà ko cần viết những câu SQL riêng cho từng object
- có thể execute stored procedures easily, map results to obj hoặc dynamic obj, bulk crud operations.

* Dapper SplitOn
- cho phép map 1 row tới nhiều obj
VD:     
    ```
    using(var connection = new SqlConnection(connectionString))
    {
        var sql = @"SELECT ProductID, ProductName, p.CategoryID, CategoryName
                    FROM Products p 
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID";
    				
        var products = await connection.QueryAsync<Product, Category, Product>(sql, (product, category) => {
            product.Category = category;
            return product;
        }, 
        splitOn: "CategoryId" );
    	
        products.ToList().ForEach(product => Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category.CategoryName}"));
    	
        Console.ReadLine();
    }
    ```
- 'splitOn' dùng để split data trên cột CategoryId. Mọi data từ cột trước CategoryId sẽ map với Product (First param), còn lại map với Category (Second input param), cuối cùng trả về giá trị là Product (TReturn param - param ở vị trí cuối)

One-To-Many
- Ta có bảng Product và Category có quan hệ được biểu diễn trong class như sau:
    ```
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        ...
        public Category Category { get; set; }
    }
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        ...
        public ICollection<Product> Products { get; set; }
    }
    ```

- Với SQL query thông thường, ta chỉ lấy được nhiều dòng với các product, category có thể trùng nhau, nhưng với Dapper:
    ```
    using(var connection = new SqlConnection(connectionString))
    {
        var sql = @"SELECT ProductID, ProductName, p.CategoryID, CategoryName
                    FROM Products p 
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID";
    				
        var products = await connection.QueryAsync<Product, Category, Product>(sql, (product, category) => {
            product.Category = category;
            return product;
        }, 
        splitOn: "CategoryID" );
    	
        products.ToList().ForEach(product => Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category.CategoryName}"));
    	
        Console.ReadLine();
    }
    ```
- Multi-mapping được thực thi như một Func generic delegate.
- Func<TFirst, TSecond, ..., TReturn> map 
- Trong ví dụ trên, input params là Product, Category, return param là Product
- Dapper maps data với first param nếu chỉ có 1 param được cung cấp

 Many-To-Many
- 2 lớp: Tag và Post
   
    ```
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public List<Post> Posts { get; set; }
    }
    public class Post
    {
        public int PostId { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public List<Tag> Tags { get; set; } 
    }
    
    

- Query: sử dụng multi-mapping như ví dụ trước, nhưng cần sử dụng 1 grouping func để combine những duplicate posts và tags
    ```
    using(var connection = new SqlConnection(connectionString))
    {
        var sql = @"SELECT p.PostId, Headline, t.TagId, TagName
                    FROM Posts p 
                    INNER JOIN PostTags pt ON pt.PostId = p.PostId
                    INNER JOIN Tags t ON t.TagId = pt.TagId";
    				
        var posts = await connection.QueryAsync<Post, Tag, Post>(sql, (post, tag) => {
            post.Tags.Add(tag);
            return post;
        }, splitOn: "TagId");
    	
        var result = posts.GroupBy(p => p.PostId).Select(g =>
        {
            var groupedPost = g.First();
            groupedPost.Tags = g.Select(p => p.Tags.Single()).ToList();
            return groupedPost;
        });
    	
        foreach(var post in result)
        {
            Console.Write($"{post.Headline}: ");
    		
            foreach(var tag in post.Tags)
            {
                Console.Write($" {tag.TagName} ");
            }
    		
            Console.Write(Environment.NewLine);
        }
    }
    ```

- Sử dụng truy vấn để lấy cả Authors của bài posts
    ```
    using(var connection = new SqlConnection(connectionString))
    {
        var sql = @"SELECT p.PostId, Headline, FirstName, LastName, t.TagId, TagName
                    FROM Posts p 
                    INNER JOIN Authors a ON p.AuthorId = a.AuthorId
                    INNER JOIN PostTags pt ON pt.PostId = p.PostId
                    INNER JOIN Tags t ON t.TagId = pt.TagId";
    				
        var posts = await connection.QueryAsync<Post, Author, Tag, Post>(sql, (post, author, tag) => {
            post.Author = author;
            post.Tags.Add(tag);
            return post;
        }, splitOn: "FirstName, TagId");
    	
        var result = posts.GroupBy(p => p.PostId).Select(g =>
        {
            var groupedPost = g.First();
            groupedPost.Tags = g.Select(p => p.Tags.Single()).ToList();
            return groupedPost;
        });
    	
        foreach(var post in result)
        {
            Console.Write($"{post.Headline}: ");
    		
            foreach(var tag in post.Tags)
            {
                Console.Write($" {tag.TagName} ");
            }
    		
            Console.Write($" by {post.Author.FirstName} {post.Author.LastName} {Environment.NewLine}");
        }
    }
    ```