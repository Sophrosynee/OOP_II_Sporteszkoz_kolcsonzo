using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SporteszkozKolcsonzo;

// 1. feladat: a várható műveleti eredményeket névvel jelöljük.
public enum KolcsonzesEredmeny
{
    Sikeres,
    MarKolcsonozve,
    NincsKolcsonozve
}

// 2–5. feladat: közös adatok és működés.
public class KolcsonozhetoEszkoz
{
    private bool kolcsonozve;
    private decimal napiDij;

    public string Megnevezes { get; }
    public bool Kolcsonozve => kolcsonozve;
    //ugyanez másképp:
    //public bool Kolcsonozve
    //{
    //    get { return kolcsonozve; }
    //}
    //ez nem inicializál

    public decimal NapiDij
    {
        get => napiDij;
        set
        {
            // Ellenőrzés az értékadás előtt: hibánál a régi díj megmarad.
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(NapiDij),
                    "A napi díjnak pozitívnak kell lennie.");

            napiDij = value;
        }
    }

    public KolcsonozhetoEszkoz(string megnevezes, decimal napiDij)
    {
        if (string.IsNullOrWhiteSpace(megnevezes))
            throw new ArgumentException("A megnevezés nem lehet üres.", nameof(megnevezes));
        kolcsonozve = false; 
        Megnevezes = megnevezes;
        NapiDij = napiDij;
    }

    public KolcsonzesEredmeny Kolcsonoz()
    {
        if (kolcsonozve)
            return KolcsonzesEredmeny.MarKolcsonozve;

        kolcsonozve = true;
        return KolcsonzesEredmeny.Sikeres;
    }

    public KolcsonzesEredmeny Visszavesz()
    {
        if (!kolcsonozve)
            return KolcsonzesEredmeny.NincsKolcsonozve;

        kolcsonozve = false;
        return KolcsonzesEredmeny.Sikeres;
    }

    public virtual decimal KolcsonzesiDij(int napok)
    {
        if (napok <= 0)
            throw new ArgumentOutOfRangeException(nameof(napok),
                "A napok számának pozitívnak kell lennie.");

        // Csak árajánlat: az állapot nem változik.
        return NapiDij * napok;
    }

    public override string ToString()
    {
        string allapot = Kolcsonozve ? "kölcsönözve" : "szabad";
        return $"{GetType().Name} | {Megnevezes} | Napi díj: {NapiDij:N2} Ft | {allapot}";
    }
}

// 6. feladat: a kerékpár a díjszámítást változtatás nélkül örökli.
public class Kerekpar : KolcsonozhetoEszkoz
{
    public int Vazmeret { get; }

    public Kerekpar(string megnevezes, decimal napiDij, int vazmeret)
        : base(megnevezes, napiDij)
    {
        if (vazmeret <= 0)
            throw new ArgumentOutOfRangeException(nameof(vazmeret),
                "A vázméretnek pozitívnak kell lennie.");

        Vazmeret = vazmeret;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Vázméret: {Vazmeret} cm";
    }
}

// 7. feladat: eltérő díjszámítás, a közös működés újrafelhasználásával.
public class Sifelszereles : KolcsonozhetoEszkoz
{
    public string Meret { get; }
    public decimal ElokeszitesiDij { get; }

    public Sifelszereles(string megnevezes, decimal napiDij,
        string meret, decimal elokeszitesiDij)
        : base(megnevezes, napiDij)
    {
        if (string.IsNullOrWhiteSpace(meret))
            throw new ArgumentException("A méret nem lehet üres.", nameof(meret));

        if (elokeszitesiDij < 0)
            throw new ArgumentOutOfRangeException(nameof(elokeszitesiDij),
                "Az előkészítési díj nem lehet negatív.");

        Meret = meret;
        ElokeszitesiDij = elokeszitesiDij;
    }

    public override decimal KolcsonzesiDij(int napok)
    {
        // Az ős ellenőrzi a napszámot is. Az előkészítés egyszeri díj.
        return base.KolcsonzesiDij(napok) + ElokeszitesiDij;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Méret: {Meret} | Előkészítési díj: {ElokeszitesiDij:N2} Ft";
    }
}

// Szorgalmi: új típus, a meglévő díjszámítás öröklésével.
public class SupDeszka : KolcsonozhetoEszkoz
{
    public int MaxTerheles { get; }

    public SupDeszka(string megnevezes, decimal napiDij, int maxTerheles)
        : base(megnevezes, napiDij)
    {
        if (maxTerheles <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxTerheles),
                "A maximális terhelésnek pozitívnak kell lennie.");

        MaxTerheles = maxTerheles;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Maximális terhelés: {MaxTerheles} kg";
    }
}

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("hu-HU");

        // 8. feladat: az előírt sorrendben, kezdetben minden eszköz szabad.
        List<KolcsonozhetoEszkoz> eszkozok = new List<KolcsonozhetoEszkoz>
        {
            new KolcsonozhetoEszkoz("Gördeszka", 1500m),
            new Kerekpar("Városi kerékpár", 2500m, 50),
            new Kerekpar("Túrakerékpár", 3000m, 56),
            new Sifelszereles("Felnőtt sífelszerelés", 4000m, "L", 1500m),
            new Sifelszereles("Junior sífelszerelés", 3500m, "M", 1000m),
            new KolcsonozhetoEszkoz("Bukósisak", 1000m)
        };

        // 9. feladat: a tényleges objektum dönti el, melyik felülírás fut le.
        Console.WriteLine("--- Eszközök és háromnapos díjak ---");
        foreach (KolcsonozhetoEszkoz eszkoz in eszkozok)
        {
            Console.WriteLine(eszkoz);
            Console.WriteLine($"Háromnapos díj: {eszkoz.KolcsonzesiDij(3):N2} Ft");
        }

        // 11. feladat: a lista meglévő objektumain dolgozunk.
        KolcsonozhetoEszkoz varosiKerekpar = eszkozok[1];
        KolcsonozhetoEszkoz felnottSifelszereles = eszkozok[3];
        Console.WriteLine("\n--- Állapotváltozások ---");
        MuveletKiirasa(varosiKerekpar, varosiKerekpar.Kolcsonoz());
        MuveletKiirasa(varosiKerekpar, varosiKerekpar.Kolcsonoz());
        MuveletKiirasa(felnottSifelszereles, felnottSifelszereles.Kolcsonoz());
        MuveletKiirasa(varosiKerekpar, varosiKerekpar.Visszavesz());
        MuveletKiirasa(varosiKerekpar, varosiKerekpar.Visszavesz());

        // 12. feladat: külön blokkok, hogy mindkét kipróbálás lefusson.
        Console.WriteLine("\n--- Kivételkezelés ---");
        try
        {
            eszkozok[0].NapiDij = 0m;
        }
        catch (ArgumentOutOfRangeException hiba)
        {
            Console.WriteLine(hiba.Message);
        }
        Console.WriteLine($"Gördeszka napi díja (elvárt: 1 500 Ft): {eszkozok[0].NapiDij:N2} Ft");

        try
        {
            Console.WriteLine(felnottSifelszereles.KolcsonzesiDij(0));
        }
        catch (ArgumentOutOfRangeException hiba)
        {
            Console.WriteLine(hiba.Message);
        }

        Console.WriteLine("\n--- Öt LINQ-lekérdezés az eredeti hat eszközre ---");
        LekerdezesekKiirasa(eszkozok);

        Console.WriteLine("\n--- Szorgalmi: SUP hozzáadása ---");
        KolcsonozhetoEszkoz sup = new SupDeszka("Túra SUP", 5000m, 120);
        eszkozok.Add(sup);
        Console.WriteLine(sup);
        LekerdezesekKiirasa(eszkozok);
    }

    // 10. feladat: csak szöveget ad vissza, a konzolra nem ír.
    private static string EredmenyUzenet(KolcsonzesEredmeny eredmeny)
    {
        return eredmeny switch
        {
            KolcsonzesEredmeny.Sikeres => "Sikeres művelet.",
            KolcsonzesEredmeny.MarKolcsonozve => "Az eszköz már kölcsönözve van.",
            KolcsonzesEredmeny.NincsKolcsonozve => "Az eszköz nincs kölcsönözve.",
            _ => throw new ArgumentOutOfRangeException(nameof(eredmeny))
        };
    }

    private static void MuveletKiirasa(KolcsonozhetoEszkoz eszkoz,
        KolcsonzesEredmeny eredmeny)
    {
        Console.WriteLine(EredmenyUzenet(eredmeny));
        Console.WriteLine(eszkoz);
    }

    // 13. feladat:
    private static void LekerdezesekKiirasa(List<KolcsonozhetoEszkoz> eszkozok)
    {
        Console.WriteLine("1. Szabad eszközök névsorban:");
        foreach (KolcsonozhetoEszkoz eszkoz in eszkozok
                     .Where(e => !e.Kolcsonozve).OrderBy(e => e.Megnevezes))
            Console.WriteLine(eszkoz);

        int kolcsonzottDarab = eszkozok.Count(e => e.Kolcsonozve);
        Console.WriteLine($"2. Kölcsönzött eszközök száma: {kolcsonzottDarab} darab");

        decimal osszesDij = eszkozok.Sum(e => e.KolcsonzesiDij(3));
        Console.WriteLine($"3. Összes háromnapos díj: {osszesDij:N2} Ft");

        KolcsonozhetoEszkoz? legdragabb = eszkozok
            .OrderByDescending(e => e.NapiDij).FirstOrDefault();
        Console.WriteLine("4. Legmagasabb napi díjú eszköz:");
        if (legdragabb is null)
            Console.WriteLine("A lista üres.");
        else
            Console.WriteLine(legdragabb);

        Console.WriteLine("5. Eszközök napi 2 000 Ft felett, csökkenő díjjal:");
        foreach (KolcsonozhetoEszkoz eszkoz in eszkozok
                     .Where(e => e.NapiDij > 2000m).OrderByDescending(e => e.NapiDij))
            Console.WriteLine(eszkoz);
    }
}
