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
using System.IO;


namespace InterfazAdmin
{
    public partial class Autorizaciones : ComercialBase, IObservador
    {

        ClassRN lrn = new ClassRN();
        public string Cadenaconexion = "";
        List<string> listaerrores = new List<string>();
        
            // .ButtonClick += new EventHandler(UserControl_ButtonClick);


        public Autorizaciones()
        {
            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";
            InitializeComponent();
        }

        private void Autorizaciones_Load(object sender, EventArgs e)
        {
            this.codigocatalogocomercial1.mSetLabelText("Cliente");

            this.codigocatalogocomercial1.mSeteartipo(1, 25);
            this.codigocatalogocomercial1.mSetLibreria(lrn);
            this.codigocatalogocomercial1.Enabled = false;
            this.button1.Enabled = false;

            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;

            //this.codigocatalogocomercial1. = this.seleccionEmpresa1.lrutaempresa;

        }

        
        protected void UserControl_ButtonClick(object sender, EventArgs e)
        {
            //handle the event 
        }

        private void OnComboChange(object sender, EventArgs e)
        {

            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            Properties.Settings.Default.Save();
            dataGridView1.DataSource = null;
            codigocatalogocomercial1.mSetDescripcion("");
            codigocatalogocomercial1.mSetCodigo("");

            dataGridView1.Rows.Clear();
            //mCargaConceptos();
        }

        private void mllenarDoctos()
        {
            List<RegOrigen> lista = new List<RegOrigen>();

            if (comboBox1.SelectedItem.ToString() == "Usuario 1")
            lista = lrn.mCargarDocumentosComercialDoctoDeCliente(1, codigocatalogocomercial1.lRegClienteProveedor.Id);
            else
                lista = lrn.mCargarDocumentosComercialDoctoDeCliente(2, codigocatalogocomercial1.lRegClienteProveedor.Id);
            dataGridView1.DataSource = lista;


            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].Visible = false;

            if (comboBox1.SelectedItem.ToString() == "Usuario 2")
                dataGridView1.Columns[19].Visible = true;


            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows[0].Selected = true;



            SendKeys.Send("Tab");
            dataGridView1.Focus();

        }

        public void OnButtonClickF3(object sender, EventArgs e)
        {
            //llenar pedidos
            mllenarDoctos();
            
        }


        private void mCargaConceptos(string usuario)
        {
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            Properties.Settings.Default.Save();

            List<RegConcepto> _RegFacturas = new List<RegConcepto>();
            List<RegConcepto> _RegPedidos = new List<RegConcepto>();
            //  List<RegConcepto> _RegPedidos = new List<RegConcepto>();

            if (usuario == "1")
            {
                _RegPedidos = lrn.mCargarConceptosPedidosComercial();
                _RegFacturas = lrn.mCargarConceptosFacturaComercial(1);
            }
            else
            {
                _RegPedidos = lrn.mCargarConceptosFacturaComercial(0); ;
                _RegFacturas = lrn.mCargarConceptosEntradaComercial();
            }

            comboBox2.DataSource = null;
            comboBox3.DataSource = null;
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            

            if (_RegPedidos.Count > 0)
            {
                
                
                comboBox2.DataSource = _RegPedidos;
                comboBox2.DisplayMember = "Nombre";
                comboBox2.ValueMember = "Codigo";
            }

            if (_RegFacturas.Count > 0)
            {
                
                
                comboBox3.DataSource = _RegFacturas;
                comboBox3.DisplayMember = "Nombre";
                comboBox3.ValueMember = "Codigo";
            }


        }

        private void Autorizaciones_Shown(object sender, EventArgs e)
        {

            this.codigocatalogocomercial1.PressSearchButton += new EventHandler(OnButtonClickF3);
        
            ISujeto lsujeto = lrn.lbd;
            lsujeto.Registrar(this);

            this.Text = " Autorizaciones" + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());
            /*
            if (Cadenaconexion != "" && Cadenaconexion != "data source =;initial catalog =CompacWAdmin ;user id = ; password = ;")
            {
                empresasComercial1.Populate(Cadenaconexion);
                empresasComercial1.SelectedItem += new EventHandler(OnComboChange);
                //mCargaConceptos();
            }
            else
            {
                this.Visible = false;
                Form5 x = new Form5();
                //this.Visible = false;
                x.ShowDialog(this);
            }
            */
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

        private void codigocatalogocomercial1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;

            RegConcepto Pedido = new RegConcepto();
            RegConcepto Factura = new RegConcepto();

            if (comboBox1.SelectedIndex == 0) // usuario1
            {
                Pedido = (RegConcepto)comboBox2.SelectedItem;
                Properties.Settings.Default.ConceptoP = Pedido.Codigo.Trim();

                Factura = (RegConcepto)comboBox3.SelectedItem;
                Properties.Settings.Default.Concepto = Factura.Codigo.Trim();

            }
            else
            {
                Pedido = (RegConcepto)comboBox3.SelectedItem;
                Properties.Settings.Default.ConceptoP = Pedido.Codigo.Trim();

                Factura = (RegConcepto)comboBox2.SelectedItem;
                Properties.Settings.Default.Concepto = Factura.Codigo.Trim();
            }

            Properties.Settings.Default.Save();

            //string archivo = botonExcel1.mRegresarNombre();

            RegOrigen x = new RegOrigen();
            x = (RegOrigen)dataGridView1.CurrentRow.DataBoundItem;


            // cliente sin credito y  
            if (comboBox1.SelectedIndex == 0) // validaciones para cotizaciones
            {
                if (codigocatalogocomercial1.lRegClienteProveedor.BanVentaCredito == 1 )  // cliente credito validar limite de credito < saldo cliente + total docto
                {
                    decimal saldo = lrn.mSaldoClienteComercial(codigocatalogocomercial1.lRegClienteProveedor.Id);
                    if (x.cTotal > saldo + codigocatalogocomercial1.lRegClienteProveedor.LimiteCredito)
                    MessageBox.Show("No es posible generar los documentos el total del documento sobrepasa el limite de credito");
                    return;
                }
            }
            if (comboBox1.SelectedIndex == 1) // validaciones para pedidos
            {
                if (codigocatalogocomercial1.lRegClienteProveedor.BanVentaCredito == 0 && x.cpendiente > 0) // cliente contado y saldo
                {
                    MessageBox.Show("No es posible generar los documentos ya que el cliente no tiene credito y no se ha pagado completamente");
                    return;
                }

            }
            string lrespuesta = "";
            if (comboBox1.SelectedItem.ToString() == "Usuario 1")
            {
                lrn.lbd._RegDoctos.Clear();
                lrespuesta = lrn.mLlenarinfoAutorizaciones(x.ciddocumento, Pedido.Codigo,"2");
                lrespuesta = lrn.mLlenarinfoAutorizaciones(x.ciddocumento, Factura.Codigo,"4");
            }
            else
            {
                lrn.lbd._RegDoctos.Clear();
                lrespuesta = lrn.mLlenarinfoAutorizaciones(x.ciddocumento, Factura.Codigo,"4");
                lrespuesta = lrn.mLlenarinfoAutorizaciones(x.ciddocumento, Pedido.Codigo,"32");
            }


            if (lrespuesta == "")
            {

                long folio = 0;
                bool incluyetimbrado = true;
                lrn.mGrabarDoctosComercial(0, ref folio, 1, 3,0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {
                    MessageBox.Show("Proceso Terminado");
                    mllenarDoctos();
                }
            }
            else
                MessageBox.Show(lrespuesta);
                
        }
        private void mGrabaErroresBitacora()
        {
        /*    StreamWriter objwriter = new StreamWriter(botonExcel2.mRegresarNombre());
            // File.Delete(botonExcel1.mRegresarNombre());
            //StreamWriter objwriter = new StreamWriter(textBox5.Text);
            foreach (string x in listaerrores)
            {
                //abrir el arcvivo de bitacora

                objwriter.WriteLine(x);
            }
            objwriter.Flush();
            objwriter.Close();
    */    
    }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            { 
            if (comboBox1.SelectedItem == "Usuario 1")
            {

                if (textBox1.Text == "123")
                    {
                        codigocatalogocomercial1.mSetCodigo("");
                        codigocatalogocomercial1.mSetDescripcion("");
                        codigocatalogocomercial1.Enabled = true;
                        dataGridView1.DataSource = null;
                        button1.Text = "Generar Pedido y Factura CFDI";
                        label3.Text = "Pedido";
                        label4.Text = "Factura CFDI";
                        button1.Enabled = true;
                        this.comboBox2.Enabled = true;
                        this.comboBox3.Enabled = true;
                        mCargaConceptos("1");
                    }
                else {
                        codigocatalogocomercial1.mSetCodigo("");
                        codigocatalogocomercial1.mSetDescripcion("");
                        codigocatalogocomercial1.Enabled = false;
                        button1.Enabled = false;
                        dataGridView1.DataSource = null;
                        this.comboBox2.Enabled = false;
                        this.comboBox3.Enabled = false;
                        MessageBox.Show("clave incorrecta");
                    }

            }
            else
                    if (textBox1.Text == "321")
                {
                    codigocatalogocomercial1.mSetCodigo("");
                    codigocatalogocomercial1.mSetDescripcion("");
                    codigocatalogocomercial1.Enabled = true;
                    dataGridView1.DataSource = null;
                    button1.Enabled = true;
                    button1.Text = "Generar Factura No CFDI Y Entrada al Almacen";
                    label3.Text = "Factura No CFDI";
                    label4.Text = "Entrada al Almacen";
                    this.comboBox2.Enabled = true;
                    this.comboBox3.Enabled = true;
                    mCargaConceptos("2");
                }
                else
                {
                    codigocatalogocomercial1.mSetCodigo("");
                    codigocatalogocomercial1.mSetDescripcion("");
                    codigocatalogocomercial1.Enabled = false;
                    button1.Enabled = false;
                    dataGridView1.DataSource = null;
                    this.comboBox2.Enabled = false;
                    this.comboBox3.Enabled = false;
                    MessageBox.Show("clave incorrecta");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codigocatalogocomercial1.mSetCodigo("");
            codigocatalogocomercial1.mSetDescripcion("");
            codigocatalogocomercial1.Enabled = false;
            button1.Enabled = false;
        }

        private void Autorizaciones_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
