using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibreriaDoctos;
using System.IO;
using Interfaces;

namespace InterfazAdmin
{
    public partial class AgrisP : Form, IObservador
    {
        ClassRN lrn = new ClassRN();
        List<string> listaerrores = new List<string>();

        public AgrisP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (mValida(txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text))
                MessageBox.Show("bien");
        }


        private bool mValida(string aServer, string abd, string ausu, string apwd, string lruta = "")
        {
            string Cadenaconexion = "data source =" + aServer + ";initial catalog =" + abd + ";user id = " + ausu + "; password = " + apwd + ";";
            SqlConnection _con = new SqlConnection();

            _con.ConnectionString = Cadenaconexion;
            try
            {
                _con.Open();
                // si se conecto grabar los datos en el cnf
                _con.Close();
                return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        private bool mValida(string aServer, string abd, string ausu, string apwd)
        {
            string Cadenaconexion = "data source =" + aServer + ";initial catalog =" + abd + ";user id = " + ausu + "; password = " + apwd + ";";
            SqlConnection _con = new SqlConnection();

            _con.ConnectionString = Cadenaconexion;
            try
            {
                _con.Open();
                // si se conecto grabar los datos en el cnf
                _con.Close();
                return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }

        private void AgrisP_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[1].Focus();
            tabControl1.Refresh();
            tabControl1.SelectedIndex = 1;
            this.Text = "Interface Agris";
            txtServerD.Text = Properties.Settings.Default.server;
            txtBDD.Text = Properties.Settings.Default.database;
            txtUsuarioD.Text = Properties.Settings.Default.user;
            txtPwdD.Text = Properties.Settings.Default.password;

            txtServer.Text = Properties.Settings.Default.serverOrigen;
            txtBD.Text = Properties.Settings.Default.databaseOrigen;
            txtUser.Text = Properties.Settings.Default.userOrigen;
            txtPass.Text = Properties.Settings.Default.passwordO;

            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            dateTimePicker2.Value = DateTime.Parse("07/17/2025");
            dateTimePicker1.Value = DateTime.Parse("07/17/2025");
            botonExcel2.mSetearEtiqueta("Archivo Bitacora");
            botonExcel2.mGeneraNombre(1);
            botonExcel2.mAsignaTipo(1);

            ISujeto lsujeto = lrn.lbd;

            lsujeto.Registrar(this);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mValida(txtServerD.Text, txtBDD.Text, txtUsuarioD.Text, txtPwdD.Text))
            {
                MessageBox.Show("Conexion Correcta");
                Properties.Settings.Default.server = txtServerD.Text;
                Properties.Settings.Default.database = txtBDD.Text;
                Properties.Settings.Default.user = txtUsuarioD.Text;
                Properties.Settings.Default.password = txtPwdD.Text;
                Properties.Settings.Default.Save();
            }
            else
                MessageBox.Show("Revise informacion de la conexion");

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (mValida(txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text))
            {
                MessageBox.Show("Conexion Correcta");
                Properties.Settings.Default.serverOrigen = txtServer.Text;
                Properties.Settings.Default.databaseOrigen = txtBD.Text;
                Properties.Settings.Default.userOrigen = txtUser.Text;
                Properties.Settings.Default.passwordO = txtPass.Text;
                Properties.Settings.Default.Save();
            }
            else
                MessageBox.Show("Revise informacion de la conexion");

        }


        void mProcesarCatalogos()
        {
            //Properties.Settings.Default.Pass = textBox3.Text;
            string lruta = "C:\\Compac\\Empresas\\" + txtBDD.Text;
            //lruta = Properties.Settings.Default.RutaEmpresaADM;

            Properties.Settings.Default.RutaEmpresaADM = lruta;
            //RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            //Properties.Settings.Default.Concepto = Factura.Codigo.Trim();
            Properties.Settings.Default.Save();

            List<RegCliente> clientes = new List<RegCliente>();
            List<RegProducto> productos = new List<RegProducto>();

            int i = lrn.mLLenarInfoCatalogos(clientes, productos);

            if (i == 1)
            {
                lrn.mGuardarInfoCatalogos(clientes, productos);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DateTime.Today.Year >= 2026)
                return;

            mProcesarCatalogos();
            MessageBox.Show("Proceso Terminado");

        }

        private void AgrisP_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }

        private void mGrabaErroresBitacora()
        {
            StreamWriter objwriter = new StreamWriter(botonExcel2.mRegresarNombre());
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


        public void Actualizar(double message)
        {
            //int x = int(message);
            try
            {
                this.progressBar1.Value = Convert.ToInt32(message);
                this.progressBar1.MarqueeAnimationSpeed = 30;
            }
            catch (Exception eeeeee)
            { }
        }

        public void Actualizar(string message)
        {
            //int x = int(message);
            listaerrores.Add(message);
        }


        private void mProcesarDocumentoAutomatico()
        {
            List<RegDocto> lDoctos = new List<RegDocto>();
            DateTime fecha1 = DateTime.Today;
            DateTime fecha2 = DateTime.Today;


            string sfecha1 = fecha1.Year.ToString() + fecha1.Month.ToString().PadLeft(2, '0') + fecha1.Day.ToString().PadLeft(2, '0');
            string sfecha2 = fecha2.Year.ToString() + fecha2.Month.ToString().PadLeft(2, '0') + fecha2.Day.ToString().PadLeft(2, '0');
            lrn.mLlenarDocumentosAgris("Facturas", lDoctos, sfecha1, sfecha2);
            lrn.mGrabarPagosAgris();
            if (listaerrores.Count != 0)
            {
                mGrabaErroresBitacora();
            }
             lDoctos = new List<RegDocto>();
            lrn.mLlenarDocumentosAgris("Pagos", lDoctos, sfecha1, sfecha2);
            lrn.mGrabarPagosAgris();
            if (listaerrores.Count != 0)
            {
                mGrabaErroresBitacora();
            }
            lDoctos = new List<RegDocto>();
            lrn.mLlenarDocumentosAgris("Devoluciones", lDoctos, sfecha1, sfecha2);
            lrn.mGrabarPagosAgris();
            if (listaerrores.Count != 0)
            {
                mGrabaErroresBitacora();
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DateTime.Today.Year >= 2026)
                return;

            List<RegDocto> lDoctos = new List<RegDocto>();
            DateTime fecha1 = dateTimePicker1.Value;
            DateTime fecha2 = dateTimePicker2.Value;


            string sfecha1 = fecha1.Year.ToString() + fecha1.Month.ToString().PadLeft(2,'0')+ fecha1.Day.ToString().PadLeft(2,'0');
            string sfecha2 = fecha2.Year.ToString() + fecha2.Month.ToString().PadLeft(2, '0') + fecha2.Day.ToString().PadLeft(2, '0');
            if (radioButton1.Checked)
            {
                lrn.mLlenarDocumentosAgris("Pagos", lDoctos, sfecha1, sfecha2);

                lrn.mGrabarPagosAgris();
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Proceso Terminado");


            }
            if (radioButton2.Checked)
            {
                lrn.mLlenarDocumentosAgris("Facturas", lDoctos,sfecha1,sfecha2);

                lrn.mGrabarPagosAgris();
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Proceso Terminado");

            }
            if (radioButton3.Checked)
            {
                lrn.mLlenarDocumentosAgris("Devoluciones", lDoctos,sfecha1,sfecha2);

                lrn.mGrabarPagosAgris();
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Proceso Terminado");

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string lhoracatalogos = "22";
            string lminutoscatalogos = "23";


            if (DateTime.Now.Hour.ToString() == lhoracatalogos && DateTime.Now.Minute.ToString() == lminutoscatalogos && (DateTime.Now.Second > 1 && DateTime.Now.Second < 5))
            {
                mProcesarCatalogos();
                mProcesarDocumentoAutomatico();

            }
        }
    }
}
