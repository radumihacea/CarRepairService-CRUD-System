using System;
using System.Windows;
using System.Windows.Controls;

namespace ClientWPFServiceAuto // Schimbă dacă proiectul tău are alt nume!
{
    public partial class MainWindow : Window
    {
        RefService.WebService1SoapClient client = new RefService.WebService1SoapClient();

        public MainWindow()
        {
            InitializeComponent();
            IncarcaDateleInTabel();
        }

        private void IncarcaDateleInTabel()
        {
            gridMasini.ItemsSource = client.ObtineMasini();
            CurataCasutele(); // Golim căsuțele după ce se încarcă lista
        }

        private void CurataCasutele()
        {
            txtId.Clear();
            txtNumar.Clear();
            txtMarca.Clear();
            txtProblema.Clear();
        }

        // --- CLICK PE BUTONUL DE ACTUALIZARE ---
        private void btnIncarca_Click(object sender, RoutedEventArgs e)
        {
            IncarcaDateleInTabel();
            txtCautare.Clear();
        }

        // --- CLICK PE BUTONUL ADĂUGARE ---
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string rezultat = client.AdaugaMasina(txtNumar.Text, txtMarca.Text, txtProblema.Text);
            MessageBox.Show(rezultat, "Informație", MessageBoxButton.OK, MessageBoxImage.Information);
            IncarcaDateleInTabel();
        }

        // --- CLICK PE BUTONUL MODIFICARE ---
        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtId.Text, out int idCurent))
            {
                string rezultat = client.ModificaMasina(idCurent, txtNumar.Text, txtMarca.Text, txtProblema.Text);
                MessageBox.Show(rezultat, "Modificare", MessageBoxButton.OK, MessageBoxImage.Information);
                IncarcaDateleInTabel();
            }
            else
            {
                MessageBox.Show("Te rog să selectezi o mașină din tabel mai întâi!", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // --- CLICK PE BUTONUL ȘTERGERE ---
        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtId.Text, out int idCurent))
            {
                var confirmare = MessageBox.Show("Sigur vrei să ștergi mașina cu ID-ul " + idCurent + "?", "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmare == MessageBoxResult.Yes)
                {
                    string rezultat = client.StergeMasina(idCurent);
                    MessageBox.Show(rezultat, "Ștergere", MessageBoxButton.OK, MessageBoxImage.Information);
                    IncarcaDateleInTabel();
                }
            }
            else
            {
                MessageBox.Show("Te rog să selectezi o mașină din tabel mai întâi!", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // --- CLICK PE BUTONUL CĂUTARE ---
        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            var rezultateCautare = client.CautaMasina(txtCautare.Text);
            gridMasini.ItemsSource = rezultateCautare;
        }

        // --- "MAGIA": Când dai click pe un rând în tabel, se completează stânga ---
        private void gridMasini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridMasini.SelectedItem != null)
            {
                // Luăm mașina pe care s-a dat click
                RefService.Masina masinaSelectata = (RefService.Masina)gridMasini.SelectedItem;

                // Punem datele ei în căsuțele din stânga
                txtId.Text = masinaSelectata.Id.ToString();
                txtNumar.Text = masinaSelectata.NumarInmatriculare;
                txtMarca.Text = masinaSelectata.Marca;
                txtProblema.Text = masinaSelectata.Problema;
            }
        }
    }
}