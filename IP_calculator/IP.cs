using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_calculator
{
    public struct IPcim
    {
        public uint decValue;
        public string IPString;
        public byte IPclass;
        public IPcim(string IPFormat)
        {
            IPString = IPFormat;
            decValue = IP.GetDecValue(IPFormat);
            IPclass = IP.GetIPClass(decValue);
        }
        public IPcim(uint IPint)
        {
            decValue = IPint;
            IPString = IP.GetIPString(IPint);
            IPclass = IP.GetIPClass(decValue);
        }
    }

    class IP
    {

        public static string GetIPString(uint decValue)
        {
            if (Loopback(decValue) || decValue > 3758096384 && Notmask(decValue)) throw new Exception("Érvénytelen IP cím");
            string IPString = "";
            for (int i = 0; i < 4; i++)
            {
                int a = 3 - i;

                IPString+=Convert.ToString(decValue / (uint)Math.Pow(256, a))+'.';

                decValue -= decValue / (uint)Math.Pow(256, a) * (uint)Math.Pow(256, a);
            }
            return IPString.Remove(IPString.Length-1);
        } 

        public static uint GetDecValue(string IPString)

        {
            uint decValue = 0;

            uint[] oktetek = Array.ConvertAll(IPString.Split('.'), uint.Parse);


            if (oktetek.Length != 4) throw new Exception("4 oktet kell ide");
            if (oktetek[0] == 127 || oktetek[0] > 224) throw new Exception("Érvényetelen 1. oktet");

            for (int i = 0; i < oktetek.Length; i++)
            {
                if (oktetek[i] < 0 || oktetek[i] > 255) throw new Exception("Az oktetek nagysága csak 0-255 lehet.");

                int a = 3 - i;

                decValue += oktetek[i] * (uint)Math.Pow(256, a);

            }

                return decValue;
        }

        public static int LegkisebbKettoHatvany(int gepszam)
        {
            int i = gepszam+3;
            int maszk = 2;
            while (maszk < i)
            {
                maszk *= 2;
            }
            return maszk;
        }

        public static byte HatvanyKitevo(int gepszam)
        {
            byte lg = 1;
            int i = gepszam + 3;
            int maszk = 2;
            while (maszk < i)
            {
                maszk *= 2;
                lg++;
            }
            return lg;
        }

        public static byte GetIPClass(uint decValue)
        {
            if (decValue < 2113929216) return 1;
            else if (decValue < 3221225472) return 2;
            else return 3;
        }
        private static bool Loopback(uint decValue)
        {
            if (decValue > 2130706432 && decValue < 2147483648) return true;
            else return false;
        }

        private static bool Notmask(uint decValue)
        {
            if (decValue > 4278190080)return false;
            else return true;
        }
    }
}
