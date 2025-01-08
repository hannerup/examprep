//headless c# 
using TemperaturMåling;
//dif port
var port = 12345;
//åbner en ny port
var server = new Server(port);
// opretter en ny thread 
var serverThread = new Thread(server.Run);
// starter threaden
serverThread.Start();
// metode der får hovedtråden til at vente i 100 mil sek 
Thread.Sleep(100);
// starter klient op på porten som forbinder som serveren lytter på
var klient = new Klient(port);
klient.Run();
