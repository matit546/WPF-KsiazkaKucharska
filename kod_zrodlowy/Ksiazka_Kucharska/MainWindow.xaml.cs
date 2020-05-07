using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ksiazka_Kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            wczytaj();
        }

        private void wczytaj()
        {
            try
            {

                Baza_danych wczytaj_baze = new Baza_danych();
                pokaz.DataContext = wczytaj_baze.M_oDataTable.DefaultView;
                pokaz.UnselectAll();
                aktualniewybrana.Content = "brak";
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.ToString());
            }
        }

        public void Wyswietl_przepis()
        {
            Przepis okno = new Przepis();
            okno.Show();
            Close();
        }

        private void dodaj_przepis(object sender, RoutedEventArgs e)
        {
            Dodanie_Przepisu okno = new Dodanie_Przepisu();
            okno.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (pokaz.SelectedItem == null)
            {
                MessageBox.Show("wybierz przepis do usuniecia");
            }
            else
            {
                MessageBoxResult wynik = MessageBox.Show("Czy na pewno chcesz usunac", "Usuwanie", MessageBoxButton.YesNo);
                if (wynik == MessageBoxResult.Yes)
                {
                    Potrawa.Aktualnie_Wybrana_Potrawa = przechwyc_nazwe.Text;
                    Baza_danych usun = new Baza_danych();
                    usun.usun_potrawe();
                    wczytaj();
                }
                else
                    MessageBox.Show("Anulowales usuwanie przepisu");
            }
            
        }

        private void Wyswietl_przepis(object sender, RoutedEventArgs e)
        {
            if (pokaz.SelectedItem == null)
            {
                MessageBox.Show("wybierz przepis");
            }
            else
            {
                Potrawa.Aktualnie_Wybrana_Potrawa = przechwyc_nazwe.Text;
                Wyswietl_przepis();
            }
        }

        private void pokaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aktualniewybrana.Content = przechwyc_nazwe.Text;
        }

        private void szukaj(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(szukanie.Text))
            {
                pokaz.IsSynchronizedWithCurrentItem = false;
                pokaz.SelectedItem = null;
                aktualniewybrana.Content = "brak";
                MessageBox.Show("Puste pole");
            }
            else
            {
                pokaz.IsSynchronizedWithCurrentItem = true;
                Baza_danych wczytaj_kategorie = new Baza_danych();
                wczytaj_kategorie.Wyszukaj_kategorie(szukanie.Text);
                pokaz.DataContext = wczytaj_kategorie.M_oDataTable.DefaultView;
                if(String.IsNullOrEmpty(przechwyc_nazwe.Text))
                {
                    MessageBox.Show("Brak kategorii");
                    wczytaj();
                }
                szukanie.Clear();
            }
   
            pokaz.IsSynchronizedWithCurrentItem = false;
        }

        private void Losowy_przepis(object sender, RoutedEventArgs e)
        {
            try
            {
                Random losuj = new Random();
                int numer = losuj.Next(0, pokaz.Items.Count);
                pokaz.SelectedItem = pokaz.Items[numer];
                Potrawa.Aktualnie_Wybrana_Potrawa = przechwyc_nazwe.Text;
                Wyswietl_przepis();
            }
            catch(Exception)
            {
                MessageBox.Show("Brak przepisow");
            }
        }

        private void odswiez(object sender, RoutedEventArgs e)
        {
            wczytaj();
        }
    }
}
