using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Instalock
{
    internal class Logging
    {
        public static void PrintLogo()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        ██╗███╗░░██╗░██████╗████████╗░█████╗░██╗░░░░░░█████╗░░█████╗░██╗░░██╗", ColorTranslator.FromHtml("#F9F92B"));
            Console.WriteLine("        ██║████╗░██║██╔════╝╚══██╔══╝██╔══██╗██║░░░░░██╔══██╗██╔══██╗██║░██╔╝", ColorTranslator.FromHtml("#F5ED2D"));
            Console.WriteLine("        ██║██╔██╗██║╚█████╗░░░░██║░░░███████║██║░░░░░██║░░██║██║░░╚═╝█████═╝░", ColorTranslator.FromHtml("#F0DB30"));
            Console.WriteLine("        ██║██║╚████║░╚═══██╗░░░██║░░░██╔══██║██║░░░░░██║░░██║██║░░██╗██╔═██╗░", ColorTranslator.FromHtml("#EDCF31"));
            Console.WriteLine("        ██║██║░╚███║██████╔╝░░░██║░░░██║░░██║███████╗╚█████╔╝╚█████╔╝██║░╚██╗", ColorTranslator.FromHtml("#E7BE34"));
            Console.WriteLine("        ╚═╝╚═╝░░╚══╝╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝╚══════╝░╚════╝░░╚════╝░╚═╝░░╚═╝", ColorTranslator.FromHtml("#E2AC36"));
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void Log(string message, string color = "#fbff2b")
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            Console.Write($"     [{time}]", ColorTranslator.FromHtml("#e0a338"));
            Console.Write(" => ", ColorTranslator.FromHtml("#ffffff"));
            Console.WriteLine($"{message}", ColorTranslator.FromHtml(color));
        }

        public static void Input(string message = "", string color = "#fbff2b")
        {
            if (string.IsNullOrEmpty(message))
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                Console.Write($"     [{time}]", ColorTranslator.FromHtml("#e0a338"));
                Console.Write(" => ", ColorTranslator.FromHtml("#ffffff"));
            }
            else
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                Console.Write($"     [{time}]", ColorTranslator.FromHtml("#e0a338"));
                Console.Write(" => ", ColorTranslator.FromHtml("#ffffff"));
                Console.WriteLine($"{message}", ColorTranslator.FromHtml(color));
                Console.Write($"     [{time}]", ColorTranslator.FromHtml("#e0a338"));
                Console.Write(" => ", ColorTranslator.FromHtml("#ffffff"));
            }
        }
    }
}