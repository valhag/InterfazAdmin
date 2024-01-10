using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InterfazAdmin
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

        }
        //XMLComercial y = new XMLComercial();
        //Gomar z = new Gomar();
        //PedidosFacturas y = new PedidosFacturas();
        Microplane y = new Microplane();

<<<<<<< HEAD
        //NewExcel y = new NewExcel();
        //Addenda y = new Addenda();
        //AddendaTest y = new AddendaTest();
        Autorizaciones y = new Autorizaciones();

        public void asignaform1(Autorizaciones ay)
=======
        public void asignaform1(Microplane ay)
>>>>>>> refs/remotes/origin/master
        {
            y = ay;
        }


        private void Form5_Load(object sender, EventArgs e)
        {
            this.Text = "Datos Conexion SQLServer";
            txtServer.Text = Properties.Settings.Default.server;
            txtBD.Text = Properties.Settings.Default.database;
            txtUser.Text = Properties.Settings.Default.user;
            txtPass.Text = Properties.Settings.Default.password;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (mValida())
            {


                Properties.Settings.Default.server = txtServer.Text;
                Properties.Settings.Default.database = txtBD.Text;
                Properties.Settings.Default.user = txtUser.Text;
                Properties.Settings.Default.password = txtPass.Text;

                Properties.Settings.Default.Save();

                this.Close();
                this.DialogResult = DialogResult.OK;
                y.Cadenaconexion = "data source =" + Properties.Settings.Default.server +
                ";initial catalog =" + Properties.Settings.Default.database + " ;user id = " + Properties.Settings.Default.user +
                "; password = " + Properties.Settings.Default.password + ";";
                //y.mllenarcomboempresas();
                //y.Visible = true;
            }
            else
                MessageBox.Show("Valores de conexion incorrectos");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtBD_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
