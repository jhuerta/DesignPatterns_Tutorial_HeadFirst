using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatePattern_HeadFirstDesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeMaker = new CoffeMaker();
            var teaMaker = new TeaMaker();

            coffeMaker.PrepareDrink();

            teaMaker.PrepareDrink();

            var myClass = new MySealedClass();
            myClass.TemplateMethod();

            Console.ReadLine();
        }
    }

    internal class TeaMaker : DrinkRecipe
    {
        public new void PrepareDrink()
        {
            Console.WriteLine("Im messing up the tea!");
        }

        protected override void insertDrinkSpecific()
        {
            Console.WriteLine("Putting a TeaBag");
        }

        protected override void addCondimentSpecific()
        {
            Console.WriteLine("Pouring some lemons");
        }
    }

    internal class CoffeMaker : DrinkRecipe
    {
        protected override void insertDrinkSpecific()
        {
            Console.WriteLine("Adding Coffee");
        }

        protected override void addCondimentSpecific()
        {
            Console.WriteLine("Adding Sugar and Milk");
        }
    }

    public abstract class DrinkRecipe : DrinkRecipeBased
    {
        public override sealed void PrepareDrink()
        {
            boilWater();
            insertDrinkSpecific();
            pourInCup();
            addCondimentSpecific();
        }

        private void pourInCup()
        {
            Console.WriteLine("Pouring in a cup");
              
        }

        protected abstract void insertDrinkSpecific();
        protected abstract void addCondimentSpecific();

        private void boilWater()
        {
            Console.WriteLine("Boiling Water");
        }
    }

    public abstract class DrinkRecipeBased : IDrinkRecipe
    {
        public abstract void PrepareDrink();
    }


    public class MySealedClass : TheProgram
    {
        public override void TemplateMethod()
        {
            Console.WriteLine("Overriding the sealed class!!!!!!!!");
        }
        protected override void DoWhatYouWant()
        {
            Console.WriteLine("Do what you want");
        }

    }

    public abstract class TheProgram : MyClass
    {
        public override void TemplateMethod()
        {
            DoThis();
            DoThat();
            DoWhatYouWant();

        }

        protected abstract void DoWhatYouWant();

        private void DoThis()
        {
            Console.WriteLine("Do this");
        }

        private void DoThat()
        {
            Console.WriteLine("Do that");
        }
    }

    public class MyClass : IMyClass
    {
        public virtual void TemplateMethoda()
        {
            Console.WriteLine("My class Test");
        }

        #region Implementation of IMyClass

        public virtual void TemplateMethod()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal interface IMyClass
    {
        void TemplateMethod();
    }

    internal interface IDrinkRecipe
    {
        void PrepareDrink();
    }
}
