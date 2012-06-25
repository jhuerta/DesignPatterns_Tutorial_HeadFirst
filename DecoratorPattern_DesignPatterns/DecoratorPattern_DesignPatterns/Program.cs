using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecoratorPattern_DesignPatterns;

namespace DecoratorPattern_DesignPatterns
{
    class Program
    {
        static void Main()
        {

            IBeverage smallFrapucchino = new Frapuccino().SetSize(Size.small);
            smallFrapucchino = new ExtraShotCondiment(smallFrapucchino);
            smallFrapucchino = new ExtraSugarCondiment(smallFrapucchino);

            IBeverage latte = new Latte();

            IBeverage darkCoffee = new DarkCoffe().SetSize(Size.medium);
            darkCoffee = new WhippedCreamCondiment(darkCoffee);
            darkCoffee = new ExtraSugarCondiment(darkCoffee);
            darkCoffee = new MochaCondiment(darkCoffee);
            Console.WriteLine("\n\nFrapuccino: " + darkCoffee.Description() + "\n\tPrice: " + darkCoffee.Cost());
            
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new MochaCondiment(latte);
            latte = new ExtraSugarCondiment(latte);


            IBeverage frapucchino = new Frapuccino().SetSize(Size.small);
            frapucchino = new ExtraShotCondiment(frapucchino);
            frapucchino = new ExtraSugarCondiment(frapucchino);

            IBeverage mediumFrapucchino = new Frapuccino().SetSize(Size.medium);
            mediumFrapucchino = new ExtraShotCondiment(mediumFrapucchino);
            mediumFrapucchino = new ExtraSugarCondiment(mediumFrapucchino);

            IBeverage largeFrapucchino = new Frapuccino().SetSize(Size.large);
            largeFrapucchino = new ExtraShotCondiment(largeFrapucchino);
            largeFrapucchino = new ExtraSugarCondiment(largeFrapucchino);



            Console.WriteLine("Latte: " + latte.Description() + "\n\tPrice: " + latte.Cost());
            Console.WriteLine("\n\nFrapuccino: " + frapucchino.Description() + "\n\tPrice: " + frapucchino.Cost());
            Console.WriteLine("\n\nFrapuccino: " + smallFrapucchino.Description() + "\n\tPrice: " + smallFrapucchino.Cost());
            Console.WriteLine("\n\nFrapuccino: " + mediumFrapucchino.Description() + "\n\tPrice: " + mediumFrapucchino.Cost());
            Console.WriteLine("\n\nFrapuccino: " + largeFrapucchino.Description() + "\n\tPrice: " + largeFrapucchino.Cost());
            Console.WriteLine("\n\nDark Coffee" + darkCoffee.Description() + "\n\tPrice: " + darkCoffee.Cost());
            
        }
    }

    class CondimentBase : IBeverage
    {
        protected IBeverage _beverage;
        protected int _cost;
        protected string _beverageDescription;

        public CondimentBase(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public int Cost()
        {
            return _cost + _beverage.Cost() + ComputeExtraCostForSize();
        }

        public Size Size()
        {
            return _beverage.Size();
        }

        private int ComputeExtraCostForSize()
        {
            switch (Size())
            {
                case DecoratorPattern_DesignPatterns.Size.small:
                    return 3;

                case DecoratorPattern_DesignPatterns.Size.medium:
                    return 5;

                case DecoratorPattern_DesignPatterns.Size.large:
                    return 7;
            }

            return 0;
        }

        public string Description()
        {
            return _beverage.Description() + " " + _beverageDescription;
        }


    }

    class MochaCondiment : CondimentBase
    {
        public MochaCondiment(IBeverage beverage) : base(beverage)
        {
            _cost = 1;
            _beverageDescription = "with a bit of chocolate";
        }
    }

    class ExtraShotCondiment : CondimentBase
    {
        public ExtraShotCondiment(IBeverage beverage)
            : base(beverage)
        {
            _cost = 2;
            _beverageDescription = "with double caffeine shot";
        }
    }

    class ExtraSugarCondiment : CondimentBase
    {
        public ExtraSugarCondiment(IBeverage beverage)
            : base(beverage)
        {
            _cost = 3;
            _beverageDescription = "with a some sugar";
        }
    }

    class WhippedCreamCondiment : CondimentBase
    {
        public WhippedCreamCondiment(IBeverage beverage)
            : base(beverage)
        {
            _cost = 4;
            _beverageDescription = "with a whipped cream";
        }
    }

    class BeverageBase : IBeverage
    {
        protected int _cost;
        protected string _beverageDescription;
        private Size _size = DecoratorPattern_DesignPatterns.Size.small;

        public BeverageBase SetSize(Size size)
        {
            _size = size;
            return this;
        }

        public BeverageBase()
        {
        }

        public BeverageBase(Size size)
        {
            _size = size;
        }

        public Size Size()
        {
            return _size;
        }

        public int Cost()
        {
            return _cost + ComputeExtraCostForSize();
        }

        public string Description()
        {
            return _beverageDescription;
        }

        private int ComputeExtraCostForSize()
        {
            switch (Size())
            {
                case DecoratorPattern_DesignPatterns.Size.small:
                    return 4;

                case DecoratorPattern_DesignPatterns.Size.medium:
                    return 6;

                case DecoratorPattern_DesignPatterns.Size.large:
                    return 8;
            }

            return 0;
        }
    }

    enum Size
    {
        small,
        medium,
        large
    }

    class DarkCoffe : BeverageBase
    {
        public DarkCoffe()
        {
            _cost = 10;
            _beverageDescription = "dark coffee";
        }
    }

    class Latte : BeverageBase
    {
        public Latte()
        {
            _cost = 15;
            _beverageDescription = "latte";
        }
    }

    class Frapuccino : BeverageBase
    {
        public Frapuccino()
        {
            _cost = 20;
            _beverageDescription = "iced crushed coffee";
        }
    }

    internal interface ICondiment : IBeverage{}
    
    interface IBeverage
    {
        int Cost();
        string Description();
        Size Size();
    }
}
