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

namespace InterfazAdmin
{
    public partial class Microplane : Form
    {

        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";

        public Microplane()
        {
            InitializeComponent();
                       Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";
        }

        private void Microplane_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz Microplane " + " " + this.ProductVersion;

            txtServer.Text = Properties.Settings.Default.serverOrigen;
            txtBD.Text = Properties.Settings.Default.databaseOrigen;
            txtUser.Text = Properties.Settings.Default.userOrigen;
            txtPass.Text = Properties.Settings.Default.passwordO;


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


            Properties.Settings.Default.databaseOrigen = "DSN=" + textBox1.Text;
            Properties.Settings.Default.Save();

            //string archivo = textBox1.Text;
            lrn.mLlenarinfoMicroplane();

            List<string> lista = new List<string>();

            bool incluyetimbrado = true;
            //lista = lrn.mGrabarDoctosComercial(1);
            lrn.mGrabarDoctosComercial(1);
            if (lista.Count != 0)
            {
                MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cadenaconexion = "data source =" + txtServer.Text + ";initial catalog =" + txtBD.Text + ";user id = " + txtUser.Text + "; password = " + txtPass.Text + ";";

            string dsn = "DSN=" + @textBox1.Text;

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
    }
}
