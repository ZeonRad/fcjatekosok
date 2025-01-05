using FC_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JatekosGUI
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly DataStore dataStore;
        private Jatekos? valasztottJatekos = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<string> Orszagok { get; init; }

        private string? valasztottOrszag;
        public string? ValasztottOrszag
        {
            get => valasztottOrszag;
            set
            {
                valasztottOrszag = value;
                Jatekosok.Clear();

                if (value != null)
                {
                    var orszag = dataStore.Jatekosok
                        .Where(x => x.Orszag == value)
                        .OrderBy(x => x.Nev)
                        .ToList();

                    foreach (var jatekos in orszagbeliJatekosok)
                    {
                        Jatekosok.Add(jatekos);
                    }
                }

                ValasztottJatekos = null;
                PropertyUpdated();
                PropertyUpdated(nameof(VanValasztottOrszag));
            }
        }

        public ObservableCollection<Jatekos> Jatekosok { get; set; } = new ObservableCollection<Jatekos>();

        private Jatekos? valasztottJatekos;
        public Jatekos? ValasztottJatekos
        {
            get => valasztottJatekos;
            set
            {
                valasztottJatekos = value;
                PropertyUpdated();
                PropertyUpdated(nameof(VanValasztottJatekos));
                PropertyUpdated(nameof(JatekosNev));
                PropertyUpdated(nameof(JatekosZaszlo));
                PropertyUpdated(nameof(JatekosAtlagosErtek));
                PropertyUpdated(nameof(JatekosPozicio));
                PropertyUpdated(nameof(JatekosAdatok));
            }
        }

        public string JatekosNev => valasztottJatekos?.Nev ?? "";
        public string JatekosZaszlo => valasztottJatekos?.Zaszlo ?? "";
        public string JatekosAtlagosErtek => valasztottJatekos?.AtlagosErtek.ToString() ?? "";
        public string JatekosPozicio => valasztottJatekos?.Pozicio ?? "";
        public string JatekosAdatok => valasztottJatekos?.Adatok ?? "";

        public bool VanValasztottOrszag => ValasztottOrszag != null;
        public bool VanValasztottJatekos => ValasztottJatekos != null;

        public MainWindowViewModel()
        {
            dataStore = DataStore.CreateCSV();
            Orszagok = dataStore.Jatekosok
                .Select(x => x.Orszag)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        void PropertyUpdated([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
