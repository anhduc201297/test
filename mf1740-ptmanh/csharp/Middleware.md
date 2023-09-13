- Middleware: là thành phần đóng vai trò tác động vào request pipeline và kết nối lại với nhau thành một xích, middleware đầu tiên nhận HTTP Request, xử lý nó và có thể chuyển cho middleware tiếp theo hoặc trả về ngay HTTP Response. Chuỗi các middleware tạo thành pipeline. Nó được sử dụng để chặn các yêu cầu và phản hồi và thực hiện các tác vụ như xác thực, ủy quyền, ghi nhật ký và bộ nhớ cache.
- Một số lợi ích của việc sử dụng middleware trong C#:
    + Hiệu suất được cải thiện: Môi trường trung gian có thể được sử dụng để lưu trữ dữ liệu được truy cập thường xuyên, điều này có thể cải thiện hiệu suất của các ứng dụng web.
    + Tăng cường bảo mật: Môi trường trung gian có thể được sử dụng để xác thực người dùng và kiểm soát quyền truy cập vào tài nguyên, điều này có thể cải thiện bảo mật của các ứng dụng web.
    + Khả năng mở rộng được nâng cao: Môi trường trung gian có thể được sử dụng để phân phối các yêu cầu trên nhiều máy chủ, điều này có thể cải thiện khả năng mở rộng của các ứng dụng web.
    + Giảm thiểu phức tạp: Môi trường trung gian có thể được sử dụng để đóng gói các tác vụ chung, điều này có thể giảm thiểu sự phức tạp của các ứng dụng web.
