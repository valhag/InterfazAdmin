using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interfaces;
using LibreriaDoctos;
using System.IO;
using System.Globalization;

namespace InterfazAdmin
{
    public partial class traspaso : Form,  IObservador
    {

        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();


        public traspaso()
        {
            InitializeComponent();
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";

        }
        string ldirectorioactual = "";
        private void traspaso_Load(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;
            lsujeto.Registrar(this);

            this.Text = " Generar Traspaso " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());
            ldirectorioactual = Directory.GetCurrentDirectory();
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

            botonExcel1.mSetearEtiqueta("Archivo Origen");
            //botonExcel1.mGeneraNombre(1);
            botonExcel1.mAsignaTipo(0);


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

        private void traspaso_Shown(object sender, EventArgs e)
        {
            

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

        private void button1_Click(object sender, EventArgs e)
        {


            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            //DateTime fecha = DateTime.Parse( DateTime.Now.ToString("dd/MM/yyyy", ci));

            DateTime xfecha2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            /*DateTime xfechalimite = DateTime.ParseExact("01/08/2023", "dd/MM/yyyy", ci, DateTimeStyles.None);
            DateTime xfecha = DateTime.ParseExact(xfecha2.ToString("dd/MM/yyyy", ci), "dd/MM/yyyy", ci, DateTimeStyles.None);

            if (xfecha >= xfechalimite)

            {
                MessageBox.Show("Error en configuracion");
                return;
            }*/


            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            Properties.Settings.Default.Save();

            string archivo = botonExcel1.mRegresarNombre();
            string lrespuesta = lrn.mLLenarInfoAdrianaTraspaso(archivo);

            if (lrespuesta == "")
            {

                long folio = 0;
                bool incluyetimbrado = true;
                listaerrores.Clear();
                lrn.mGrabarDoctosComercial(0, ref folio, 0, 0, 0,1); 
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Se genero traspaso con folio " + folio);
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Se genero traspaso con folio " + folio);  //MessageBox.Show("Proceso Terminado");
            }
            else
                MessageBox.Show(lrespuesta);
        }

        private void mGrabaErroresBitacora()
        {
            //StreamWriter objwriter = n ew StreamWriter(botonExcel2.mRegresarNombre());
            // File.Delete(botonExcel1.mRegresarNombre());
            //StreamWriter objwriter = new StreamWriter(textBox5.Text);


            string cad = ldirectorioactual + "\\bitacora" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            cad = cad + ".txt";
            StreamWriter objwriter = new StreamWriter(cad); 

            foreach (string x in listaerrores)
            {
                //abrir el arcvivo de bitacora

                objwriter.WriteLine(x);
            }
            objwriter.Flush();
            objwriter.Close();
            MessageBox.Show("Existen errores por favor revise bitacora "+ cad);

        }

        private void traspaso_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }
    }
}
