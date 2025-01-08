using System.Net.Sockets;
using System.Text;

public class Klient
{
    
    private int _port;

    public Klient(int port)
    {
        this._port = port;
    }

    public void Run()
    {
        try
        {
            // Opretter en client og opretter forbindelse til serveren
            var client = new TcpClient("127.0.0.1", _port);
            Console.WriteLine("Client connected");
            //henter paylaoden fra serveren
            var stream = client.GetStream();
            // laver et array af number 
            var numbers = new[] { 15, 35, 5, 21, 22, 21};
            foreach (var number in numbers)
            {
                // tager heltal koder om til en string i ascii format
                var message = number.ToString();
                var data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                
            // Læser svar fra serveren
                var buffer = new byte[1024];
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Response for  {response}");
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}