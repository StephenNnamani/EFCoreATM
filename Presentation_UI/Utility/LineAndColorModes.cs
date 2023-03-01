using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_UI.Utility
{
    public static class LineAndColorModes
    {
        public static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================");
            Console.WriteLine("WELCOME TO STEPHEN EF CORE ATM");
            Console.WriteLine("=============================================\n");
            Console.ResetColor();
        }

        public static void Services(string service)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================");
            Console.WriteLine(service.ToUpper());
            Console.WriteLine("=============================================\n");
            Console.ResetColor();
        }

        public static void Yellow(string str) 
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public static void Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public static void Cyan(string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static void AnimationSlide()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("-");
                Thread.Sleep(60);
                Cyan("-");
                Thread.Sleep(60);

            }
            Console.Clear();
        }
    }
}
