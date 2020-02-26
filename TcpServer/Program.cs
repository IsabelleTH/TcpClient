using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a TcpListener to listen to incoming requests to the server
            TcpListener listener = null;
            //create a try / catch block 
            try
            {
                //The listener will listen to IP Address and port number specified in the parameters
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8001);
                //Start the listener
                listener.Start();
                Console.WriteLine("The server has started...");
                //Create a while loop that loops through as long as the statement is true
                while(true)
                {
                    Console.WriteLine("Waiting for incoming connections...");
                    //Create a tcp variable set to the AcceptTcpClient that accepts a pending connection request. This blocks any other incoming connections. Once it gets connected it unblocks the connection and returns a TcpClient object
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Accepted new client connection...");
                    //Create a steam reader and a steam writer and passing in the client object we got from the client connection
                    //Servern läser strömmen med hjälp av reader
                    StreamReader reader = new StreamReader(client.GetStream());
                    StreamWriter writer = new StreamWriter(client.GetStream());

                    //Create an empty string in which the user input will be stored
                    string str = string.Empty;

                    //Create an empty string to hold input from server
                    string strServer = string.Empty;
                    //Create a while loop in which it does not stop unless user input == exit || the string is null
                    //Skapa en foreach loop när något skrivs och skriv ut detta om det är flera klienter
                    while(!(str = reader.ReadLine()).Equals("Exit") || (str == null))
                    {
                        Console.WriteLine("Client: " + str);
                        strServer = Console.ReadLine();
                        Console.WriteLine();
                        writer.WriteLine("Server: " + strServer);
                        writer.Flush();
                    }

                    reader.Close();
                    writer.Close();
                    client.Close();
                }
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if(listener != null)
                    listener.Stop();
            }
        }
    }
}
