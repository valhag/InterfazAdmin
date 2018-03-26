using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibreriaDoctos;
using Interfaces;
using System.IO;

namespace InterfazAdmin
{
    public partial class PedidosFacturas : Form, IObservador
    {
        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();
     
        public PedidosFacturas()
        {
            
            InitializeComponent();
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";
        }

        public void Actualizar(double message)
        {
            //int x = int(message);
            try
            {
                //this.progressBar1.Value = Convert.ToInt32(message);
            }
            catch (Exception eeeeee)
            { }
        }

        public void Actualizar(string message)
        {
            //int x = int(message);
            listaerrores.Add(message);
        }

        private void PedidosFacturas_Load(object sender, EventArgs e)
        {
            if (this == null)
            {
               
            }
        }
        private void OnComboChange(object sender, EventArgs e)
        {
            mCargaConceptos();

        }


        private void mCargaConceptos()
        {
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
            List<RegConcepto> _RegPedidos = new List<RegConcepto>();
            
            _RegFacturas = lrn.mCargarConceptosFacturacfdiComercial();


            
            if (_RegFacturas.Count > 0)
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DataSource = _RegFacturas;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Codigo";
            }

            _RegPedidos = lrn.mCargarConceptosPedidosComercial();
            if (_RegPedidos.Count > 0)
            {
                comboBox2.DataSource = null;
                comboBox2.Items.Clear();
                comboBox2.DataSource = _RegPedidos;
                comboBox2.DisplayMember = "Nombre";
                comboBox2.ValueMember = "Codigo";
            }
            /*_RegPagos = lrn.mCargarConceptosPagocfdiComercial();
            if (_RegPagos.Count > 0)
            {
                comboBox3.DataSource = null;
                comboBox3.Items.Clear();
                comboBox3.DataSource = _RegPagos;
                comboBox3.DisplayMember = "Nombre";
                comboBox3.ValueMember = "Codigo";
            }
            _RegNC = lrn.mCargarConceptosNCcfdiComercial();
            if (_RegNC.Count > 0)
            {
                comboBox4.DataSource = null;
                comboBox4.Items.Clear();
                comboBox4.DataSource = _RegNC;
                comboBox4.DisplayMember = "Nombre";
                comboBox4.ValueMember = "Codigo";
            }*/

        }

        private void PedidosFacturas_Shown(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;
            lsujeto.Registrar(this);

            this.Text = " Interfaz Pedidos/facturas " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            if (Cadenaconexion != "" && Cadenaconexion != "data source =;initial catalog =CompacWAdmin ;user id = ; password = ;")
            {
                empresasComercial1.Populate(Cadenaconexion);
                mCargaConceptos();
                empresasComercial1.SelectedItem += new EventHandler(OnComboChange);
            }
            else
            {
                this.Visible = false;
                Form5 x = new Form5();
                //this.Visible = false;
                x.ShowDialog(this);
            }
            botonExcel1.mGeneraNombre(2, "CARGA DE PEDIDOS");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            //RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            //Properties.Settings.Default.Concepto = Factura.Codigo.Trim();
            Properties.Settings.Default.Save();

            string archivo = botonExcel1.mRegresarNombre();
            lrn.mLLenarInfoPedidosFacturas(archivo);

            List<string> lista = new List<string>();

            bool incluyetimbrado = true;
            lista = lrn.mGrabarDoctos(incluyetimbrado, 1);
            if (lista.Count != 0)
            {
                MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");
        }
    }
}
