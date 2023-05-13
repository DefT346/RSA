using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAEDS
{
    internal class Interface
    {
        public static void Run()
        {
            while (true)
            {
                Console.Write("\n> ");
                Commands.Parse(Console.ReadLine());
            }
        }
    }
}
