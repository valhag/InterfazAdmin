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
    public partial class cfdiTraslado : Form
    {

        ClassRN lrn = new ClassRN();
        public cfdiTraslado()
        {

            InitializeComponent();
            seleccionEmpresa1.lsistema = 1;
        }

        private void cfdiTraslado_Load_1(object sender, EventArgs e)
        {
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

        }

        private void cfdiTraslado_Shown(object sender, EventArgs e)
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
            MessageBox.Show(seleccionEmpresa1.lrutaempresa);
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
            _RegFacturas = lrn.mCargarConceptosCartaPorte();
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

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime zz = DateTime.Parse("20/06/2022");

            //if (DateTime.Today >= zz)
              //  return;

            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Concepto = comboBox1.SelectedValue.ToString();
            Properties.Settings.Default.Save();

            string lcuantos = lrn.mLlenarTraslado(botonExcel1.mRegresarNombre());
            List<string> lista = new List<string>();

            MessageBox.Show(lcuantos);
            if (lcuantos == "")
            {

                //MessageBox.Show(lcuantos.ToString());
                //
                lista = lrn.mGrabarDoctosTraslado(false, 1);
                if (lista.Count == 0)
                    MessageBox.Show("Proceso Terminado");
                else
                    MessageBox.Show("Error en el proceso");
            }
            else
                MessageBox.Show(lcuantos);
        }
    }
}
