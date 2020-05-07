using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Ksiazka_Kucharska
{
    class Baza_danych
    {
        private SQLiteDataAdapter m_oDataAdapter = null;
        private DataSet m_oDataSet = null;
        private DataTable m_oDataTable;
        public DataTable M_oDataTable
        {
            get { return m_oDataTable; }
            set { m_oDataTable = value; }
        }
        private static bool czy_edycja;
        public static bool Czy_Edycja
        {
            get { return czy_edycja; }
            set { czy_edycja = value; }
        }

        public static string zwroc;
        public Baza_danych()
        {
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                SQLiteCommand oCommand = baza.CreateCommand();
                oCommand.CommandText = "SELECT * FROM potrawy";
                m_oDataAdapter = new SQLiteDataAdapter(oCommand.CommandText, baza);
                SQLiteCommandBuilder oCommandBuilder = new SQLiteCommandBuilder(m_oDataAdapter);
                m_oDataSet = new DataSet();
                m_oDataAdapter.Fill(m_oDataSet);
                m_oDataTable = m_oDataSet.Tables[0];
            }

        }

        public Baza_danych(string cokliknales)
        {
            string sql = $"SELECT * FROM potrawy where nazwa_potrawy like'{cokliknales}'";
            string sql2 = $"SELECT  skladniki FROM potrawy where nazwa_potrawy like'{cokliknales}'";
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, baza))
                {
                    cmd.ExecuteNonQuery();
                    m_oDataAdapter = new SQLiteDataAdapter(cmd.CommandText, baza);

                }
                SQLiteCommand oCommand = baza.CreateCommand();
                SQLiteCommandBuilder oCommandBuilder = new SQLiteCommandBuilder(m_oDataAdapter);
                m_oDataSet = new DataSet();
                m_oDataAdapter.Fill(m_oDataSet);
                m_oDataTable = m_oDataSet.Tables[0];
                using (SQLiteCommand cmd = new SQLiteCommand(sql, baza))
                {
                    cmd.ExecuteNonQuery();
                    m_oDataAdapter = new SQLiteDataAdapter(cmd.CommandText, baza);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        zwroc = reader["skladniki"].ToString();
                    }
                }
            }
        }

        public MemoryStream wczytaj_zdjecie()
        {
            string sql = $"SELECT zdjecie FROM potrawy where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
            byte[] bajty;
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, baza))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        bajty = (byte[])reader["zdjecie"];
                    }
                }

            }
            Bitmap mojabitmapa = new Bitmap("brak.jpg");
            byte[] bajt;
            ImageConverter konwersja = new ImageConverter();
            bajt = (byte[])konwersja.ConvertTo(mojabitmapa, typeof(byte[]));
            if (bajt.Length==bajty.Length)
            {
                return null;
            }
            MemoryStream memory = new MemoryStream(bajty);
            return memory;
        }
         
        public void Wyszukaj_kategorie(string _kategoria)
        {
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                SQLiteCommand oCommand = baza.CreateCommand();
                oCommand.CommandText = $"SELECT  nazwa_potrawy FROM potrawy where kategoria_1  like'{_kategoria}'or kategoria_2  like'{_kategoria}' or kategoria_3  like'{_kategoria}'or nazwa_potrawy like'{_kategoria}'";
                m_oDataAdapter = new SQLiteDataAdapter(oCommand.CommandText, baza);
                SQLiteCommandBuilder oCommandBuilder = new SQLiteCommandBuilder(m_oDataAdapter);
                m_oDataSet = new DataSet();
                m_oDataAdapter.Fill(m_oDataSet);
                m_oDataTable = m_oDataSet.Tables[0];

            }
        }

        public void usun_potrawe()
        {
            string sql;
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                sql= $"DELETE FROM potrawy where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, baza))
                {
                    cmd.ExecuteNonQuery();
                }
                //SQLiteCommand oCommand = baza.CreateCommand();
                //oCommand.CommandText = $"DELETE FROM potrawy where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
                //oCommand.ExecuteNonQuery();
            }

        }

        public void Zedytuj_przepis(string _nazwa_potrawy, string _opis_potrawy, string[] _skladniki, string[] _Kategorie, byte []bajty)
        {
            string a = "";
            string sql;
            for (int i = 0; i < _skladniki.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(_skladniki[i])) { break; }
                a = a + _skladniki[i];
            }

            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                if (bajty == null)
                {
                    sql = $"UPDATE potrawy SET nazwa_potrawy = '{_nazwa_potrawy}', opis_potrawy ='{_opis_potrawy}', skladniki='{a}',kategoria_1='{_Kategorie[0]}', kategoria_2='{_Kategorie[1]}',kategoria_3='{_Kategorie[2]}' where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
                }
                else
                {
                  sql = $"UPDATE potrawy SET nazwa_potrawy = '{_nazwa_potrawy}', opis_potrawy ='{_opis_potrawy}', skladniki='{a}',kategoria_1='{_Kategorie[0]}', kategoria_2='{_Kategorie[1]}',kategoria_3='{_Kategorie[2]}',zdjecie=@bajty where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
                }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, baza))
                {
                    cmd.Parameters.Add(new SQLiteParameter("bajty", bajty));
                    cmd.ExecuteNonQuery();
                }
                
                //SQLiteCommand oCommand = oSQLiteConnection.CreateCommand();
                //oCommand.CommandText = $"UPDATE potrawy SET nazwa_potrawy = '{_nazwa_potrawy}', opis_potrawy ='{_opis_potrawy}', skladniki='{a}',kategoria_1='{_Kategorie[0]}', kategoria_2='{_Kategorie[1]}',kategoria_3='{_Kategorie[2]}' where nazwa_potrawy like'{Potrawa.Aktualnie_Wybrana_Potrawa}'";
                //oCommand.ExecuteNonQuery();
            }
        }

        public void Dodajprzepis(string nazwa_potrawy, string opis_potrawy, string[] skladniki, string[] Kategorie, byte[] bajty)
        {
            string a="";
            for( int i=0; i < skladniki.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(skladniki[i])) { break; }
                a = a + skladniki[i];
            }
            using (SQLiteConnection baza = new SQLiteConnection("Data Source=baza.s3db"))
            {
                baza.Open();
                SQLiteCommand oCommand = baza.CreateCommand();
                oCommand.CommandText = $"INSERT INTO potrawy(nazwa_potrawy,opis_potrawy,skladniki,kategoria_1,kategoria_2,kategoria_3,zdjecie) VALUES ('{nazwa_potrawy}',@opis_potrawy,'{a}','{Kategorie[0]}','{Kategorie[1]}','{Kategorie[2]}',@bajty)";
                oCommand.Parameters.Add(new SQLiteParameter("bajty", bajty));
                oCommand.Parameters.Add(new SQLiteParameter("opis_potrawy", opis_potrawy));
                oCommand.ExecuteNonQuery();
            }

        }

    }

    

}
