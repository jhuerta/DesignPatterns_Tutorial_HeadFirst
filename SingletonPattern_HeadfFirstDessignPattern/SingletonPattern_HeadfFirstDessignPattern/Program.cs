using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingletonPattern_HeadfFirstDessignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var boilerDarkChocolate = ChocolateBoiler.getInstance();
            var boilerWhiteChocolate = ChocolateBoiler.getInstance();
            var boilerChocolateWithOrange = ChocolateBoiler.getInstance();
            var boilerWhiteChocolateWithFruits = ChocolateBoiler.getInstance();
            var boilerDarkChocolateWithMint = ChocolateBoiler.getInstance();


            Console.WriteLine("Dark Chocolate is Empty: " + boilerDarkChocolate.isEmpty());
            Console.WriteLine("Dark White is Empty: " + boilerDarkChocolate.isEmpty());

            Console.WriteLine("Filling the Dark Chocolate Boiler");
            boilerDarkChocolate.Fill();
            Console.WriteLine("Dark Chocolate is Empty: " + boilerDarkChocolate.isEmpty());
            Console.WriteLine("Dark White is Empty: " + boilerDarkChocolate.isEmpty());

            Console.WriteLine("Emptying the White Chocolate Boiler");
            boilerWhiteChocolate.Drained();
            Console.WriteLine("Dark Chocolate is Empty: " + boilerDarkChocolate.isEmpty());
            Console.WriteLine("Dark White is Empty: " + boilerDarkChocolate.isEmpty());


        }
    }

    public class ChocolateBoiler
    {
        private static ChocolateBoiler _chocolateBoiler = CreateChocolateBoiler();
        private bool boiled = false;
        private bool empty = true;

        private ChocolateBoiler(){
        }

        private static ChocolateBoiler CreateChocolateBoiler()
        {
            Console.WriteLine("Creating an instance!!!!!!!!!!!!!!");
            return new ChocolateBoiler();
        }
        public static  ChocolateBoiler getInstance()
        {
            return _chocolateBoiler;

            if(_chocolateBoiler == null)
            {
                Console.WriteLine("Creating an instance!!!!!!!!!!!!!!");
                _chocolateBoiler = new ChocolateBoiler();
            }
            return _chocolateBoiler;
        }

        public bool isBoiled()
        {
            return boiled;
        }

        public bool isEmpty()
        {
            return empty;
        }
        public void Boil()
        {
            if( !empty && !boiled)
            {
                boiled = true;
            }
        }

        public void Fill()
        {
            if(empty && !boiled)
            {
                empty = false;
                boiled = false;
            }

        }
        public void Drained()
        {
            if(!empty && boiled)
            {
                empty = true;

            }
        }
    }
}
