using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternOfPatterns_HeadFirstDessignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            var observer = new Observer();
            var countingDuckFactory = new CountingDuckFactory();

            var mallarDuck = countingDuckFactory.createMallarDuck();
            //mallarDuck.addObserver(observer);

            var rubberDuck = countingDuckFactory.createRubberDuck();
            //rubberDuck.addObserver(observer);

            var redheadDuck = countingDuckFactory.createRedHeadDuck();
            //redheadDuck.addObserver(observer);

            var duckCallDuck = countingDuckFactory.createDuckCallDuck();
            //duckCallDuck.addObserver(observer);

            var fakeDuckIsAGoose = countingDuckFactory.createGooseDucky();
            //fakeDuckIsAGoose.addObserver(observer);

            //mallarDuck.quack();
            //rubberDuck.quack();
            //redheadDuck.quack();
            //duckCallDuck.quack();
            //fakeDuckIsAGoose.quack();

            var flock = new Flock();
            flock.addQuackable(mallarDuck);
            flock.addQuackable(duckCallDuck);
            flock.addQuackable(fakeDuckIsAGoose);
            flock.addQuackable(rubberDuck);
            flock.addQuackable(redheadDuck);
            //flock.addObserver(observer);
            flock.quack();



            Console.ReadKey();
        }

        public interface IOBservable
        {
            void addObserver(Observer observer);
            void notifyObserver();
        }
        public class Observable : IOBservable
        {
            Observer observer = new Observer();

            public Observable(IOBservable duck)
            {
                this.duck = duck;
            }

            private IOBservable duck;

            public void addObserver(Observer _observer)
            {
                observer = _observer;
            }

            public void notifyObserver()
            {
                observer.update(duck);
            }
        }

        static void TestDucksQuacking()
        {

            var countingDuckFactory = new CountingDuckFactory();
            var duckFactory = new DuckFactory();
            
            IQuackable mallarDuck;
            IQuackable duckCallDuck;
            IQuackable fakeDuckIsAGoose;
            IQuackable rubberDuck;
            IQuackable redheadDuck;

            var flock = new Flock();
            
            CreateDucks(countingDuckFactory, out mallarDuck, out duckCallDuck, out fakeDuckIsAGoose, out rubberDuck, out redheadDuck);

            flock.addQuackable(mallarDuck);
            flock.addQuackable(duckCallDuck);
            flock.addQuackable(fakeDuckIsAGoose);
            flock.addQuackable(rubberDuck);
            flock.addQuackable(redheadDuck);

            SimulateQuacks(redheadDuck, duckCallDuck, fakeDuckIsAGoose, mallarDuck, rubberDuck);

            flock.quack();

            Console.WriteLine("Number of Quacks: {0}", QuackCounter.getQuacks());

            CreateDucks(duckFactory, out mallarDuck, out duckCallDuck, out fakeDuckIsAGoose, out rubberDuck, out redheadDuck);

            flock.addQuackable(mallarDuck);
            flock.addQuackable(duckCallDuck);
            flock.addQuackable(fakeDuckIsAGoose);
            flock.addQuackable(rubberDuck);
            flock.addQuackable(redheadDuck);

            flock.quack();
            flock.quack();
            flock.quack();

            flock.quack();

            SimulateQuacks(redheadDuck, duckCallDuck, fakeDuckIsAGoose, mallarDuck, rubberDuck);

            Console.WriteLine("Number of Quacks: {0}", QuackCounter.getQuacks());

            Console.ReadKey();
        }


        public class Flock : IQuackable
        {
            private Observable observable;
            private IList<IQuackable> listOfQuackables;

            public Flock()
            {
                observable = new Observable(this);
                this.listOfQuackables = new List<IQuackable>();
            }

            public void addQuackable(IQuackable quackable)
            {
                listOfQuackables.Add(quackable);
            }

            public void quack()
            {
                foreach(IQuackable quackable in listOfQuackables)
                {
                    quackable.quack();
                    notifyObserver();
                }
            }


            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }
        }

        private static void SimulateQuacks(IQuackable redheadDuck, IQuackable duckCallDuck, IQuackable fakeDuckIsAGoose,
                                     IQuackable mallarDuck, IQuackable rubberDuck)
        {
            var simDuck = new Simulator();
            simDuck.SimulateQack(mallarDuck, 1);
            simDuck.SimulateQack(rubberDuck, 2);
            simDuck.SimulateQack(redheadDuck, 3);
            simDuck.SimulateQack(duckCallDuck, 4);
            simDuck.SimulateQack(fakeDuckIsAGoose, 5);
        }

        private static void CreateDucks(DuckFactoryBase countingDuckFactory, out IQuackable mallarDuck, out IQuackable duckCallDuck,
                                              out IQuackable fakeDuckIsAGoose, out IQuackable rubberDuck,
                                              out IQuackable redheadDuck)
        {
            mallarDuck = countingDuckFactory.createMallarDuck();
            rubberDuck = countingDuckFactory.createRubberDuck();
            redheadDuck = countingDuckFactory.createRedHeadDuck();
            duckCallDuck = countingDuckFactory.createDuckCallDuck();
            fakeDuckIsAGoose = countingDuckFactory.createGooseDucky();
        }

        public class QuackCounter : IQuackable
        {
            private Observable observable;
            private IQuackable duck;
            private static int numberOfQacks = 0;
            public QuackCounter(IQuackable duck)
            {
                this.duck = duck;
                observable = new Observable(this);
            }

            public void quack()
            {
                duck.quack();
                numberOfQacks++;
            }

            public static int getQuacks()
            {
                return numberOfQacks;
            }


            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }
        }

        public class Simulator
        {
            private IQuackable simDuck;

            public Simulator(IQuackable simDuck)
            {
                this.simDuck = simDuck;
            }

            public Simulator()
            {
                this.simDuck = new DuckCall();
            }

            public void SimulateQack(IQuackable duck,int numberOfQuacks)
            {
                for (int i = 0; i < numberOfQuacks;i++ )
                {
                    duck.quack();
                }
            }

            public void SimulateQack()
            {
                simDuck.quack();
            }
        }

        public interface IQuackable : IOBservable
        {
            void quack();
        }



        public class MallarDuck:IQuackable
        {
            private Observable observable;

            public MallarDuck()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine(" {0} - Quack!", this.GetType().Name);
                notifyObserver();

            }

            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }
        }

        public class RubberDuck : IQuackable
        {
            private Observable observable;

            public RubberDuck()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine(" {0} - Blish blish quash!", this.GetType().Name);
                notifyObserver();
            }

            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }

        }

        public class RedheadDuck : IQuackable
        {
            private Observable observable;

            public RedheadDuck()
            {
                observable = new Observable(this);

            }

            public void quack()
            {
                Console.WriteLine(" {0} - Redredredquaaaaaaaaaaaack!", this.GetType().Name);
                notifyObserver();
            }
            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }

        }

        public class DuckCall : IQuackable
        {
            private Observable observable;

            public DuckCall()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine(" {0} - Kuuuuaaash!", this.GetType().Name);
                notifyObserver();
            }
            public void addObserver(Observer observer)
            {
                observable.addObserver(observer);
            }

            public void notifyObserver()
            {
                observable.notifyObserver();
            }
        }

        public class Goose
        {
            public void honk()
            {
                Console.WriteLine(" {0} - Honk honk hoooonk!", this.GetType().Name);
            }
        }

        public class Observer
        {
            public void update(IOBservable duck)
            {
                Console.WriteLine("OBSERVER - {0} just quacked!", duck.GetType().Name);
            }
        }

    
    }


    public abstract class DuckFactoryBase
    {
        public abstract Program.IQuackable createRubberDuck();
        public abstract Program.IQuackable createRedHeadDuck();
        public abstract Program.IQuackable createDuckCallDuck();
        public abstract Program.IQuackable createMallarDuck();
        public abstract Program.IQuackable createGooseDucky();
    }

    public class CountingDuckFactory : DuckFactoryBase
    {

        public override Program.IQuackable createRubberDuck()
        {
            return new Program.QuackCounter(new Program.RubberDuck());
        }

        public override Program.IQuackable createRedHeadDuck()
        {
            return new Program.QuackCounter(new Program.RedheadDuck());
        }

        public override Program.IQuackable createDuckCallDuck()
        {
            return new Program.QuackCounter(new Program.RubberDuck());
        }

        public override Program.IQuackable createMallarDuck()
        {
            return new Program.QuackCounter(new Program.MallarDuck());
        }

        public override Program.IQuackable createGooseDucky()
        {
            return new Program.QuackCounter(new FakeDuckWithGoose(new Program.Goose()));
        }
    }


    public class DuckFactory : DuckFactoryBase
    {

        public override Program.IQuackable createRubberDuck()
        {
            return new Program.RubberDuck();
        }

        public override Program.IQuackable createRedHeadDuck()
        {
            return new Program.RedheadDuck();
        }

        public override Program.IQuackable createDuckCallDuck()
        {
            return new Program.RubberDuck();
        }

        public override Program.IQuackable createMallarDuck()
        {
            return new Program.MallarDuck();
        }

        public override Program.IQuackable createGooseDucky()
        {
            return new FakeDuckWithGoose(new Program.Goose());
        }
    }


    internal class FakeDuckWithGoose : Program.IQuackable
    {
        private Program.Goose _goose;
        private Program.Observable observable;

        public FakeDuckWithGoose(Program.Goose goose)
        {
            observable = new Program.Observable(this);

            _goose = goose;
        }

        public void quack()
        {
            _goose.honk();
            notifyObserver();
        }


        public void addObserver(Program.Observer observer)
        {
            observable.addObserver(observer);
        }

        public void notifyObserver()
        {
            observable.notifyObserver();
        }
    }
}
