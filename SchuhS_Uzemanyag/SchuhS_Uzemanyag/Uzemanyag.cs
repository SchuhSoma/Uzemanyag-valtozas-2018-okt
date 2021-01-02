using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuhS_Uzemanyag
{
    class Uzemanyag
    {
        public string Datum;
        public int Ev;
        public int Honap;
        public int Nap;
        public int BenzinAr;
        public int GazolajAr;

        public Uzemanyag(string sor)
        {
            var dbok = sor.Split(';');           
            this.Datum = dbok[0];
            var dbok1 = Datum.Split('.');
            this.Ev = int.Parse(dbok1[0]);
            this.Honap = int.Parse(dbok1[1]);
            this.Nap = int.Parse(dbok1[2]);                       
            this.BenzinAr = int.Parse(dbok[1]);
            this.GazolajAr = int.Parse(dbok[2]);
        }
    }
}
