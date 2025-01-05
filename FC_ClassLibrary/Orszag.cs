using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_ClassLibrary
{
    public record Orszag(int Orszag_Id, string OrszagNev)
    {
        public static Orszag? CreateCsV(string line)
        {
            var s = line.Split(';');
            try
            {
                return new Orszag(int.Parse(s[0]), s[1]);
            }
            catch { return null; }
        }

    }
}
