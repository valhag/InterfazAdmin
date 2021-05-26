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
using Interfaces;


namespace InterfazAdmin
{
    public partial class ComercialBase : Form
    {

        protected ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        public List<string> listaerrores = new List<string>();
        public ComercialBase()
        {
            InitializeComponent();
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";

        }

        protected void mGrabaErroresBitacora(string archivo)
        {
            StreamWriter objwriter = new StreamWriter(archivo);
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

        private void ComercialBase_Load(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;
            //lsujeto.Registrar(this);

            this.Text = " Interfaz Excel/AddendaTest " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            if (Cadenaconexion != "" && Cadenaconexion != "data source =;initial catalog =CompacWAdmin ;user id = ; password = ;")
            {
                empresasComercial1.Populate(Cadenaconexion);
                //mCargaConceptos();
                //   empresasComercial1.SelectedItem += new EventHandler(OnComboChange);
            }
            else
            {

                Form5 y = new Form5();
                y.Visible = false;
                DialogResult lresp = y.ShowDialog(this);
                if (lresp == DialogResult.OK)
                {
                    Cadenaconexion = "data source =" + Properties.Settings.Default.server +
                    ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
                    "; password = " + Properties.Settings.Default.password + ";";
                    empresasComercial1.Populate(Cadenaconexion);
                    //MessageBox.Show("Conexion correcta, volver a ejecutar el exe");
                    //this.Close();
                }
                //this.Visible = false;
                //Form5 x = new Form5();
                //this.Visible = false;
                //x.ShowDialog(this);
            }
        }

        private void empresasComercial1_Load(object sender, EventArgs e)
        {

        }

        private void ComercialBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }
    }
}
