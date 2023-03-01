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

        public static void Yellow() 
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void Cyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static void AnimationSlide()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("-");
                Thread.Sleep(60);
                Yellow();
                Console.Write("-");
                Thread.Sleep(60);

            }
            Console.Clear();
        }
    }
}
