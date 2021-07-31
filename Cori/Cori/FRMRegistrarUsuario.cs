using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaRN;
using AForge.Video;
using AForge.Video.DirectShow;


namespace Cori
{
    public partial class FRMRegistrarUsuario : DevComponents.DotNetBar.Office2007Form
    {
        #region variables
        private aperson persona = new aperson();
        private ausuari usuario = new ausuari();
        private atipusu tipoUsuario = new atipusu();
        private xnumcor correlativo = new xnumcor();
        private bool banderaImg = false;
        public string cod_usu_mod = "";
        public string carnet_mod = "";
        public bool modificar = false;
        public bool existePersona = false;
        //variables para el funcinamiento de la camara
        private bool existenDispositivos = false;
        private bool fotografiaEcha = false;
        private FilterInfoCollection dispositivosdeVideo;
        private VideoCaptureDevice fuentedeVideo = null;
        //lista tipo de usuarios del sistema
        List<atipusu> ListaTipoUsuario;

        #endregion

        #region constructor
        public FRMRegistrarUsuario()
        {
            InitializeComponent();
            // Inicializar combo tipoUsuario
            CargarComboTipoUsuario();
            // Inicializar Dispositivos Camara
            //BuscarDispositivos();
        }
        #endregion

        #region eventos
        private void BTNExaminar_Click(object sender, EventArgs e)
        {
            if(OFDSelecionarImagen.ShowDialog() == DialogResult.OK)
            {
                PCBFotografia.Image = Image.FromFile(OFDSelecionarImagen.FileName);
            }
        }
        private void BTNLimpiarFoto_Click(object sender, EventArgs e)
        {
            PCBFotografia.Image = global::Cori.Properties.Resources.imgUsuario;
            OFDSelecionarImagen.FileName = "";
            banderaImg = false;
        }
        private void OFDSelecionarImagen_FileOk(object sender, CancelEventArgs e)
        {
            banderaImg = true;
        }
        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BTNCapturar_Click(object sender, EventArgs e)
        {
            if (existenDispositivos)
            {
                Capturar();
                banderaImg = true;
            }
        }
        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCasillas();
        }
        private void FRMRegistrarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta seguro desea salir?", "Mensaje de Confirmacion",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                CerrarFormulario();
            }
        }
        private void FRMRegistrarUsuario_Load(object sender, EventArgs e)
        {
            BuscarDispositivos();
            //habilitar camara
           /* if (existenDispositivos)
            {
                fuentedeVideo = new VideoCaptureDevice(dispositivosdeVideo
                                    [dispositivosdeVideo.Count-2].MonikerString);
                fuentedeVideo.NewFrame += new NewFrameEventHandler(MostrarImagen);
                fuentedeVideo.Start();
            }
            else
            {
                MessageBox.Show("No tiene camara instalada",
                                "Información",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }*/
            //abrir formulario registro
            if (this.modificar)
            {
                BTNGuardar.Text = "Modificar";
                this.Text = "Modificar Usuario";
                //JalarDatos();
            }
            else
            {
                BTNGuardar.Text = "Guardar";
                this.Text = "Registrar Usuario";
                LimpiarCasillas();
            }
            SWTEstado.Focus();
        }
        private void BTNCamara_Click(object sender, EventArgs e)
        {
            CerrarCamara();
            int i = CMBDispositivos.SelectedIndex;
            string NombreVideo = dispositivosdeVideo[i].MonikerString;
            fuentedeVideo = new VideoCaptureDevice(NombreVideo);
            fuentedeVideo.NewFrame += new NewFrameEventHandler(MostrarImagen);
            fuentedeVideo.Start();
        }
        #endregion

        #region metodos
        private void CargarComboTipoUsuario()
        {
            List<String> Lista = new List<string>();
            Lista.Add("MESERO");
            Lista.Add("CAJERO");
            Lista.Add("ADMISTRADOR");
            Lista.Add("COCINA");
            Lista.Add("BARMAN");
            Lista = Lista.OrderBy(q => q).ToList();
            foreach (String A in Lista)
            {
                CMBTipoUsuario.Items.Add(A);
            }

        }
        private void LimpiarCasillas()
        {
            SWTEstado.Value = true;
            TBXCedula.Text = "";
            TBXApepat.Text = "";
            TBXApemat.Text = "";
            TBXNombre.Text = "";
            TBXDireccion.Text = "";
            TBXCelular.Text = "";
            TBXTelefono.Text = "";
            CMBTipoUsuario.Text = "";
            TBXUsuario.Text = "";
            TBXContrasena.Text = "";
            PCBFotografia.Image = global::Cori.Properties.Resources.imgUsuario;
            OFDSelecionarImagen.FileName = "";
            banderaImg = false;
        }
        #endregion

        #region camara
        private void BuscarDispositivos()
        {
            dispositivosdeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (dispositivosdeVideo.Count == 0)
            {
                existenDispositivos = false;
            }
            else
            {
                existenDispositivos = true;
                for (int i = 0; i < dispositivosdeVideo.Count; i++)
                    CMBDispositivos.Items.Add(dispositivosdeVideo[i].Name.ToString());
                CMBDispositivos.Text = dispositivosdeVideo[0].Name.ToString();
            }
                
        }
        private void Capturar()
        {
            if (fuentedeVideo != null)
            {
                if (fuentedeVideo.IsRunning)
                {
                    PCBFotografia.Image = PCBCamara.Image;
                }
            }
        }
        private void CerrarFormulario()
        {
            CerrarCamara();
            Dispose();
            Close();
        }
        private void CerrarCamara()
        {
            if (fuentedeVideo != null)
            {
                if (fuentedeVideo.IsRunning)
                {
                    fuentedeVideo.SignalToStop();
                    fuentedeVideo = null;
                }
            }
        }
        private void MostrarImagen(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap imagen = (Bitmap)eventArgs.Frame.Clone();
            PCBCamara.Image = imagen;
        }





        #endregion

        

        private void BTNGuardar_Click(object sender, EventArgs e)
        {
            if (DatosIntegridad())
            {
                //falta añadir codigo
                MessageBox.Show("haber que passa");
            }

        }
        private bool DatosIntegridad()
        {
            return true;
        }
    }
}
