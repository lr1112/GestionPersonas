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
using GestionPersonas.Entidades;
using GestionPersonas.BLL;

namespace GestionPersonas.UI.Registros
{
    /// <summary>
    /// Interaction logic for rAportes.xaml
    /// </summary>
    public partial class rAportes : Window
    {
        private Aportes aporte = new Aportes();
        public rAportes()
        {
            InitializeComponent();
            this.DataContext = this.aporte;
            LlenarComboPersona();
            LlenarComboTipoAportes();
        }
        private void LlenarComboPersona()
        {
            this.PersonaIdComboBox.ItemsSource = PersonasBLL.GetList(x => true);
            this.PersonaIdComboBox.SelectedValuePath = "PersonaId";
            this.PersonaIdComboBox.DisplayMemberPath = "Nombres";

            if (PersonaIdComboBox.Items.Count > 0)
                PersonaIdComboBox.SelectedIndex = 0;
        }
        private void LlenarComboTipoAportes()
        {
            this.TipoAporteComboBox.ItemsSource = AportesBLL.GetTiposAportes(x => true);
            this.TipoAporteComboBox.SelectedValuePath = "TipoAporteId";
            this.TipoAporteComboBox.DisplayMemberPath = "Descripcion";

            if (TipoAporteComboBox.Items.Count > 0)
                TipoAporteComboBox.SelectedIndex = 0;
        }


        private bool Validar()
        {
            bool esValido = true;

            if (ConceptoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transacción Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (PersonaIdComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transacción Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (MontoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transacción Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }
        private void Cargar()
        {
            this.DetalleDataGrid.ItemsSource = null;
            this.DetalleDataGrid.ItemsSource = this.aporte.Detalle;
            this.DataContext = null;
            this.DataContext = this.aporte;
        }
        private void Limpiar()
        {
            aporte = new Aportes();
            Cargar();
            LlenarComboPersona();
            LlenarComboTipoAportes();
        }
        private void DetalleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(AporteIdTextBox.Text, out int AporteId);
            var Aporte = AportesBLL.Buscar(AporteId);

            if (Aporte != null)
                this.aporte = Aporte;
            else
                this.aporte = new Aportes();
            Cargar();
        }

        private void AgregarDetalleButton_Click(object sender, RoutedEventArgs e)
        {
            aporte.Detalle.Add(new AportesDetalle
            {
                AporteId = aporte.AporteId,
                TipoAporteId = (int)TipoAporteComboBox.SelectedValue,
                Monto = (double)Convert.ToDouble(MontoTextBox.Text),
                //Total = MontoTextBox.Text = listado.Sum(x => x.Monto).ToString("N2");

            });

            Cargar();
        }

        private void NButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = AportesBLL.Guardar(this.aporte);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion Exitosa!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes existe = AportesBLL.Buscar(this.aporte.AporteId);

            if (RolesBLL.Eliminar(this.aporte.AporteId))
            {
                Limpiar();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible eliminarlo", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void RemoverPermiso_Click_1(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.SelectedIndex < 0)
                return;//Con estas 2 lineas ya no vuelve a pasar
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                aporte.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }
    }
}
