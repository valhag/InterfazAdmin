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
    public partial class Addenda : Form
    {

        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();
        public Addenda()
        {
            InitializeComponent();
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";
        }

        private void Addenda_Load(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;
            botonExcel2.mGeneraNombre(3, "Reporte de Ingresos al 18 de diciembre del 2018");
            //lsujeto.Registrar(this);
        }

        private void Addenda_Shown(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;
            //lsujeto.Registrar(this);

            this.Text = " Interfaz Excel/Addenda " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            if (Cadenaconexion != "" && Cadenaconexion != "data source =;initial catalog =CompacWAdmin ;user id = ; password = ;")
            {
                empresasComercial1.Populate(Cadenaconexion);
                mCargaConceptos();
                //   empresasComercial1.SelectedItem += new EventHandler(OnComboChange);
            }
            else
            {
                this.Visible = false;
                Form5 x = new Form5();
                //this.Visible = false;
                x.ShowDialog(this);
            }
            /*   botonExcel1.mGeneraNombre(2, "CARGA DE PEDIDOS");
               botonExcel2.mSetearEtiqueta("Archivo Bitacora");
               botonExcel2.mGeneraNombre(1);
               botonExcel2.mAsignaTipo(1);*/
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
            //  List<RegConcepto> _RegPedidos = new List<RegConcepto>();

            _RegFacturas = lrn.mCargarConceptosFacturaComercial();



            if (_RegFacturas.Count > 0)
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DataSource = _RegFacturas;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Codigo";
            }


        }

        private void Addenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            Properties.Settings.Default.Save();

            string archivo = botonExcel2.mRegresarNombre();
            string lrespuesta = lrn.mLLenarInfoMontesori(archivo);

            if (lrespuesta == "")
            {

                long folio = 0;
                bool incluyetimbrado = true;
                lrn.mGrabarDoctosComercial(2, ref folio, 1, 0, 0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Proceso Terminado");
            }
            else
                MessageBox.Show(lrespuesta);
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
    }
}    

