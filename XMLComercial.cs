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
using Interfaces;

namespace InterfazAdmin
{
    public partial class XMLComercial : Form, IObservador
    {
        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();

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
            timer1.Interval = 1000;
            timer1.Start();

            ISujeto lsujeto = lrn.lbd;
            lsujeto.Registrar(this);

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

        private void mProcesar()
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
            long folio = 0;
            lrn.mGrabarDoctosComercial(1, ref folio);
            if (listaerrores.Count != 0)
            {
                MessageBox.Show("Existen errores por favor revise bitacora");
                mGrabaErroresBitacora();

            }
            else
                MessageBox.Show("Proceso Terminado");
            
            
        }

        private void mGrabaErroresBitacora()
        {

            // File.Delete(botonExcel1.mRegresarNombre());
            StreamWriter objwriter = new StreamWriter(textBox5.Text);
            foreach (string x in listaerrores)
            {
                //abrir el arcvivo de bitacora

                objwriter.WriteLine(x);
            }
            objwriter.Flush();
            objwriter.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            mProcesar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            //second = second + 1;
            if (hora == numericUpDown1.Value && minuto == numericUpDown2.Value)
            {
                mProcesar();
                //timer1.Stop();
                //MessageBox.Show("Exiting from Timer....");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }
    }
}
