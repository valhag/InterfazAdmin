using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibreriaDoctos;
using Interfaces;


namespace InterfazAdmin
{
    public partial class AddendaTest : ComercialBase, IObservador
    {

        public AddendaTest()
        {
            InitializeComponent();
        }

        private void AddendaTest_Load(object sender, EventArgs e)
        {
            empresasComercial1.SelectedItem += new EventHandler(OnComboChange);

            ISujeto lsujeto = lrn.lbd;

            lsujeto.Registrar(this);

            botonExcel1.mSetearEtiqueta("Archivo Bitacora");
            botonExcel1.mGeneraNombre(1);
            botonExcel1.mAsignaTipo(1);


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

        private void AddendaTest_Shown(object sender, EventArgs e)
        {
            mCargaConceptos();
        }

        private void OnComboChange(object sender, EventArgs e)
        {

            mCargaConceptos();

        }

        private void mCargaConceptos()
        {
            if (empresasComercial1.aliasbdd != "")
            {

                Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
                Properties.Settings.Default.Save();

                List<RegConcepto> _RegFacturas = new List<RegConcepto>();
                List<RegConcepto> _RegDevoluciones = new List<RegConcepto>();
                List<RegConcepto> _RegDebito = new List<RegConcepto>();

                _RegFacturas = lrn.mCargarConceptosFacturaComercial();
                _RegDebito = lrn.mCargarConceptosFacturaComercial();

                _RegDevoluciones = lrn.mCargarConceptosDevolucionComercial();


                if (_RegFacturas.Count > 0)
                {
                    comboBox1.DataSource = null;
                    comboBox1.Items.Clear();
                    comboBox1.DataSource = _RegFacturas;
                    comboBox1.DisplayMember = "Nombre";
                    comboBox1.ValueMember = "Codigo";
                }

                if (_RegDevoluciones.Count > 0)
                {
                    comboBox2.DataSource = null;
                    comboBox2.Items.Clear();
                    comboBox2.DataSource = _RegDevoluciones;
                    comboBox2.DisplayMember = "Nombre";
                    comboBox2.ValueMember = "Codigo";
                }
                if (_RegDebito.Count > 0)
                {
                    comboBox3.DataSource = null;
                    comboBox3.Items.Clear();
                    comboBox3.DataSource = _RegDebito;
                    comboBox3.DisplayMember = "Nombre";
                    comboBox3.ValueMember = "Codigo";
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Pass = textBox3.Text;
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            RegConcepto Factura = (RegConcepto)comboBox1.SelectedItem;
            RegConcepto Devolucion = (RegConcepto)comboBox2.SelectedItem;
            RegConcepto Debito = (RegConcepto)comboBox3.SelectedItem;


            Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            Properties.Settings.Default.ConceptoD = Devolucion.Codigo.Trim();
            Properties.Settings.Default.ConceptoP = Debito.Codigo.Trim();


            Properties.Settings.Default.Pass = textBox3.Text;




            Properties.Settings.Default.Save();

            string archivo = botonExcel2.mRegresarNombre();
            string lrespuesta = lrn.mLLenarInfoAddendas(archivo);
            //return;
            if (lrespuesta == "")
            {

                long folio = 0;
                bool incluyetimbrado = true;
                lrn.mGrabarDoctosComercial(0, ref folio, 1,2, 0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora(botonExcel1.mRegresarNombre());
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                    MessageBox.Show("Proceso Terminado");
            }
            else
                MessageBox.Show(lrespuesta);
        }

        private void empresasComercial1_Load(object sender, EventArgs e)
        {
           
           // mCargaConceptos();
        }
    }
}
