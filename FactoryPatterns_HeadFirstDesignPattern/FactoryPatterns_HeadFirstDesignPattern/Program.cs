using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryPatterns_HeadFirstDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IPizzaFactory chicagoPizzaFactory = new ChicagoPizzaFactory();
            IPizzaFactory newYorkPizzaFactory = new NewYorkPizzaFactory();

            var chicagoPizzaStore = new ChicagoPizzaStore();

            var nyPizzaStore = new NewYorkPizzaStore(newYorkPizzaFactory);
            var nyPizzaStoreError = new NewYorkPizzaStore();

            var arandaPizzaStore = new ArandaPizzaStore();

            var arandaPizza = arandaPizzaStore.OrderPizza(PizzaType.Margarita);

            var chicagoPizzaMargarita = chicagoPizzaStore.OrderPizza(PizzaType.Margarita);

            var nyPizzaMargarita = nyPizzaStore.OrderPizza(PizzaType.Margarita);
            nyPizzaStoreError.OrderPizza(PizzaType.Margarita);
        }

   
    }

    public class ArandaPizzaStore : PizzaStore
    {
        protected override IPizza CreatePizza(PizzaType type)
        {
            return new ArandaPizza();
        }


    }

    public class ArandaPizza : IPizza
    {
        public void prepare()
        {
            Console.WriteLine("Aranda Pizza - Preparing ...");
        }

        public void bake()
        {
            Console.WriteLine("Aranda Pizza - Baking ...");
        }

        public void box()
        {
            Console.WriteLine("Aranda Pizza - Boxing ...");
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        

        protected override IPizza CreatePizza(PizzaType type)
        {
            Console.WriteLine("Chicago, Chicago ... we have our own way of doing the pizza");

            Console.WriteLine("We only make Margarita Pizza - We done use the factory.");
            
            Console.WriteLine("Thanks for giving a factory ... but no use");

            return new ChicagoMargaritaPizza();
        }
    }

    public class NewYorkPizzaStore : PizzaStore
    {
        private readonly IPizzaFactory factory;

        public NewYorkPizzaStore(IPizzaFactory factory)
        {
            this.factory = factory;
        }

        public NewYorkPizzaStore()
        {
            
        }

        protected override IPizza CreatePizza(PizzaType type)
        {
            Console.WriteLine("Im from New York ... we sing before creating the pizza!");
            
            if(factory == null)
            {
                Console.WriteLine("If no factory giveng ... the pizza is ChicagoCheesePizza!");
                return new ChicagoCheesePizza();
            }

                
            return factory.createPizza(type);
        }
    }

    public class ChicagoPizzaFactory : IPizzaFactory
    {
        public IPizza createPizza(PizzaType type)
        {
            switch (type)
            {
                case PizzaType.Cheesy:
                    Console.WriteLine("Chicago Factor - Creating Pizza Cheese Chicago");
                    return new ChicagoCheesePizza();
                case PizzaType.Margarita:
                    Console.WriteLine("Chicago Factor - Creating Pizza Margarita Chicago");
                    return new ChicagoMargaritaPizza();
                case PizzaType.Pepperony:
                    Console.WriteLine("Chicago Factor - Creating Pizza Pepperony Chicago");
                    return new ChicagoPepperonyPizza();
            }
            return new ChicagoCheesePizza();
        }
    }

    public class ChicagoCheesePizza : IPizza
    {
        public void prepare()
        {
            Console.WriteLine("Chicago - Cheese Pizza - Preparing .....");
        }

        public void bake()
        {
            Console.WriteLine("Chicago - Cheese Pizza - Baking .....");
            
        }

        public void box()
        {
            Console.WriteLine("Chicago - Cheese Pizza - Boxing .....");
         
        }
    }

    public class ChicagoMargaritaPizza : IPizza
    {
        public void prepare()
        {
            Console.WriteLine("Pizza Itself - Preparing...");
        }

        public void bake()
        {
            Console.WriteLine("Pizza Itself - Baking...");
            
        }

        public void box()
        {
            Console.WriteLine("Pizza Itself - Boxing...");
            
        }
    }

    public class ChicagoPepperonyPizza : IPizza
    {
        public void prepare()
        {
            throw new NotImplementedException();
        }

        public void bake()
        {
            throw new NotImplementedException();
        }

        public void box()
        {
            throw new NotImplementedException();
        }
    }

    public class NewYorkPepperonyPizza : IPizza
    {
        public void prepare()
        {
            throw new NotImplementedException();
        }

        public void bake()
        {
            throw new NotImplementedException();
        }

        public void box()
        {
            throw new NotImplementedException();
        }
    }

    public class NewYorkMargaritaPizza : IPizza
    {
        public void prepare()
        {
            Console.WriteLine("Preparing...");
        }

        public void bake()
        {
            Console.WriteLine("Baking...");

        }

        public void box()
        {
            Console.WriteLine("Boxing...");

        }
    }
    public class NewYorkCheesePizza : IPizza
    {
        public void prepare()
        {
            throw new NotImplementedException();
        }

        public void bake()
        {
            throw new NotImplementedException();
        }

        public void box()
        {
            throw new NotImplementedException();
        }
    }

    public class NewYorkPizzaFactory : IPizzaFactory
    {
        public IPizza createPizza(PizzaType type)
        {
            switch (type)
            {
                case PizzaType.Cheesy:
                    Console.WriteLine("NY Factory - Creating Pizza Cheese New York");
                    return new NewYorkCheesePizza();
                case PizzaType.Margarita:
                    Console.WriteLine("NY Factory - Creating Pizza Margarita New York");
                    return new NewYorkMargaritaPizza();
                case PizzaType.Pepperony:
                    Console.WriteLine("NY Factory - Creating Pizza Pepperony New York");
                    return new NewYorkPepperonyPizza();

            }
            return new NewYorkCheesePizza();
        }
    }

    public abstract class PizzaStore
    {
        

        public IPizza OrderPizza(PizzaType type)
        {
            IPizza pizza;

            pizza = CreatePizza(type);

            pizza.prepare();
            pizza.bake();
            pizza.box();
            return pizza;
        }

        protected abstract IPizza CreatePizza(PizzaType type);
    }

    public interface IPizzaFactory
    {
        IPizza createPizza(PizzaType type);
    }

    public interface IPizza
    {
        void prepare();
        void bake();
        void box();
    }

    public enum PizzaType
    {
        Cheesy,
        Pepperony,
        Margarita
    }
}
