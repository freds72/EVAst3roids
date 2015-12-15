using System;
using System.Threading;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.UserInput;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Reflection;

namespace EVAst3roids
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InstalledUICulture;

            try
            {
                GameScreenManager game = new GameScreenManager();
                game.Add(new TitleScreen(game));
                game.Initialize();
                game.Run();
            }
            catch (Exception ex)
            {
#if DEBUG
                // Set the TcpListener on port 13000.
                int port = 13000;

                // TcpListener server = new TcpListener(port);
                TcpListener server = new TcpListener(IPAddress.Any, port);

                // Start listening for client requests.
                server.Start();

                Logger.Error("Wait debugger");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                using (TcpClient client = server.AcceptTcpClient())
                {
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    foreach(String it in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                    {
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(it + "\n");
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                    }

                    while (ex != null)
                    {
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(ex.Message + "\n");
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);

                        msg = System.Text.Encoding.ASCII.GetBytes(ex.StackTrace + "\n");
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        ex = ex.InnerException;
                    }
                }

                // Stop listening for new clients.
                server.Stop();
            }
        }
#endif
    }
}
