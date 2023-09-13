# Middleware

## Khái niệm

**Request pipeline:** Là một cơ chế xử lý một request đầu vào và kết thúc với đầu ra là một response. Pipeline chỉ ra cách mà ứng dụng phản hồi với HTTP Request.

**Middleware:** Request đến từ trình duyệt sẽ đi qua Kestrel web server rồi qua pipeline và quay trở lại khi xử lý xong để trả về client. Các thành phần đơn lẻ tạo nên pipeline này được gọi là middleware.

**Pipeline:** Trong ứng dụng ASP.NET Core, các middlware kết nối lại với nhau thành một xích, middleware đầu tiên nhận HTTP Request, xử lý nó và có thể chuyển cho middleware tiếp theo hoặc trả về ngay HTTP Response. Chuỗi các middleware theo thứ tự như vậy gọi là pipeline.

## Cách thức hoạt động

- Đầu tiên, HTTP Request đến. Kestrel web server nhặt lấy request và tạo một HttpContext và gán nó vào Middleware đầu tiên trong request pipeline.

- Middleware đầu tiên sẽ nhận request, xử lý và gán nó cho middleware tiếp theo. Quá trình này tiếp diễn cho đến khi đi đến middleware cuối cùng. Tùy thuộc bạn muốn pipeline của bạn có bao nhiêu middleware.

- Middleware cuối cùng sẽ trả request ngược lại cho middleware trước đó, và sẽ ngắt quá trình trong request pipeline.

- Mỗi Middleware trong pipeline sẽ tuần tự có cơ hội thứ hai để kiểm tra lại request và điểm chỉnh response trước khi được trả lại.

- Cuối cùng, response sẽ đến Kestrel nó sẽ trả response về cho client. Bất cứ middleware nào trong request pipeline đều có thể ngắt request pipeline tại chỗ đó với chỉ một bước đơn giản là không gán request đó đi tiếp. 

## Vai trò của Middleware

Middleware là cầu nối giữa tương tác của người dùng và hệ thống. Đóng vai trò trung gian giữa request/response và các xử lý logic bên trong web server, ví dụ:

- Authentication Middleware: Cho phép bạn thực hiện xác thực người dùng trước khi yêu cầu được chuyển đến controller hoặc endpoint cụ thể. ASP.NET Core cung cấp middleware để hỗ trợ các phương thức xác thực như Cookie, JWT,...

- Logging Middleware: Ghi lại thông tin quan trọng về các yêu cầu và phản hồi trong ứng dụng. Điều này giúp theo dõi và gỡ lỗi ứng dụng dễ dàng hơn.

- Compression Middleware: Giảm kích thước dữ liệu gửi giữa máy chủ và máy khách, giúp tối ưu hóa hiệu suất ứng dụng.

- Caching Middleware: Middleware caching có thể lưu trữ phiên bản đã xử lý của một trang hoặc tài nguyên để tăng tốc quá trình trả về chúng khi có yêu cầu tương tự.

- Error Handling Middleware: Bắt và xử lý các ngoại lệ xảy ra trong quá trình xử lý yêu cầu. 

- Routing Middleware: Xác định điểm cuối nào sẽ xử lý yêu cầu dựa trên URL của nó. Điều này giúp chuyển hướng yêu cầu đến controller và action tương ứng. 