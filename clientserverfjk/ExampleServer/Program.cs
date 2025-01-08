using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ExampleServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TcpListener server = new TcpListener(IPAddress.Any, 7080);

            try
            {
                server.Start();
                Console.WriteLine("Server has started!");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("New client connected!");
                    Thread clientThread = new Thread(() => handleClient(client));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in server: {ex.Message} - ST: {ex.StackTrace}");
            }
        }

        static void handleClient(TcpClient client)
        {
            bool isFirstOdd = true;
            bool isFirstEven = true;
            while (true)
            {
                #region Get client request
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {dataReceived}");
                #endregion
                string stringReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                if (stringReceived.Equals("eom"))
                {
                    break;
                }
                string response;
                try
                {
                    int intReceived = Int32.Parse(stringReceived);
                    int number = int.Parse(dataReceived);
                    if (number % 2 == 0)
                    {
                        if (isFirstEven)
                        {
                            response = "lige";
                            isFirstEven = false;
                        }
                        else
                        {
                            response = "igen lige";
                        }
                    }
                    else
                    {
                        if (isFirstOdd)
                        {
                            response = "ulige";
                            isFirstOdd = false;
                        }
                        else
                        {
                            response = "igen ulige";
                        }
                    }

                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception caught in handleClient: {ex.Message} - ST: {ex.StackTrace}");
                }
            }
        }
    }
}


