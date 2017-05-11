using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibreriaDoctos;
using System.IO;

namespace InterfazAdmin
{
    public partial class FacturacionMasiva : Form
    {
        ClassRN lrn = new ClassRN();

        public FacturacionMasiva()
        {
            InitializeComponent();
        }

        private void FacturacionMasiva_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz Facturacion Masiva " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            seleccionEmpresa1.SelectedItem += new EventHandler(OnComboChange);
            mCargaConceptos();
            

            botonExcel1.mGeneraNombre(2,"FacturacionMasiva");
        }

        private void mCargaConceptos()
        {
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
            _RegFacturas = lrn.mCargarConceptosFacturacfdi();

            if (_RegFacturas.Count == 0)
            {
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DataSource = _RegFacturas;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Codigo";
            }
            

        }

        private void OnComboChange(object sender, EventArgs e)
        {
            mCargaConceptos();
        





        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto =Factura.Codigo.Trim();
            Properties.Settings.Default.Save();

            string archivo = botonExcel1.mRegresarNombre();
            lrn.mLLenarInfoFacturacionMasiva(archivo);

            List<string> lista = new List<string>();

            bool incluyetimbrado = true;
            lista = lrn.mGrabarDoctos(incluyetimbrado,1);
            if (lista.Count != 0)
            {
                MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {

        }
    }
}
