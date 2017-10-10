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
using System.Xml;

namespace InterfazAdmin
{
    public partial class XMLComercial : Form
    {
        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";

        public XMLComercial()
        {
            InitializeComponent();
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";
            //Archivo = Properties.Settings.Default.archivo;
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
            _RegFacturas = lrn.mCargarConceptosFacturacfdiComercial();

            if (_RegFacturas.Count > 0)
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DataSource = _RegFacturas;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Codigo";
            }


        }

        private void XMLComercial_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz xml " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            if (Cadenaconexion != "" && Cadenaconexion != "data source =;initial catalog =CompacWAdmin ;user id = ; password = ;")
            {
                empresasComercial1.Populate(Cadenaconexion);
                mCargaConceptos();
                empresasComercial1.SelectedItem += new EventHandler(OnComboChange);
            }
            else
            {
                Form5 x = new Form5();
                x.ShowDialog(this);
            }


            
            //
            
            //mCargaConceptos();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {


            

            Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();
            Properties.Settings.Default.Save();

            string archivo = textBox1.Text;
            //lrn.mLLenarInfoFacturacionMasiva(archivo);
            lrn.mLlenarinfoXML(archivo);

            List<string> lista = new List<string>();

            bool incluyetimbrado = true;
            //lista = lrn.mGrabarDoctosComercial(1);
            if (lista.Count != 0)
            {
                MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");
            
            

            

            
        }
    }
}
