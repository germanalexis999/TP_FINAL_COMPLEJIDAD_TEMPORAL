using System;
using System.Windows.Forms;

namespace WiW
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Utils.init_patron(); // Inicializa y busca el dataset
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicio());
        }
    }
}