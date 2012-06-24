using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdapterPattern_HeadFirstDesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var duck = new WildDuck();
            var turkey = new WildTurkey();

            var cook = new Client();

            Base myBase = new Base();
            Base myBaseAsChild = new Child();
            Child myChildAsChild = new Child();

            myBase.DoSomething();
            myBaseAsChild.DoSomething();
            myChildAsChild.DoSomething();

            Console.WriteLine("Cook will cook duck");
            cook.CookDuck(duck);

            Console.WriteLine("Cook will cook turkey");
            cook.CookTurkey(turkey);

            Console.WriteLine("Cook will cook WILD DUCK");
            cook.CookWildDuck(duck);

            Console.WriteLine("Cook will cook WILD TURKEY as a WILD DUCK");
            WildTurkeyToWildDuck wildTurkeyIntoWildDuck = new WildTurkeyToWildDuck(turkey);
            Console.WriteLine("Before Cooking: " + wildTurkeyIntoWildDuck.Flavor());
            cook.CookWildDuck(wildTurkeyIntoWildDuck);


            Console.WriteLine("Cook is gonna cook a turkey, but he does not know!");
            cook.CookDuck(new TurkeyDuckAdapter(turkey));

            

            //Console.WriteLine("\nLets make the turkey walk!");
            //turkeyDuckWannaBe.Walk();

            //Console.WriteLine("\n\n\nTurn for the real duck");
            //coolDuck.Walk();

            Console.ReadLine();

        }
    }


    public class Base
{
        public void DoSomething()
        {
            Console.WriteLine("Base");
        }
}

public class Child : Base
{
    public new void DoSomething()
    {
        Console.WriteLine("Child");
    }
}


    internal class WildTurkeyToWildDuck : WildDuck
    {
        private readonly WildTurkey turkey;

        public WildTurkeyToWildDuck(WildTurkey turkey)
        {
            this.turkey = turkey;
        }

        public override string Flavor()
        {
            Console.WriteLine("new new new");
            return turkey.Flavor();
        }
    }

    internal class Client
    {
        public void CookDuck(IDuck duck)
        {
            Console.WriteLine("Im cooking this duck: " + duck.Flavor());
        }

        public void CookTurkey(ITurkey turkey)
        {
            Console.WriteLine("Im cooking this turkey: " + turkey.Flavor());
        }

        public void CookWildDuck(WildDuck wildDuck)
        {
            Console.WriteLine("Cooking WILD DUCK:" + wildDuck.Flavor());
        }
    }

    internal class TurkeyDuckAdapter : IDuck
    {
        private readonly ITurkey turkey;

        private TurkeyDuckAdapter(){}

        public TurkeyDuckAdapter(ITurkey turkeyDuckWannaBe)
        {
            turkey = turkeyDuckWannaBe;
        }

        public void Walk()
        {
            turkey.Swim();
            Console.Write("They want me to walk ....");   
        }

        public string Flavor()
        {
            return turkey.Flavor() + "\n(but I'll be marinated and cant taste like a duck)";
        }
    }

    internal class WildTurkey : ITurkey
    {
        public void Swim()
        {
            Console.WriteLine("Im a Turkety. I swim!");
        }

        public string Flavor()
        {
            return "I taste like a Turkey!";
        }
    }

    internal interface ITurkey
    {
        void Swim();
        string Flavor();
    }

    internal class WildDuck : IDuck
    {
        public void Walk()
        {
            Console.WriteLine("WILD DUCK: Im a duck and I walk!");
        }

        public virtual string Flavor()
        {
            return "WILD DUCK: I taste like a duck";
        }
    }

    internal interface IDuck
    {
        void Walk();
        string Flavor();
    }
}
