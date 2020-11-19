using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLdb sQLdb = new SQLdb();
            AccountManger accountManger = new AccountManger();

            bool login = false;

            do
            {
                string username = string.Empty;
                string password = string.Empty;

                Console.WriteLine("\t1 - Login");
                Console.WriteLine("\t2 - Register\n");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        Console.Write("Enter your username: ");
                        username = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        password = Console.ReadLine();

                        if (accountManger.LogIn(username, password))
                        {
                            Console.WriteLine("Du er nu logget in");
                        }
                        else
                        {
                            Console.WriteLine("Fejl");
                        }
                        break;
                    case ConsoleKey.D2:
                        Console.Write("Create your username: ");
                        username = Console.ReadLine();
                        Console.Write("Create your password: ");
                        password = Console.ReadLine();

                        if (accountManger.Register(username, password))
                        {
                            Console.WriteLine("Du har nu lavet en konto");
                        }
                        else
                        {
                            Console.WriteLine("Fejl");
                        }
                        break;
                    default:
                        break;
                }
                Console.ReadKey(true);
                Console.Clear();
            } while (!login);
        }
    }
}
