# OOP II feladatsor

## Sporteszköz-kölcsönző

Készíts konzolos alkalmazást a bankszámlás mintafeladat alapján! A program eszközöket, azok kölcsönzési állapotát és díját kezeli.

## 1 Hozd létre a KolcsonzesEredmeny felsorolást

- Sikeres

- MarKolcsonozve

- NincsKolcsonozve

## 2 Készítsd el a KolcsonozhetoEszkoz osztályt

| Tag | Típus | Elvárt működés |
| --- | --- | --- |
| kolcsonozve | bool | Privát mező; kezdetben false. |
| Megnevezes | string | Nyilvánosan olvasható; csak a konstruktorban állítható. |
| NapiDij | decimal | Nyilvánosan olvasható és beállítható; csak pozitív lehet. |
| Kolcsonozve | bool | Nyilvánosan olvasható; a privát mező értékét adja. |

A NapiDij tulajdonsághoz használj privát háttérmezőt! Hibás beállításnál dobj ArgumentOutOfRangeException kivételt; a korábbi érték maradjon meg.

## 3 Készíts konstruktort

- Paraméterei: megnevezés és napi díj.

- Az üres vagy csak szóközökből álló megnevezésnél dobj ArgumentException kivételt.

- A napi díjat a NapiDij tulajdonságon keresztül állítsd be.

- Új eszköz mindig szabad állapotban jöjjön létre.

## 4 Készíts kölcsönzési és visszavételi műveleteket

- public KolcsonzesEredmeny Kolcsonoz(): ha már kölcsönözve van, adjon MarKolcsonozve eredményt; egyébként állítsa az állapotot true-ra, és adjon Sikeres eredményt.

- public KolcsonzesEredmeny Visszavesz(): ha nincs kölcsönözve, adjon NincsKolcsonozve eredményt; egyébként állítsa az állapotot false-ra, és adjon Sikeres eredményt.

- Sikertelen műveletnél az állapot ne változzon. Ezeket a metódusokat nem kell virtuálissá tenni.

## 5 Készíts díjszámítást és szöveges megjelenítést

- public virtual decimal KolcsonzesiDij(int napok): nem pozitív napszámnál dobjon ArgumentOutOfRangeException kivételt; egyébként az eredmény NapiDij * napok.

- Írd felül a ToString() metódust! Jelenjen meg a típus, a megnevezés, a napi díj és az állapot: „szabad” vagy „kölcsönözve”. A pénzösszeg két tizedesjeggyel és Ft jelöléssel szerepeljen.

- Az eszközosztály ne kérjen be adatot és ne írjon a konzolra.

## 6 Készíts Kerekpar osztályt

- Származzon a KolcsonozhetoEszkoz osztályból.

- Új tulajdonsága: Vazmeret, int típusú, kívülről csak olvasható; a vázméretet centiméterben tárolja.

- Konstruktorparaméterei: megnevezés, napi díj, vázméret. Használd az ősosztály konstruktorát!

- A vázméretnek pozitívnak kell lennie; hibánál dobj ArgumentOutOfRangeException kivételt.

- A díjszámítást örökölje változtatás nélkül. A ToString() egészítse ki az ős szövegét a vázmérettel és a cm jelöléssel.

## 7 Készíts Sifelszereles osztályt

- Származzon a KolcsonozhetoEszkoz osztályból.

- Új, kívülről csak olvasható tulajdonságai: Meret (string, például „M”) és ElokeszitesiDij (decimal).

- Konstruktorparaméterei: megnevezés, napi díj, méret, előkészítési díj. Használd a base(...) hívást!

- Az üres vagy csak szóközökből álló méretnél dobj ArgumentException kivételt. Negatív előkészítési díjnál dobj ArgumentOutOfRangeException kivételt; nulla díj megengedett.

- Írd felül a KolcsonzesiDij(int napok) metódust! Az alapdíjat a base.KolcsonzesiDij(napok) hívással számítsd ki, és ehhez add hozzá az egyszeri előkészítési díjat.

- A napszám ellenőrzését ne ismételd meg: azt az ős metódusa végzi. Az előkészítési díjat ne szorozd meg a napok számával!

- A ToString() egészítse ki az ős szövegét a mérettel és az előkészítési díjjal; a díj két tizedesjeggyel és Ft jelöléssel jelenjen meg.

## 8 Hozz létre közös eszközlistát

Hozz létre List<KolcsonozhetoEszkoz> listát az alábbi hat objektummal, ebben a sorrendben! Az első és az utolsó objektum közvetlenül az alaposztály példánya legyen. Kezdetben minden eszköz szabad.

| Típus | Megnevezés | Napi díj | További adatok |
| --- | --- | --- | --- |
| KolcsonozhetoEszkoz | Gördeszka | 1 500 Ft | — |
| Kerekpar | Városi kerékpár | 2 500 Ft | Vázméret: 50 cm |
| Kerekpar | Túrakerékpár | 3 000 Ft | Vázméret: 56 cm |
| Sifelszereles | Felnőtt sífelszerelés | 4 000 Ft | Méret: L; előkészítés: 1 500 Ft |
| Sifelszereles | Junior sífelszerelés | 3 500 Ft | Méret: M; előkészítés: 1 000 Ft |
| KolcsonozhetoEszkoz | Bukósisak | 1 000 Ft | — |

## 9 Jelenítsd meg az eszközöket és a díjakat

- foreach ciklussal járd be a közös listát, KolcsonozhetoEszkoz típusú változón keresztül.

- Írd ki az objektumokat a felülírt ToString() használatával; ne készíts típusonként külön elágazást.

- Minden eszköznél jelenítsd meg a háromnapos kölcsönzés díját is a KolcsonzesiDij(3) hívásával.

| Eszköz | Háromnapos díj |
| --- | --- |
| Gördeszka | 4 500 Ft |
| Városi kerékpár | 7 500 Ft |
| Túrakerékpár | 9 000 Ft |
| Felnőtt sífelszerelés | 13 500 Ft |
| Junior sífelszerelés | 11 500 Ft |
| Bukósisak | 3 000 Ft |

## 10 Készíts eredményüzenetet adó metódust

- A főprogramban készíts string EredmenyUzenet(KolcsonzesEredmeny eredmeny) metódust!

- Mindhárom felsorolásértékhez adjon érthető magyar üzenetet: sikeres művelet; az eszköz már kölcsönözve van; az eszköz nincs kölcsönözve.

- A metódus csak a szöveget adja vissza; a kiírást a hívó végezze.

## 11 Próbáld ki az állapotváltozásokat

A lista objektumain végezd el sorrendben az alábbi műveleteket! Minden lépés után írd ki az eredmény magyar üzenetét és az érintett eszköz állapotát!

| Művelet | Elvárt eredmény | Állapot utána |
| --- | --- | --- |
| Városi kerékpár kölcsönzése | Sikeres | kölcsönözve |
| Városi kerékpár ismételt kölcsönzése | MarKolcsonozve | kölcsönözve |
| Felnőtt sífelszerelés kölcsönzése | Sikeres | kölcsönözve |
| Városi kerékpár visszavétele | Sikeres | szabad |
| Városi kerékpár ismételt visszavétele | NincsKolcsonozve | szabad |

A műveletsor végén csak a Felnőtt sífelszerelés legyen kölcsönözve. A következő lekérdezésekhez ezt az állapotot használd!

## 12 Próbáld ki a kivételkezelést

- A Gördeszka napi díját próbáld 0-ra állítani. Kapd el az ArgumentOutOfRangeException kivételt, írd ki az üzenetét, majd ellenőrizd, hogy a díj továbbra is 1 500 Ft.

- A Felnőtt sífelszerelésre kérj 0 napos díjszámítást. Kapd el az ArgumentOutOfRangeException kivételt, és írd ki az üzenetét.

- A két kipróbálás külön try–catch blokkba kerüljön, hogy mindkettő lefusson. A program ezután folytatódjon a lekérdezésekkel!

## 13 Készíts öt LINQ lekérdezést

Mindegyik eredményt érthető felirattal jelenítsd meg! A lekérdezések ne módosítsák a lista elemeit. Az ellenőrző eredmények a 11–12. feladat utáni állapotra vonatkoznak.

### 1 Szabad eszközök névsorban

Listázd a szabad eszközöket megnevezés szerint növekvő sorrendben! Ellenőrzés: 5 eszköz jelenik meg; a Felnőtt sífelszerelés nem szerepel köztük.

### 2 Kölcsönzött eszközök száma

Számold meg a kölcsönzött eszközöket! Elvárt eredmény: 1 darab.

### 3 Összes háromnapos díj

Számítsd ki, mennyibe kerülne a lista összes eszközének háromnapos kölcsönzése! Az állapotuktól függetlenül minden eszközt vegyél figyelembe. A díjszámító metódust használd, hogy az előkészítési díjak is beleszámítsanak! Elvárt eredmény: 49 000 Ft.

### 4 Legmagasabb napi díjú eszköz

Keresd meg a legmagasabb napi díjú eszközt. A kapott objektumot jelenítsd meg; null esetén jelezd, hogy a lista üres! Elvárt találat: Felnőtt sífelszerelés, napi 4 000 Ft.

### 5 Eszközök napi kétezer forint felett

Listázd azokat az eszközöket, amelyek napi díja szigorúan nagyobb 2 000 Ft-nál, napi díj szerint csökkenő sorrendben! A kölcsönzési állapotot itt ne vizsgáld. Elvárt sorrend: Felnőtt sífelszerelés; Junior sífelszerelés; Túrakerékpár; Városi kerékpár.

## 14 Ellenőrizd és javítsd a kódot

- Az ismételt kölcsönzés és visszavétel nem módosítja hibásan az állapotot.

- Az egynapos díj helyes; a 0 és a negatív napszám kivételt okoz. A sífelszerelés előkészítési díja csak egyszer számít bele.

- A nulla előkészítési díj megengedett; a nulla napi díj és vázméret nem megengedett.

- A díjszámítás a kölcsönzési állapottól független, és azt nem változtatja meg.

- A közös működést nem másoltad át a leszármazottakba; az eszközosztályokban nincs Console.WriteLine() vagy adatbekérés.

## Szorgalmi

Készíts harmadik leszármazottat SupDeszka néven! Új, csak olvasható tulajdonsága az int típusú MaxTerheles (értéke csak pozitív lehet) (kg). Használd az ős konstruktorát, ellenőrizd a terhelhetőséget, és egészítsd ki a ToString() szövegét! A díjszámítást örökölje. Adj egy példányt a listához, majd futtasd újra az öt lekérdezést! Az ellenőrző eredmények ekkor természetesen megváltozhatnak.
