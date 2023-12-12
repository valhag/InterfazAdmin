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
using System.Globalization;

namespace InterfazAdmin
{
    public partial class PedidosSeries : ComercialBase, IObservador
    {

        public ClassRN lrn = new LibreriaDoctos.ClassRN();

        public void Actualizar(double message)
        {
            //int x = int(message);
            try
            {
                //this.progressBar1.Value = Convert.ToInt32(message);
            }
            catch (Exception eeeeee)
            { }
        }

        public PedidosSeries()
        {
            InitializeComponent();
        }

        protected void mCambioEmpresa(object sender, EventArgs e)
        {
            //handle the event
            //MessageBox.Show("hola");
         /*   this.codigocatalogo1.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;
            this.codigocatalogo2.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;

            this.codigocatalogo3.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;

            this.codigocatalogo4.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;

            this.codigocatalogo5.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;

/*            this.codigocatalogo6.lrutaempresa = this.seleccionEmpresa1.lrutaempresa;*/
            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            Properties.Settings.Default.Save();

            

        }

        public void Actualizar(string message)
        {
            //int x = int(message);
            listaerrores.Add(message);
        }

        private void PedidosSeries_Load(object sender, EventArgs e)
        {
            ISujeto lsujeto = lrn.lbd;

            lsujeto.Registrar(this);


            Properties.Settings.Default.database = "CompacWAdmin";
            Properties.Settings.Default.Save();
            Cadenaconexion = "data source =" + Properties.Settings.Default.server +
      ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
      "; password = " + Properties.Settings.Default.password + ";";

            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());
            empresasComercial1.SelectedItem += new EventHandler(mCambioEmpresa);

            this.codigocatalogocomercial1.mSetLabelText("Cliente");

            this.codigocatalogocomercial1.mSeteartipo(2,0);
            this.codigocatalogocomercial1.mSetLibreria(lrn);


            Properties.Settings.Default.RutaEmpresaADM = empresasComercial1.aliasbdd;
            Properties.Settings.Default.Save();
            //this.codigocatalogocomercial1.Enabled = false;

            //empresasComercial1.SelectedItem += new EventHandler(OnComboChange);

        }

        private void OnComboChange(object sender, EventArgs e)
        {

            

        }

        long liddocumento = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (liddocumento == 0)
            {
                lrn.lbd._RegDoctos.Clear();
                lrn.lbd._RegDoctoOrigen.cCodigoCliente = codigocatalogocomercial1.mGetCodigo();
                lrn.lbd._RegDoctoOrigen.cFecha = DateTime.Parse(textBox3.Text);
                lrn.lbd._RegDoctoOrigen.cSerie = textBox1.Text;
                lrn.lbd._RegDoctoOrigen.cFolio = long.Parse(textBox2.Text);
                lrn.lbd._RegDoctoOrigen.cCodigoConcepto = "2";

                liddocumento = lrn.lbd.mGrabarDoctosComercialEncabezado();
                if (liddocumento == 0)
                {
                    MessageBox.Show("Error al crear el documento");
                    return;
                }

            }

            PedidosSeriesMovimientos x = new PedidosSeriesMovimientos(lrn, empresasComercial1.aliasbdd, liddocumento,null);
            x.ShowDialog();
            mBuscarPedido();

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        
        }
        private void mllenargridmovtos(RegDocto doc)
        {
            dataGridView1.Rows.Clear();
            int i = 0;            
            foreach (RegMovto mov in doc._RegMovtos)
            {
                dataGridView1.Rows.Add(mov.cIdMovto, mov.cCodigoProducto, mov.cNombreProducto, mov.cCodigoAlmacen, mov.cNombreAlmacen, mov.cUnidades, mov.cPrecio, mov.cneto, mov.cImpuesto, mov.cTotal);
                List<RegCliente> lseries = lrn.mCargarSeriesPedidosComercial(mov.cIdMovto);
                if (lseries.Count < mov.cUnidades)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    mov.idseries.Clear();
                    foreach (RegCliente z in lseries)
                    {
                        mov.idseries.Add(z.Id);
                    }
                }
                i++;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            PedidosSeriesMovimientos x = new PedidosSeriesMovimientos(lrn, empresasComercial1.aliasbdd, liddocumento, dataGridView1.CurrentRow);
            x.ShowDialog();
            mBuscarPedido();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lret = mBuscarPedido();
            if (lret == 0)
                MessageBox.Show("Pedido No existe");
        }
        RegDocto doc = new RegDocto();
        private int mBuscarPedido()
        {
            dataGridView1.Rows.Clear();

            // BUscar documento
            doc = new RegDocto();
            doc = lrn.mBuscarDoctoComercial(textBox2.Text, textBox1.Text, "2");
            if (doc.cIdDocto > 0)
            {
                codigocatalogocomercial1.mSetCodigo(doc.cCodigoCliente);
                codigocatalogocomercial1.mSetDescripcion(doc.cRazonSocial);
                textBox3.Text = doc.cFecha.ToString();
                mllenargridmovtos(doc);
                return 1;
            }
            return 0;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PedidosSeriesMovimientos x = new PedidosSeriesMovimientos(lrn, empresasComercial1.aliasbdd, liddocumento, dataGridView1.CurrentRow);
                x.ShowDialog();
                mBuscarPedido();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            //DateTime fecha = DateTime.Parse( DateTime.Now.ToString("dd/MM/yyyy", ci));

            DateTime xfecha2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            DateTime xfechalimite = DateTime.ParseExact("01/02/2024", "dd/MM/yyyy", ci, DateTimeStyles.None);
            DateTime xfecha = DateTime.ParseExact(xfecha2.ToString("dd/MM/yyyy", ci), "dd/MM/yyyy", ci, DateTimeStyles.None);

            if (xfecha >= xfechalimite)

            {
                MessageBox.Show("Error en configuracion");
                return;
            }
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                if (x.DefaultCellStyle.BackColor == Color.Yellow)
                {
                    MessageBox.Show("Para poder generar la factura es necesario que todos los productos tengas asignadas sus numeros de serie");
                    return;
                }
            }
            lrn.lbd._RegDoctos.Clear();
            doc.cCodigoConcepto = "4";
            lrn.lbd._RegDoctos.Add(doc);
            long lultimofolio = 0;
            listaerrores.Clear();
            string lret = lrn.mGrabarDoctosComercial(0,ref lultimofolio,0,0,0,0);
            if (listaerrores.Count != 0)
            {
                MessageBox.Show(listaerrores[0]);
                //mGrabaErroresBitacora(botonExcel1.mRegresarNombre());
                //MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");

        }
    }
}
