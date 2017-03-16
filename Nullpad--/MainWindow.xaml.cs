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

        public MainWindow()
        {
            InitializeComponent();

            basisDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                txtHoofdtekst.Text = File.ReadAllText(huidigeFile);
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
            File.WriteAllText(huidigeFile, txtHoofdtekst.Text);
        }

        private void itemSluiten_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
