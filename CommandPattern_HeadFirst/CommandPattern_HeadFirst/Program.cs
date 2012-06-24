using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPattern_HeadFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new RemoteController();

            var tv = new TV();
            var fan = new Fan();
            var video = new Video();

            CommandObject videoActionOn = new VideoActionOn(video);
            CommandObject videoActionOff = new VideoActionOff(video);
            CommandObject tvActionOn = new TvActionOn(tv);
            CommandObject tvActionOff = new TvActionOff(tv);
            CommandObject fanActionOn = new FanActionOn(fan);
            CommandObject fanActionOff = new FanActionOff(fan);

            controller.AddActionOn(videoActionOn);
            controller.AddActionOff(videoActionOff);
            controller.AddActionOn(tvActionOn);
            controller.AddActionOff(tvActionOff);
            controller.AddActionOn(fanActionOn);
            controller.AddActionOff(fanActionOff);

            Console.WriteLine(controller);

            Console.WriteLine("----> Video On");
            controller.ExecuteOn(0);
            controller.Undo();


            Console.WriteLine("----> TV On");
            controller.ExecuteOn(1);
            controller.Undo();

            Console.WriteLine("----> Fan On");
            controller.ExecuteOn(2);
            controller.Undo();

            Console.WriteLine("Nothing Assigned");
            controller.ExecuteOn(3);
            controller.Undo();

            Console.WriteLine("Nothing Assigned");
            controller.ExecuteOn(4);
            controller.Undo();

            Console.WriteLine("----> Video Off");
            controller.ExecuteOff(0);
            controller.Undo();

            Console.WriteLine("----> TV Off");
            controller.ExecuteOff(1);
            controller.Undo();

            Console.WriteLine("----> Fan Off");
            controller.ExecuteOff(2);
            controller.Undo();

            Console.WriteLine("Nothing Assigned");
            controller.ExecuteOff(3);
            controller.Undo();

            Console.WriteLine("Nothing Assigned");
            controller.ExecuteOff(4);
            controller.Undo();

            Console.WriteLine(" ---- MULTIPLE ACTIONS AND MULTIPLE UNDOES ----");
            controller.ExecuteOn(0);
            controller.ExecuteOn(1);
            controller.ExecuteOn(2);
            controller.ExecuteOff(0);
            controller.ExecuteOff(1);
            controller.ExecuteOff(2);
        
            controller.Undo();
            controller.Undo();
            controller.Undo();

            controller.Undo();
            controller.Undo();
            controller.Undo();

            controller.Undo();
            controller.Undo();
            controller.Undo();
            controller.Undo();
            controller.Undo();
            controller.Undo();


        }
    }

    internal class RemoteController
    {
        public override string ToString()
        {
            Console.WriteLine(" --- REMOTE CONTROLLER ---");
            string output = "";
            for (int i = 0; i < commandOnList.Count; i++)
            {
                output += String.Format("On[{0}]: {1} ", i, commandOnList[i].GetType().Name);
                output += String.Format("\tOff[{0}]: {1} \n", i, commandOffList[i].GetType().Name);
            }
            return output;
        }

        private class NullCommand : CommandObject
        {
            public void Execute()
            {
                Console.WriteLine(" -- nothing to be done --");
            }

            public void Undo()
            {
                Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
            }
        }

        private readonly List<CommandObject> commandOnList = new List<CommandObject>();
        private readonly List<CommandObject> commandOffList = new List<CommandObject>();
        private readonly CommandObject nullCommand = new NullCommand();
        private CommandObject lastCommand;
        private List<CommandObject> lifoCommandList = new List<CommandObject>();

        public void AddActionOn(CommandObject commandOn)
        {
            commandOnList.Add(commandOn);
        }

        public void AddActionOff(CommandObject commandOff)
        {
            commandOffList.Add(commandOff);
        }

        public void ExecuteOn(int commandOnPosition)
        {
            if (PositionOutOfList(commandOnPosition, commandOnList))
            {
                nullCommand.Execute();
                lastCommand = nullCommand;
                lifoCommandList.Add(nullCommand);
                return;
            }
            commandOnList[commandOnPosition].Execute();
            lastCommand = commandOffList[commandOnPosition];
            lifoCommandList.Add(commandOnList[commandOnPosition]);
        }
        
        public void ExecuteOff(int commandOffPosition)
        {
            if (PositionOutOfList(commandOffPosition,commandOffList))
            {
                nullCommand.Execute();
                lastCommand = nullCommand;
                lifoCommandList.Add(nullCommand);
                return;
            }
            commandOffList[commandOffPosition].Execute();
            lastCommand = commandOffList[commandOffPosition];
            lifoCommandList.Add(commandOffList[commandOffPosition]);
        }

        private bool PositionOutOfList(int commandOffPosition, List<CommandObject> commandList)
        {
            return commandOffPosition >= commandList.Count;
        }

        public void Undo()
        {
            if(lifoCommandList.Count == 0)
            {
                nullCommand.Execute();
                return;
            }

            lifoCommandList.Last().Execute();
            lifoCommandList.Remove(lifoCommandList.Last());
        }
    }

    internal class VideoActionOn : CommandObject
    {
        private readonly Video video;
        private VideoActionOn(){}


        public VideoActionOn(Video video)
        {
            this.video = video;
        }

        public void Execute()
        {
            video.On();
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}",this.GetType().Name);
        }
    }

    internal class VideoActionOff : CommandObject
    {
        private readonly Video video;
        private VideoActionOff(){}

        public VideoActionOff(Video video)
        {
            this.video = video;
        }

        public void Execute()
        {
            video.Off();
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
        }
    }

    internal class TvActionOn : CommandObject
    {
        private readonly TV tv;
        private TvActionOn(){}


        public TvActionOn(TV tv)
        {
            this.tv = tv;
        }

        public void Execute()
        {
            tv.On();
            tv.VolumeUp(5);
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
        }
    }

    internal class TvActionOff : CommandObject
    {
        private readonly TV tv;
        private TvActionOff(){}


        public TvActionOff(TV tv)
        {
            this.tv = tv;
        }

        public void Execute()
        {
            tv.Off();
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
        }
    }

    internal class FanActionOn : CommandObject
    {
        private readonly Fan fan;
        private FanActionOn() { }

         public FanActionOn(Fan fan)
        {
            this.fan = fan;
        }

        public void Execute()
        {
            fan.SetSpeed(Fan.FanSpeed.High);
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
        }
    }

    internal class FanActionOff : CommandObject
    {
        private readonly Fan fan;
        private FanActionOff(){}

         public FanActionOff(Fan fan)
        {
            this.fan = fan;
        }

        public void Execute()
        {
            fan.SetSpeed(Fan.FanSpeed.Off);
        }

        public void Undo()
        {
            Console.WriteLine("I'm undoing the action for {0}", this.GetType().Name);
        }
    }

    internal interface CommandObject
    {
        void Execute();
        void Undo();
    }

    public class Video
    {
        public void On(){Console.WriteLine("Video ON");}
        public void Off() { Console.WriteLine("Video OFF"); }
    }
    public class TV
    {
        public void VolumeUp(int volume) { Console.WriteLine("TV - Volume UP by: " + volume.ToString()); }
        public void VolumeDown(int volume) { Console.WriteLine("TV - Volume DOWN by: " + volume.ToString()); }

        public void Off()
        {
                Console.WriteLine("TV - Turning TV OFF");
        }

        public void On()
        {
            Console.WriteLine("TV - Turning TV On");
        }

    }
    public class Fan
    {
        public void SetSpeed(FanSpeed fanSpeed){ Console.WriteLine("Fan - Fan speed: " + fanSpeed); }

        public enum FanSpeed
        {
            Off,
            Low,
            Medium,
            High
        }
    }

}
