using FC_ClassLibrary;
DataStore Data = DataStore.CreateCSV();

Console.WriteLine($"5.feladat: {Data.Ballabas()} bal lábas játékos található.");

Console.Write("6. feladat: Játékos neve: ");
string? nev = Console.ReadLine();
Console.WriteLine(Data.JatekosAdatlapja(nev));

Console.WriteLine("7. feladat: A 4 legtöbb átlagos értékkel rendelkező játékos:");
Console.WriteLine(Data.Top4());