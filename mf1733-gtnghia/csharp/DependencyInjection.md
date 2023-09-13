I. Khái niệm
 1. Dependency Inversion Principle (D trong SOLID)
    các module cùng phụ thuộc vào 1 interface không đổi.
    Ta có thể dễ dàng thay thế, sửa đổi module cấp thấp mà
    không ảnh hưởng gì tới module cấp cao
 2. Inverse Control (Dependency Inversion)
    Design pattern được tạo ra theo Dependency Inversion
    Có nhiều cách hiện thực pattern này: ServiceLocator, Event, Delegate, 
    Dependency Injection là một trong các cách đó

II.Đặc điểm Dependency injection
    Có 3 cách để tạo Dependency injection:
    1. Inject thông qua Constructor method
       when we use DI Horn to Class Car through constructor method of class Car,
       we can change code in Class Horn and don't need to change anything in Class Car
       For example, i insert in Horn property Level<int> to describe level of Horn
       and insert parameters into constructor method of class Horn.
       When I use it, I don't need to change Class Car, so I assign the Horn level when i
       declare it

    public class Horn
    {
        public int level = 0;

        public Horn(int _level)
        {
            this.level = _level;
        }
        public void Beep() => Console.WriteLine("Beep - beep - beep ...");
    }

    public class Car
    {
        Horn horn { get; set; }

        public Car(Horn _horn)
        {
            this.horn = _horn; // inject by constructor method
        }
        public void Beep()
        {
            
            horn.Beep();
        }
    }

    class DI
    {
        public static void Main(string[] args)
        {
            Horn horn = new Horn(7);
            var car = new Car(horn); // inject into class Car
            car.Beep();
        }
    }

2. Setter injection: Dependency được tiêm vào thông qua setter method
3. Interface injection: 
    Dependency sẽ cung cấp một Interface, 
    trong đó có chứa hàm có tên là Inject. Client phải triển 
    khai một Interface mà có một setter method dành cho việc nhận 
    dependency và truyền nó vào class thông qua việc gọi hàm Inject 
    của Interface đó.

III. Advantages and disadvantages
  1. Advantages
    - Giảm sự kết dính giữa các module
    - Code dễ bảo trì, dễ thay thế module
    - Giảm khối lượng khi test và viết Unit Test
    - Biểu thị rõ ràng quan hệ giữa các module (Vì các dependency đều được inject vào constructor)

  2. Disadvantage
    - Khó debug (do sử dụng interface -> khó xác định module được gọi đến)
    - Các object được khởi tạo toàn bộ ngay từ đầu, có thể làm giảm performance
    - Làm tăng độ phức tạp của code
 
IV. Dependency Injection Library of .NET 
  (Microsoft.Extensions.DependencyInjection)
  1. DI Container: ServiceCollection
    - Register services (Class)
    - ServiceProvider -> Get services
    - Register of service(3 types): 
      + Singleton:
          I getService IClassC in the first time, it will initial service IClassC
          when I call getService IClassC in anytime after that, it will get IClassC
          was initialized earlier
      + Scoped:
          Similar with Singleton but Service scope is only within inital scope
          each scope will initialize service IClassC once
      + Transient:
          Each I call getService it will initialize service IClassC once
     
    # Code example:

    interface IClassC
    {
        public void ActionC();
    }

    interface IClassB
    {
        public void ActionB();
    }

    class ClassC : IClassC
    {
        public ClassC() => Console.WriteLine("ClassC is created");
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }

    class ClassB : IClassB 
    {
        // Class B depends on Class C
        IClassC c_dependency;

        public ClassB(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB is created");
        }

        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }

    class ClassC1 : IClassC
    {
        public ClassC1() => Console.WriteLine("ClassC1 is created");
        public void ActionC()
        {
            Console.WriteLine("Action in C1");
        }
    }

    class ClassB1 : IClassB
    {
        IClassC c_dependency;
        public ClassB1(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB1 is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in B1");
            c_dependency.ActionC();
        }
    }

    class ClassA
    {
        // Class A depends on Class B
        IClassB b_dependency;

        public ClassA(IClassB classb)
        {
            b_dependency = classb;
            Console.WriteLine("ClassA is created");
        }

        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Scoped service
            // similar with Singleton but Service scope is only within inital scope
            // each scope will initialize service IClassC once
            services.AddScoped<IClassC, ClassC>();

            var provider = services.BuildServiceProvider();

            for (int i = 0; i < 5; i++)
            {
                IClassC c = provider.GetService<IClassC>();

                // watching ObjectC's Hash code for 5 iterations
                // how many times c is initialized?
                // hash code of object is unique
                Console.WriteLine(c.GetHashCode());
            }

            using (var scope = provider.CreateScope())
            {
                var provider1 = scope.ServiceProvider; 
                for (int i = 0; i < 5; i++)
                {
                    IClassC c = provider1.GetService<IClassC>();
                    Console.WriteLine(c.GetHashCode());
                }
            }

            var provider = services.BuildServiceProvider();

            ClassA a = provider.GetService<ClassA>();
            a.ActionA();

        }
    }