I. Concept
	- Pipeline: Trong ứng dụng ASP.NET Core, các middlware kết nối lại với nhau
	thành một xích, middleware đầu tiên nhận HTTP Request, xử lý nó và có thể
	chuyển cho middleware tiếp theo hoặc trả về ngay HTTP Response. Chuỗi các
	middleware theo thứ tự như vậy gọi là pipeline.
	
	- Middleware: như là các dịch vụ nhỏ, đăng ký vào ứng dụng bằng cách sử
	dụng đối tượng IApplicationBuilder, sau đó ứng dụng căn sẽ xây dựng lên
	các pipeline (luồng xử lý) cho các truy vấn gửi đến.

II. Features
	1. Nguyên lý hoạt động
		- Đầu tiên, HTTP Request đến. Kestrel web server nhặt lấy request và tạo
		  một HttpContext và gán nó vào Middleware đầu tiên trong request pipeline.
		
		- Middleware đầu tiên sẽ nhận request, xử lý và gán nó cho middleware tiếp
		  theo. Quá trình này tiếp diễn cho đến khi đi đến middleware cuối cùng. Tùy
		  thuộc bạn muốn pipeline của bạn có bao nhiêu middleware.
		
		- Middleware cuối cùng sẽ trả request ngược lại cho middleware trước đó,
		  và sẽ ngắt quá trình trong request pipeline.
		
		- Mỗi Middleware trong pipeline sẽ tuần tự có cơ hội thứ hai để kiểm tra
		  lại request và điểm chỉnh response trước khi được trả lại.
		
		- Cuối cùng, response sẽ đến Kestrel nó sẽ trả response về cho client. Bất
		  cứ middleware nào trong request pipeline đều có thể ngắt request pipeline
		  tại chỗ đó với chỉ một bước đơn giản là không gán request đó đi tiếp.
	
	2. Vai trò
		- Xử Lý Yêu Cầu (Request Processing): Middleware có thể thực hiện xử lý trước 
		  khi yêu cầu HTTP của client đến được tới các controller hoặc endpoint chính của
		  ứng dụng. Ví dụ, bạn có thể sử dụng middleware để xác thực người dùng, kiểm tra 
	  	  quyền truy cập, thực hiện ghi log, và thậm chí chuyển hướng yêu cầu đến một endpoint khác.
		
		- Chuyển Tiếp (Forwarding): Middleware có thể chuyển tiếp yêu cầu tới các
		  thành phần khác của ứng dụng. Điều này cho phép bạn xây dựng các ứng dụng
		  có cấu trúc lớp hoặc có nhiều ứng dụng con trong một ứng dụng lớn hơn.
		
		- Xử Lý Kết Quả (Response Processing): Sau khi controller hoặc endpoint đã
		  xử lý yêu cầu, middleware có thể thực hiện xử lý trước khi kết quả được trả về cho client.
		
		  Ví dụ, bạn có thể sử dụng middleware để thêm header vào phản hồi, nén dữ liệu,
		  hoặc thậm chí chuyển đổi kết quả thành một định dạng khác.
		- Logging và Giám Sát (Logging and Monitoring): Middleware thường được sử
		  dụng để ghi log các hoạt động của ứng dụng, cho phép theo dõi và gỡ lỗi dễ dàng.
		  Nó cũng có thể được sử dụng để thu thập dữ liệu về hiệu suất và sử dụng ứng dụng.
		
		- Quản lý Phiên (Session Management): Middleware có thể được sử dụng để
		  quản lý phiên làm việc của người dùng, cho phép lưu trữ và truy xuất
		  thông tin phiên và trạng thái liên quan đến người dùng.
		
		- Bảo Mật (Security): Middleware thường được sử dụng để thực hiện các nhiệm vụ
		  bảo mật như xác thực người dùng, bảo vệ khỏi tấn công CSRF
		  (Cross-Site Request Forgery), và kiểm tra quyền truy cập.
		
		- Cache và Tối Ưu Hóa (Caching and Optimization): Middleware có thể được
		  sử dụng để tạo và quản lý bộ nhớ cache, giúp cải thiện hiệu suất của
		  ứng dụng bằng cách lưu trữ và tái sử dụng dữ liệu đã xử lý.