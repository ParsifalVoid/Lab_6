using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using лаба2;

namespace Лаба_5
{
    internal class Program
    {
       static void Main(string[] args)
        {
            MyProgram myProgram = new MyProgram();
            myProgram.load();
            while (true)
            {
                myProgram.Menu();
                int variant = 0;
                Boolean isWork = true;

                try
                {
                    variant = Int32.Parse(Console.ReadLine());
                    isWork = myProgram.handler(variant);
                }
                catch
                {
                    isWork = myProgram.handler(0);
                }

                if(!isWork)
                {
                    myProgram.printBinary();
                    myProgram.save();
                    myProgram.printNew_Text();
                    break;
                }
            }
        }
    }
}



