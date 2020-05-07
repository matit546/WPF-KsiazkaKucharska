using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiazka_Kucharska
{
    class Potrawa
    {

        private static string aktualnie_wybrana_potrawa;
        public static string Aktualnie_Wybrana_Potrawa
        {
            get {return aktualnie_wybrana_potrawa; }
            set { aktualnie_wybrana_potrawa = value; }
        }
        private static string wpisana_kategoria;
        public static string Wpisana_kategoira
        {
            get { return wpisana_kategoria; }
            set { wpisana_kategoria = value; }
        }
        private string nazwa_potrawy;
        public string Nazwa_potrawy
        {
            get { return nazwa_potrawy; }
            set { nazwa_potrawy = value; }
        }
        private string opis_potrawy;
        public string Opis_potrawy
        {
            get { return opis_potrawy; }
            set { opis_potrawy = value; }
        }
        private static string wlasna_wartosc;
        public static string Wlasna_wartosc
        {
            get { return wlasna_wartosc; }
            set {wlasna_wartosc = value; }
        }
        private string[] kategoria_potrawy;
        public string[] Kategoria_potrawy
        {
            get { return kategoria_potrawy; }
            set { kategoria_potrawy = value; }
        }
        private string[] poszczegolne_skladniki;
        public string[] Poszczegolne_skladniki
        {
            get { return poszczegolne_skladniki; }
            set { poszczegolne_skladniki = value; }
        }

        public Potrawa()
        {
        }

        public Potrawa(Potrawa kopia)
        {
            Kategoria_potrawy = new string[3];
            nazwa_potrawy = kopia.nazwa_potrawy;
            opis_potrawy = kopia.opis_potrawy;
            kategoria_potrawy[0] = kopia.kategoria_potrawy[0];
            kategoria_potrawy[1] = kopia.kategoria_potrawy[1];
            kategoria_potrawy[2] = kopia.kategoria_potrawy[2];

        }
        public void Przypisz_kategorie(string _nazwa, string _opis,string kategoria,string kategoria_2, string kategoria_3)
        {
            Nazwa_potrawy = _nazwa;
            Opis_potrawy = _opis;
            Kategoria_potrawy = new string[] { kategoria, kategoria_2, kategoria_3 };
  
        }

        public bool sprawdz_czy_cos_zmienione(Potrawa obiekt, string _nazwa, string _opis, string _kategoria1, string _kategoria2, string _kategoria3)
        {
            if (obiekt.nazwa_potrawy == _nazwa||obiekt.opis_potrawy == _opis||obiekt.kategoria_potrawy[0]== _kategoria1 || obiekt.kategoria_potrawy[1] == _kategoria2 || obiekt.kategoria_potrawy[2] == _kategoria3)
            {
                return true;
            }
            else return false;

        }

        public void  poszczegolny_skladnik(string zmienna)
        {
            Poszczegolne_skladniki = new string[50];
            string b = "";
            int i = 0;
            foreach (char znak in zmienna)
            {
                if(znak.ToString()!="#")
                b = b + znak;
                if (znak.ToString() == "#")
                {
                    Poszczegolne_skladniki[i] = b;
                    i++;
                    b = "";
                }

            }

        }

       

      
    }
}
