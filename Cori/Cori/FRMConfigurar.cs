using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAD;
using CapaRN;


namespace Cori
{
    public partial class FRMConfigurar : DevComponents.DotNetBar.Office2007Form
    {
        #region constructor
        public FRMConfigurar()
        {
            InitializeComponent();
        }
        #endregion

        #region variables
        private string server;
        private string port;
        private string user;
        private string password;
        private string db;
        private bool seGuardo;
        #endregion

        #region metodos
        private void valoresIniciales()
        {
            try
            {
                TXTServidor.Text = server = AppConfWriter.ReadSetting("SV");
                TXTPuerto.Text = port = AppConfWriter.ReadSetting("PT");
                TXTUsuario.Text = user = AppConfWriter.ReadSetting("US");
                TXTPassword.Text = password = AppConfWriter.ReadSetting("PW");
                TXTBaseDatos.Text = db = AppConfWriter.ReadSetting("BD");
            }
            catch
            {
                MessageBox.Show("No se pudo leer el archivo de configuracion",
                                "Fallo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
        private bool probarConexion()
        {
            bool ban = true;
            if ((TXTServidor.Text.Replace(" ", "") != "") &&
                (TXTPuerto.Text.Replace(" ", "") != "") &&
                (TXTUsuario.Text.Replace(" ", "") != "") &&
                (TXTBaseDatos.Text.Replace(" ", "") != ""))
            {
                CLConexionMYSQL conexionMYSQL = new CLConexionMYSQL(TXTServidor.Text,
                                                                    TXTPuerto.Text,
                                                                    TXTUsuario.Text,
                                                                    TXTPassword.Text,
                                                                    TXTBaseDatos.Text);
                if (conexionMYSQL.Conectar_Inicio())
                {
                    ban = true;
                    conexionMYSQL.Desconectar();
                    BTNGuardar.Enabled = true;
                }
                else
                {
                    ban = false;
                    BTNGuardar.Enabled = false;
                }
            }
            return ban;
        }
        #endregion

        #region eventos
        private void TXTServidor_TextChanged(object sender, EventArgs e)
        {
            BTNGuardar.Enabled = false;
        }
        #endregion

        private void FRMConfigurar_Load(object sender, EventArgs e)
        {
            valoresIniciales();
            if (probarConexion())
            {
                BTNGuardar.Enabled = true;
            }
            else
            {
                BTNGuardar.Enabled = true;
            }
        }

        private void BTNPing_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "ping";
            process.StartInfo.Arguments = TXTServidor.Text;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            MessageBox.Show(output, "Resultado del ping");
        }

        private void BTNProbar_Click(object sender, EventArgs e)
        {
            if (probarConexion())
            {
                MessageBox.Show("Conexion exitosa. Puede guardar esta configuracion",
                                "Confirmacion",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error en la conexion. Datos invalidos ",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void BTNGuardar_Click(object sender, EventArgs e)
        {
            bool ban = true;
            if ((TXTServidor.Text.Replace(" ", "") != "") &&
                (TXTPuerto.Text.Replace(" ", "") != "") &&
                (TXTUsuario.Text.Replace(" ", "") != "") &&
                (TXTBaseDatos.Text.Replace(" ", "") != ""))
            {
                try
                {
                    AppConfWriter.WriteSetting("SV", TXTServidor.Text);
                    AppConfWriter.WriteSetting("PT", TXTPuerto.Text);
                    AppConfWriter.WriteSetting("US", TXTUsuario.Text);
                    AppConfWriter.WriteSetting("PW", TXTPassword.Text);
                    AppConfWriter.WriteSetting("BD", TXTBaseDatos.Text);
                    seGuardo = true;
                    MessageBox.Show("Se guardo correctamente, la aplicacion se reiniciara",
                                    "Confirmacion",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    Application.Restart();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("Error al guardar los datos: " + Ex,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los datos antes de conexion",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}
