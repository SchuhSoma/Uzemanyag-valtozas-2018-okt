using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SchuhS_Uzemanyag
{
    class Program
    {
        static List<Uzemanyag> UzemanyagList;
        static List<int> BenzinGazolajKulTisztan;
        static List<int> BenzinGazolajKul;
        static int MinKulonbseg;
        static Dictionary<int, int> EvValtozasKozottiNapok;
        static int BekertEv;
        static void Main(string[] args)
        {
            Feladat2Beolvasas(); Console.WriteLine("\n-------------------------------------\n");
            Feladat3ValtozasSzama(); Console.WriteLine("\n-------------------------------------\n");
            Feladat4KulonbsegMin(); Console.WriteLine("\n-------------------------------------\n");
            Feladat5MinElofordulasokSzama(); Console.WriteLine("\n-------------------------------------\n");
            Feladat6SzokoEv(); Console.WriteLine("\n-------------------------------------\n");
            Feladat7Euro(); Console.WriteLine("\n-------------------------------------\n");
            Feladat8EvBeker(); //Console.WriteLine("\n-------------------------------------\n");
            Console.ReadKey();
        }

        private static void Feladat8EvBeker()
        {
            Console.WriteLine("8.Feladat: évszám bekérése a felhasználótól, amíg a bekért évszám nem 2011 és 2016 közötti");
            int BekertEv = 0;
            bool Teljesul = false;
            do
            {
                Console.Write("\tKérem adjon meg egy évszámot 2011 és 2016 között: ");
                BekertEv = int.Parse(Console.ReadLine());
                Teljesul = true;
            } while (BekertEv > 2017 || BekertEv < 2010);
            if (Teljesul == true) { Feladat9();}
            
        }

        private static void Feladat9()
        {
            Console.WriteLine("\n-------------------------------------\n");
           Console.WriteLine("\n-------------------------------------\n");
            Console.WriteLine("10.Feladat:");
            EvValtozasKozottiNapok = new Dictionary<int, int>();
            TimeSpan ElteltIdo;
            string Napok;
            int NapokSzama;
            int MaxNapKeresettEvben = int.MinValue;
            for (int i = 1; i < UzemanyagList.Count; i++)
            {
                ElteltIdo =UzemanyagList[i].ValtoztatasDatuma - UzemanyagList[i - 1].ValtoztatasDatuma;
                Napok = ElteltIdo.ToString("dd");
                NapokSzama = int.Parse(Napok);
              /*Console.WriteLine("{0} : {1}",UzemanyagList[i].Ev,NapokSzama);*/
                if(!EvValtozasKozottiNapok.ContainsValue(NapokSzama) && !EvValtozasKozottiNapok.ContainsKey(UzemanyagList[i].Ev))
                {
                    EvValtozasKozottiNapok.Add(UzemanyagList[i].Ev, NapokSzama);
                }
                if(UzemanyagList[i].Ev==BekertEv && MaxNapKeresettEvben < NapokSzama)
                { MaxNapKeresettEvben = NapokSzama; }
                
            }
            Console.WriteLine("\tA Vizsgált évben ({0}) két árváltozás között eltelt legnagyobb idő : {1}", BekertEv, MaxNapKeresettEvben);
            foreach (var e in EvValtozasKozottiNapok)
            {
                if(e.Key==BekertEv && MaxNapKeresettEvben<e.Value)
                {
                    MaxNapKeresettEvben = e.Value;
                }
            }
            Console.WriteLine("\tA Vizsgált évben ({0}) két árváltozás között eltelt legnagyobb idő : {1}",BekertEv,MaxNapKeresettEvben);
        }

        private static void Feladat7Euro()
        {
            Console.WriteLine("7.Feladat: az üzemanyagárakat euró valutanembe átszámolva, két tizedesjegy pontossággal tartalmazza");
            double ArfolyamBenzin = 0;
            double ArfolyamGazolaj = 0;
            int dbEll = 0;
            var sw = new StreamWriter(@"euro.txt", false, Encoding.UTF8);
            foreach (var u in UzemanyagList)
            {
                ArfolyamBenzin = (double)u.BenzinAr / 307.7;
                ArfolyamGazolaj = (double)u.GazolajAr / 307.7;
                //Console.WriteLine("{0};{1};{2}",u.Datum,ArfolyamBenzin,ArfolyamGazolaj);
                sw.WriteLine("{0};{1:0.00};{2:0.00}", u.Datum, ArfolyamBenzin, ArfolyamGazolaj);
                dbEll++;
            }
            if (dbEll == UzemanyagList.Count) { Console.WriteLine("\tSikeres file-ba iratás"); }
            else { Console.WriteLine("\tSikertelen kiiratás"); }
        }

        private static void Feladat6SzokoEv()
        {
            Console.WriteLine("6.Feladat: Döntse el, hogy a vizsgált időszakban volt-e szökőnapon árváltozás");
            bool Dontes = false;
            string TalaltNap = " ";
            foreach (var u in UzemanyagList)
            {
                if(u.Ev%4==0 && u.Datum.Contains("02.24"))
                {
                    Dontes = true;
                    TalaltNap = u.Datum;
                }
            }
            if (Dontes == true) { Console.WriteLine("\tVolt szökőnapi árváltozás, ezen a napon : {0}",TalaltNap); }
            else { Console.WriteLine("\tNem volt szökőnapi árváltozás"); }
        }

        private static void Feladat5MinElofordulasokSzama()
        { 
            Console.WriteLine("5.Feladat: az előző feladatban meghatározott legkisebb különbség hányszor fordult elő a vizsgált időszakban");
            int db = 0;
            foreach (var b in BenzinGazolajKul)
            {
                if (MinKulonbseg == b) db++;
            }
            Console.WriteLine("\tEnnyi alkalommal volt a különbség minimális : {0}",db);
        }

        private static void Feladat4KulonbsegMin()
        {
            Console.WriteLine("4.Feladat: a benzin és gázolaj árak között mekkora volt a legkisebb különbség a vizsgált időszakban");
            int KulonbsegBenzinGazolaj = 0;
            BenzinGazolajKul = new List<int>();
            BenzinGazolajKulTisztan = new List<int>();
            foreach (var u in UzemanyagList)
            {
                
                KulonbsegBenzinGazolaj = Math.Abs(u.BenzinAr - u.GazolajAr);
                BenzinGazolajKul.Add(KulonbsegBenzinGazolaj);
                if(!BenzinGazolajKulTisztan.Contains(KulonbsegBenzinGazolaj))
                {
                    BenzinGazolajKulTisztan.Add(KulonbsegBenzinGazolaj);
                }                
            }
            BenzinGazolajKulTisztan.Sort();
            MinKulonbseg = int.MaxValue;
            foreach (var b in BenzinGazolajKul)
            {
                //Console.WriteLine("\t{0}",b);
                if(b<MinKulonbseg)
                { MinKulonbseg = b; }
            }
            Console.WriteLine("\tA legkisebb különbség a vizsgált időszakban: {0}", MinKulonbseg);
        }

        private static void Feladat3ValtozasSzama()
        {
            Console.WriteLine("3.Feladat: hányszor történt változás a vizsgált időszakban");
            Console.WriteLine("\tÁrváltozások száma a vizsgált időszakban: {0}", UzemanyagList.Count);
        }

        private static void Feladat2Beolvasas()
        {
            Console.WriteLine("2.Feladat: Beolvasás");
            UzemanyagList = new List<Uzemanyag>();
            var sr = new StreamReader(@"uzemanyag.txt", Encoding.UTF8);
            int dbBeolvas = 0;
            while(!sr.EndOfStream)
            {
                UzemanyagList.Add(new Uzemanyag(sr.ReadLine()));
                dbBeolvas++;
            }
            sr.Close();
            if(dbBeolvas>0)
            {
                Console.WriteLine("\tSikeres beolvasás, bolvasott sorok száma: {0}", dbBeolvas);
            }
            else { Console.WriteLine("\tSikertelen beolvasás keresd a hibát"); }
        }
    }
}
