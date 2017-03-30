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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace Nullpad__
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string huidigeFile = "";
        private string basisDir;
        private DateTime geboorteDatum = new DateTime(1990, 1, 2);
        List<Persoon> personen = new List<Persoon>();

        public MainWindow()
        {
            InitializeComponent();

            basisDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            personen.Add(new Persoon { Voornaam = "Willy", Achternaam = "Janssens", Geboortedatum = new DateTime(1990, 1, 2) });
            personen.Add(new Persoon { Voornaam = "Ed", Achternaam = "Sheeran", Geboortedatum = new DateTime(2000, 12, 26) });
            personen.Add(new Persoon { Voornaam = "Hans", Achternaam = "Van Broeckhoven", Geboortedatum = new DateTime(1981, 10, 12) });

            parsedDataGrid.ItemsSource = personen;
        }

        private void itemOpenen_Click(object sender, RoutedEventArgs e)
        {
            //StreamReader inputStream;
            OpenFileDialog verkenner = new OpenFileDialog();

            verkenner.InitialDirectory = basisDir;

            if (verkenner.ShowDialog().Value)
            {
                huidigeFile = verkenner.FileName;
                //inputStream = File.OpenText(huidigeFile);
                //txtInhoud.Text = inputStream.ReadToEnd();
                //inputStream.Close();

                //txtInhoud.Text = File.OpenText(huidigeFile).ReadToEnd();

                txtFileContents.Text = File.ReadAllText(huidigeFile);
            }
        }

        private void itemOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (huidigeFile == "")
            {
                SaveFileDialog opslaanDir = new SaveFileDialog();

                opslaanDir.InitialDirectory = basisDir;

                if (opslaanDir.ShowDialog().Value)
                {
                    huidigeFile = opslaanDir.FileName;
                }
            }

            //StreamWriter outputStream = File.CreateText(huidigeFile);
            //outputStream.Write(txtInhoud.Text);
            //outputStream.Close();

            File.WriteAllText(huidigeFile, txtFileContents.Text);
        }

        private void itemOpslaanAls_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog opslaanDir = new SaveFileDialog();

            opslaanDir.InitialDirectory = basisDir;

            if (opslaanDir.ShowDialog().Value)
            {
                huidigeFile = opslaanDir.FileName;

                File.WriteAllText(huidigeFile, txtFileContents.Text);
            }
        }

        private void itemSluiten_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void itemParsen_Click(object sender, RoutedEventArgs e)
        {
            List<Persoon> parsedPersonen = new List<Persoon>();

            string[] filedata = txtFileContents.Text.Split('\n');

            try
            {
                foreach (string rij in filedata)
                {
                    //MessageBox.Show(String.Format("[{0}]", rij.Trim()));

                    //is rij geldig?

                    //Trim() verwijdert spaties aan beide uiteinden (van rij)
                    //Split() maakt een arr van strings, m.b.v. het 'verwijderen' van het scheidingsteken (i.d.g.: ';')
                    string[] velden = rij.Trim().Split(';');

                    if (velden.Length != 3)
                    {
                        MessageBox.Show(String.Format("Kon [{0}] niet parsen. Aantal velden: {1}.", rij.Trim(), velden.Length));
                    }
                    else if (rij.Trim() == "")
                    {
                        //negeer lege rijen
                        //ik denk toch dat ik het met "break;" moet doen ... maar ik ben niet zeker
                        break;
                    }
                    else
                    {
                        Persoon p = new Persoon();
                        p.Voornaam = velden[0];
                        p.Achternaam = velden[1];
                        p.Geboortedatum = DateTime.Parse(velden[2]);
                        parsedPersonen.Add(p);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            parsedDataGrid.ItemsSource = parsedPersonen;
        }

        private void itemPersonenlijst_Click(object sender, RoutedEventArgs e)
        {
            string personenString = "";

            foreach (Persoon p in personen)
            {
                personenString += p.ToString() + Environment.NewLine;
            }

            MessageBox.Show(personenString);
        }
    }
}