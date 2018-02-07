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

        int lhoraanterior = 0;
            int    lminutoanterior =0;

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
            List<RegConcepto> _RegDevoluciones = new List<RegConcepto>();
            List<RegConcepto> _RegPagos = new List<RegConcepto>();
            List<RegConcepto> _RegNC = new List<RegConcepto>();

            _RegFacturas = lrn.mCargarConceptosFacturacfdiComercial();

            

            if (_RegFacturas.Count > 0)
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DataSource = _RegFacturas;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Codigo";
            }

            _RegDevoluciones = lrn.mCargarConceptosDevolucioncfdiComercial();
            if (_RegDevoluciones.Count > 0)
            {
                comboBox2.DataSource = null;
                comboBox2.Items.Clear();
                comboBox2.DataSource = _RegDevoluciones;
                comboBox2.DisplayMember = "Nombre";
                comboBox2.ValueMember = "Codigo";
            }
            _RegPagos = lrn.mCargarConceptosPagocfdiComercial();
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

            botonExcel1.mSetearEtiqueta("Archivo Bitacora");
            botonExcel1.mGeneraNombre(1);
            botonExcel1.mAsignaTipo(1);
            
            //
            
            //mCargaConceptos();
        }

        private void mProcesar(int manual=1)
        {


            Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            RegConcepto Devolucion = (RegConcepto)comboBox2.SelectedItem;
            Properties.Settings.Default.ConceptoD = Devolucion.Codigo.Trim();

            RegConcepto Pago = (RegConcepto)comboBox3.SelectedItem;
            Properties.Settings.Default.ConceptoP = Pago.Codigo.Trim();

            Properties.Settings.Default.Save();

            string archivo = textBox1.Text;
            //lrn.mLLenarInfoFacturacionMasiva(archivo);
            string lllenardocumento = lrn.mLlenarinfoXML(archivo);

            if (lllenardocumento == "")
            {
                List<string> lista = new List<string>();

                bool incluyetimbrado = true;
                long folio = 0;
                listaerrores.Clear();
                lrn.mGrabarDoctosComercial(1, ref folio,1);
                if (listaerrores.Count != 0)
                {
                    if (manual == 1)
                        MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();

                }
                else
                    if (manual == 1)
                        MessageBox.Show("Proceso Terminado");
            }
            else
                MessageBox.Show(lllenardocumento);
            
            
        }

        private void mGrabaErroresBitacora()
        {
            StreamWriter objwriter = new StreamWriter(botonExcel1.mRegresarNombre());
            // File.Delete(botonExcel1.mRegresarNombre());
            //StreamWriter objwriter = new StreamWriter(textBox5.Text);
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
            mProcesar(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            //second = second + 1;
            Properties.Settings.Default.RutaOrigen = textBox1.Text;
            Properties.Settings.Default.RutaBien = textBox2.Text;
            Properties.Settings.Default.RutaMal = textBox4.Text;


            Properties.Settings.Default.Save();


            if (hora == numericUpDown1.Value && minuto == numericUpDown2.Value)
            {
                if (hora != lhoraanterior && minuto != lminutoanterior)
                {
                    label9.Text = "Procesando con temporizador";
                    lhoraanterior = hora;
                    lminutoanterior = minuto;
                    mProcesar(0);
                    label9.Text = "";
                }

                //timer1.Stop();
                //MessageBox.Show("Exiting from Timer....");
            }
            else
            {
                lhoraanterior = 0;
                lminutoanterior =0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }

        private void XMLComercial_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }
    }
}
