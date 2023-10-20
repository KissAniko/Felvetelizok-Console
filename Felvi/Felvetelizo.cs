using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felvi
{
    internal class Felvetelizo
    {
        string nev;
        char nem;
        int pontszam;
        string szak;
        string nyelvVizsga;
        public int xy;
        public static int xy2;

        public Felvetelizo(string[] strings)
        {
            Strings = strings;
        }

        public Felvetelizo(string nev, char nem, int pontszam, string szak, string nyelvVizsga)
        {
            this.Nev = nev;
            this.Nem = nem;
            this.Pontszam = pontszam;
            this.Szak = szak;
            this.NyelvVizsga = nyelvVizsga;
        }

        public static void Hatar()
        {
            Console.WriteLine("A ponthatár: ");
        }

        public string Nev { get => nev; set => nev = value; }
        public char Nem { get => nem; set => nem = value; }
        public int Pontszam { get => pontszam; set => pontszam = value; }
        public string Szak { get => szak; set => szak = value; }
        public string NyelvVizsga { get => nyelvVizsga; set => nyelvVizsga = value; }
        public string[] Strings { get; }



        // Property (tulajdonság) ---> Egy paraméter nélküli függvény, vagy eljárás.
        // Ebben az esetben nem kell zárójelet alkalmazni a meghívás során. Lsd Program.cs (Első eset)
        public string NyelvvizsgaSzoveg1
        { get
            {
                if (NyelvVizsga!= "Nincs")
                    return NyelvVizsga + "fokú";
                return "Nincs";
            }
                
        }

        // Ebben az esetben függvényként írjuk meg. A különbség annyi, hogy az előzőben 
        // nem volt zárójel és volt get. Ebben van zárójel és nem kell get.
        // A Program.cs-ben mindkettőt másképp hívjuk meg.  (Ez a második eset)

        public string NyelvvizsgaSzoveg2()
        {

            if (NyelvVizsga != "Nincs")
                return NyelvVizsga + "fokú";
            return "Nincs";
        }

    }
}
