using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TemperaturMåling;
// dif class server
public class Server
{
    // fields
    private int _port;
    private HashSet<int> _numbers;

    //constructor der tager port som parameter
    public Server(int port)
    {

        this._port = port;
        //hashset gør at tiden det tager lookup tid går hurtig men kan tage lang tid at insert 
        _numbers = new HashSet<int>();

    }
    // run metode 
    public void Run()
    {
        // kører try da hvis der kommer en exception kan vi håndtere dette uden programmet crasher
        try
        {
            // tager lokalhost og lytter på en given port 
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
            // starter serveren
            server.Start();
            Console.WriteLine("Server started");
            // mens dette kører 
            while (true)
            {
                //hvis der kommer en tcp forbindelse gemmer den som client
                var client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");
                //henter payloaden fra klienten af
                var stream = client.GetStream();
                //mens dette kører
                while (true)
                {
                    // hvor meget der læses af gangen
                    var bytes = new byte[1024];
                    var bytesRead = stream.Read(bytes, 0, bytes.Length);
                    // hopper ud af løkken hvis der ikke bliver læst mere
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    //Tager det rå data fra klienten, og behandler nu data som ASCII
                    var clientMessage = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    Console.WriteLine($"Modtaget svar: {clientMessage}");
                    //Tager den parsede string clientMessage, og parser den til et heltal (Hvis data ikke indeholder et heltal kaster denne metode en exception)
                    var number = int.Parse(clientMessage);
                    // Tager tallet fra klienten, og håndtere tallet, funktionen returnere det svar der skal sendes tilbage til klienten
                    var responseMessage = HandleNumber(number);
                    // Tilføjer tallet til _numbers, hvis tallet allerede er i datasættet returnere Add false, da hashset ikke tager imod duplikanter
                    _numbers.Add(number);
                    
                    // Encoder responseMessage i UTF8 (enternet standarden), data er nu som bytes (rådata), dette
                    // data kan ikke indeholde råt binær data, da det ikke alt er repræsenterbar som UTF8
                    // Hvis man ønsker at sende rådata bør man bruge base64, men dette fylder 4/3 mere.
                    var responseData = Encoding.UTF8.GetBytes(responseMessage);
                    // Sender data til klienten
                    stream.Write(responseData, 0, responseData.Length);

                }

                // Server lukker nu forbindelsen til klienten.
                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught while handling client connection: {ex.Message} - ST: {ex.StackTrace}");
        }
    }

    // privat metode der håndterer klient data, parset i heltal
    private string Temperatur(int number)
    {
        if (number > 30)
        {
            return "VARM";
        }
        else if (number < 10)
        {
            return "KOLD";
        }
        else
        {
            return "NEUTRAL";
        }
    }
    // private metode der håndterer at hvis den ser et gentagne tal så adder den Gentaget til besked der bliver sendt til klient
    private string HandleNumber(int number)
    {
        var temperature = Temperatur(number);
        if (_numbers.Contains(number))
        {
            return "GENTAGET " + temperature;
        }

        return temperature;
    }
}