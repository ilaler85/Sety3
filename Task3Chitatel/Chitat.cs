using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Task3Chitatel
{
    public class Chitatel
    {
        Random random = new Random();
        int port = 8888;
        string address = "127.0.0.1";

        public Chitatel()
        {
            Thread thread1 = new Thread(read_);
            thread1.Start();
            
            Thread thread2 = new Thread(read_);
            thread2.Start();
        }

        private void read_()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Thread.Sleep(random.Next(1,5000));
                    byte[] data = Encoding.Unicode.GetBytes("Пришли строку");
                    Console.WriteLine("Пришли строку");
                    stream.Write(data, 0, data.Length);
                    

                    data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    Console.WriteLine(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }

    class Chitat
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Читатели");
            Chitatel chitatel = new Chitatel();
            Console.ReadKey();
        }
    }
}
