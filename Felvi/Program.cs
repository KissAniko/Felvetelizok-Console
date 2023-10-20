using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Felvi
{
    internal class Program
    {

        //  static List<Felvetelizo> datas2;


        static List<Felvetelizo> Betoltes(string fajlNev)
        {

//---------------------------------- Egyik betöltés --------------------------------------------------


            /*       List<Felvetelizo> list = new List<Felvetelizo>();

                   var datas = File.ReadAllLines(fajlNev);      
                   foreach (var data in datas)
                   {
                      list.Add(new Felvetelizo(data.Split(',')));
                   }
                  return list; */



//---------------------------------- másik féle betöltés  -----------------------------------------------
            /*    
                     List <Felvetelizo> felvetelizok = new List<Felvetelizo>();
            //       var datas = File.ReadAllLines("felvi.txt").Skip(',');    // ... most nincs fejléc

                     var datas = File.ReadAllLines("felvi.txt");       
                     foreach(var data in datas)
                     {
                         felvetelizok.Add(new Felvetelizo(data.Split(','))); 
                     }
             */
//---------------------------------------- harmadik féle betöltés-----------------------------------------

            /*
                           datas2 = new List<Felvetelizo>();
                            foreach (var item in File.ReadAllLines("felvi.txt", Encoding.UTF8))  
                            {
                                string[] sor = item.Split(',');
                                string nev = sor[0];
                                char nem = Convert.ToChar(sor[1]);
                                int pontszam = Convert.ToInt32(sor[2]);
                                string szak = sor[3];
                                string nyelvVizsga = sor[4];
                            }
            */
 //----------------------------------- LINQ betöltés -----------------------------------------------------------

            /*     return File.ReadAllLines(fajlNev).Skip(1).ToList().Select(x => new Felvetelizo(x.Split(','))).ToList();  // ki van véve a fejléc,
                                                                                                                          //ami jelen esetben nincs az
                                                                                                                       // adatbázisban ezért ---> 
            */
            return File.ReadAllLines(fajlNev).ToList()                 // kilistázzuk a fájl sorait
              .Select(x => new Felvetelizo(x.Split(','))).ToList();    // a Select áttransformálja objektumlistává, amit újra 
                                                                       // kell listázni... lsd ---> ToList még 1x.                                     
        }

//----------------------------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {

            // Ellenőrzés:

            /*        var sorok = File.ReadAllLines("felvi.txt");

                    foreach (var sor in sorok)
                    {
                        Console.WriteLine(sor);

                    }     */


            //        List<Felvetelizo> felvetelizok = Betoltes("felvi.txt");

            List<Felvetelizo> felvetelizok = new List<Felvetelizo>();


            string[] sorok = File.ReadAllLines("felvi.txt");
            for (int i = 0; i < sorok.Length; i++)
            {
                string[] adatok = sorok[i].Split(',');
                string nev = adatok[0];
                char nem = Convert.ToChar(adatok[1]);
                int pontszam = Convert.ToInt32(adatok[2]);
                string szak = adatok[3];
                string nyelvVizsga = adatok[4];


                Felvetelizo felvetelizo = new Felvetelizo(nev, nem, pontszam, szak, nyelvVizsga);
                felvetelizok.Add(felvetelizo);
            }
            Felvetelizo.Hatar();
            Console.WriteLine(felvetelizok[0].xy); //nem statikus példa
            Console.WriteLine(Felvetelizo.xy2); //statikus

//-------------------------------------------------------------------------------------------------------------------------------

            /*  Jelenjenek meg azoknak a tenulóknak a neve és pontszáma, akinek felsőfokú nyelvvizsgája van
                és legalább 300 pontot elértek.
                Az eredmény legyen pontszámszerint csökkenő, azon belül névszerint növekvő sorrendben.  */


            //string[] adatok3 = File.ReadAllLines("felvi.txt", Encoding.UTF8);

            var f1 = felvetelizok.Where(x => x.NyelvVizsga == "Felső")   // egybe is írható ---> Where(x=>x.NyelvVizsga == "Felső" && x => x.Pontszam >= 300
                            .Where(x => x.Pontszam >= 300)
                            .OrderByDescending(x => x.Pontszam)
                            .ThenBy(x => x.Nev)
                            .ToList();

            foreach (var felvetelis in f1)
            {
                Console.WriteLine($"{felvetelis.Nev}, {felvetelis.Pontszam}");
            }
            for (int i = 0; i < f1.Count; i++)
            {
                Console.WriteLine($"{f1[i].Nev}, {f1[i].Pontszam}");
            }

//---------------------------------------------------------------------------------------------------------

            /* Írjuk ki a második felvételiző adataiból milyen szintű nyelvvizsgája van.  */

            // Ebben az esetben nem használunk zárójelet, mert a Felvtelzo.cs-ben már kezelve van.(Első eset)

            Console.WriteLine($"{f1[1].NyelvvizsgaSzoveg1}");

            // Ebben az esetben pedig használunk ()-et, mivel a Felvetelizo.cs-ben is függvényként írtuk meg.(Második eset)

            Console.WriteLine($"{f1[1].NyelvvizsgaSzoveg2()}");

//--------------------------------------------------------------------------------------------------------------

            /* Jelenjen meg annak a tanulónak a neve és pontszáma, aki Informatika tanár szakra,
             * a legmagasabb pontszámmal lett felvéve. Több egyezés esetén elég egy tanulót is kiírni.
             * Értékét (MAX) vagy a helyét keressük? ez alapján ítéljük meg, melyiket fogjuk alkalmazni.
             * 
             MIvel a tanulónak a nevére és pontszámára is kiváncsiak vagyunk, ezért ebben az esetben...*/

            /* SQL-ben így nézne ki: SELECT nev, pontszam
                                     FROM tanulo
                                     WHERE = "Informatika tanár"
                                     ORDERBY pontszam DESC
                                     LIMIT 1;   */

            var f2 = felvetelizok.Where(x => x.Szak == "Informatika tanár")
                                  .OrderByDescending(x => x.Pontszam)
                                  .FirstOrDefault();     // Ha csak First-t használnánk, hibába ütközhetne a meghívás után.
                                                         // Viszont a default esetében figyelni kell a null értékre is,
                                                        //   ha esetleg nem lenne ilyen szak..  ezért ezt is ki kell íratni ---> if...

            if (f2 != null)
            {
            Console.WriteLine($"Tanuló neve: {f2.Nev}, Pontszáma: {f2.Pontszam}");

            }
            else
            {
                Console.WriteLine("Nincs ilyen tanuló a listában");
            }

//˛˛˛˛˛˛˛˛˛˛˛˛

            // Ez  egy First példa, ahol meg kivételkezelést  érdemes használni, kiküszöbölve ezzel azt az eshetőséget,
            // hogy rossz eredményünk szülessen... vagy éppen hibát jelezzen futás közbe.


            try
            {
                var f3 = felvetelizok.Where(x => x.Szak == "Tánctanár")
                                    .OrderByDescending(x => x.Pontszam)
                                    .First();
                Console.WriteLine($"Tanuló neve: {f2.Nev}, Pontszáma: {f2.Pontszam}");

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Nincs ilyen tanuló a listában");
            }
            catch(Exception ex)    // Ez az összes kivételt elkapja
            {
                Console.WriteLine($"Hiba történt : {ex.Message}");

            }

            // A kivételkezeléseknél a kiíratás sorrendjében futnak le a kivételkezelések. Ezért jelenleg,
            // a "Nincs ilyen tanuló a listában" fog meghíváskor megjelenni.

//----------------------------------------------------------------------------------------------------------------
            Console.WriteLine();

            /* Szakonként szeretnénk tudni a pontszámok átlagát. Jelenítsd meg a szakokat és az 
               átlagos pontszámot.    */

            /* MySQL-ben: SELECT szak AVG (pontszam)
                          FROM tanulo
                          GRUOP BY szak;  */

           foreach(var item in felvetelizok.GroupBy(x => x.Szak))
            {
                Console.WriteLine($"{item.Key}, átlagos pontszám: {Math.Round(item.Average(x => x.Pontszam),2)}");

                // Így is kiíratható a kerekítés:
                Console.WriteLine($"{item.Key}, átlagos pontszám: {item.Average(x => x.Pontszam):N2}");

            }

        }
    }
}

