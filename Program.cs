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

            if (!Utils.DatasetEncontrado)
            {
                MessageBox.Show("No se encontró la carpeta 'datasets' con los archivos necesarios. La aplicación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // No mostrar el formulario
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicio());
        }
    }
}