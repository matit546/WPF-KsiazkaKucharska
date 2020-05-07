using System;
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
using System.Windows.Shapes;
using System.IO;
namespace Ksiazka_Kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy Przepis.xaml
    /// </summary>
    public partial class Przepis : Window
    {

        public Przepis()
        {
            InitializeComponent();
            wczytaj();
        }
        public void  wczytaj_zdjecie(MemoryStream stream)
        {
            if (stream == null)
            {
                return;
            }
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
            this.Background = new ImageBrush(imageSource);
        }

        public void wczytaj()
        {
            try
            {
                Baza_danych wczytaj_baze2 = new Baza_danych(Potrawa.Aktualnie_Wybrana_Potrawa);
                nazwa.DataContext = wczytaj_baze2.M_oDataTable.DefaultView;
                opis.DataContext = wczytaj_baze2.M_oDataTable.DefaultView;
                kategoria1.DataContext = wczytaj_baze2.M_oDataTable.DefaultView;
                kategoria2.DataContext = wczytaj_baze2.M_oDataTable.DefaultView;
                kategoria3.DataContext = wczytaj_baze2.M_oDataTable.DefaultView;
                wczytaj_zdjecie(wczytaj_baze2.wczytaj_zdjecie());
                Potrawa skladniki_1 = new Potrawa();
                skladniki_1.poszczegolny_skladnik(Baza_danych.zwroc);
                for (int i = 0; i < skladniki_1.Poszczegolne_skladniki.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(skladniki_1.Poszczegolne_skladniki[i])) { return; }
                    dzialaj.Items.Add(skladniki_1.Poszczegolne_skladniki[i]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void zamknij_okno(object sender, RoutedEventArgs e)
        {
            MainWindow okno_glowne = new MainWindow();
            okno_glowne.Show();
            Close();
        }

        private void Edytuj_przepis(object sender, RoutedEventArgs e)
        {
            Baza_danych.Czy_Edycja = true;
            Dodanie_Przepisu okno = new Dodanie_Przepisu();
            okno.Show();
            Close();
 
        }
    }
}
