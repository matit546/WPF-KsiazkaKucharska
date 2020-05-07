using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
namespace Ksiazka_Kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy Dodanie_Przepisu.xaml
    /// </summary>
    public partial class Dodanie_Przepisu : Window
    {
        List<string> lista = new List<string>();
        Domyslne_skladniki filter = new Domyslne_skladniki();
        string super_sciezka= "brak.jpg";
        bool edycja = false;
        public void odswiez()
        {
            Domyslne_skladniki inicjializacja = new Domyslne_skladniki();
            if (zapobiegacz_bledu == 0)
            {
                ilosc.ItemsSource = (inicjializacja.Iloscc);
                ilosc_czego.ItemsSource = (inicjializacja.Ilosc_czego);
            }
            Skladniki.ItemsSource = (inicjializacja.Skladniki);
        }
        public void wczytaj_zdjecie(MemoryStream stream)
        {
            if(stream==null)
            {
                return;
            }
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
            zdjecie.Source = imageSource;
        }
        public void wczytaj_okno_edycji()
        {
            Potrawa skladniki_1 = new Potrawa();

                Baza_danych wczytaj_baze3 = new Baza_danych(Potrawa.Aktualnie_Wybrana_Potrawa);
                cofnij.Visibility = System.Windows.Visibility.Hidden;
                _zakoncz.Visibility = System.Windows.Visibility.Hidden;
                Edytuj.Visibility = System.Windows.Visibility.Visible;
                cofnij_edycja.Visibility = System.Windows.Visibility.Visible;
                kliknij_do_dodania.Visibility = System.Windows.Visibility.Hidden;
                 kliknij_do_edycji.Visibility = System.Windows.Visibility.Visible;
                edytuj_skladnik.Visibility = System.Windows.Visibility.Visible;
                nazwa_potrawy.DataContext = wczytaj_baze3.M_oDataTable.DefaultView;
                opis.DataContext = wczytaj_baze3.M_oDataTable.DefaultView;
                kategoria.DataContext = wczytaj_baze3.M_oDataTable.DefaultView;
                kategoria2.DataContext = wczytaj_baze3.M_oDataTable.DefaultView;
                kategoria3.DataContext = wczytaj_baze3.M_oDataTable.DefaultView;
               wczytaj_zdjecie(wczytaj_baze3.wczytaj_zdjecie());
                skladniki_1.Przypisz_kategorie(nazwa_potrawy.Text, opis.Text,kategoria.Text, kategoria2.Text, kategoria3.Text);
                skladniki_1.poszczegolny_skladnik(Baza_danych.zwroc);
            for (int i = 0; i < skladniki_1.Poszczegolne_skladniki.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(skladniki_1.Poszczegolne_skladniki[i])) { return; }
                    dodane_skladniki.Items.Add(skladniki_1.Poszczegolne_skladniki[i]);
                }

        }

        public Dodanie_Przepisu()
        {
            InitializeComponent();
            odswiez();
            if (Baza_danych.Czy_Edycja == true)
            {
                wczytaj_okno_edycji();
            }
            nazwa_potrawy.MaxLength = 50;
            opis.MaxLength = 2000;
            kategoria.MaxLength = 50;
            kategoria2.MaxLength = 50;
            kategoria3.MaxLength = 50;
            ilosc_liter_nazwa_potrawy.Content = nazwa_potrawy.Text.Length.ToString() + "/" + nazwa_potrawy.MaxLength.ToString() + " liter";
            ilosc_liter_opis.Content = opis.Text.Length.ToString() + "/" + opis.MaxLength.ToString() + " liter";
            ilosc_liter_kat_1.Content = kategoria.Text.Length.ToString() + "/" + kategoria.MaxLength.ToString() + " liter";
            ilosc_liter_kat_2.Content = kategoria2.Text.Length.ToString() + "/" + kategoria2.MaxLength.ToString() + " liter";
            ilosc_liter_kat_3.Content = kategoria3.Text.Length.ToString() + "/" + kategoria3.MaxLength.ToString() + " liter";
            ilosc_skladnikow.Content = dodane_skladniki.Items.Count.ToString() + "/50 skladnikow";
        }

        public void Dodaj_Zedytuj(int ktore)
        {
            if (String.IsNullOrEmpty(nazwa_potrawy.Text) || String.IsNullOrEmpty(opis.Text) || String.IsNullOrEmpty(kategoria.Text)
                || String.IsNullOrEmpty(kategoria2.Text) || String.IsNullOrEmpty(kategoria3.Text) || dodane_skladniki.Items.Count == 0)
            {
                MessageBox.Show("Wypelnij wszystkie pola");
            }
            else
            {
                try
                {


                    Bitmap mojabitmapa = new Bitmap(super_sciezka);
                    byte[] bajt;
                    ImageConverter konwersja = new ImageConverter();

                    bajt = (byte[])konwersja.ConvertTo(mojabitmapa, typeof(byte[]));
                    if (ktore == 1)
                    {
                        if (edycja == false)
                        {
                            bajt = null;
                            edycja = true;
                        }

                    }
                    string[] zmienna = new string[50];
                    for (int i = 0; i < dodane_skladniki.Items.Count; i++)
                    {
                        zmienna[i] = dodane_skladniki.Items[i].ToString();
                        zmienna[i] = zmienna[i] + "#";
                    }
                    Potrawa nowa_potrawa = new Potrawa();
                    nowa_potrawa.Przypisz_kategorie(nazwa_potrawy.Text, opis.Text, kategoria.Text, kategoria2.Text, kategoria3.Text);
                    Baza_danych baza = new Baza_danych();
                    if (ktore == 0)
                    {
                        baza.Dodajprzepis(nowa_potrawa.Nazwa_potrawy, nowa_potrawa.Opis_potrawy, zmienna, nowa_potrawa.Kategoria_potrawy, bajt);
                    }
                    else
                    {
                        baza.Zedytuj_przepis(nowa_potrawa.Nazwa_potrawy, nowa_potrawa.Opis_potrawy, zmienna, nowa_potrawa.Kategoria_potrawy, bajt);
                        MessageBox.Show("Zedytowano");
                    }
                    MainWindow okno = new MainWindow();
                    okno.Show();

                    this.Close();
                }
                catch(Exception)
                {
                    MessageBox.Show("taka nazwa juz istnieje");
                    edycja = false;
                }
           

            }
        }
        private void zakoncz(object sender, RoutedEventArgs e)
        {
            Dodaj_Zedytuj(0);
        }

    
        private void cofnij_do_glownego_okna(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }

        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {

            Dodaj_Zedytuj(1);
            Baza_danych.Czy_Edycja = false;
        }

        private void cofnij_edycja_Click(object sender, RoutedEventArgs e)
        {
            Baza_danych.Czy_Edycja = false;
            Przepis otworz_okno = new Przepis();
            otworz_okno.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Skladniki.SelectedItem == null || ilosc.SelectedItem == null || ilosc_czego.SelectedItem == null)
            {
                MessageBox.Show("wybierz skladnik");
            }
            else
            {
                string zmienna;
                zmienna = Skladniki.SelectedItem.ToString() + " " + ilosc.SelectedItem.ToString() + " " + ilosc_czego.SelectedItem.ToString();
                if (dodane_skladniki.Items.Count < 50)
                {
                    dodane_skladniki.Items.Add(zmienna);
                    ilosc_skladnikow.Content = dodane_skladniki.Items.Count.ToString() + "/50 skladnikow";
                }
                else
                    MessageBox.Show("maksymalnie 50 skladnikow");
            }
        }

        private void Zedytuj_skladnik(object sender, RoutedEventArgs e)
        {
            try
            {
                string zmienna;
                zmienna = Skladniki.SelectedItem.ToString() + " " + ilosc.SelectedItem.ToString() + " " + ilosc_czego.SelectedItem.ToString();
                int wybrany_skladnik = dodane_skladniki.SelectedIndex;
                dodane_skladniki.Items.RemoveAt(dodane_skladniki.SelectedIndex);
                dodane_skladniki.Items.Insert(wybrany_skladnik, zmienna);
            }
            catch (Exception)
            {
                MessageBox.Show("Najpierw wypelnij wszystkie wartosci powyzej, nastepnie wybierz dany skladnik po lewej i na koncu wcisnij ten przycisk");
            }
        }

        private void usun_skladnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dodane_skladniki.Items.RemoveAt(dodane_skladniki.SelectedIndex);
                ilosc_skladnikow.Content = dodane_skladniki.Items.Count.ToString() + "/50 skladnikow";
            }
            catch (Exception)
            {
                MessageBox.Show("wybierz skladnik do usuniecia po lewej");
            }
        }

        private void Skladniki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            try
            {
                if (Skladniki.SelectedItem.ToString() == "wpisz sam")
                {
                    zapobiegacz_bledu = 1;
                    wlasny_skladnik wlasny = new wlasny_skladnik();
                    wlasny.ShowDialog();
                    Domyslne_skladniki dodaj = new Domyslne_skladniki();
                    dodaj.dodaj_wartosci_skladniki(Skladniki.SelectedIndex);
                    Skladniki.ItemsSource = dodaj.Skladniki;
                    Skladniki.SelectedIndex--;
                }
            }
            catch (Exception)
            {

            }
        }
        private void ilosc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (ilosc.SelectedItem.ToString() == "wpisz sam")
                {
                    zapobiegacz_bledu = 1;
                    wlasny_skladnik wlasny = new wlasny_skladnik();
                    wlasny.ShowDialog();
                    Domyslne_skladniki dodaj = new Domyslne_skladniki();
                    dodaj.dodaj_wartosci_ilosc(ilosc.SelectedIndex);
                    ilosc.ItemsSource = dodaj.Iloscc;
                    ilosc.SelectedIndex--;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("zle zamknales okno");
            }
        }

        private void ilosc_czego_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ilosc_czego.SelectedItem.ToString() == "wpisz sam")
                {
                    zapobiegacz_bledu = 1;
                    wlasny_skladnik wlasny = new wlasny_skladnik();
                    wlasny.ShowDialog();
                    Domyslne_skladniki dodaj = new Domyslne_skladniki();
                    dodaj.dodaj_wartosci_ilosc_czego(ilosc_czego.SelectedIndex);
                    ilosc_czego.ItemsSource = dodaj.Ilosc_czego;
                    ilosc_czego.SelectedIndex--;
                }
            }
            catch (Exception)
            {
             MessageBox.Show("zle zamknales okno");
            }
        }
        int licznik = 0;
        int zapobiegacz_bledu = 0;
        private void sortuj(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(filter_1.Text))
            {
                odswiez();
                return;
            }
            odswiez();
            if (licznik == 0)
            {

                for (int i = 0; i < Skladniki.Items.Count - 1; i++)
                {
                    lista.Add(Skladniki.Items[i].ToString());
                }
                licznik++;
            }

            filter.Skladniki.Clear();
            Skladniki.ItemsSource = filter.Skladniki;

            string zmienna = filter_1.Text;
       
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].IndexOf(filter_1.Text) == 0)
                {
                    filter.Dynamiczna_pamiec(lista[i]);
                    Skladniki.ItemsSource = filter.Skladniki;
                }

            }
            Skladniki.IsDropDownOpen = true;
        }

        private void Odswiez_Skladniki(object sender, MouseButtonEventArgs e)
        {
            Skladniki.SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filter_1.Text = null;
            Skladniki.IsDropDownOpen = true;
        }

        private void nazwa_potrawy_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter_nazwa_potrawy.Content = nazwa_potrawy.Text.Length.ToString() + "/" + nazwa_potrawy.MaxLength.ToString() + " liter";
        }

        private void opis_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter_opis.Content = opis.Text.Length.ToString() + "/" + opis.MaxLength.ToString() + " liter";
        }

        private void kategoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter_kat_1.Content = kategoria.Text.Length.ToString() + "/" + kategoria.MaxLength.ToString() + " liter";
   
        }

        private void kategoria2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter_kat_2.Content = kategoria2.Text.Length.ToString() + "/" + kategoria2.MaxLength.ToString() + " liter";
   
        }

        private void kategoria3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter_kat_3.Content = kategoria3.Text.Length.ToString() + "/" + kategoria3.MaxLength.ToString() + " liter";

        }

        private void dodaj_zdjecie(object sender, MouseButtonEventArgs e)
        {
            edycja = true;
            OpenFileDialog otworz = new OpenFileDialog();
           if(otworz.ShowDialog()==true)
            {
                super_sciezka = otworz.FileName;
                Uri moje_uri = new Uri(super_sciezka);
                zdjecie.Source = new BitmapImage(moje_uri);
            }
        }
    }
}
