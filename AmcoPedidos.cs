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
    public partial class AmcoPedidos : Form
    {
        ClassRN lrn = new ClassRN();
        public AmcoPedidos()
        {
            InitializeComponent();
        }

        private void mProcesar(Boolean conMensajes)
        {
            List<string> lista = new List<string>();

            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Concepto = comboBox1.SelectedValue.ToString();
            Properties.Settings.Default.Save();

            int lcuantos = lrn.mLlenarInfoAmcoPedidos(textBox1.Text);

            if (lcuantos > 0)
            {

                //MessageBox.Show(lcuantos.ToString());
                lista = lrn.mGrabarDoctos(false, 1);
            }

            


            

        }


        private void button1_Click(object sender, EventArgs e)
        {
            mProcesar(true);
            

            
        }
        void tmr_Tick(object sender, EventArgs e)
        {
//            MessageBox.Show("uno");
            if (DateTime.Now.Minute == 4)
            {
                //this.ShowInTaskbar = true;
                //this.WindowState = FormWindowState.Normal;
                //MessageBox.Show("uno");
                mProcesar(false);
            }
        }

        private void AmcoPedidos_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz AMCO " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());


            Timer tmr = new Timer();
            tmr.Interval = 15000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();

            //seleccionEmpresa1.SelectedItem += new EventHandler(OnComboChange);
            //mCargaConceptos();
        }

        private void mCargaConceptos()
        {
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
            _RegFacturas = lrn.mCargarConceptosFacturacfdi();

            //if (comboBox1.Items.Count == 0)
            //{
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DataSource = _RegFacturas;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Codigo";
            //}

        }

        private void AmcoPedidos_Shown(object sender, EventArgs e)
        {

            this.seleccionEmpresa1.SelectedItem += new EventHandler(OnComboChange);
            try
            {
                mLlenaComboConceptos();
            }
            catch (Exception eeee)
            {

            }
        }
        private void OnComboChange(object sender, EventArgs e)
        {
            mLlenaComboConceptos();
        }
        private void mLlenaComboConceptos()
        {
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
                _RegFacturas = lrn.mCargarConceptosPedido();
            try
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DataSource = _RegFacturas;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Codigo";
            }
            catch (Exception eee)
            {

            }

        }
    }
}
