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
    public partial class Complemento : Form
    {
        public ClassRN lrn = new LibreriaDoctos.ClassRN();
        List<RegProducto> lista = new List<RegProducto>();
        
        public Complemento()
        {
            InitializeComponent();
        }

        private void codigocatalogo1_Load(object sender, EventArgs e)
        {

        }

        private void Complemento_Load(object sender, EventArgs e)
        {
            this.Text = "Complemento Arancelario 1   " + ProductVersion.ToString();
            //codigocatalogo1.PressSearchButton += new EventHandler(botonf3);
            //codigocatalogo2.PressSearchButton += new EventHandler(botonf3);
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            seleccionEmpresa1.SelectedItem += new EventHandler(mCambioEmpresa);

            this.codigocatalogo1.mSetLabelText("Clasif 1:");
            this.codigocatalogo2.mSetLabelText("Clasif 2:");
            this.codigocatalogo3.mSetLabelText("Clasif 3:");
            this.codigocatalogo4.mSetLabelText("Clasif 4:");
            this.codigocatalogo5.mSetLabelText("Clasif 5:");
            this.codigocatalogo6.mSetLabelText("Clasif 6:");

            this.codigocatalogo1.mSeteartipo(4,25);
            this.codigocatalogo1.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo2.mSeteartipo(4,26);
            this.codigocatalogo2.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo3.mSeteartipo(4, 27);
            this.codigocatalogo3.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo4.mSeteartipo(4, 28);
            this.codigocatalogo4.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo5.mSeteartipo(4, 29);
            this.codigocatalogo5.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo6.mSeteartipo(4, 30);
            this.codigocatalogo6.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;


        }

        protected void mCambioEmpresa(object sender, EventArgs e)
        {
            //handle the event
            //MessageBox.Show("hola");
            this.codigocatalogo1.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo2.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            
            this.codigocatalogo3.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            
            this.codigocatalogo4.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            
            this.codigocatalogo5.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            
            this.codigocatalogo6.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();

            this.codigocatalogo1.mClean();
            this.codigocatalogo2.mClean();
            this.codigocatalogo3.mClean();
            this.codigocatalogo4.mClean();
            this.codigocatalogo5.mClean();
            this.codigocatalogo6.mClean();


        }

        /*protected void mCambioEmpresa(object sender, EventArgs e)
        {
            //handle the event
            //MessageBox.Show("hola");
        }*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();
            lista.Clear();
            lista = lrn.mMostrarProductos(codigocatalogo1.mGetCodigo(), codigocatalogo2.mGetCodigo(), codigocatalogo3.mGetCodigo(), codigocatalogo4.mGetCodigo(), codigocatalogo5.mGetCodigo(), codigocatalogo6.mGetCodigo());
            dataGridView1.DataSource = lista;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;




        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Boolean lvalidaclave = mValidarClaveArancelaria(textBox1.Text);
            if (lvalidaclave == true)
            {
                if (dataGridView1.RowCount > 0)
                {
                    lrn.mGrabarComplemento(lista, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    MessageBox.Show("Se grabaron " + lista.Count.ToString() + " productos");

                    this.codigocatalogo1.mClean();
                    this.codigocatalogo2.mClean();
                    this.codigocatalogo3.mClean();
                    this.codigocatalogo4.mClean();
                    this.codigocatalogo5.mClean();
                    this.codigocatalogo6.mClean();

                    this.dataGridView1.DataSource = null;
                }
                else
                    MessageBox.Show("Despliegue informacion de productos");
            }
            else
                    MessageBox.Show ("clave arancelaria incorrecta");
            

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        Boolean mValidarClaveArancelaria(string clave)
        {
            string[] strArray = {"49019906"
,"49019904"
,"49119903"
,"48211001"
,"96100001"
,"49119999"
,"85234901"
,"39261001"
,"85182299"
,"85219099"
,"85444299"
,"85286101"
,"84713001"
,"39269099"
,"48191001"
,"95030012"};
            string findThisString = textBox1.Text;
            int strNumber;
            int strIndex = 0;
            Boolean lencontrado = false;
            for (strNumber = 0; strNumber < strArray.Length; strNumber++)
            {
                strIndex = strArray[strNumber].IndexOf(findThisString);
                if (strIndex >= 0)
                {
                    // break;
                    if (findThisString == strArray[strNumber])
                    {
                        lencontrado = true;
                        break;
                    }
                }
            }
            return lencontrado;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        

            if (textBox1.Text != "")
            {
                Boolean lencontrado = mValidarClaveArancelaria(textBox1.Text);
                if (lencontrado == false)
                {
                    MessageBox.Show("Clave Arancelaria Incorrecta intente de nuevo");
                    //textBox1.Text = "";
                }
            }


        }

        private void codigocatalogo1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
