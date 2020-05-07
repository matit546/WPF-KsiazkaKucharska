using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiazka_Kucharska
{
    class Domyslne_skladniki
    {



        private List<string> ilosc_czego;
        public List<string> Ilosc_czego
        {
            get { return ilosc_czego; }
            set { ilosc_czego = value; }
        }

        private List<string> skladniki;
        public List<string> Skladniki
        {
            get { return skladniki; }
            set { skladniki = value; }
        }

        private List<String> iloscc;
        public List<string> Iloscc
        {
            get { return iloscc; }
            set { iloscc = value; }
        }

        public Domyslne_skladniki()
        {
            domyslne_wartosci();
        }
        public void domyslne_wartosci()
        {
            Iloscc = new List<string>();
            Iloscc.Add("0,1");
            Iloscc.Add("0,25");
            Iloscc.Add("0,5");
            Iloscc.Add("0,75");
            Iloscc.Add("1");
            Iloscc.Add("1,5");
            Iloscc.Add("2");
            Iloscc.Add("3");
            Iloscc.Add("5");
            Iloscc.Add("10");
            Iloscc.Add("wpisz sam");

            Skladniki = new List<string>();
            Skladniki.Add("bulka");
            Skladniki.Add("szynka");
            Skladniki.Add("ser");
            Skladniki.Add("pomidor");
            Skladniki.Add("salata");
            Skladniki.Add("chleb");
            Skladniki.Add("cebula");
            Skladniki.Add("maslo");
            Skladniki.Add("mleko");
            Skladniki.Add("platki");
            Skladniki.Add("woda");
            Skladniki.Add("makaron");
            Skladniki.Add("sos pomidorowy");
            Skladniki.Add("wpisz sam");

            Ilosc_czego = new List<string>();
            Ilosc_czego.Add("litr");
            Ilosc_czego.Add("litra");
            Ilosc_czego.Add("sztuka");
            Ilosc_czego.Add("sztuki");
            Ilosc_czego.Add("plaster");
            Ilosc_czego.Add("sztuka");
            Ilosc_czego.Add("sztuki");
            Ilosc_czego.Add("plastry");
            Ilosc_czego.Add("plastry");
            Ilosc_czego.Add("gram");
            Ilosc_czego.Add("kg");
            Ilosc_czego.Add("dag");
            Ilosc_czego.Add("wpisz sam");
        }
        public void dodaj_wartosci_ilosc(int indeks)
        {

            Iloscc.Insert(indeks, Potrawa.Wlasna_wartosc);
            Iloscc.RemoveAt(indeks - 1);
        }

        public void dodaj_wartosci_ilosc_czego(int indeks)
        {

            Ilosc_czego.Insert(indeks, Potrawa.Wlasna_wartosc);
            Ilosc_czego.RemoveAt(indeks - 1);
        }

        public void dodaj_wartosci_skladniki(int indeks)
        {

            Skladniki.Insert(indeks, Potrawa.Wlasna_wartosc);
            Skladniki.RemoveAt(indeks - 1);
        }

        public void Dynamiczna_pamiec(string zmienna)
        {
            Skladniki = new List<string>();
            Skladniki.Add(zmienna);
        }
    }
}
