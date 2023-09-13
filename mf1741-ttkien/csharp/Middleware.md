* Middleware trong ASP.NET Core
1. Request Pipeline
   - Request Pipeline là cơ chế bắt đầu khi các request bắt đầu được xử lý với một đối tượng Request đầu vào và kết thúc với đầu ra là một response. Pipeline chỉ ra cách mà ứng dụng phản hồi với HTTP Request. Request đến từ trình duyệt đi qua pipeline và quay trở lại khi xử lý xong để trả về client. Các thành phần đơn lẻ tạo nên pipeline này được gọi là middleware.
2. Middleware
   - Middleware là thành phần của phần mềm đóng vai trò tác động vào request pipeline (luồng request) để xử lý chúng và tạo ra response phản hồi lại client. Mỗi một tiến trình middleware thao tác với các request nhận được từ middleware trước nó. Nó cũng có thể quyết định gọi middleware tiếp theo trong pipeline hoặc trả về response cho middleware ngay trước nó (ngắt pipeline).
3. Cách chúng làm việc
   - Đầu tiên, HTTP Request đến (trực tiếp hoặc qua External web server) ứng dụng. Kestrel web server nhặt lấy request và tạo một HttpContext và gán nó vào Middleware đầu tiên trong request pipeline.
   - Middleware đầu tiên sẽ nhận request, xử lý và gán nó cho middleware tiếp theo. Quá trình này tiếp diễn cho đến khi đi đến middleware cuối cùng.
   - Middleware cuối cùng sẽ trả request ngược lại cho middleware trước đó, và sẽ ngắt quá trình trong request pipeline.
   - Mỗi Middleware trong pipeline sẽ tuần tự có cơ hội thứ hai để kiểm tra lại request và điểm chỉnh response trước khi được trả lại.
   - Cuối cùng, response sẽ đến Kestrel nó sẽ trả response về cho client. Bất cứ middleware nào trong request pipeline đều có thể ngắt request pipeline tại chỗ đó với chỉ một bước đơn giản là không gán request đó đi tiếp.
4. Configure Middleware
   - Chúng ta có thể cấu hình Middleware trong Configure method của lớp startup bằng cách sử dụng IApplicationBuilder instance. Ví dụ sau thêm một phần mềm trung gian bằng phương thức Run trả về chuỗi "Hello World!" trên mỗi yêu cầu.

   public class Startup
   {
       public Startup()
       {
       } 
       public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
       {
           //configure middleware using IApplicationBuilder here..
            
           app.Run(async (context) =>
           {              
               await context.Response.WriteAsync("Hello World!");  
           });

           // other code removed for clarity.. 
       }
   }

   Run() là một phương thức mở rộng IApplicationBuilder bổ sung phần mềm trung gian đầu cuối vào đường dẫn yêu cầu của ứng dụng. Phần mềm trung gian được định cấu hình ở trên trả về phản hồi bằng chuỗi "Xin chào thế giới!" cho mỗi yêu cầu.
5. Configure Multiple Middleware
   - Để cấu hình nhiều Middleware, hãy sử dụng phương thức Use(). Nó tương tự như phương thức Run() ngoại trừ việc nó bao gồm tham số tiếp theo để gọi phần mềm trung gian tiếp theo trong chuỗi.

   public void Configure(IApplicationBuilder app, IHostingEnvironment env)
   {
       app.Use(async (context, next) =>
       {
           await context.Response.WriteAsync("Hello World From 1st Middleware!");

           await next();
       });

       app.Run(async (context) =>
       {
           await context.Response.WriteAsync("Hello World From 2nd Middleware"); 
       });
   }

   Ví dụ trên sẽ hiển thị "Hello World From 1st Middleware!Hello World From 2nd Middleware!" trong trình duyệt.