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
    public partial class AMCO : Form
    {
        ClassRN lrn = new ClassRN();
        public AMCO()
        {
            InitializeComponent();
        }

        private void AMCO_Load(object sender, EventArgs e)
        {
            this.Text = " Interfaz AMCO " + " " + this.ProductVersion; 
            lrn.mSeteaDirectorio(Directory.GetCurrentDirectory());
            botonExcel1.mAsignaTipo(1);
            botonExcel2.mSetearEtiqueta("Archivo Bitacora");
            botonExcel2.mGeneraNombre(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RutaEmpresaADM = seleccionEmpresa1.lrutaempresa;
            Properties.Settings.Default.Save();

                lrn.mLlenarinfo(botonExcel1.mRegresarNombre(), txtObservaciones777.Text, txtObservaciones888.Text, txtObservaciones999.Text, txtReferencia.Text, txtObservacionesMov.Text, textRefMovto777.Text, textTextoExtraMov777.Text, textRefMovto888.Text, textTextoExtraMov888.Text, textRefMovto999.Text, textTextoExtraMovto999.Text);
            List<string> lista = new List<string>();

            /*DateTime zz = DateTime.Parse("01/02/2015");

            if (DateTime.Today > zz)
                return;*/
            lista = lrn.mGrabarDoctos(false,0);
            if (lista.Count != 0)
            {
                MessageBox.Show(lista[0].ToString());
            }
            else
                MessageBox.Show("Proceso Terminado");

            if (lrn.lbd.lvar.Count > 0)
            {
                string larchivo = @botonExcel2.mRegresarNombre();
                // checar si el archivo existe
                int lresp = 0;
                if (System.IO.File.Exists(larchivo))
                {
                    DialogResult dialogResult = MessageBox.Show("Archivo Bitacora Existe Desea Sobreescribirlo", "Confirmacion", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.IO.File.Delete(larchivo);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(larchivo);

                foreach (string www in lrn.lbd.lvar)
                {

                    sw.WriteLine(www);
                }
                sw.Close();
                MessageBox.Show("Revise archivo bitacora hay productos sin existencia no grabados");
            }


        }
    }
}
