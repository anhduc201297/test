- Dependency Injection (DI): là mẫu thiết kế để đạt được đảo ngược điều khiển( IoC), khiến cho các thành phần không phụ thuộc trực tiếp vào nhau mà phụ thuộc vào các abstract hoặc interface. Từ đó khiến việc bảo trì và kiểm tra dễ dàng hơn.
- Ví dụ:
    public class Horn {
    public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
    }
    public class Car {
        // horn là một Dependency của Car
        Horn horn;
        // dependency Horn được đưa vào Car qua hàm khởi tạo
        public Car(Horn horn) => this.horn = horn;

        public void Beep () {
            // Sử dụng Dependency đã được Inject
            horn.Beep ();
        }
    }
    Khi sử dụng:
    Horn horn = new Horn();
    var car = new Car(horn); // horn inject vào car
    car.Beep(); // Beep - beep - beep ...

- Giải thích: ở ví dụ trên sử dụng inject bằng hàm khởi tạo( constructors), bằng cách inject này khi ta có sử đổi lại class Horn thì cũng không ảnh hưởng đến hoạt động của class Car mà chỉ cần sửa lại đối tượng horn.
  VD:
    public class Horn {
        int level; // thêm độ lớn còi xe
        public Horn (int level) => this.level = level; // thêm khởi tạo level

            public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
        }
    Horn horn = new Horn(10);

    Car car = new Car(horn); // horn inject vào car
    car.Beep(); // Beep - beep - beep ...

- Hiện có rất nhiều thư viện hỗ trợ DI( chủ yếu là inject bằng hàm khởi tạo), phổ biến như:
    + Ninject
    + Autofac
    + Castle Windsor
    + Unity
- Lợi ích của sử dụng DI:
    + Kết nối lỏng: các thành phần không phụ thuộc trực tiếp vào nhau. Dễ kiểm tra vào bảo trì hơn.
    + Khả năng kiểm tra: Dễ dàng tạo các phụ thuộc( dependency) giả cho unit tests.
    + Tái sử dụng: Vì các thành phần không phụ thuộc trực tiếp vào nhau nên có thể sử dụng lại ở nhiều trường hợp khác nhau.
    + Khả năng mở rộng: Dễ dàng mở rộng bằng việc thêm các thuộc tính hoặc các phụ thuộc mới.