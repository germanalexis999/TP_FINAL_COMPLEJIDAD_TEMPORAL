using tp2;
using tpfinal;
using System.Runtime.InteropServices;

namespace WiW
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            pathDataSet.Text = Utils.patron;

            if (!Utils.DatasetEncontrado)
            {
                MessageBox.Show(
                    "No se pudo encontrar automáticamente en el proyecto la carpeta 'datasets'.\nPor favor, selecciónela manualmente.",
                    "Carpeta datasets no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 juego = new Form1();
                FormUser select = new FormUser(juego, true);
                select.Show();
                this.Hide();
            }
            catch (Exception)
            {
                DialogResult dr = MessageBox.Show("Directorio invalido, Por favor seleccione otro...", "Aviso", MessageBoxButtons.OK);
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void barra_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}