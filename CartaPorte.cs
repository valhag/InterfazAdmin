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
    public partial class CartaPorte : Form
    {
        ClassRN lrn = new ClassRN();
        public CartaPorte()
        {

            InitializeComponent();
            seleccionEmpresa1.lsistema = 1;
        }

        private void CartaPorte_Load(object sender, EventArgs e)
        {
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            

        }

        private void CartaPorte_Shown(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime zz = DateTime.Parse("01/02/2021");

            if (DateTime.Today < zz)
                return;

            List<string> lista = new List<string>();

            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Concepto = "4";// comboBox1.SelectedValue.ToString();
            Properties.Settings.Default.ConceptoP = "5"; // comboBox1.SelectedValue.ToString();
            Properties.Settings.Default.Save();

            int lcuantos = lrn.mLlenarInfoFresko(textBox1.Text);

            if (lcuantos > 0)
            {

                //MessageBox.Show(lcuantos.ToString());
                //
                lista = lrn.mGrabarDoctosFresko(false, 1);
                MessageBox.Show("Proceso Terminado");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                textBox1.Text = openFileDialog.FileName;

            }
        }

        private void seleccionEmpresa1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
