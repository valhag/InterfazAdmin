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
    public partial class Montessori : Form
    {
        ClassRN lrn = new ClassRN();
        public Montessori()
        {
            InitializeComponent();
        }

        private void Montessori_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz Facturacion Masiva " + " " + this.ProductVersion;
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());

            //seleccionEmpresa1.SelectedItem += new EventHandler(OnComboChange);
            //mCargaConceptos();


            //botonExcel1.mGeneraNombre(2, "FacturacionMasiva");
        
        }
    }
}
