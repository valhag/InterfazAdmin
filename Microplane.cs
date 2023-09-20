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
using System.Data.Odbc;
using Interfaces;

namespace InterfazAdmin
{
    public partial class Microplane : Form, IObservador
    {

        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();

        public Microplane()
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
                this.progressBar1.Value = Convert.ToInt32(message);
            }
            catch (Exception eeeeee)
            { }
        }

        public void Actualizar(string message)
        {
            //int x = int(message);
            listaerrores.Add(message);
        }

        private void Microplane_Load(object sender, EventArgs e)
        {

            ISujeto lsujeto = lrn.lbd;
            //IObservador lobs = (IObservador)this;
            //lobs.Miforma = this;
            lsujeto.Registrar(this);

            this.Text = " Interfaz Microplane " + " " + this.ProductVersion;

            txtServer.Text = Properties.Settings.Default.serverOrigen;
            txtBD.Text = Properties.Settings.Default.databaseOrigen;
            txtUser.Text = Properties.Settings.Default.userOrigen;
            txtPass.Text = Properties.Settings.Default.passwordO;


            textBox1.Text = Properties.Settings.Default.DSN;

            textBox2.Text = Properties.Settings.Default.UltimoFolio;
            textBox4.Text = "0";


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
        }

        private void Microplane_Shown(object sender, EventArgs e)
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

            progressBar1.Value = 0;
            listaerrores.Clear();



            Properties.Settings.Default.Pass = textBox3.Text;
            
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            /*
            Properties.Settings.Default.serverOrigen = txtServer.Text;
            Properties.Settings.Default.databaseOrigen = txtBD.Text;
            Properties.Settings.Default.userOrigen = txtUser.Text;
            Properties.Settings.Default.passwordO = txtPass.Text;
             */


            Properties.Settings.Default.databaseOrigen = "DSN=" + textBox1.Text + ";UID=Reports;Pwd=;";
            Properties.Settings.Default.DSN = textBox1.Text;
            Properties.Settings.Default.Save();

            //string archivo = textBox1.Text;

            int lFolioInicial = int.Parse(textBox2.Text);
            int lFolioFinal = int.Parse(textBox4.Text);
            lrn.mLlenarinfoMicroplane(lFolioInicial, lFolioFinal);

            List<string> lista = new List<string>();

            
            long lultimofolio = 0;
            if (checkBox1.Checked == true)
            {

                bool incluyetimbrado = true;
                //lista = lrn.mGrabarDoctosComercial(1);
                
                lrn.mGrabarDoctosComercial(1, ref lultimofolio,0,1);
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();

                }
                else
                    MessageBox.Show("Proceso Terminado");

                Properties.Settings.Default.UltimoFolio = lultimofolio.ToString();
                textBox2.Text = lultimofolio.ToString();
                Properties.Settings.Default.Save();

            }
            else
            {
                MessageBox.Show("Se leyeron " + lrn.lbd._RegDoctos.Count() + " documentos");
                Properties.Settings.Default.UltimoFolio = lultimofolio.ToString();
                textBox2.Text = lultimofolio.ToString();
                Properties.Settings.Default.Save();
            }
            
        }


        private void mGrabaErroresBitacora()
        {

           // File.Delete(botonExcel1.mRegresarNombre());
            StreamWriter objwriter = new StreamWriter(botonExcel1.mRegresarNombre());
            foreach (string x in listaerrores)
            { 
                //abrir el arcvivo de bitacora
                
                objwriter.WriteLine(x);
            }
            objwriter.Flush();
            objwriter.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cadenaconexion = "data source =" + txtServer.Text + ";initial catalog =" + txtBD.Text + ";user id = " + txtUser.Text + "; password = " + txtPass.Text + ";";

            string dsn = "DSN=" + @textBox1.Text + ";UID=Reports;Pwd=;";

            OdbcConnection DbConnection = new OdbcConnection(dsn);
            try
            {
                DbConnection.Open();
                MessageBox.Show("Conexion Correcta");
            }
            catch(Exception eeeee)
            {
                MessageBox.Show("Revise datos de conexion " + eeeee.Message);
                return;
            }


            Properties.Settings.Default.databaseOrigen = dsn;
            Properties.Settings.Default.DSN = textBox1.Text;
            Properties.Settings.Default.Save();

            /*Properties.Settings.Default.serverOrigen = txtServer.Text;
            Properties.Settings.Default.databaseOrigen = txtBD.Text;
            Properties.Settings.Default.userOrigen = txtUser.Text;
            Properties.Settings.Default.passwordO = txtPass.Text;*/

            

            /*
            System.Data.SqlClient.SqlConnection _con = new System.Data.SqlClient.SqlConnection();

            _con.ConnectionString = Cadenaconexion;
            try
            {
                _con.Open();
                // si se conecto grabar los datos en el cnf
                _con.Close();
                MessageBox.Show("Conexion Correcta");
            }
            catch (Exception ee)
            {
                MessageBox.Show("Revise datos de conexion");
            }
             * */
        }

        private void Microplane_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }
    }
}
