using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using StatePattern_HeadFirstDesigns;

namespace Gumballs
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int count = 66;
            string location = "Londres";

            //if (args.Length != 2)
            //{
            //    Console.WriteLine("Gumballs <location> <inventory>");
            //    Environment.Exit(1);
            //}

            //location = args[0];
            //count = int.Parse(args[1]);

            GumballMachine gumballMachine = new GumballMachine(count, location);

            Console.WriteLine("Starting Gumball server...");

            try
            {
                HttpChannel channel = new HttpChannel(9998);
                ChannelServices.RegisterChannel(channel, false);
                RemotingServices.Marshal(gumballMachine, "GumballMachine");

                Console.WriteLine("Press Enter to quit\n\n");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                throw new RemoteException(e.Message, e);
            }
        }
    }
}