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

namespace InterfazAdmin
{

    //   public string Cadenaconexion = "";

    public partial class Produccion : Form
    {
        ClassRN lrn = new ClassRN();

        List<string> listaerrores = new List<string>();


        public Produccion()
        {
            InitializeComponent();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private bool mValida()
        {
            string Cadenaconexion = "data source =" + txtServer.Text + ";initial catalog =" + txtBD.Text + ";user id = " + txtUser.Text + "; password = " + txtPass.Text + ";";
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

        string Cadenaconexion = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (mValidando())

                MessageBox.Show("Valores de conexion correctos");


            else
                MessageBox.Show("Valores de conexion incorrectos");

        }

        private Boolean mValidando()
        {
            if (mValida())
            {
                Properties.Settings.Default.server = txtServer.Text;
                Properties.Settings.Default.database = txtBD.Text;
                Properties.Settings.Default.user = txtUser.Text;
                Properties.Settings.Default.password = txtPass.Text;
                Properties.Settings.Default.RutaEmpresaADM = "c:\\compac\\empresas\\" + txtBD.Text;


                Properties.Settings.Default.Save();

                Cadenaconexion = "data source =" + Properties.Settings.Default.server +
                ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
                "; password = " + Properties.Settings.Default.password + ";";
                return true;
            }
            else
            {
                return false;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        List<RegAlmacen> almacenes;

        private void Produccion_Load(object sender, EventArgs e)
        {
            //this.Text = "Datos Conexion SQLServer";
            txtServer.Text = Properties.Settings.Default.server;
            txtBD.Text = Properties.Settings.Default.database;
            txtUser.Text = Properties.Settings.Default.user;
            txtPass.Text = Properties.Settings.Default.password;
            this.Text = " Produccion " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            almacenes = lrn.mCargarAlmacenesComercialv2();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        RegDocto d = new RegDocto();

        private void BuscarPedidoCompra(DataGridView dataGridView1)
        {
            dataGridView1.DataSource = null;
            string folio = textBox2.Text;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            List<RegDocto> listadoctos = lrn.mCargarDocumentosComercialReferencia("19", folio, ref dt, ref dt1);

            if (d != null)
            {

                label8.Text = d.cRazonSocial;
                label9.Text = d.cFecha.ToString();
                List<regmovtocorto> lista = new List<regmovtocorto>();
                List<regmovtooc> listaoc = new List<regmovtooc>();

                foreach (RegMovto x in d._RegMovtos)
                {


                    if (x.cneto == 1)
                    {
                        regmovtocorto y = new regmovtocorto();
                        y._nombrealmacen = x.cNombreAlmacen;
                        y._nombreproducto = x.cNombreProducto;
                        y._codigoalmacen = x.cCodigoAlmacen;
                        y._codigoproducto = x.cCodigoProducto;
                        y._precio = x.cPrecio;
                        y._unidades = x.cMargenUtilidad;

                        lista.Add(y);
                    }

                    regmovtooc z = new regmovtooc();
                    z._nombrealmacen = x.cNombreAlmacen;
                    z._nombrecomponente = x.ctextoextra2;
                    z._codigoalmacen = x.cCodigoAlmacen;
                    z._codigoproducto = x.ctextoextra1;
                    z._precio = x.cPrecio;
                    z._unidades = x.cUnidades;
                    //z._codigopaquete= x.cco
                    //z.Id = 0;
                    listaoc.Add(z);
                }
                dataGridView1.DataSource = lista;
                //dataGridView2.DataSource = d._RegMovtos;

                var proveedores = lrn.mCargarProveedoresComercial();

                var combobox = (DataGridViewComboBoxColumn)dataGridView2.Columns[0];


                combobox.DisplayMember = "razonsocial";
                combobox.ValueMember = "Id";
                combobox.DataSource = proveedores;

                combobox.DefaultCellStyle.NullValue = "(Ninguno)                     ";

                dataGridView2.DataSource = listaoc;

                dataGridView2.Columns["_codigoproducto"].Visible = false;
                dataGridView2.Columns["_codigoalmacen"].Visible = false;


                //dataGridView2.Rows[0].Cells["proveedor"].Value = "(Ninguno)                     ";

                


                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView2.Columns["unidades"].ReadOnly = true;
                dataGridView2.Columns["producto"].ReadOnly = true;
                dataGridView2.Columns["almacen"].ReadOnly = true;

            }
            else
            {
                MessageBox.Show("Documento No Existe");
                label8.Text = "";
                label9.Text = "";

            }
        }

        


        private void BuscarPedidoOC(DataGridView dataGridView1, int index)
        {
            //dataGridView1.DataSource = null;
            List<regmovtocorto> lista = new List<regmovtocorto>();
            List<regmovtooc> listaoc = new List<regmovtooc>();

            string folio = textBox1.Text;
            if (index == 0)
                if (checkBox1.Checked == true)
                    d = lrn.mBuscarDoctoComercialProduccion(folio, "2", 3);
                else
                    d = lrn.mBuscarDoctoComercialProduccion(folio, "2", 0);
            if (d.cFolio != 0)
            {

                label8.Text = d.cRazonSocial;
                label9.Text = d.cFecha.ToString();


                lista.Clear();
                listaoc.Clear();

                foreach (RegMovto x in d._RegMovtos)
                {


                    if (x.cneto == 1)
                    {
                        regmovtocorto y = new regmovtocorto();
                        y._nombrealmacen = x.cNombreAlmacen;
                        y._nombreproducto = x.cNombreProducto;
                        y._codigoalmacen = x.cCodigoAlmacen;
                        y._codigoproducto = x.cCodigoProducto;
                        y._precio = x.cPrecio;
                        y._unidades = x.cMargenUtilidad;
                        y._proveedor = 0;
                        y._doctopedido = int.Parse(d.cIdDocto.ToString());

                        lista.Add(y);
                    }

                    regmovtooc z = new regmovtooc();
                    z._nombrealmacen = x.cNombreAlmacen;
                    z._nombrecomponente = x.ctextoextra2;
                    z._codigoalmacen = x.cCodigoAlmacen;
                    z._codigoproducto = x.ctextoextra1;
                    z._precio = 0;
                    z._unidades = x.cMargenUtilidad * x.cUnidades;
                    z._proveedor = 0;
                    //z.Id = 0;
                    z._idmovtoorigen = int.Parse(x.cIdMovtoOrigen.ToString());
                    z._codigopaquete = x.ctextoextra3;
                    z._ccantidadcomponente = x.cimporteextra2;
                    listaoc.Add(z);
                }
                dataGridView1.DataSource = lista;

                dataGridView1.Columns["_proveedor"].Visible = false;

                dataGridView1.Columns["_codigoproducto"].Visible = false;
                dataGridView1.Columns["_codigoalmacen"].Visible = false;
                dataGridView1.Columns["_doctopedido"].Visible = false;
               

                //dataGridView2.DataSource = d._RegMovtos;

                List<RegProveedor> proveedores = new List<RegProveedor>();
                proveedores = lrn.mCargarProveedoresComercial();
                List<proveedorv2> proveedoresv2 = new List<proveedorv2>();

                foreach (RegProveedor v in proveedores)
                {
                    proveedorv2 zz = new proveedorv2();
                    zz.Id = int.Parse(v.Id.ToString());
                    zz.Nombre = v.RazonSocial;
                    proveedoresv2.Add(zz);
                }


                var combobox = (DataGridViewComboBoxColumn)dataGridView2.Columns["Proveedor"];


                combobox.DisplayMember = "Nombre";
                combobox.ValueMember = "Id";



                combobox.DataSource = proveedoresv2;
                combobox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;


                dataGridView2.DataSource = listaoc;

                dataGridView2.Columns["_codigoproducto"].Visible = false;
                dataGridView2.Columns["_codigoalmacen"].Visible = false;
                dataGridView2.Columns["_idmovtoorigen"].Visible = false;
                dataGridView2.Columns["_codigopaquete"].Visible = false;
                dataGridView2.Columns["_ccantidadcomponente"].Visible = false;

                //dataGridView2.Columns["_idmovtoorigen"].Visible = false;


                //dataGridView2.Rows[0].Cells["proveedor"].Value = "(Ninguno)                     ";

                combobox.DefaultCellStyle.NullValue = "(Ninguno)                     ";


                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView2.Columns["unidades"].ReadOnly = true;
                dataGridView2.Columns["producto"].ReadOnly = true;
                dataGridView2.Columns["almacen"].ReadOnly = true;

            }
            else
            {
                MessageBox.Show("Documento No Existe");
                label8.Text = "";
                label9.Text = "";

                lista.Clear();
                listaoc.Clear();

                dataGridView1.DataSource = lista;
                dataGridView2.DataSource = listaoc;

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

            BuscarPedidoOC(dataGridView1, tabControl1.SelectedIndex);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            // leer el grid de explosion de materiales

            List<RegDocto> doctos = new List<RegDocto>();


            dataGridView2.AllowUserToOrderColumns = true;

            // this.dataGridView2.Sort(this.dataGridView2.Columns["proveedor"], ListSortDirection.Ascending);

            List<regmovtooc> listaoc = new List<regmovtooc>();


            foreach (DataGridViewRow x in dataGridView2.Rows)
            {
                regmovtooc movto = new regmovtooc();
                movto._proveedor = int.Parse(x.Cells["proveedor"].Value.ToString());

                movto._codigoproducto = x.Cells["_codigoproducto"].Value.ToString();
                movto._codigoalmacen = x.Cells["_codigoalmacen"].Value.ToString();
                movto._precio = decimal.Parse(x.Cells["precio"].Value.ToString());
                movto._unidades = decimal.Parse(x.Cells["unidades"].Value.ToString());
                movto._codigopaquete = x.Cells["_codigopaquete"].Value.ToString();
                movto._ccantidadcomponente = decimal.Parse(x.Cells["_ccantidadcomponente"].Value.ToString());

                //movto._idmovtoorigen = int.Parse(x.Cells["_id"].Value.ToString());
                listaoc.Add(movto);
            }

            var listaoc1 = listaoc.OrderBy(x => x._proveedor);

            string lproveedor = "";
            RegDocto nd = new RegDocto();
            foreach (var z in listaoc1)
            {

                if (lproveedor != z._proveedor.ToString())
                {
                    if (nd.cCodigoCliente != "")
                    {
                        //agregarlo a la lista
                        doctos.Add(nd);
                        nd = new RegDocto();
                    }
                    nd.cCodigoCliente = lrn.mBuscarClienteComercialId(int.Parse(z._proveedor.ToString())).Codigo;
                    nd.cFecha = System.DateTime.Now;
                    nd.cCodigoConcepto = "19";
                    nd.cFolio = -1;
                    nd.cReferencia = textBox1.Text;
                    nd.cMoneda = "Peso Mexicano";
                    nd.cTextoExtra3 = textBox1.Text;

                    nd.cTextoExtra3 = dataGridView1.Rows[0].Cells["_doctopedido"].Value.ToString();
                }

                RegMovto m = new RegMovto();

                m.cCodigoAlmacen = z._codigoalmacen;
                m.cCodigoProducto = z._codigoproducto;
                m.cPrecio = z._precio;
                m.cUnidades = z._unidades;
                m.cimporteextra1 = z._unidades;
                lproveedor = z._proveedor.ToString();
                m.ctextoextra3 = z._codigopaquete;
                m.cimporteextra2 = z._ccantidadcomponente;
                nd._RegMovtos.Add(m);
                //MessageBox.Show(z._proveedor);
            }

            if (nd.cCodigoCliente != "")
            {
                //agregarlo a la lista
                doctos.Add(nd);
            }

            lrn.mSetDoctos(doctos);
            //return;

            string lrespuesta = "Validar Bitacora";
            if (doctos.Count > 0)

            {

                long folio = -1;
                lrn.mGrabarDoctosComercial(2, ref folio, 0, 0, 0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {

                    MessageBox.Show("Proceso Terminado");
                    label8.Text = "";
                    label9.Text = "";

                    List<regmovtocorto> lista = new List<regmovtocorto>();
                    List<regmovtooc> listaoc11 = new List<regmovtooc>();

                    lista.Clear();
                    listaoc11.Clear();

                    dataGridView1.DataSource = lista;
                    dataGridView2.DataSource = listaoc11;
                }
            }
            else
                MessageBox.Show(lrespuesta);
        }

        private void mGrabaErroresBitacora()
        {
            StreamWriter objwriter = new StreamWriter("c:\\bitacorachida.log");
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public class infogrid
        {
            public string folio { get; set; }
            public int idalmacen { get; set; }

            public string razonsocial { get; set; }

            public string pedido { get; set; }

            public string nombreproducto { get; set; }

            public decimal unidades { get; set; }

            public decimal unidadesdestino { get; set; }
            public decimal precio { get; set; }

            public string codigoproducto { get; set; }
            public string codigoalmacen { get; set; }
            public string codigocliente { get; set; }

            public int idmovto { get; set; }

            public string textoextra3 { get; set; }

            public decimal importeextra2 { get; set; }

        }

        public class infogridcompras : infogrid
        {
            public string nombrealmacenorigen { get; set; }
            public string codigoalmacenorigen { get; set; }

            public int orden { get; set; }

            public decimal costo { get; set; }

            public decimal cuantospt { get; set; }

        }

        public class almacenv2
        {
            public string Nombre { get; set; }
            public int Id { get; set; }
        }

        public class proveedorv2
        {
            public string Nombre { get; set; }
            public int Id { get; set; }
        }

        

        private void textBox2_Leave(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<infogrid> listagrid = new List<infogrid>();
            //dataGridView4.DataSource = listagrid;
            lrn.mCargarDocumentosComercialReferencia("19", textBox2.Text, ref dt, ref dt1);

            if (dt.Rows.Count > 0)
            {

                lblClienteCompras.Text = dt1.Rows[0]["cRazonSocial"].ToString();
                lblFechaCompras.Text = dt1.Rows[0]["cFecha"].ToString();




                foreach (DataRow x in dt.Rows)
                {
                    infogrid item = new infogrid();
                    item.folio = x["cFolio"].ToString();
                    item.idalmacen = int.Parse(x["cIdAlmacen"].ToString());
                    item.razonsocial = x["cRazonSocial"].ToString();
                    item.pedido = x["pedido"].ToString();

                    item.nombreproducto = x["cnombreproducto"].ToString();
                    item.precio = decimal.Parse(x["cprecio"].ToString());
                    //item.unidades = decimal.Parse(x["cunidades"].ToString());
                    //item.unidades = decimal.Parse(x["c"].ToString());
                    item.unidades = decimal.Parse(x["cimporteextra1"].ToString());
                    item.codigoproducto = x["ccodigoproducto"].ToString();
                    item.codigocliente = x["ccodigocliente"].ToString();
                    item.codigoalmacen = x["ccodigoalmacen"].ToString();

                    item.idmovto = int.Parse(x["cidmovimiento"].ToString());




                    item.unidadesdestino = decimal.Parse(x["cimporteextra1"].ToString());
                    item.textoextra3 = x["ctextoextra3"].ToString();
                    item.importeextra2 = decimal.Parse(x["cimporteextra2"].ToString());

                    listagrid.Add(item);

                }


                List<almacenv2> almacenesv2 = new List<almacenv2>();
                foreach (RegAlmacen zz in almacenes)
                {
                    almacenv2 xx = new almacenv2();
                    xx.Id = int.Parse(zz.Id.ToString());
                    xx.Nombre = zz.Nombre;
                    almacenesv2.Add(xx);
                }

                comboBoxAlmacenOC.ValueMember = "Id";
                comboBoxAlmacenOC.DisplayMember = "Nombre";
                comboBoxAlmacenOC.DataSource = almacenesv2;
                


                var combobox2 = (DataGridViewComboBoxColumn)dataGridView4.Columns["idalmacen"];
                combobox2.DisplayMember = "Nombre";
                combobox2.ValueMember = "Id";
                combobox2.DataSource = almacenesv2;
                dataGridView4.DataSource = listagrid;


                
                dataGridView4.Columns["CodigoCliente"].Visible = false;
                dataGridView4.Columns["CodigoProducto"].Visible = false;
                dataGridView4.Columns["CodigoAlmacen"].Visible = false;
                dataGridView4.Columns["idmovto"].Visible = false;
                dataGridView4.Columns["textoextra3"].Visible = false;
                dataGridView4.Columns["importeextra2"].Visible = false;
                dataGridView4.Columns["idalmacen"].Visible = false;


                dataGridView4.Columns["idalmacen"].Width = 150;
                dataGridView4.Columns["NombreProductoOC"].Width = 150;
            }
            else
            {
                MessageBox.Show("Documento No Existe");
                lblClienteCompras.Text = "";
                lblFechaCompras.Text = "";

                listagrid.Clear();
               
                dataGridView4.DataSource = listagrid;
                
            }

            //dataGridView4.Columns["codigoalmacenorigen"].Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int lfolio = 0;
            List<RegDocto> _RegDocto = new List<RegDocto>();

            RegDocto lRegDocto = new RegDocto();

            foreach (DataGridViewRow x in dataGridView4.Rows)
            {
                if (lfolio != int.Parse(x.Cells["folio"].Value.ToString()))
                {
                    if (lfolio > 0)
                    {
                        _RegDocto.Add(lRegDocto);
                    }
                    lRegDocto = new RegDocto();
                    lRegDocto.cFolio = -1;
                    lRegDocto.cFecha = System.DateTime.Now;
                    lRegDocto.cCodigoConcepto = "21";
                    lRegDocto.cCodigoCliente = x.Cells["CODIGOCLIENTE"].Value.ToString();
                    lRegDocto.cReferencia = x.Cells["PedidoOC"].Value.ToString();
                    lRegDocto.cMoneda = "Peso Mexicano";
                }

                RegMovto m = new RegMovto();
                m.cCodigoAlmacen = x.Cells["CODIGOALMACEN"].Value.ToString();

                int idalmacen = int.Parse(x.Cells["idalmacen"].Value.ToString());

                idalmacen = comboBoxAlmacenOC.SelectedIndex;

                foreach (RegAlmacen alm in almacenes)
                {
                    if (alm.Id == idalmacen)
                        m.cCodigoAlmacen = alm.Codigo;
                }

                

                m.cCodigoProducto = x.Cells["CODIGOPRODUCTO"].Value.ToString().Trim();
                m.cUnidades = decimal.Parse(x.Cells["UnidadesOC1"].Value.ToString().Trim());

                m.cUnidades = decimal.Parse(x.Cells["UnidadesaCompra"].Value.ToString().Trim());
                m.cPrecio = decimal.Parse(x.Cells["precioOC"].Value.ToString().Trim());
                m.cIdMovtoOrigen = long.Parse(x.Cells["idmovto"].Value.ToString());

                m.cimporteextra1 = decimal.Parse(x.Cells["UnidadesaCompra"].Value.ToString().Trim());
                m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString();
                m.cimporteextra2 = decimal.Parse(x.Cells["importeextra2"].Value.ToString().Trim());

                lRegDocto._RegMovtos.Add(m);




                lfolio = int.Parse(x.Cells["folio"].Value.ToString());

            }
            if (lfolio > 0)
            {
                _RegDocto.Add(lRegDocto);
            }

            lrn.mSetDoctos(_RegDocto);

            string lrespuesta = "Validar Bitacora";
            if (_RegDocto.Count > 0)

            {

                long folio = -1;
                lrn.mGrabarDoctosComercial(2, ref folio, 0, 0, 0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {
                    MessageBox.Show("Proceso Terminado");
                    lblClienteCompras.Text = "";
                    lblFechaCompras.Text = "";

                    List<infogrid> listagrid = new List<infogrid>();

                    listagrid.Clear();
                    
                    dataGridView4.DataSource = listagrid;

                }
            }   
            else
                MessageBox.Show(lrespuesta);


        }

        private void Produccion_FormClosed(object sender, FormClosedEventArgs e)
        {
            lrn.mCerrarSdkComercial();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            lrn.mCargarDocumentosComercialReferencia("21", textBox3.Text, ref dt, ref dt1);

            List<infogridcompras> listagrid = new List<infogridcompras>();

            if (dt.Rows.Count > 0)
            {
                lblClienteProduccion.Text = dt1.Rows[0]["cRazonSocial"].ToString();
                lblFechaProduccion.Text = dt1.Rows[0]["cFecha"].ToString();


                foreach (DataRow x in dt.Rows)
                {
                    infogridcompras item = new infogridcompras();
                    item.folio = x["cFolio"].ToString();

                    item.idalmacen = int.Parse(x["cIdAlmacen"].ToString());


                    item.razonsocial = x["cRazonSocial"].ToString();
                    item.pedido = x["pedido"].ToString();

                    item.nombreproducto = x["cnombreproducto"].ToString();
                    item.nombrealmacenorigen = x["cnombrealmacen"].ToString();
                    item.precio = decimal.Parse(x["cprecio"].ToString());
                    item.unidades = decimal.Parse(x["cimporteextra1"].ToString());
                    //item.unidades = decimal.Parse(x["c"].ToString());
                    item.codigoproducto = x["ccodigoproducto"].ToString();
                    item.codigocliente = x["ccodigocliente"].ToString();
                    item.codigoalmacen = x["ccodigoalmacen"].ToString();

                    item.codigoalmacenorigen = x["ccodigoalmacen"].ToString();

                    item.idmovto = int.Parse(x["cidmovimiento"].ToString());

                    item.unidadesdestino = decimal.Parse(x["cimporteextra1"].ToString());
                    item.textoextra3 = x["ctextoextra3"].ToString();
                    item.importeextra2 = decimal.Parse(x["cimporteextra2"].ToString());


                    listagrid.Add(item);

                }
                //List<RegAlmacen> almacenes = lrn.mCargarAlmacenesComercialv2();

                List<almacenv2> almacenesv2 = new List<almacenv2>();
                foreach (RegAlmacen zz in almacenes)
                {
                    almacenv2 xx = new almacenv2();
                    xx.Id = int.Parse(zz.Id.ToString());
                    xx.Nombre = zz.Nombre;
                    almacenesv2.Add(xx);
                }



                var combobox3 = (DataGridViewComboBoxColumn)dataGridView3.Columns["idalmacencompra"];
                combobox3.DisplayMember = "Nombre";
                combobox3.ValueMember = "Id";
                combobox3.DataSource = almacenesv2;


                comboBoxCompra.DisplayMember = "Nombre";
                comboBoxCompra.ValueMember = "Id";
                comboBoxCompra.DataSource = almacenesv2;


                dataGridView3.DataSource = listagrid;

                dataGridView3.Columns["CodigoCliente"].Visible = false;
                dataGridView3.Columns["CodigoProducto"].Visible = false;
                dataGridView3.Columns["CodigoAlmacen"].Visible = false;
                dataGridView3.Columns["idmovto"].Visible = false;
                dataGridView3.Columns["orden"].Visible = false;
                dataGridView3.Columns["costo"].Visible = false;
                dataGridView3.Columns["cuantospt"].Visible = false;
                dataGridView3.Columns["textoextra3"].Visible = false;
                dataGridView3.Columns["importeextra2"].Visible = false;
                dataGridView3.Columns["idalmacencompra"].Visible = false;



                dataGridView3.Columns["CodigoAlmacenOrigen"].Visible = false;

                dataGridView3.Columns["idalmacencompra"].Width = 150;
                dataGridView3.Columns["NombreProductocompra"].Width = 150;

                dataGridView3.Columns["idalmacencompra"].DisplayIndex = 6;
                dataGridView3.Columns["preciocompra"].DisplayIndex = 7;
                dataGridView3.Columns["unidadescompra"].DisplayIndex = 8;
                dataGridView3.Columns["UnidadesaProduccion"].DisplayIndex = 9;


                dataGridView3.Columns["unidadescompra"].DefaultCellStyle.Format = "N2";
            }
            else
            {
                MessageBox.Show("Documento No Existe");
                lblClienteProduccion.Text = "";
                lblFechaProduccion.Text = "";

                listagrid.Clear();

                dataGridView3.DataSource = listagrid;

            }

        }

        private void dataGridView4_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            /*
            if (e.ColumnIndex == dataGridView4.Columns["UnidadesaCompra"].Index)
            {

                int i;

                if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    //label1.Text = "please enter numeric";
                }
                else
                {
                    // the input is numeric 
                }


                dataGridView4.Rows[e.RowIndex].ErrorText = "";
                int newInteger;

                if (dataGridView4.Rows[e.RowIndex].IsNewRow) { return; }
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    dataGridView4.Rows[e.RowIndex].ErrorText = "the value must be a Positive integer";
                }
                
            }*/
        }

        private void dataGridView4_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == dataGridView4.Columns["UnidadesaCompra"].Index)
            {
                if (decimal.Parse(dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > decimal.Parse(dataGridView4.Rows[e.RowIndex].Cells["UnidadesOC1"].Value.ToString()))
                {
                    MessageBox.Show("Error");
                    dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (decimal)0.0;
                }
                //                MessageBox.Show(dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            
        }

        private void dataGridView3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           /* if (e.ColumnIndex == dataGridView3.Columns["UnidadesaProduccion"].Index)
            {
                dataGridView3.Rows[e.RowIndex].ErrorText = "";
                int newInteger;

                // Don't try to validate the 'new row' until finished  
                // editing since there 
                // is not any point in validating its initial value. 
                if (dataGridView3.Rows[e.RowIndex].IsNewRow) { return; }
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    dataGridView3.Rows[e.RowIndex].ErrorText = "the value must be a Positive integer";
                }
                /*else {
                    MessageBox.Show( dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
            }*/
            
        }

        private void dataGridView3_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["UnidadesaProduccion"].Index)
            {
                if (decimal.Parse(dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > decimal.Parse(dataGridView3.Rows[e.RowIndex].Cells["unidadescompra"].Value.ToString()))
                {
                    MessageBox.Show("Error");
                    dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (decimal)0.0;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int lfolio = 0;
            List<RegDocto> _RegDocto = new List<RegDocto>();

            RegDocto lRegDocto = new RegDocto();

            lRegDocto = new RegDocto();
            lRegDocto.cFolio = -1;
            lRegDocto.cFecha = System.DateTime.Now;
            lRegDocto.cCodigoConcepto = "35";
            //   lRegDocto.cCodigoCliente = x.Cells["CODIGOCLIENTE"].Value.ToString();
            lRegDocto.cReferencia = textBox3.Text ;
            lRegDocto.cMoneda = "Peso Mexicano";


            foreach (DataGridViewRow x in dataGridView3.Rows) // salida
            {
                RegMovto m = new RegMovto();
                m.cCodigoAlmacen = x.Cells["CODIGOALMACEN"].Value.ToString();
                m.cCodigoProducto = x.Cells["CODIGOPRODUCTO"].Value.ToString().Trim();

                m.cUnidades = decimal.Parse(x.Cells["UnidadesaProduccion"].Value.ToString().Trim());
                m.cPrecio = decimal.Parse(x.Cells["preciocompra"].Value.ToString().Trim());
                
                
                m.cimporteextra1 = decimal.Parse(x.Cells["UnidadesaProduccion"].Value.ToString().Trim());
                m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString().Trim();

                lRegDocto._RegMovtos.Add(m);

            }
            _RegDocto.Add(lRegDocto);


            lRegDocto = new RegDocto();
            lRegDocto.cFolio = -1;
            lRegDocto.cFecha = System.DateTime.Now;
            lRegDocto.cCodigoConcepto = "34";
            //   lRegDocto.cCodigoCliente = x.Cells["CODIGOCLIENTE"].Value.ToString();
            lRegDocto.cReferencia = textBox3.Text;
            lRegDocto.cMoneda = "Peso Mexicano";

            foreach (DataGridViewRow x in dataGridView3.Rows)
            {

                RegMovto m = new RegMovto();
                int idalmacen = int.Parse(x.Cells["idalmacenCompra"].Value.ToString());

                idalmacen = comboBoxCompra.SelectedIndex;
                foreach (RegAlmacen alm in almacenes)
                {
                    if (alm.Id == idalmacen)
                        m.cCodigoAlmacen = alm.Codigo;
                }

                //m.cCodigoAlmacen = x.Cells["CODIGOALMACEN"].Value.ToString();
                m.cCodigoProducto = x.Cells["CODIGOPRODUCTO"].Value.ToString().Trim();

                m.cUnidades = decimal.Parse(x.Cells["UnidadesaProduccion"].Value.ToString().Trim());
                m.cPrecio = decimal.Parse(x.Cells["preciocompra"].Value.ToString().Trim());
                m.cIdMovtoOrigen = long.Parse(x.Cells["idmovto"].Value.ToString());


                m.cimporteextra1 = decimal.Parse(x.Cells["UnidadesaProduccion"].Value.ToString().Trim());
                m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString();
                m.cimporteextra2 = decimal.Parse(x.Cells["importeextra2"].Value.ToString().Trim());

                lRegDocto._RegMovtos.Add(m);
            }
            _RegDocto.Add(lRegDocto);

            lrn.mSetDoctos(_RegDocto);

            string lrespuesta = "Validar Bitacora";
            if (_RegDocto.Count > 0)

            {

                long folio = -1;
                lrn.mGrabarDoctosComercial(2, ref folio, 0, 0, 0); ;
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {
                    MessageBox.Show("Proceso Terminado");
                    lblClienteProduccion.Text = "";
                    lblFechaProduccion.Text = "";

                    List<infogridcompras> listagrid = new List<infogridcompras>();

                    listagrid.Clear();

                    dataGridView3.DataSource = listagrid;

                }
            }
            else
                MessageBox.Show(lrespuesta);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            lrn.mCargarDocumentosComercialReferencia("34", textBox4.Text, ref dt, ref dt2,1);

            List<infogridcompras> listagrid = new List<infogridcompras>();

            if (dt.Rows.Count > 0)
            {


                lblClientePT.Text = dt2.Rows[0]["cRazonSocial"].ToString();
                lblFechaPT.Text = dt2.Rows[0]["cFecha"].ToString();


                foreach (DataRow x in dt.Rows)
                {
                    infogridcompras item = new infogridcompras();
                    item.folio = x["cFolio"].ToString();

                    item.idalmacen = int.Parse(x["cIdAlmacen"].ToString());


                    // item.razonsocial = x["cRazonSocial"].ToString();
                    item.pedido = x["pedido"].ToString();

                    item.nombreproducto = x["cnombreproducto"].ToString();
                    item.nombrealmacenorigen = x["cnombrealmacen"].ToString();
                    item.precio = decimal.Parse(x["cprecio"].ToString());
                    item.unidades = decimal.Parse(x["cimporteextra1"].ToString());
                    //item.unidades = decimal.Parse(x["c"].ToString());
                    item.codigoproducto = x["ccodigoproducto"].ToString();
                    item.codigocliente = x["ccodigocliente"].ToString();
                    item.codigoalmacen = x["ccodigoalmacen"].ToString();

                    item.codigoalmacenorigen = x["ccodigoalmacen"].ToString();

                    item.idmovto = int.Parse(x["cidmovimiento"].ToString());

                    item.unidadesdestino = decimal.Parse(x["unidadessalidamt"].ToString());


                    item.textoextra3 = x["ctextoextra3"].ToString();

                    item.importeextra2 = decimal.Parse(x["cimporteextra2"].ToString());

                    item.orden = int.Parse(x["orden"].ToString());

                    item.costo = decimal.Parse(x["costo"].ToString());
                    item.cuantospt = decimal.Parse(x["cuantos"].ToString());
                    listagrid.Add(item);

                }
                //List<RegAlmacen> almacenes = lrn.mCargarAlmacenesComercialv2();

                List<almacenv2> almacenesv2 = new List<almacenv2>();
                foreach (RegAlmacen zz in almacenes)
                {
                    almacenv2 xx = new almacenv2();
                    xx.Id = int.Parse(zz.Id.ToString());
                    xx.Nombre = zz.Nombre;
                    almacenesv2.Add(xx);
                }


                comboBoxPT.DisplayMember = "Nombre";
                comboBoxPT.ValueMember = "Id";
                comboBoxPT.DataSource = almacenesv2;

                var combobox4 = (DataGridViewComboBoxColumn)dataGridView5.Columns["idalmacenpt"];
                combobox4.DisplayMember = "Nombre";
                combobox4.ValueMember = "Id";
                combobox4.DataSource = almacenesv2;


                dataGridView5.DataSource = listagrid;

                dataGridView5.Columns["CodigoCliente"].Visible = false;
                dataGridView5.Columns["CodigoProducto"].Visible = false;
                dataGridView5.Columns["CodigoAlmacen"].Visible = false;

                dataGridView5.Columns["idmovto"].Visible = false;
                dataGridView5.Columns["orden"].Visible = false;
                dataGridView5.Columns["costo"].Visible = false;
                dataGridView5.Columns["cuantospt"].Visible = false;
                dataGridView5.Columns["textoextra3"].Visible = false;
                dataGridView5.Columns["importeextra2"].Visible = false;
                dataGridView5.Columns["idalmacenpt"].Visible = false;



                dataGridView5.Columns["CodigoAlmacenOrigen"].Visible = false;
                dataGridView5.Columns["RazonSocial"].Visible = false;
                dataGridView5.Columns["idalmacenpt"].Width = 150;
                dataGridView5.Columns["NombreProductoentrada"].Width = 150;

                dataGridView5.Columns["idalmacenpt"].DisplayIndex = 6;
                dataGridView5.Columns["precioentrada"].DisplayIndex = 7;
                dataGridView5.Columns["unidadesentrada"].DisplayIndex = 8;
                dataGridView5.Columns["Unidadespt"].DisplayIndex = 9;
            }
            else
            {
                MessageBox.Show("Documento No Existe");
                lblClientePT.Text = "";
                lblFechaPT.Text = "";

                listagrid.Clear();

                dataGridView5.DataSource = listagrid;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int lfolio = 0;
            List<RegDocto> _RegDocto = new List<RegDocto>();

            RegDocto lRegDocto = new RegDocto();

            string lpt = "";
            int cuantosh = 0;
            int cuantosa = 0;
            decimal lprecio = 0;

            lfolio = 0;
            RegMovto m = new RegMovto();









            lRegDocto = new RegDocto();
            lRegDocto.cFolio = -1;
            lRegDocto.cFecha = System.DateTime.Now;
            lRegDocto.cCodigoConcepto = "35";
            lRegDocto.cReferencia = textBox4.Text;
            lRegDocto.cMoneda = "Peso Mexicano";

            foreach (DataGridViewRow x in dataGridView5.Rows)
            {
                m = new RegMovto();
                m.cCodigoAlmacen = x.Cells["CODIGOALMACEN"].Value.ToString();
                m.cCodigoProducto = x.Cells["CODIGOPRODUCTO"].Value.ToString().Trim();

                m.cUnidades = decimal.Parse(x.Cells["Unidadespt"].Value.ToString().Trim());
                m.cPrecio = decimal.Parse(x.Cells["precioentrada"].Value.ToString().Trim());
                m.cIdMovtoOrigen = long.Parse(x.Cells["idmovto"].Value.ToString());

                m.cimporteextra1 = decimal.Parse(x.Cells["Unidadespt"].Value.ToString().Trim());
                m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString().Trim();
                lRegDocto._RegMovtos.Add(m);
                lfolio = int.Parse(x.Cells["pedidoentrada"].Value.ToString());
            }
            _RegDocto.Add(lRegDocto);


            //List<RegAlmacen> almacenes = lrn.mCargarAlmacenesComercialv2();


            lfolio = 0;
            lpt = "";


            lRegDocto = new RegDocto();
            lRegDocto.cFolio = -1;
            lRegDocto.cFecha = System.DateTime.Now;
            lRegDocto.cCodigoConcepto = "340";
            lRegDocto.cReferencia = textBox4.Text;
            lRegDocto.cMoneda = "Peso Mexicano";

            foreach (DataGridViewRow x in dataGridView5.Rows)
            {
                if (int.Parse(x.Cells["orden"].Value.ToString()) == 1)
                {
                    m = new RegMovto();
                    int idalmacen = int.Parse(x.Cells["idalmacenpt"].Value.ToString());

                    idalmacen = comboBoxPT.SelectedIndex;

                    foreach (RegAlmacen alm in almacenes)
                    {
                        if (alm.Id == idalmacen)
                            m.cCodigoAlmacen = alm.Codigo;
                    }
                    m.cCodigoProducto = x.Cells["textoextra3"].Value.ToString();
                    m.cUnidades = decimal.Parse(x.Cells["cuantospt"].Value.ToString().Trim());

                    m.cPrecio = decimal.Parse(x.Cells["costo"].Value.ToString().Trim());
                    //= decimal.Parse(x.Cells["costo"].Value.ToString().Trim());
                    
                    m.cimporteextra1 = decimal.Parse(x.Cells["cuantospt"].Value.ToString().Trim());
                    m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString().Trim();
                    m.cimporteextra2 = decimal.Parse(x.Cells["importeextra2"].Value.ToString().Trim());
                    lRegDocto._RegMovtos.Add(m);




                }
            }
            _RegDocto.Add(lRegDocto);


            lrn.mSetDoctos(_RegDocto);

            string lrespuesta = "Validar Bitacora";
            if (_RegDocto.Count > 0)

            {

                long folio = -1;
                lrn.mGrabarDoctosComercial(2, ref folio, 0, 0, 0);
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {       
                    MessageBox.Show("Proceso Terminado");
                    lblClientePT.Text = "";
                    lblFechaPT.Text = "";

                    List<infogridcompras> listagrid = new List<infogridcompras>();

                    listagrid.Clear();

                    dataGridView5.DataSource = listagrid;
                    }
            }
            else
                MessageBox.Show(lrespuesta);

        }

        private void txtFacturar_Leave(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            lrn.mCargarDocumentosComercialReferencia("340", txtFacturar.Text, ref dt, ref dt1);

            List<infogridcompras> listagrid = new List<infogridcompras>();

            if (dt.Rows.Count > 0)
            {


                lblClienteFacturacion.Text = dt1.Rows[0]["cRazonSocial"].ToString();
                lblFechaFacturacion.Text = dt1.Rows[0]["cFecha"].ToString();

                foreach (DataRow x in dt.Rows)
                {
                    infogridcompras item = new infogridcompras();
                    item.folio = x["cFolio"].ToString();

                    item.idalmacen = int.Parse(x["cIdAlmacen"].ToString());


                    // item.razonsocial = x["cRazonSocial"].ToString();
                    item.pedido = x["pedido"].ToString();

                    item.nombreproducto = x["cnombreproducto"].ToString();
                    item.nombrealmacenorigen = x["cnombrealmacen"].ToString();

                    item.precio = decimal.Parse(x["cprecio"].ToString());

                    item.unidades = decimal.Parse(x["cimporteextra1"].ToString());
                    //item.unidades = decimal.Parse(x["c"].ToString());
                    item.codigoproducto = x["ccodigoproducto"].ToString();
                    item.codigocliente = x["ccodigoclientepedido"].ToString();
                    item.codigoalmacen = x["ccodigoalmacen"].ToString();

                    item.codigoalmacenorigen = x["ccodigoalmacen"].ToString();

                    item.idmovto = int.Parse(x["cidmovimiento"].ToString());

                    item.unidadesdestino = decimal.Parse(x["cimporteextra1"].ToString());
                    item.textoextra3 = x["ctextoextra3"].ToString();

                    item.importeextra2 = decimal.Parse(x["cimporteextra2"].ToString());

                    item.orden = int.Parse(x["orden"].ToString());

                    item.costo = decimal.Parse(x["costo"].ToString());
                    item.cuantospt = decimal.Parse(x["cpreciopedido"].ToString());
                    listagrid.Add(item);

                }
                /*

                List<almacenv2> almacenesv2 = new List<almacenv2>();
                foreach (RegAlmacen zz in almacenes)
                {
                    almacenv2 xx = new almacenv2();
                    xx.Id = int.Parse(zz.Id.ToString());
                    xx.Nombre = zz.Nombre;
                    almacenesv2.Add(xx);
                }



                var combobox4 = (DataGridViewComboBoxColumn)dataGridView5.Columns["idalmacenpt"];
                combobox4.DisplayMember = "Nombre";
                combobox4.ValueMember = "Id";
                combobox4.DataSource = almacenesv2;
                */

                dataGridView6.DataSource = listagrid;
                dataGridView6.Columns["CodigoCliente"].Visible = false;
                dataGridView6.Columns["CodigoProducto"].Visible = false;
                dataGridView6.Columns["CodigoAlmacen"].Visible = false;
                dataGridView6.Columns["idmovto"].Visible = false;
                dataGridView6.Columns["CodigoAlmacenOrigen"].Visible = false;
                dataGridView6.Columns["RazonSocial"].Visible = false;
                //dataGridView6.Columns["idalmacenpt"].Width = 150;
                dataGridView6.Columns["NombreProductopt"].Width = 150;
                //dataGridView6.Columns["idalmacenpt"].DisplayIndex = 6;
                dataGridView6.Columns["preciopt"].DisplayIndex = 7;
                dataGridView6.Columns["cantidadpt"].DisplayIndex = 8;
                dataGridView6.Columns["unidadesFactura"].DisplayIndex = 9;


                dataGridView6.Columns["orden"].Visible = false;
                dataGridView6.Columns["costo"].Visible = false;
                dataGridView6.Columns["cuantospt"].Visible = false;
                dataGridView6.Columns["textoextra3"].Visible = false;
                dataGridView6.Columns["importeextra2"].Visible = false;
            }
            else
            { 
                MessageBox.Show("Documento No Existe");
                lblClienteFacturacion.Text = "";
                lblFechaFacturacion.Text = "";

                listagrid.Clear();

                dataGridView6.DataSource = listagrid;
            }


        }

        private void txtFacturar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            int lfolio = 0;
            List<RegDocto> _RegDocto = new List<RegDocto>();

            RegDocto lRegDocto = new RegDocto();

            lfolio = 0;
            RegMovto m = new RegMovto();









            lRegDocto = new RegDocto();
            lRegDocto.cFolio = -1;
            lRegDocto.cFecha = System.DateTime.Now;
            lRegDocto.cCodigoConcepto = "4";
            
            lRegDocto.cReferencia = txtFacturar.Text;
            lRegDocto.cMoneda = "Peso Mexicano";

            foreach (DataGridViewRow x in dataGridView6.Rows)
            {
                lRegDocto.cCodigoCliente = x.Cells["CODIGOCLIENTE"].Value.ToString();

                m = new RegMovto();
                m.cCodigoAlmacen = x.Cells["CODIGOALMACEN"].Value.ToString();
                m.cCodigoProducto = x.Cells["CODIGOPRODUCTO"].Value.ToString().Trim();

                m.cUnidades = decimal.Parse(x.Cells["UnidadesFactura"].Value.ToString().Trim());
                m.cPrecio = decimal.Parse(x.Cells["cuantospt"].Value.ToString().Trim());
                m.cIdMovtoOrigen = long.Parse(x.Cells["idmovto"].Value.ToString());

                m.cimporteextra1 = decimal.Parse(x.Cells["UnidadesFactura"].Value.ToString().Trim());
                m.ctextoextra3 = x.Cells["textoextra3"].Value.ToString().Trim();
                lRegDocto._RegMovtos.Add(m);
                //lfolio = int.Parse(x.Cells["pedidoentrada"].Value.ToString());
            }
            _RegDocto.Add(lRegDocto);


            

            lrn.mSetDoctos(_RegDocto);

            string lrespuesta = "Validar Bitacora";
            if (_RegDocto.Count > 0)

            {

                long folio = -1;
                lrn.mGrabarDoctosComercial(2, ref folio, 0, 0, 0);
                if (listaerrores.Count != 0)
                {
                    MessageBox.Show("Existen errores por favor revise bitacora");
                    mGrabaErroresBitacora();
                    //MessageBox.Show(lista[0].ToString());
                }
                else
                {
                    MessageBox.Show("Proceso Terminado");
                    lblClienteFacturacion.Text = "";
                    lblFechaFacturacion.Text = "";

                    List<infogridcompras> listagrid = new List<infogridcompras>();

                    listagrid.Clear();

                    dataGridView6.DataSource = listagrid;
                }
            }
            else
                MessageBox.Show(lrespuesta);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFacturar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxAlmacenOC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {


            MessageBox.Show("solo se permiten numeros capture correctamente ");

            /*MessageBox.Show("solo se permiten numeros " + e.Context.ToString());
            
            
            
            
            
            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (e.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }*/

            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }
            //dataGridView4.CurrentCell.Value = 0;
        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("solo se permiten numeros capture correctamente ");
            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }
        }
    }



    
}


