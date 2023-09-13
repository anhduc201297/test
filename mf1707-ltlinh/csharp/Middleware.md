Request đến từ trình duyệt sẽ đi qua Kestrel web server rồi qua pipeline và quay trở lại khi xử lý xong để trả về client. Các thành phần đơn lẻ tạo nên pipeline này được gọi là middleware.
Middleware trong c# thương được yêu cầu để xử lý các yêu cầu HTTP qua một số chức năng như xác thực, phân quyền, 
nén dữ liệu, kiểm tra nhật kí, hay xử lý CORS

-Cách thức hoạt động của middleware
1. Middleware đầu tiên sẽ nhận request, xử lý và gán nó cho middleware tiếp theo
2. Middleware cuối cùng sẽ trả request ngược lại cho middleware trước đó,

Một số trường hợp
1. Middleware nhận 1 HTTP Request gửi đến và phát sinh ra HTTP Response để trả về:
- Middleware này thường được gọi là "terminal middleware" hoặc "endpoint middleware".
- Nhiệm vụ chính của nó là xử lý yêu cầu và tạo ra phản hồi cho trình duyệt hoặc client.
- Đây là nơi mà bạn xây dựng logic chính của ứng dụng và thường trả về nội dung HTML, JSON, hoặc bất kỳ dạng dữ liệu nào khác cho client.
Ví dụ Middleware cuối cùng trong pipline có nhiệm vụ trả về tất cả các cuốn sách dạng json
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/books", async context =>
    {
        // Xử lý logic để lấy danh sách sách từ cơ sở dữ liệu
        var books = await _bookRepository.GetAllBooks();

        // Trả về danh sách sách dưới dạng JSON
        await JsonSerializer.SerializeAsync(context.Response.Body, books);
    });
});

2. Nhận một HTTP Request gửi đến, thi hành một số tác vụ (có thể là sửa đổi HTTP Request), sau đó chuyển đến một middleware khác:
- Middleware trong trường hợp này có thể thực hiện một số công việc như xác thực, kiểm tra quyền truy cập, ghi nhật ký, sửa đổi yêu cầu HTTP, thêm thông tin vào yêu cầu, v.v.
- Sau khi thực hiện các tác vụ, middleware này có thể chuyển yêu cầu đến middleware tiếp theo bằng cách gọi _next(context) trong ASP.NET Core hoặc một cơ chế tương tự trong các framework khác.
Ví dụ trong đăng nhập 
Nếu người dùng chưa đăng nhập, middleware này có thể chuyển hướng họ đến trang đăng nhập hoặc trả về một thông báo lỗi.

3.Nhận HTTP Response, sửa nó và chuyển đến một Middleware khác:
Middleware ở đây có thể sửa đổi phản hồi HTTP trước khi nó được trả về client.
Ví dụ, bạn có thể thêm tiêu đề (header) vào phản hồi, nén dữ liệu trước khi gửi đi, hoặc thậm chí thay đổi nội dung phản hồi.
Middleware này cũng có thể chuyển phản hồi đến middleware tiếp theo trong pipeline.

4. Vai trò của middleware
- là cấu nối giữa tương tác của người dùng và hệ thống, đón vai trò trung gian giữa request/ response và các xử lý login bên trong web server như yêu cầu đăng nhập, chuyển hướng người dùng, xác thực truy cập,...
