using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibreriaDoctos;

namespace InterfazAdmin
{
    public partial class PedidosSeriesMovimientos : Form
    {

        public ClassRN lrn;
        public string rutaempresa;
        long liddocumento;
        DataGridViewRow movto;
        public PedidosSeriesMovimientos()
        {
            InitializeComponent();
        }
        public PedidosSeriesMovimientos(ClassRN alrn, string arutaempresa, long aiddocumento, DataGridViewRow amovto)
        {
            rutaempresa = arutaempresa;
            lrn = alrn;
            liddocumento= aiddocumento;
            movto = amovto;
            InitializeComponent();
           
        }

        private void PedidosSeriesMovimientos_Load(object sender, EventArgs e)
        {
            // dataGridView1.Rows.Insert(0, "", "");
            //dataGridView1.Rows.Insert(0, "", "");
            dataGridView1.AllowUserToAddRows = false;
            this.Text = "Pedidos Series  " + ProductVersion.ToString();

            this.codigocatalogocomercial1.mSetLabelText("Producto");

            this.codigocatalogocomercial1.mSeteartipo(4, 0);
            this.codigocatalogocomercial1.mSetLibreria(lrn);

            this.codigocatalogocomercial2.mSetLabelText("Almacen");

            this.codigocatalogocomercial2.mSeteartipo(5, 0);
            this.codigocatalogocomercial2.mSetLibreria(lrn);

            if (movto != null)
            {
                this.codigocatalogocomercial1.mSetCodigo(movto.Cells[1].Value.ToString());
                this.codigocatalogocomercial1.mSetDescripcion(movto.Cells[2].Value.ToString());
                this.codigocatalogocomercial2.mSetCodigo(movto.Cells[3].Value.ToString());
                this.codigocatalogocomercial2.mSetDescripcion(movto.Cells[4].Value.ToString());
                this.tbDecimales1.mSetearDecimal(movto.Cells[6].Value.ToString());
                this.tbDecimales2.mSetearDecimal(movto.Cells[8].Value.ToString());
                this.tbDecimales3.mSetearDecimal(movto.Cells[9].Value.ToString());
                List<RegCliente> lseries = lrn.mCargarSeriesPedidosComercial(long.Parse(movto.Cells[0].Value.ToString()));

                dataGridView1.AllowUserToAddRows = true;
                foreach (RegCliente x in lseries)
                {
                    
                    dataGridView1.Rows.Insert(0, x.Codigo, x.RazonSocial, x.Id);
                    
                }
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.AllowUserToAddRows = false;
            }
            //mov.cCodigoProducto, mov.cNombreProducto, mov.cCodigoAlmacen, mov.cNombreAlmacen, mov.cUnidades, mov.cPrecio, mov.cneto, mov.cImpuesto, mov.cTotal
            tbDecimales4.mSetearDecimal(movto.Cells[5].Value.ToString());
            decimal lneto = decimal.Parse(movto.Cells[5].Value.ToString()) * decimal.Parse(movto.Cells[6].Value.ToString());
            tbDecimales4.mSetearDecimal(movto.Cells[5].Value.ToString());
            tbDecimales5.mSetearDecimal(lneto.ToString());



        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("entrando grid ");
         /*   if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Rows[0].Cells[0].Value != null)
                    dataGridView1.Rows.Insert(0, "", "");
            }
            else
                dataGridView1.Rows.Insert(0, "", "");
        */
            }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        
        }
        void proceso(string Serie)
        {
            if (tbDecimales1.mRegresarDecimal() == "")
            {
                MessageBox.Show("Capture Precio");
                return;
            }

            if (codigocatalogocomercial1.mGetCodigo() == "")
            {
                MessageBox.Show("Capture Producto");
                return;
            }
            if (codigocatalogocomercial2.mGetCodigo() == "")
            {
                MessageBox.Show("Capture Almacen");
                return;
            }


            RegCliente serie =  lrn.mBuscarSerieComercial(Serie, codigocatalogocomercial1.mGetCodigo(), codigocatalogocomercial2.mGetCodigo());
            if (serie.Id != 0)
            {

                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.Rows.Insert(0, Serie, serie.RazonSocial, serie.Id);
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                /*decimal valor = ((dataGridView1.Rows.Count-1) * decimal.Parse(tbDecimales1.mRegresarDecimal()));
                valor = valor * (decimal)0.16;

                tbDecimales2.mSetearDecimal(valor.ToString());
                valor = valor + ((dataGridView1.Rows.Count-1) * decimal.Parse(tbDecimales1.mRegresarDecimal()));
                tbDecimales3.mSetearDecimal(valor.ToString());*/
                if (dataGridView1.Rows.Count-1 == int.Parse(tbDecimales4.mRegresarDecimal()))
                {
                    //MessageBox.Show("ch;ido");
                    mGrabarMovtoSeries();
                    this.Close();
                }
            }

            else
            {
                MessageBox.Show("Serie no existe / tiene un estado incorrecto / ya esta asignada a otro pedido");
            }
            // Make the Row at index 1 the Current setting the CurrentCell property
            
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    if (dataGridView1.Rows.Count == int.Parse(tbDecimales4.mRegresarDecimal()))
                        MessageBox.Show("No es posible asignar mas series");
                    else  
                        proceso(textBox1.Text);

                    textBox1.Text = "";
                    textBox1.Focus();
                }

            
            
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public RegDocto regglobal = new RegDocto();



        private void button1_Click(object sender, EventArgs e)
        {
            mGrabarMovtoSeries();
            /*RegDocto doc = new RegDocto();
            doc.cIdDocto = liddocumento;
            doc.cFecha = DateTime.Now;
            RegMovto mov = new RegMovto();
            mov.cCodigoAlmacen = codigocatalogocomercial2.mGetCodigo();
            mov.cCodigoProducto = codigocatalogocomercial1.mGetCodigo();
            mov.cUnidades = dataGridView1.Rows.Count;
            mov.cPrecio = decimal.Parse(tbDecimales1.mRegresarDecimal().ToString());
            doc._RegMovtos.Add(mov);
            regglobal._RegMovtos.Add(mov);
            long lregresa =  lrn.lbd.mGrabarMovimientoComercial(doc, dataGridView1);*/
            
            //limpiarpantalla();

        }

        private void mGrabarMovtoSeries()
        {
            /*RegDocto doc = new RegDocto();
            doc.cIdDocto = liddocumento;
            doc.cFecha = DateTime.Now;
            RegMovto mov = new RegMovto();
            mov.cCodigoAlmacen = codigocatalogocomercial2.mGetCodigo();
            mov.cCodigoProducto = codigocatalogocomercial1.mGetCodigo();
            mov.cUnidades = dataGridView1.Rows.Count;
            mov.cPrecio = decimal.Parse(tbDecimales1.mRegresarDecimal().ToString());
            doc._RegMovtos.Add(mov);*/
            //regglobal._RegMovtos.Add(mov);
            long lregresa = lrn.lbd.mGrabarSeriesPedidoComercial(long.Parse(movto.Cells[0].Value.ToString()), dataGridView1);

            //limpiarpantalla();
        }
        private void limpiarpantalla()
        {
            codigocatalogocomercial1.mClean();
            codigocatalogocomercial2.mClean();
            tbDecimales1.mSetearDecimal("0");
            tbDecimales2.mSetearDecimal("0");
            tbDecimales3.mSetearDecimal("0");
            dataGridView1.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*RegDocto doc = new RegDocto();
            doc.cIdDocto = liddocumento;
            doc.cFecha = DateTime.Now;
            RegMovto mov = new RegMovto();
            mov.cCodigoAlmacen = codigocatalogocomercial2.mGetCodigo();
            mov.cCodigoProducto = codigocatalogocomercial1.mGetCodigo();
            mov.cUnidades = dataGridView1.Rows.Count;
            mov.cPrecio = decimal.Parse(tbDecimales1.mRegresarDecimal().ToString());
            doc._RegMovtos.Add(mov);
            regglobal._RegMovtos.Add(mov);
            long lregresa = lrn.lbd.mGrabarMovimientoComercial(doc, dataGridView1);
            */
            this.Close();
        }

        private void PedidosSeriesMovimientos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataGridView1.Rows.Count < int.Parse(tbDecimales4.mRegresarDecimal()))
            {
                DialogResult lrest = MessageBox.Show("No ha completado el total de series Desea grabar y completar despues?","Pregunta",MessageBoxButtons.YesNo);
                if (lrest == DialogResult.Yes)
                    mGrabarMovtoSeries();
                //this.Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
            }
        }
    }
}
