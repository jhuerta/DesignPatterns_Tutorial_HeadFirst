using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Iterator_CompositePattern_HeadFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var dinnerMenu = new DinnerMenu();
            var breakfastMenu = new BreakfastMenu();
            var  cafeMenu = new CafeMenu();


            MenuComponent breakfastCMenu = new Menu("PANCAKE HOUSE", "Breakfast");
            MenuComponent dinnerCMenu = new Menu("FINE DINING ITALIAN", "Dinner");
            MenuComponent coffeeCMenu = new Menu("THE TEA HOSE", "Tea Time");
            MenuComponent foodCourtMenu = new Menu("THE FOOD COURT - 24 Hours", "All day food");

            foodCourtMenu.add(breakfastCMenu);
            foodCourtMenu.add(dinnerCMenu);
            foodCourtMenu.add(coffeeCMenu);

            MenuItem pasta = new MenuItem(5, "Lunch: Pasta", "Pasta", false);
            MenuItem soup = new MenuItem(10, "Lunch: Soup", "Soup", true);
            MenuItem salad = new MenuItem(15, "Lunch: Salda", "Salda", true);

            MenuItem tea = new MenuItem(6, "Tea Time: Tea", "Tea", true);
            MenuItem biscuit = new MenuItem(9, "Tea Time: Biscuit", "Biscuit", false);
            MenuItem cake = new MenuItem(12, "Tea Time: Cake", "Cake", false);

            MenuItem coffee = new MenuItem(11, "Breakfast: Coffee", "Coffee", true);
            MenuItem fruit = new MenuItem(22, "Breakfast: Fruit", "Fruit", true);
            MenuItem muesly = new MenuItem(33, "Breakfast: Muesly", "Muesly", false);

            breakfastCMenu.add(coffee);
            breakfastCMenu.add(fruit);
            breakfastCMenu.add(muesly);

            dinnerCMenu.add(pasta);
            dinnerCMenu.add(soup);
            dinnerCMenu.add(salad);

            coffeeCMenu.add(tea);
            coffeeCMenu.add(biscuit);
            coffeeCMenu.add(cake);

            WaitressComposite superWaittress = new WaitressComposite(foodCourtMenu);

            superWaittress.printMenu();

            Console.ReadLine();
            
            var javaWaitress = new JavaWaitressIterator(breakfastMenu, dinnerMenu, cafeMenu);
            
            var dinnerOrderId = javaWaitress.AddMenu(dinnerMenu);
            var breakfastOrderId = javaWaitress.AddMenu(breakfastMenu);
            var cafeOrderId = javaWaitress.AddMenu(cafeMenu);

            javaWaitress.PrintOrder(dinnerOrderId);
            javaWaitress.PrintOrder(breakfastOrderId);
            javaWaitress.PrintOrder(cafeOrderId);

            javaWaitress.PrintOrders();

            javaWaitress.PrintOrdersWithIterators();

            Console.ReadLine();
            
            return;

            Console.WriteLine("Testing New Cafe Menu added");

            javaWaitress.printCafeMenu_WithEnumerator();

            javaWaitress.printCafeMenu_WithIterator();

            Console.ReadLine();
            return;

            Console.WriteLine("\nPrinting BREAKFAST MENU_____________________");
            javaWaitress.printBreakfastMenu_WithEnumerator();

            Console.WriteLine("\nPrinting LUNCH MENU_____________________");
            javaWaitress.printDinnerMenu_WithEnumerator();

            Console.WriteLine("\nPrinting ALL menu_____________________");
            javaWaitress.printMenu();

            Console.WriteLine("\nPrinting vegetarian dishes_____________________");

            Console.WriteLine("Item {0} is vegetarian: {1}", "Set A", javaWaitress.isItemVegetarian("Set A"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Set B", javaWaitress.isItemVegetarian("Set B"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Set C", javaWaitress.isItemVegetarian("Set C"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Set D", javaWaitress.isItemVegetarian("Set D"));

            Console.WriteLine("Item {0} is vegetarian: {1}", "Dinner Set A", javaWaitress.isItemVegetarian("Dinner Set A"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Dinner Set B", javaWaitress.isItemVegetarian("Dinner Set B"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Dinner Set C", javaWaitress.isItemVegetarian("Dinner Set C"));
            Console.WriteLine("Item {0} is vegetarian: {1}", "Dinner Set D", javaWaitress.isItemVegetarian("Dinner Set D"));
            
            Console.ReadLine();
        }

        public abstract class MenuComponent
        {
            public virtual void add(MenuComponent menuComponent)
            {
                throw new NotImplementedException();
            }

            public virtual void remove(MenuComponent menuComponent)
            {
                throw new NotImplementedException();
            }

            public virtual MenuComponent getChild(int childId)
            {
                throw new NotImplementedException();
            }

            public string getName()
            {
                throw new NotImplementedException();
            }

            public string getDescription()
            {
                throw new NotImplementedException();
            }

            public double getPrice()
            {
                throw new NotImplementedException();
            }

            public bool isVegetarian()
            {
                throw new NotImplementedException();
            }

            public virtual void print()
            {
                throw new NotImplementedException();
            }

        }

        public class WaitressComposite
        {
            private MenuComponent _menus;
            public WaitressComposite(MenuComponent menus)
            {
                this._menus = menus;
            }
            public void printMenu()
            {
                _menus.print();
            }
        }

        public interface IIterator
        {
            bool hasNext();
            Object next();
        }
        public class DinnerIterator : IIterator
        {
            private int currentPosition = 0;
            private MenuItem[] dinnerMenuItems;
            

            public DinnerIterator(DinnerMenu dinnerMenu)
            {
                dinnerMenuItems = dinnerMenu.getMenuItems();
            }

            public bool hasNext()
            {
                return (currentPosition < dinnerMenuItems.Count());
            }

            public object next()
            {
                object currentItem = dinnerMenuItems[currentPosition];
                currentPosition++;
                return currentItem;
            }
        }
        public class BreakfastIterator : IIterator
        {
            private int currentPosition = 0;
            private readonly ArrayList breakfastMenuItems;


            public BreakfastIterator(BreakfastMenu breakfastMenu)
            {
                breakfastMenuItems = breakfastMenu.getMenuItems();
            }

            public bool hasNext()
            {
                return (currentPosition < breakfastMenuItems.Count);
            }

            public object next()
            {
                object currentItem = breakfastMenuItems[currentPosition];
                currentPosition++;
                return currentItem;
            }

        }
        public class MenuItem : MenuComponent
        {
            private readonly bool vegetarian;
            private readonly string name;
            private readonly string description;
            private readonly double price;

          
            public MenuItem(double price, string description, string name, bool vegetarian)
            {
                this.price = price;
                this.description = description;
                this.name = name;
                this.vegetarian = vegetarian;
            }

            public string getName()
            {
                return name;
            }

            public string getDescription()
            {
                return description;
            }

            public double getPrice()
            {
                return price;
            }

            public bool isVegetarian()
            {
                return vegetarian;
            }

            public override void print()
            {
                Console.WriteLine(" {0},", getName());
                if(isVegetarian())
                {
                    Console.Write("(v)");
                }
                Console.Write(", {0}", getPrice());
                Console.Write("    -- {0}", getDescription());
            }

        }

        public class Menu : MenuComponent
        {
            private IList<MenuComponent> menuComponents = new List<MenuComponent>();
            private String name;
            private String description;


            public Menu(string description, string name)
            {
                this.name = name;
                this.description = description;
            }

            public override void add(MenuComponent menuCoponent)
            {
                menuComponents.Add(menuCoponent);
            }

            public override void remove(MenuComponent menuCoponent)
            {
                menuComponents.Remove(menuCoponent);
            }

            public override MenuComponent getChild(int i)
            {
                return menuComponents.ElementAt(i);
            }

            public new string getName()
            {
                return name;
            }

            public new string getDescription()
            {
                return description;
            }

            public override void print()
            {
                Console.Write("\n" + getName());
                Console.Write(", " + getDescription());
                Console.Write("--------------------");

                foreach (var menuComponent in menuComponents)
                {
                    menuComponent.print();
                }
            }
        }

        public class JavaWaitressIterator
        {
            private readonly IMenuEnumerable _breakfastMenu;
            private readonly IMenuEnumerable _dinnerMenu;
            private readonly IMenuEnumerable _cafeMenu;
            private IList<IMenuEnumerable> orders;

            public JavaWaitressIterator(IMenuEnumerable breakfast, IMenuEnumerable dinnerMenu, IMenuEnumerable cafeMenu)
            {
                orders = new List<IMenuEnumerable>();

                _breakfastMenu = breakfast;
                _dinnerMenu = dinnerMenu;
                _cafeMenu = cafeMenu;
            }

            public void printMenu()
            {
                printDinnerMenu_WithEnumerator();
                printBreakfastMenu_WithEnumerator();
            }

            public void print(IIterator iterator)
            {
                while (iterator.hasNext())
                {
                    PrintMenuItem(iterator.next());
                }
            }

            public void print(IEnumerator enumerator)
            {
                while (enumerator.MoveNext())
                {
                    PrintMenuItem(enumerator.Current);
                }
            }

            public void printBreakfastMenu_WithEnumerator()
            {
                print(_breakfastMenu.createEnumerator());
            }

            public void printDinnerMenu_WithEnumerator()
            {
                print(_dinnerMenu.createEnumerator());
            }

            public void printCafeMenu_WithEnumerator()
            {
                print(_cafeMenu.createEnumerator());
            }


            public void printBreakfastMenu_WithIterator()
            {
                print(_breakfastMenu.createIterator());
            }

            public void printDinnerMenu_WithIterator()
            {
                print(_dinnerMenu.createIterator());
            }

            public void printCafeMenu_WithIterator()
            {
                print(_cafeMenu.createIterator());
            }

            private void PrintMenuItem(object menuItemObj)
            {
                var menuItem = ConvertToMenuItem(menuItemObj);

                Console.Write("Name: {0} ", menuItem.getName());
                Console.Write("Desc: {0} ", menuItem.getDescription());
                Console.Write("$: {0} ", menuItem.getPrice());
                Console.Write("IsVeg: {0} \n", menuItem.isVegetarian());
            }

            private MenuItem ConvertToMenuItem(object possibleMenuItemObj)
            {
                if (possibleMenuItemObj.GetType().Name == typeof (MenuItem).Name)
                {
                    return (MenuItem) possibleMenuItemObj;
                }

                throw new Exception("No MenuType object!");
            }


            public bool isItemVegetarian(string name)
            {
                var dinnerMenuEnumerator = _dinnerMenu.createEnumerator();
                var breakfastMenuEnumerator = _breakfastMenu.createEnumerator();

                while (dinnerMenuEnumerator.MoveNext())
                {
                    if (CheckNameAndVegetarian((MenuItem)dinnerMenuEnumerator.Current, name))
                    {
                        return true;
                    }
                }

                while (breakfastMenuEnumerator.MoveNext())
                {
                    if (CheckNameAndVegetarian((MenuItem)breakfastMenuEnumerator.Current, name))
                    {
                        return true;
                    };
                }

                return false;
            }
        
            private bool CheckNameAndVegetarian(MenuItem menuItem,string name)
            {
                if (menuItem.getName() == name)
                    return menuItem.isVegetarian();

                return false;
            }

            public int AddMenu(IMenuEnumerable menu)
            {
                orders.Add(menu);
                return orders.Count-1;
            }

            public void PrintOrder(int menuId)
            {
                Console.WriteLine("\nMENU: {0} _________________ \n", orders[menuId].GetType().Name);
                print(orders[menuId].createEnumerator());
            }

            public void PrintOrders()
            {
                foreach (var order in orders)
                {
                    Console.WriteLine("\nNEW MENU: {0} _________________ \n",order.GetType().Name);
                    print(order.createIterator());
                }
            }

            public void PrintOrdersWithIterators()
            {
                var ordersIterator = orders.GetEnumerator();
                while(ordersIterator.MoveNext())
                {
                    Console.WriteLine("\nNEW MENU: {0} _________________ \n", ordersIterator.Current.GetType().Name);
                    print(ordersIterator.Current.createEnumerator());
                }
            }
        }

        public class CafeMenu : IMenuEnumerable
        {
            private readonly Hashtable menuItems = new Hashtable();
            public CafeMenu()
            {
                addItem("1. Coffee Item A", "Coffee and toast breakfast", true, 3);
                addItem("2. Coffee Item A.1", "Coffee and toast with extra egg breakfast", false, 4);
                addItem("3. Coffee Item A.2", "Coffee, juics and sausage", false, 5);
                addItem("4. Coffee Item B", "Tea, juics and sausage", true, 6);
                addItem("5. Coffee Item B.1", "Cereals, juice and cup of cofee", false, 3);
                addItem("6. Coffee Item B.2", "Muesly, fruits and tea", true, 5);
                addItem("7. Coffee Item C.1", "Muesly, fruits and tea", true, 5);
                addItem("8. Coffee Item C.2", "Muesly, fruits and tea", true, 5);
                addItem("9. Coffee Item D.1", "Muesly, fruits and tea", true, 5);
                addItem("10. Coffee Item D.2", "Muesly, fruits and tea", true, 5);
                addItem("11. Coffee Item E.1", "Muesly, fruits and tea", true, 5);
                addItem("12. Coffee Item E.2", "Muesly, fruits and tea", true, 5);
            }

            private void addItem(string name, string description, bool isVegetarian, int price)
            {
                var menuItem = new MenuItem(price, description, name, isVegetarian);
                menuItems.Add(menuItem.getName(),menuItem);
            }

            public Hashtable getItems()
            {
                return menuItems;
            }

            public IIterator createIterator()
            {
                return new CafeMenuIterator(this);
            }

            public IEnumerator createEnumerator()
            {
                // Either Using the Values getEnumerator (part of the class)
                return getItems().Values.GetEnumerator();

                // Or we can create our own Enumerator!!!!!!
                //return new CafeMenuEnumerator(this);
            }
        }


        public class JavaWaitress
        {
            private BreakfastMenu breakFast;
            private DinnerMenu lunchMenu;
            private ArrayList breakFastItems;
            private MenuItem[] lunchItems;

            public JavaWaitress(BreakfastMenu breakFast, DinnerMenu lunchMenu)
            {
                this.breakFast = breakFast;
                this.lunchMenu = lunchMenu;
                breakFastItems = breakFast.getMenuItems();
                lunchItems = lunchMenu.getMenuItems();
            }

            public void printMenu()
            {
                Console.WriteLine("Lunch Menu");
                printDinnerMenu();

                Console.WriteLine("Breakfast Menu");
                printBreakfastMenu();
            }

            public void printBreakfastMenu()
            {
                for (int i = 0; i < breakFastItems.Count; i++)
                {
                    var breakFastItem = (MenuItem) breakFastItems[i];
                    Console.WriteLine("Name: {0}", breakFastItem.getName());
                    Console.WriteLine("Description: {0}", breakFastItem.getDescription());
                    Console.WriteLine("Price: {0}", breakFastItem.getPrice());
                    Console.WriteLine("Is Vegetarian: {0}", breakFastItem.isVegetarian());
                }
            }

            public void printDinnerMenu()
            {
                for (int i = 0; i < lunchItems.Count(); i++)
                {
                    var lunchItem = lunchItems[i];
                    if (lunchItem == null)
                    {
                        continue;
                    }
                    Console.WriteLine("Name: {0}", lunchItem.getName());
                    Console.WriteLine("Description: {0}", lunchItem.getDescription());
                    Console.WriteLine("Price: {0}", lunchItem.getPrice());
                    Console.WriteLine("Is Vegetarian: {0}", lunchItem.isVegetarian());
                }
            }

            public bool isItemVegetarian(string name)
            {
                for (int i = 0; i < lunchItems.Count(); i++)
                {
                    var menuItem = lunchMenu.getMenuItems()[i];
                    if (menuItem == null)
                    {
                        continue;
                    }
                    if (menuItem.getName() == name)
                        return menuItem.isVegetarian();
                }

                foreach (MenuItem menuItem in breakFastItems)
                {
                    if (menuItem.getName() == name)
                        return menuItem.isVegetarian();
                }

                return false;
            }
        }

        public interface IMenuEnumerable
        {
            IIterator createIterator();
            IEnumerator createEnumerator();
        }
        public class BreakfastMenu : IMenuEnumerable
        {
            private ArrayList menuItems;

            public IIterator createIterator()
            {
                return new BreakfastIterator(this);
            }

            public IEnumerator createEnumerator()
            {
                return menuItems.GetEnumerator();
            }

            public BreakfastMenu()
            {
                menuItems = new ArrayList();

                addItem("Set A", "Kids breakfast", true, 3);
                addItem("Set B", "Teenagers meal", false, 6);
                addItem("Set C", "Adults meal", false, 30);
                addItem("Set D", "Couples set", false, 15);
            }

            private void addItem(string name, string description, bool vegetarian, double price)
            {
                var menuItem = new MenuItem(price, description, name, vegetarian);
                menuItems.Add(menuItem);
            }

            public ArrayList getMenuItems()
            {
                return menuItems;
            }
        }

        public class DinnerMenu : IMenuEnumerable
        {
            private const int MAX_ITEMS = 4;
            private int numberOfItems = 0;
            private readonly MenuItem[] menuItems;


            public IIterator createIterator()
            {
                return new DinnerIterator(this);
            }

            public IEnumerator createEnumerator()
            {
                return menuItems.GetEnumerator();
            }

            public DinnerMenu()
            {
                menuItems = new MenuItem[MAX_ITEMS];

                addItem("Dinner Set A", "Tiny dinner", true, 33);
                addItem("Dinner Set B", "Supppper dinner", false, 11);
                addItem("Dinner Set C", "Just simple dinner", false, 22);
                addItem("Dinner Set D", "Light dinner", false, 10);
            }

            private void addItem(string name, string description, bool vegetarian, double price)
            {
                if (numberOfItems > MAX_ITEMS)
                    throw new Exception("Max number of items added");

                var menuItem = new MenuItem(price, description, name, vegetarian);

                menuItems[numberOfItems] = menuItem;
                numberOfItems++;
            }

            public MenuItem[] getMenuItems()
            {
                return menuItems;
            }
        }

        public class CafeMenuIterator : IIterator
        {
            private IEnumerator keyItemsEnumerator;
            private bool _hasNext;
            private MenuItem _next = new MenuItem(0, string.Empty, string.Empty, false);
            private Hashtable _cafeMenuItems;


            public CafeMenuIterator(CafeMenu cafeMenuItems)
            {
                _cafeMenuItems = cafeMenuItems.getItems();
                keyItemsEnumerator = cafeMenuItems.getItems().Keys.GetEnumerator();
                _next = MoveCursor();
            }

            public bool hasNext()
            {
                return _hasNext;
            }

            public object next()
            {
                var currentObject = _next;

                _next = MoveCursor();

                return currentObject;
            }

            private MenuItem MoveCursor()
            {
                _hasNext = keyItemsEnumerator.MoveNext();
                
                if (_hasNext)
                    return (MenuItem) _cafeMenuItems[keyItemsEnumerator.Current];
                
                return _next;
            }
        }



        public class CafeMenuEnumerator : IEnumerator
        {
            private readonly Hashtable items;
            private readonly IEnumerator keyEnumerator;

            public CafeMenuEnumerator(CafeMenu cafeMenu)
            {
                items = cafeMenu.getItems();
                keyEnumerator = items.Keys.GetEnumerator();
            }

            public bool MoveNext()
            {
                return keyEnumerator.MoveNext();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            object IEnumerator.Current
            {
                get { return items[keyEnumerator.Current]; }
            }
        }

    }

    
}
