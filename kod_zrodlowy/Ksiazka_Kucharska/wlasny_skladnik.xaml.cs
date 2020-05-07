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

namespace Ksiazka_Kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy wlasny_skladnik.xaml
    /// </summary>
    public partial class wlasny_skladnik : Window
    {
        public wlasny_skladnik()
        {
            InitializeComponent();
            twoja_rzecz.Focus();
            twoja_rzecz.MaxLength = 20;
            ilosc_liter.Content = twoja_rzecz.Text.Length.ToString()+"/"+twoja_rzecz.MaxLength.ToString()+" liter";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Potrawa.Wlasna_wartosc = twoja_rzecz.Text;
            Close();
        }

        private void twoja_rzecz_TextChanged(object sender, TextChangedEventArgs e)
        {
            ilosc_liter.Content = twoja_rzecz.Text.Length.ToString() + "/" + twoja_rzecz.MaxLength.ToString() + " liter";
            if (twoja_rzecz.Text.Length==20)
            {
                MessageBox.Show("max liter");
            }
        }
    }
}
