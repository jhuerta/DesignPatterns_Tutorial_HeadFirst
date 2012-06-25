using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;

namespace RemotePattern_HeadFirstDesignPatterns
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var hotelService = new HotelService();
            //var customer = new Customer(hotelService);
            //Console.WriteLine(customer.HotelDescription(5));

            var hotelRemoteService = new HotelServiceServer();
            hotelRemoteService.Start();

            try
            {
                var hotelServiceProxy = (HotelService)Activator.GetObject(
                    typeof(IHotelService), "http://localhost:9998/HotelService");

                var slowHotelServiceProxy = (SlowHotelServiceProxy) Activator.GetObject(
                    typeof (IHotelService), "http://localhost:9999/SlowHotelServiceProxy");

                var customer = new Customer(hotelServiceProxy);
                Console.WriteLine(customer.HotelDescription(5));

                var slowCustomer = new Customer(slowHotelServiceProxy);
                Console.WriteLine(slowCustomer.HotelDescription(10));
            } 
            catch (Exception e) 
            {
                throw new RemoteException (e.Message, e);
            }




            Console.ReadLine();
        }
    }


    public class Customer
    {
        private readonly IHotelService hotelService;
        public string hotelDescription="";

        public Customer(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public string HotelDescription(int id)
        {
            return hotelService.GetHotelDescription(id);
        }
    }

    public class SlowHotelServiceProxy: MarshalByRefObject,IHotelService
    {
        public IHotelService slowHotelService = new SlowHotelService();
        public string hotelDescription = "";
        public string GetHotelDescription(int id)
        {
            hotelDescription = "Retrieving Hotel Descriptio Info ...";
            var hotelDescriptionThread = new Thread(delegate()
                                                        {
                                                            Console.WriteLine(HotelDescription(id));
                                                        });
            hotelDescriptionThread.Start();
            return hotelDescription;
        }

        private string HotelDescription(int id)
        {
            return slowHotelService.GetHotelDescription(id);
        }
        public int GetAvailableRooms()
        {
            throw new NotImplementedException();
        }

    }
    public interface IHotelService
    {
        string GetHotelDescription(int id);
        int GetAvailableRooms();
    }

    public class HotelServiceServer
    {
        public void Start()
        {
            var service = new HotelService();
            var slowServiceProxy = new SlowHotelServiceProxy();
            Console.WriteLine("Starting Remote Hotel Service...");

            try
            {
                IDictionary prop = new Hashtable();
                prop["name"] = "HotelService";
                prop["port"] = "9998";
                ChannelServices.RegisterChannel(new HttpChannel(prop, null, null));
                RemotingServices.Marshal(service, prop["name"].ToString());

                IDictionary slowProp = new Hashtable();
                slowProp["name"] = "SlowHotelServiceProxy";
                slowProp["port"] = "9999";
                ChannelServices.RegisterChannel(new HttpChannel(slowProp, null, null));
                RemotingServices.Marshal(slowServiceProxy, slowProp["name"].ToString());
            }
            catch (Exception e)
            {
                throw new RemoteException(e.Message, e);
            }
        }
    }

    public class RemoteException : Exception
    {
        public RemoteException(string message, Exception exception)
        {
            throw new Exception(String.Format("Remote Exception!: {0} - Message: {1}", exception.Message, message));
        }
    }

    public class HotelService : MarshalByRefObject,IHotelService
    {
        private int hotelId;

        public string GetHotelDescription(int id)
        {
            hotelId = id;
            return String.Format("GetHotelDescription: {0} - Class Name: {1}", id, GetType().Name);
        }

        public int GetAvailableRooms()
        {
            return hotelId;
        }
    }

    public class SlowHotelService : MarshalByRefObject,IHotelService
    {
        public string GetHotelDescription(int id)
        {
            Thread.Sleep(5000);
            return "Slow Get HotelDescription (after 5secs)";
        }

        public int GetAvailableRooms()
        {
            throw new NotImplementedException();
        }
    }
}

