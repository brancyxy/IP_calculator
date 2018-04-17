using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_calculator
{
    
    class Network
    {
        private string nev;
        private int gepszam;
        private IPcim halCim;
        private IPcim broadcast;
        private IPcim elsohost;
        private IPcim utolsohost;
        private IPcim maszk;
        private int lkh;

        public Network(string nev, uint halCim, int gepszam)
        {
            this.gepszam = gepszam;
            this.nev = nev;
            this.halCim = new IPcim(halCim);

            lkh = IP.LegkisebbKettoHatvany(gepszam);
            maszk = Maszk(gepszam);
            elsohost = new IPcim(this.halCim.decValue + 1);
            utolsohost = new IPcim((uint)(this.halCim.decValue + lkh - 2));
            broadcast = new IPcim((uint)(this.halCim.decValue + lkh - 1));
        }

        public IPcim Kiirmindent()
        {
            Console.WriteLine(nev);
            Console.WriteLine("\tHálózat azonosító cím: "+halCim.IPString);
            Console.WriteLine("\tAlhálózati maszk: "+maszk.IPString);
            Console.WriteLine("\tElső hoszt: "+elsohost.IPString);
            Console.WriteLine("\tUtolsó hoszt: "+utolsohost.IPString);
            Console.WriteLine("\tBroadcast:"+broadcast.IPString);
            return (new IPcim(this.broadcast.decValue + 1));
        }

        private IPcim Maszk(int gepszam)
        {
            uint ipcim=0;
            int i = 32-IP.HatvanyKitevo(gepszam);
            int j = 31;
            for (int k=i; k >0; k--)
            {
                ipcim += (uint)Math.Pow(2, j);
                j--;
            }
            return new IPcim(ipcim);
        }
    }
}
