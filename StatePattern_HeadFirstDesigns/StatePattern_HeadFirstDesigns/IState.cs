using System;

namespace StatePattern_HeadFirstDesigns
{
    public interface IState
    {
        void insertQuarter();
        void ejectQuarter();
        void trunCrank();
        void dispense();
    }

    [Serializable]
    public class SoldState : IState
    {
        [NonSerialized]
        private GumballMachine gumballMachine;

        public SoldState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void insertQuarter()
        {
            Console.WriteLine("insertQuarter: Nothig to be done");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("ejectQuarter: Nothing to be done");

        }

        public void trunCrank()
        {
            Console.WriteLine("trunCrank: NOthing to be done");
        }

        public void dispense()
        {
            gumballMachine.releaseBall();
            if (gumballMachine.BallsInventory > 0)
            {
                gumballMachine.CurrentState = gumballMachine.getHasNoQuarterState();
                return;
            }

            gumballMachine.CurrentState = gumballMachine.getSoldOutState();
        }

    }

    [Serializable]
    public class SoldOutState : IState
    {
        [NonSerialized]
        private GumballMachine gumballMachine;

        public SoldOutState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }
        public void insertQuarter()
        {
            Console.WriteLine("insertQuarter: Nothing to be done");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("ejectQuarter: Nothing to be done");
        }

        public void trunCrank()
        {
            Console.WriteLine("trunCrank: Nothing to be done");
        }

        public void dispense()
        {
            Console.WriteLine("dispense: Nothing to be done");
        }
    }

    [Serializable]
    public class HasNoQuarterState : IState
    {
        [NonSerialized]
        private GumballMachine gumballMachine;

        public HasNoQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }
        public void insertQuarter()
        {
            Console.WriteLine("insertQuarter: Accepting the quarter");
            gumballMachine.CurrentState = gumballMachine.getHasQuarterState();
        }

        public void ejectQuarter()
        {
            Console.WriteLine("ejectQuarter: You have no quarters");
        }

        public void trunCrank()
        {
            Console.WriteLine("trunCrank: Insert quarter first");
        }

        public void dispense()
        {
            Console.WriteLine("dispense: NO quarters inserted. Please insert quarter first");
        }
    }

    [Serializable]
    public class HasQuarterState : IState
    {
        [NonSerialized] 
        private GumballMachine gumballMachine;

        public HasQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }
        public void insertQuarter()
        {
            Console.WriteLine("insertQuarter: There is a quarter in. Not accepting this quarter.");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("ejectQuarter: OK. Here you have the quarter");
            gumballMachine.CurrentState = gumballMachine.getHasNoQuarterState();
        }

        public void trunCrank()
        {
            if (gumballMachine.BallsInventory > 0 && ThisUserIsAWinner())
            {
                gumballMachine.CurrentState = gumballMachine.getWinnerState();
                return;
            }
            gumballMachine.CurrentState = gumballMachine.getSoldState();
        }

        private bool ThisUserIsAWinner()
        {
            var randomNumer = DateTime.Now.Millisecond % 2;// RandomNumber(1, 2);
            return randomNumer == 1;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public void dispense()
        {
            Console.WriteLine("dispense: Nothing to be done");
        }
    }

    [Serializable]
    public class WinnerState : IState
    {



        [NonSerialized]
        private GumballMachine gumballMachine;

        public WinnerState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void insertQuarter()
        {
            Console.WriteLine("Nothing to be done");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("Nothing to be done");
        }

        public void trunCrank()
        {
            Console.WriteLine("Nothing to be done");
        }

        public void dispense()
        {
            Console.WriteLine("YOU ARE THE WINNER! Here you have the 2nd ball!");
            gumballMachine.releaseBall();
            if (gumballMachine.BallsInventory > 0)
            {
                gumballMachine.CurrentState = gumballMachine.getHasNoQuarterState();
                return;
            }

            gumballMachine.CurrentState = gumballMachine.getSoldOutState();
        }
    }
}