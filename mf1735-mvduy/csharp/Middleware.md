# Middleware

## Định nghĩa

- Middleware trong ASP.NET là những "bộ lọc" hoặc "xử lý trung gian" mà bạn có thể thêm vào ứng dụng web của mình để thực hiện các công việc xử lý yêu cầu HTTP trước khi chúng đến được tới mã ứng dụng chính hoặc sau khi mã ứng dụng đã xử lý xong yêu cầu đó.
- Tóm lại nó là giai đoạn trung gian nằm nằm giữa HTTP request và HTTP response

## Một vài ứng dụng của Middleware
- Middleware xác thực có thể kiểm tra đăng nhập của người dùng trước khi cho phép họ truy cập trang web.
- Middleware ghi nhật ký có thể ghi lại mọi yêu cầu và phản hồi để bạn có thể theo dõi hoạt động của ứng dụng.
- Middleware định tuyến có thể định hướng yêu cầu đến các đường dẫn khác nhau dựa trên URL.
- Middleware tối ưu hóa có thể nén dữ liệu trước khi gửi nó đến trình duyệt để tăng tốc độ trang web.