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

namespace Cori
{
    public partial class FRMPrincipal : DevComponents.DotNetBar.RibbonForm
    {

        #region constructor
        public FRMPrincipal()
        {
            InitializeComponent();
        }
        #endregion
        #region eventos
        private void BTNConector_Click(object sender, EventArgs e)
        {
            FRMConfigurar configurar = new FRMConfigurar();
            configurar.ShowDialog();
        }
        private void BTNUsuario_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Dispose();
            }

            FRMListaUsuario f1 = new FRMListaUsuario();
            f1.MdiParent = this;
            f1.Show();
        }

        #endregion

        private void BTNCliente_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Dispose();
            }

            FRMListaClientes f1 = new FRMListaClientes();
            f1.MdiParent = this;
            f1.Show();
        }

        private void BTNProducto_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Dispose();
            }

            FRMListaProducto f1 = new FRMListaProducto();
            f1.MdiParent = this;
            f1.Show();
        }
    }
}
