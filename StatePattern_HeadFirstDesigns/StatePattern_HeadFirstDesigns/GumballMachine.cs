using System;

namespace StatePattern_HeadFirstDesigns
{
    public class GumballMachine : MarshalByRefObject, IGumballMachineRemote

    {
        private int ballsInventory;
        private IState currentState;

        private IState hasQuarterState;
        private IState hasNoQuarterState;
        private IState soldState;
        private IState soldOutState;
        private IState winnerState;
        public string location;

        public IState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public int BallsInventory
        {
            get { return ballsInventory; }
        }

        public GumballMachine(int numberOfBalls, string location)
        {
            ballsInventory = numberOfBalls;
            this.location = location;

            hasQuarterState = new HasQuarterState(this);
            hasNoQuarterState = new HasNoQuarterState(this);
            soldState = new SoldState(this);
            soldOutState = new SoldOutState(this);
            winnerState = new WinnerState(this);

            currentState = ThereAreBalls() ? hasNoQuarterState : soldOutState;
        }

        private bool ThereAreBalls()
        {
            return BallsInventory > 0;
        }

        public IState getHasQuarterState()
        {
            return hasQuarterState;
        }

        public IState getHasNoQuarterState()
        {
            return hasNoQuarterState;
        }

        public IState getSoldState()
        {
            return soldState;
        }

        public IState getSoldOutState()
        {
            return soldOutState;
        }

        public void insertQuarter()
        {
            currentState.insertQuarter();
        }

        public void ejectQuarter()
        {
            currentState.ejectQuarter();
        }

        public void trunCrank()
        {
            currentState.trunCrank();
            currentState.dispense();
        }

        internal void releaseBall()
        {
            Console.WriteLine("Balling rolling ...");
            if (BallsInventory > 0)
            {
                ballsInventory = BallsInventory - 1;
            }
        }

        public void turnCrank()
        {
            currentState.trunCrank();
        }

        public IState getWinnerState()
        {
            return winnerState;
        }

        public int GetCount()
        {
            return BallsInventory;
        }

        public string GetLocation()
        {
            return location;
        }

        public IState GetState()
        {
            return this.currentState;
        }
    }

    internal class GumballMonitor
    {
        private IGumballMachineRemote gumballMachine;

        public GumballMonitor(IGumballMachineRemote gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void printReport()
        {
            string message = String.Format("Inventory: {0}\nState:{1}\nLocation:{2}", gumballMachine.GetCount(),
                                           gumballMachine.GetState(), gumballMachine.GetLocation());
            Console.WriteLine(message);
        }
    }

    public interface IGumballMachineRemote
    {
        int GetCount();
        string GetLocation();
        IState GetState();
    }
}