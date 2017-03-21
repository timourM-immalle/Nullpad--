using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullpad__
{
    class Personen
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatum { get; set; }

        //niet met DataGrid, maar met ListView:
        //public override string ToString()
        //{
        //    return Voornaam + " " + Achternaam + " (" + Geboortedatum.ToShortDateString() + ")";
        //}
    }
}
