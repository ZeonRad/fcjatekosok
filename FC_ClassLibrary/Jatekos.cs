using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_ClassLibrary
{
    public record Jatekos(int Jatekos_Id, string Nev,int Orszag_Id,string Pozicio,string Lab)
    {
        public string Orszag { get; set; }

        public static Jatekos? CreateCSV(string line)
        {
            var s = line.Split(';');
            try
            {
                return new Jatekos(int.Parse(s[0]), s[1], int.Parse(s[2]), s[3], s[4]);
            }
            catch { return null; }
        }
    }
}
