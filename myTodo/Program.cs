using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace myTodo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> todos = new List<string>();  //Vytvoření prázdného seznamu

            if (File.Exists("todos.txt")) // Načtení úkolů ze souboru todos.txt, pokud už existuje. Pokud ne, tak se vytvoří
            {
                todos = new List<string>(File.ReadAllLines("todos.txt"));
            }
            else
            {
                using FileStream fs = File.Create("todos.txt"); ;
            }
            while (true) // začátek smyčky programu
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("------------------------------");
                Console.WriteLine($"-          todo              -");
                Console.WriteLine("------------------------------");
                Console.ResetColor();

                //Vypsání úkolů nebo hláška, že je prázdný
                Console.WriteLine("Tvoje úkoly : \n");
                Console.ForegroundColor = ConsoleColor.Blue;
                if (todos.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Tvůj seznam úkolů je prázdný.");
                }
                else
                {
                    for (int i = 0; i < todos.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "." + todos[i]);
                    }
                }
                Console.ResetColor();

                Console.WriteLine("\nPomocí příkazů : \"přidat\", \"upravit\", \"hotovo\" můžeš měnit tvůj seznam úkolů.\nPro ukončení programu napiš \"konec\"");
                string userAction = Console.ReadLine().Trim();

                // blok if-else pro větvení programu podle zadaných příkazů + zachycení případných výjimek
                if (userAction.StartsWith("přidat"))
                {
                    try
                    {
                        string todo = userAction.Substring(7);
                        todos.Add(todo);
                        File.WriteAllLines("todos.txt", todos);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Za slovo \"přidat\" a mezeru piš nový úkol");
                        Thread.Sleep(2500);
                    }
                }
                else if (userAction.StartsWith("upravit"))
                {
                    try
                    {
                        int number = int.Parse(userAction.Substring(7));
                        Console.WriteLine(number);
                        if (number >= 0 && number <= todos.Count)
                        {
                            Console.WriteLine("Napiš nový úkol : ");
                            string newTodo = Console.ReadLine();
                            todos[number - 1] = newTodo;
                            File.WriteAllLines("todos.txt", todos);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Takové číslo úkolu neexistuje.");
                            Thread.Sleep(2500);
                        }
                    }
                    catch(FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Za příkaz \"upravit\" napiš číslo úkolu, který chceš upravovat");
                        Thread.Sleep(2500);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Za příkaz \"upravit\" napiš číslo úkolu, který chceš upravovat");
                        Thread.Sleep(2500);
                    }
                }
                else if (userAction.StartsWith("hotovo"))
                {
                    try
                    {
                       int number = int.Parse(userAction.Substring(7));
                        if (number >0 && number <= todos.Count)
                        {
                            string todoToRemove = todos[number - 1];
                            todos.RemoveAt(number - 1);
                            Console.WriteLine($"Úkol \"{todoToRemove}\" byl odstraněn.");
                            File.WriteAllLines("todos.txt", todos);
                            Thread.Sleep(2500);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Takové číslo úkolu neexistuje.");
                            Thread.Sleep(2500);
                        }
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Neplatné číslo");
                        Thread.Sleep(2500);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Za příkaz \"hotovo\" napiš číslo úkolu, který máš hotový.");
                        Thread.Sleep(2500);
                    }
                }
                else if (userAction.StartsWith("konec"))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("neplatné zadání! Zkuste znovu.\n");
                    Thread.Sleep(2500);
                }
            }
        }

        private static void Catch(FormatException formatException)
        {
            throw new NotImplementedException();
        }
    }
}