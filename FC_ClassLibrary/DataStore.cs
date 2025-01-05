using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_ClassLibrary
{
    public class DataStore
    {
        List<Jatekos> jatekosok;
        List<JatekosAdat> jatekosadatok;
        List<Orszag> orszagok;

        public IEnumerable<Jatekos> Jatekosok=>jatekosok;
        public IEnumerable<JatekosAdat> Jatekosadatok => jatekosadatok;
        public IEnumerable<Orszag> Orszagok => orszagok;

        private DataStore(string directory) { 
        jatekosok = File.ReadAllLines(Path.Combine(directory,"jatekosok.csv")).Select(Jatekos.CreateCSV).Where(x=>x != null).Select(x=>x!).ToList();
        jatekosadatok = File.ReadAllLines(Path.Combine(directory, "jatekosadatok.csv")).Select(JatekosAdat.CreateCSV).Where(x => x != null).Select(x => x!).ToList();
        orszagok = File.ReadAllLines(Path.Combine(directory,"orszag.csv")).Select(Orszag.CreateCsV).Where(x=>x !=null).Select(x=>x!).ToList();
        }
        public static DataStore CreateCSV(string directory = "input") => new DataStore(directory);
        public int AtlagErtekeles(JatekosAdat jatekos)
        {
            int osszErtekeles = jatekos.Pace + jatekos.Shooting + jatekos.Passing +
                                jatekos.Dribbling + jatekos.Defending + jatekos.Physical;

            int kategoriakSzama = 6;

            int atlagErtekeles = (int)Math.Round((double)osszErtekeles / kategoriakSzama);

            return atlagErtekeles;
        }
        public int Ballabas()
        {
            return Jatekosok.Count(x => x.Lab == "Bal");
        }
        
        public string JatekosAdatlapja(string nev)
        {
            var jatekos = jatekosok.FirstOrDefault(x => x.Nev == nev);

            if (jatekos != null)
            {
                var jatekosAdat = Jatekosadatok.FirstOrDefault(x => x.Jatekos_Id == jatekos.Jatekos_Id);
                if (jatekosAdat != null)
                {
                    var orszag = Orszagok.FirstOrDefault(x => x.Orszag_Id == jatekos.Orszag_Id)?.OrszagNev;
                    return $"Név: {jatekos.Nev}\nPozíció: {jatekos.Pozicio}\nOrszág: {orszag}\nÁtlagos érték: {AtlagErtekeles(jatekosAdat)}" +
                        $"\nLáb: {jatekos.Lab}";
                }
            }

            return "Ilyen néven nem található játékos.";
        }
        public string Top4()
        {
            var jatekosAtlagok = Jatekosok
                .Select(jatekos => new {
                    jatekos.Nev,
                    AtlagErtekeles = jatekosadatok
                        .Where(adat => adat.Jatekos_Id == jatekos.Jatekos_Id)
                        .Select(adat => AtlagErtekeles(adat))
                        .FirstOrDefault()
                })
                .OrderByDescending(x => x.AtlagErtekeles)
                .Take(4)
                .ToList();

            var eredmeny = new StringBuilder();
            foreach (var jatekos in jatekosAtlagok)
            {
                eredmeny.AppendLine($"{jatekos.Nev}: {jatekos.AtlagErtekeles} pont");
            }

            return eredmeny.ToString();
        }
    }
}
