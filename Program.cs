using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace InterfazAdmin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FacturacionMasiva());
            //Application.Run(new XMLComercial());
            //Application.Run(new AddendaTest());

            //Application.Run(new PedidosFacturas());

            //Application.Run(new AmcoPedidos());

            //Application.Run(new NewExcel());
            //Application.Run(new Montessori());
            //Application.Run(new Autorizaciones());
            //Application.Run(new CartaPorte());
            //Application.Run(new Microplane());
            //Application.Run(new cfdiTraslado());
            //Application.Run(new traspaso());
            Application.Run(new Produccion());
        }
    }
}
