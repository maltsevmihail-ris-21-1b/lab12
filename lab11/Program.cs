using OOP__lab10;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace lab12
{
    enum MainMenu
    {
        Add = 1,
        Delete,
        Clear,
        Exit
    }
    enum AddMenu
    {
        Engine = 1,
        TurbojetEngine,
        InternalCombustionEngine,
        DiselEngine,
        Exit
    }

    enum DelMenu
    {
        Value = 1,
        Index,
        Exit
    }

    internal class Program
    {
        public static CycledList<Engine> list = new CycledList<Engine>();
        public static CycledList<Engine> newList = new CycledList<Engine>();
        public static Random rand = new Random();
        public static string[] FUELS = new string[] { "summer", "winter", "arctic" };
        static void Main(string[] args)
        {
            ShowMenu();
        }
        public static void ShowMenu()
        {
            bool extFlag = false;
            
            while (!extFlag)
            {
                Console.Clear();
                PrintList();
                Console.WriteLine(" 1 - Добавить элемент в список");
                Console.WriteLine(" 2 - Удалить элемент из списка");
                Console.WriteLine(" 3 - Очистить список");
                Console.WriteLine(" 4 - Выход");
                int ans = UserInteractor.GetFromUser("Выберите действие [1-4]");
                switch ((MainMenu)ans)
                {
                    case MainMenu.Add:
                        {
                            AddElement();
                            break;
                        }
                    case MainMenu.Delete:
                        {
                            DelElement();
                            break;
                        }
                    case MainMenu.Clear:
                        {
                            list.Clear();
                            //newList = list.Copy();
                            //newList.Remove(0);
                            break;
                        }
                    case MainMenu.Exit:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Try again!");
                            break;
                        }
                }
            }
        }
        public static void PrintList()
        {
            foreach (Engine engine in list)
            {
                Console.WriteLine(engine.ToString());
            }
        }
        public static void AddElement()
        {
            bool extFlag = false;
            while (!extFlag)
            {
                Console.WriteLine();
                Console.WriteLine(" 1 - Добавить Engine");
                Console.WriteLine(" 2 - Добавить TurbojetEngine");
                Console.WriteLine(" 3 - Добавить InternalCombustionEngine");
                Console.WriteLine(" 4 - Добавить DiselEngine");
                Console.WriteLine(" 5 - Назад");
                int ans = UserInteractor.GetFromUser("Выберите действие [1-5]");
                switch ((AddMenu)ans)
                {
                    case AddMenu.Engine:
                        {
                            list.Add(new Engine(rand.Next(10000)));
                            break;
                        }
                    case AddMenu.TurbojetEngine:
                        {
                            list.Add(new TurbojetEngine(rand.Next(1000), rand.Next(500)));
                            break;
                        }
                    case AddMenu.InternalCombustionEngine:
                        {
                            list.Add(new InternalCombustionEngine(rand.Next(100000), rand.Next(50000000)));
                            break;
                        }
                    case AddMenu.DiselEngine:
                        {
                            list.Add(new DiselEngine(rand.Next(100000), FUELS[rand.Next(FUELS.Length)]));
                            break;
                        }
                    case AddMenu.Exit:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Try again!");
                            break;
                        }
                }
            }
        }
        public static void DelElement()
        {
            bool extFlag = false;
            while (!extFlag)
            {
                Console.WriteLine();
                Console.WriteLine(" 1 - Удалить по значению");
                Console.WriteLine(" 2 - Удалить по индексу");
                Console.WriteLine(" 3 - Назад");
                int ans = UserInteractor.GetFromUser("Выберите действие [1-3]");
                switch ((DelMenu)ans)
                {
                    case DelMenu.Value:
                        {
                            int weight = UserInteractor.GetFromUser("Введите вес для удаления: ");
                            if (!list.Remove(new Engine(weight)))
                                Console.WriteLine("Something went wrong!");
                            else
                                Console.WriteLine("Success!");
                            break;
                        }
                    case DelMenu.Index:
                        {
                            int index = UserInteractor.GetFromUser("Введите индекс для удаления: ");
                            if (!list.Remove(index))
                                Console.WriteLine("Something went wrong!");
                            else
                                Console.WriteLine("Success!");
                            break;
                        }
                    case DelMenu.Exit:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Try again!");
                            break;
                        }
                }
            }
        }
    }
}