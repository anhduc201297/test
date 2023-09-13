# ASP.NET Core - Middleware
## I. Giới thiệu Middleware
Một Middleware là một module code nó nhận yêu cầu gửi đến Request và trả về Response. Cụ thể trong ASP.NET Core, middlewarre có thể:
- Nhận một HTTP Request gửi đến và phát sinh ra HTTP Response để trả về
- Nhận một HTTP Request gửi đến, thi hành một số tác vụ (có thể là sửa đổi HTTP Request), sau đó chuyển đến một middleware khác.
- Nhận HTTP Response, sửa nó và chuyển đến một Middleware khác

![Imgae](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/request-delegate-pipeline.png?view=aspnetcore-7.0)

## II. Vai trò của Middleware
Middleware là cầu nối giữa tương tác của người dùng và hệ thống. Đóng vai trò trung gian giữa request/response và các xử lý logic bên trong web server, ví dụ:
- Cần xác thực người dùng để quyết định xem họ có được phép truy cập đến route hiện tại hay không.
- Yêu cầu đăng nhập
- Chuyển hướng người dùng
- Thay đổi/chuẩn hoá các tham số
- Xử lý request đầu vào và response được tạo ra,...
