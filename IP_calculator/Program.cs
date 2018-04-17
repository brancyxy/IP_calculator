using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_calculator
{

    class Program
    {
        static IPcim kezdoIP;
        static List<int> irodaGepSzamok = new List<int>();
        static List<Network> halozatok = new List<Network>();
        static int irsz;
        static int rsz;

        static void Main(string[] args)
        {
            Input();
            Ellenoriz();
            Console.Clear();
            RouterHalozatok();
            Kiir();
            Console.ReadKey(true);
            Console.Clear();
        }

        private static void Kiir()
        {
            uint halCim = kezdoIP.decValue;
            for (int i = 0; i < irsz; i++)
            {
                halozatok.Add(new Network((i + 1) + ". iroda hálózat", halCim, irodaGepSzamok[i]));
                halCim = halozatok[i].Kiirmindent().decValue;
            }
            for (int i = irsz; i < irsz+rsz; i++)
            {
                halozatok.Add(new Network((i + 1) + ". router hálózat", halCim, irodaGepSzamok[i]));
                halCim = halozatok[i].Kiirmindent().decValue;
            }
        }

        private static void Ellenoriz()
        {
            int sum = 0; 
            foreach (var irsz in irodaGepSzamok)
            {
                sum += IP.LegkisebbKettoHatvany(irsz);   
            }
            if (sum > Math.Pow(256, 4 - kezdoIP.IPclass)) Console.WriteLine("Sum overclockin ya got there");
        }

        private static void Input()
        {
            bool done = false;
            while (!done)
            {
                irodaGepSzamok.Clear();
                Console.Clear();
                try
                {
                    Console.WriteLine("Input 1: irodaszám:");
                    irsz = int.Parse(Console.ReadLine());

                    Console.WriteLine("Input 2: router szám");
                    rsz = int.Parse(Console.ReadLine()) - 1;

                    for (int i = 0; i < irsz; i++)
                    {
                        Console.WriteLine(i + 1 + ". iroda gépszáma:");
                        irodaGepSzamok.Add(int.Parse(Console.ReadLine()));
                    }

                    Console.WriteLine("IP cím:");
                    kezdoIP = new IPcim(Console.ReadLine());

                    done = true;
                }

                catch (Exception e)
                {
                    Console.WriteLine("Szám az szám, IP az IP (XXX.XXX.XXX.XXX), prefix 8-32\n" + e.Message);
                    Console.ReadKey();
                }
            }
            irodaGepSzamok.Sort();
            irodaGepSzamok.Reverse();
        }

        private static void RouterHalozatok()
        {
            for (int i = 0; i < rsz; i++)
            {
                irodaGepSzamok.Add(1);
            }
        }
    }
}
