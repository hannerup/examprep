using System.Net.Sockets;
using System.Text;

namespace ExampleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 7080);
                Console.WriteLine($"Connected to server");
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    string message = Console.ReadLine();

                    byte[] dataToSend = Encoding.ASCII.GetBytes(message);
                    stream.Write(dataToSend, 0, dataToSend.Length);
                    Console.WriteLine("Data sent!");

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Server response: {response}");


                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in client: {ex.Message} - ST: {ex.StackTrace}");
                throw;
            }
            
        }
    }
}
