using Opc.Ua;
using Opc.Ua.Configuration;
using System;
using System.Threading;

namespace OpcClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int stopTimeout = Timeout.Infinite;
            bool autoAccept = false;
            string endpointURL = "opc.tcp://DESKTOP-GHG6DG6:53530/OPCUA/SimulationServer";
            int clientRunTime = stopTimeout <= 0 ? Timeout.Infinite : stopTimeout * 1000;

            POpcClient client = new POpcClient(endpointURL, autoAccept);
            try
            {
                client.run().Wait(); 
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            ManualResetEvent quitEvent = new ManualResetEvent(false);
            try
            {
                Console.CancelKeyPress += (sender, eArgs) =>
                {
                    quitEvent.Set();
                    eArgs.Cancel = true;
                };
            }
            catch
            {
            }

            // wait for timeout or Ctrl-C
            quitEvent.WaitOne(clientRunTime);

        }
    }
}
