Middleware là phần mềm được tập hợp thành một hệ thống ứng dụng để xử lý các yêu cầu và phản hồi. Mỗi thành phần:

-   Chọn xem có chuyển yêu cầu đến thành phần tiếp theo trong pipeline hay không.

-   Có thể thực hiện công việc trước và sau thành phần tiếp theo trong pipeline.

Đường dẫn yêu cầu ASP.NET Core bao gồm một chuỗi các delegate yêu cầu, được gọi lần lượt.
Mỗi delegate có thể thực hiện các thao tác trước và sau delegate tiếp theo. Các delegate xử lý exceptions phải được gọi sớm trong quy trình để họ có thể nắm bắt được các exceptions xảy ra ở các giai đoạn sau của quy trình.

-   Nối các delegates với nhau bằng: Use
-   Tham số next đại diện cho delegate tiếp theo trong pipeline.
-   Khi một delegate không pass, nó được gọi short-circuiting the request pipeline

-   'Run' delegates không nhận next param, Run delegate đầu tiên luôn là đầu và cuối pipeline
-   Các middleware phổ biến trong .net web:

*   UseCors: Xác định cài đặt liên quan đến CORS:

    -   Cho phép hoặc từ chối các yêu cầu từ các nguồn cụ thể (domain hoặc origin)
    -   Xác định phương tức HTTP được phép từ nguồn cụ thể
    -   Xác định các tiêu đề yêu cầu được phép trong yêu cầu CORS
    -   UseCors phải đặt trước UseResponseCaching

*   UseResponseCaching: Xác định khi nào các responses có thể lưu vào cache -> lưu responses -> phản hồi requests = responses trong cache:

    -   Dựa trên HTTP cache headers
    -   Thường không có lợi với UI apps vì browsers thường cấu hình request headers là prevent caching
    -   Có thể hữu ích cho public GET và HEAD API request
    -   Chỉ caches nếu responses có status code = 200 (OK)

*   UseStaticFiles: Phục vụ các tệp tĩnh như ảnh, css, js và các tệp khác trực tiếp từ thư mục được cấu hình

*   UseAuthentication: Xác thực yêu cầu đến ứng dụng
    -   Kiểm tra danh tính người dùng
    -   Là một phần quan trọng trong bảo mật ứng dụng
*   UseAuthorization: Xác định quyền truy cập

-   Các loại middleware phổ biến trong 1 kịch bản ứng dụng:

*   1, Exception/error handling: UseDeveloperExceptionPage, UseDatabaseErrorPage, UseExceptionHandler, UseHsts
*   2, HTTPS Redirection Middleware: UseHttpsRedirection
*   3, Static File Middleware
*   4, Cookie Policy Middleware
*   5, Routing Middleware
*   6, Authentication Middleware
*   7, Authorization Middleware
*   8, Session Middleware
*   9, Endpoint Routing Middleware

---- Branch the middleware pipeline

-   Sử dụng để phân nhánh pipeline, dựa trên request path: Map
-   MapWhen phân nhánh dựa trên kết quả của hàm được định nghĩa, mọi hàm Func<HttpContext, bool> có thể được dùng để map request với một nhánh của pipeline
