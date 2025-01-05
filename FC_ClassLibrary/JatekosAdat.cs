using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_ClassLibrary
{
    public record JatekosAdat(int Jatekos_Id, int Pace, int Shooting, int Passing, int Dribbling, int Defending, int Physical)
    {
        public static JatekosAdat? CreateCSV(string line)
        {
            var s = line.Split(';');
            try
            {
                return new JatekosAdat(int.Parse([0]), int.Parse([1]), int.Parse([2]), int.Parse([3]), int.Parse([4]), int.Parse([5]), int.Parse([6]));
            }
            catch { return null; }
        }
    }
}
