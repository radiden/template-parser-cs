namespace parser
{
    public class Vars
    {
        public static string ToParse = @"<p>
{Wariant: Kolor szuflady}

{Cecha: lóżka, wymiary}

{Cecha: dupa}

#IF{CECHA:Lóżka, wymiary}=99 X 9999
{
aaaaaaaaa
}

#IF {WaRiaNt    :     KoLor szUFlady} = Niebieski
{
bbbbbbbbbbbbbbbbbbbbbbbb
}
Lózko ERYK jest stworzone dla pokoju dwójki Twoich dzieci, nawet jesli dzieli je znaczna róznica wieku. 
#SWITCH {CECHA: Lóżka, wymiary}
@DEFAULT:
default tekst
@VALUE: 190 X 80
Rozmiar 190x80 cm tworzy wygodna przestrzen zarówno dla kilkulatka, jak i dla nastoletniej pociechy. 
@VALUE: 200 X 90
Duzy rozmiar 200x90 cm jest ogromnym atrybutem tego rozwiazania. Tworzy wygodna przestrzen dla kilkulatka, ale bedzie idealny takze dla nastolatków, którym dostarczy idealnych warunków do regeneracyjnego snu i odpoczynku. 
@VALUE: 160 X 90
Rozmiar 160x80 cm jest idealny dla kilkuletnich dzieci, chociaz bedzie funkcjonalny takze dla poczatkujacych nastolatków. 

#ENDSWITCH
. 
</p>
<p>
Wiemy jednak, ze najwazniejsze dla Ciebie jest ich bezpieczenstwo. Tylko to jest w stanie zapewnic Ci spokojny sen. Dlatego konstrukcja lózka Eryk powstala z wytrzymalego drewna, co gwarantuje wyjatkowa stabilnosc. Zwracajac uwage na kazdy szczegól, lózko wzmocnilismy srubami stalowymi wysokiej jakosci. Dzieki temu mozemy zaproponowac Tobie oraz Twoim dzieciom miejsca wypoczynku, z których kazde wytrzyma 100 kg nacisku. To dowód na to, ze jest to inwestycja na lata, która sluzyc bedzie zarówno kilkuletnim brzdacom, jak i nastolatkom. 
</p>
<p>
Lózko dopasowane jest dla najmlodszych dzieki dwustronnym barierkom, które mozna zamontowac na jednym lub dwóch poziomach. Ochronia one Twoje dzieci przed upadkiem, jesli w nocy nadal rozpiera je energia i ciezko im lezec spokojnie w jednym miejscu. W takim przypadku przydatny okazuje sie równiez stelaz z elastycznych listew drewnianych, który sprzyja wlasciwej pozycji ciala podczas snu i stanowi doskonale podparcie, co zaowocuje zdrowym kregoslupem w przyszlosci. 
</p>
<p>

#SWITCH {WARIANT: Kolor ramy}
@DEFAULT:
dupa!
@VALUE: Mleko
!Gleboki, czekoladowy kolor jest neutralny, zatem wpasuje sie w aranzacje pokoju, nawet gdy ten jest przeznaczony jednoczesnie dla dziewczynki i chlopca. Wystarczy wydzielic strefy ulubionymi kolorami na scianach i kompromis gotowy. 
@VALUE: Biała
Kolor bialy jest neutralny, zatem wpasuje sie w aranzacje pokoju, nawet gdy ten jest przeznaczony jednoczesnie dla dziewczynki i chlopca. Wystarczy wydzielic strefy ulubionymi kolorami na scianach i kompromis gotowy. 
@VALUE: Grafit
Kolor grafitowy jest neutralny, zatem wpasuje sie w aranzacje pokoju, nawet gdy ten jest przeznaczony jednoczesnie dla dziewczynki i chlopca. Wystarczy wydzielic strefy ulubionymi kolorami na scianach i kompromis gotowy. 
@VALUE: Sosna
Kolor sosny podkreslilismy jedynie bezwonnym lakierem. Dzieki temu stelaz jest odporny na zabrudzenia, wytrzymaly i bezpieczny. Dostosowany takze dla alergików. Naturalne drewno ociepli kazdy pokój kilkulatka. Co wiecej, jest neutralne, zatem przypadnie do gustu zarówno chlopcu, jak i dziewczynce. Wystarczy wydzielic strefy ulubionymi kolorami na scianach i kompromis gotowy. 

#ENDSWITCH
 
#SWITCH {WARIANT: Kolor szuflady}
@DEFAULT:

@VALUE: Brak

@VALUE: Niebieski
Niebieska szuflada z ciekawym wykonczeniem w postaci futurystycznych uchwytów pozwoli utrzymac porzadek. Moze sluzyc jako schowek na posciel lub zabawki. Ponadto jest ciekawym elementem ozdobnym, podkreslajacym chlopiecy charakter pokoju. Do nadania pozadanych kolorów zastosowalismy antyalergiczne, bezwonne i calkowicie bezpieczne farby wodne, posiadajace wszelkie atesty. To dodatkowe zabezpieczenie dla dzieci z wrazliwa skóra i sklonnoscia do uczulen. 
@VALUE: Różowy
Rózowa szuflada z ciekawym wykonczeniem w postaci futurystycznych uchwytów pozwoli utrzymac porzadek. Moze sluzyc jako schowek na posciel lub zabawki. Ponadto jest ciekawym elementem ozdobnym, który nada wnetrzu dziewczecego uroku. Do nadania pozadanych kolorów zastosowalismy antyalergiczne, bezwonne i calkowicie bezpieczne farby wodne, posiadajace wszelkie atesty. To dodatkowe zabezpieczenie dla dzieci z wrazliwa skóra i sklonnoscia do uczulen. 
@VALUE: Zielony
Zielona szuflada z ciekawym wykonczeniem w postaci futurystycznych uchwytów pozwoli utrzymac porzadek. Moze sluzyc jako schowek na posciel lub zabawki. Ponadto jest ciekawym elementem ozdobnym. Do nadania pozadanych kolorów zastosowalismy antyalergiczne, bezwonne i calkowicie bezpieczne farby wodne, posiadajace wszelkie atesty. To dodatkowe zabezpieczenie dla dzieci z wrazliwa skóra i sklonnoscia do uczulen. 
@VALUE: Biały
Biala szuflada z ciekawym wykonczeniem w postaci futurystycznych uchwytów pozwoli utrzymac porzadek. Moze sluzyc jako schowek na posciel lub zabawki. Ponadto jest ciekawym elementem ozdobnym. Do nadania pozadanych kolorów zastosowalismy antyalergiczne, bezwonne i calkowicie bezpieczne farby wodne, posiadajace wszelkie atesty. To dodatkowe zabezpieczenie dla dzieci z wrazliwa skóra i sklonnoscia do uczulen. 
@VALUE: Grafit
Grafitowa szuflada z ciekawym wykonczeniem w postaci futurystycznych uchwytów pozwoli utrzymac porzadek. Moze sluzyc jako schowek na posciel lub zabawki. Ponadto jest ciekawym elementem ozdobnym. Do nadania pozadanych kolorów zastosowalismy antyalergiczne, bezwonne i calkowicie bezpieczne farby wodne, posiadajace wszelkie atesty. To dodatkowe zabezpieczenie dla dzieci z wrazliwa skóra i sklonnoscia do uczulen. 

#ENDSWITCH
. 
</p>
<p>
Wraz z lózkiem w prezencie otrzymasz dwa materace piankowe, pasujace do stelazu, aby Twoje dzieci mogly od razu korzystac z jego zalet. </p>";
    }
}