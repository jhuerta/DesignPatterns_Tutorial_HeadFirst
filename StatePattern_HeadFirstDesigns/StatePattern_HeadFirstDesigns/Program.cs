using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;


namespace StatePattern_HeadFirstDesigns
{
    public class Program
    {
        static void Main(string[] args)
        {


            try
            {
                var channel = new HttpChannel();

                ChannelServices.RegisterChannel(channel, false);

                var gumballMachineRemote =
                (IGumballMachineRemote)Activator.GetObject(
                    typeof(IGumballMachineRemote), "http://localhost:9998/GumballMachine");

                var monitor = new GumballMonitor(gumballMachineRemote);

                monitor.printReport();

            }
            catch (Exception e)
            {
                throw new RemoteException(e.Message, e);
            }



            var gumballMachine = new GumballMachine(15, "Madrid");
            var gumballMonitor = new GumballMonitor(gumballMachine);
            gumballMonitor.printReport();

            //for (int i = 0; i < 20; i++)
            //{

            //    gumballMachine.insertQuarter();
            //    gumballMachine.trunCrank();
            //}

            Console.ReadLine();




        }
    }



    public class RemoteException : Exception
    {
        public RemoteException(string message, Exception cause) : base(message, cause) { }
        public RemoteException(string message) : base(message) { }
    }


}

