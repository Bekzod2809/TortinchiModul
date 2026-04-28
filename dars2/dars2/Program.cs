//namespace dars2;

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        //Thread thread1 = new Thread(Doo);
//        //thread1.Start();
//        //Thread thread2 = new Thread(Doo2);
//        //thread2.Start();
//        //Thread thread3 = new Thread(Doo3);
//        //thread3.Start();
//        //Thread thread4 = new Thread(Doo4);
//        //thread4.Start();
//        //thread4.Join();
//        //Thread thread5 = new Thread(Doo5);
//        //thread5.Start();
//        //Thread thread = new Thread(Doo6);
//        //thread.Start();
//        Thread thread = new Thread(Doo7);

//    }
//    public static void Doo()
//    {
//        Console.WriteLine("Doo ishladi");
//        for (int i = 1; i <= 10; i++)
//        {
//            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} Raqam: {i}");
//        }
//        Console.WriteLine("Doo tugadi");
//    }
//    public static void Doo2()
//    {
//        Console.WriteLine("Doo2 ishladi");
//        for (int i = 1; i <= 20; i++)
//        {
//            Console.WriteLine($"ThreadID:{Thread.CurrentThread.ManagedThreadId} Harf: A");
//        }
//        Console.WriteLine("Doo2 tugadi");
//    }
//    public static void Doo3()
//    {
//        Console.WriteLine("Doo3 ishladi");
//        for (int i = 1; i <= 20; i++)
//        {
//            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} Harf: B");
//        }
//        Console.WriteLine("Doo3 tugadi");
//    }
//    public static void Doo4()
//    {
//        for (int i = 1; i <= 5; i++)
//        {
//            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} Son: {i}");
//        }
//    }
//    public static void Doo5()
//    {
//        for (int i = 6; i <= 10; i++)
//        {
//            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} Son: {i}");
//        }
//    }
//    public static void Doo6()
//    {
//        for (int i = 1; i <= 20; i++)
//        {
//            Console.WriteLine($"ThreadId : {Thread.CurrentThread.ManagedThreadId} Harf: A");
//        }
//    }
//    public static void Doo7()
//    {
//        for (int i = 1; i <= 10; i++)
//        {
//            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} Hello");
//        }
//    }
//}
