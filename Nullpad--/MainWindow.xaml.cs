﻿using System;
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
            //OpenFileDialog verkennerOpenen = new OpenFileDialog();
            //verkennerOpenen.InitialDirectory = basisDir;

            //if (verkennerOpenen.ShowDialog().Value /*hetzelfde als verkennerOpenen.ShowDialog() == true, aangezien het van type bool? is*/)
            //{
            //    MessageBox.Show(verkennerOpenen.FileName);
            //}

            ////StreamReader inputStream;
            OpenFileDialog verkenner = new OpenFileDialog();

            verkenner.InitialDirectory = basisDir;

            if (verkenner.ShowDialog().Value)
            {
                huidigeFile = verkenner.FileName;
                ////inputStream = File.OpenText(huidigeFile);
                ////txtHoofdtekst.Text = inputStream.ReadToEnd();
                ////inputStream.Close();
                txtFileContents.Text = File.ReadAllText(huidigeFile);
            }
        }

        private void itemOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (huidigeFile == "")
            {
                SaveFileDialog opTeSlaan = new SaveFileDialog();

                opTeSlaan.InitialDirectory = basisDir;

                if (opTeSlaan.ShowDialog().Value)
                {
                    huidigeFile = opTeSlaan.FileName;
                }
            }

            //StreamWriter outputStream = File.CreateText(huidigeFile);
            //outputStream.Write(txtHoofdtekst.Text);
            //outputStream.Close();
            File.WriteAllText(huidigeFile, txtFileContents.Text);
        }

        private void itemOpslaanAls_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog opTeSlaan = new SaveFileDialog();

            opTeSlaan.InitialDirectory = basisDir;

            if (opTeSlaan.ShowDialog().Value)
            {
                huidigeFile = opTeSlaan.FileName;
                File.WriteAllText(huidigeFile, txtFileContents.Text);
            }
        }

        private void itemSluiten_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            List<Persoon> parsedPersonen = new List<Persoon>();

            string[] filedata = txtFileContents.Text.Split('\n');
            try
            {
                foreach (var row in filedata)
                {
                    // MessageBox.Show(String.Format("[{0}]", row.Trim()));

                    // is row valid?
                    string[] fields = row.Trim().Split(';');

                    if (fields.Length != 3)
                    {
                        MessageBox.Show(
                            String.Format("Couldn't parse [{0}]. Number of fields: {1}.",
                                row.Trim(),
                                fields.Length)
                        );
                    }
                    else if (row.Trim() == "")
                    {
                        // ignore empty rows
                    }
                    else
                    {
                        var p = new Persoon();
                        p.Voornaam = fields[0];
                        p.Achternaam = fields[1];
                        p.Geboortedatum = DateTime.Parse(fields[2]);
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
    }

    //private void itemPersonenlijst_Click(object sender, RoutedEventArgs e)
    //{
    //    string personenString = "";

    //    foreach (Persoon p in personen)
    //    {
    //        personenString += p.ToString() + Environment.NewLine;
    //    }

    //    MessageBox.Show(personenString);
    //}
}