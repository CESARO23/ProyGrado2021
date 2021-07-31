using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cori
{
    public partial class FRMListaUsuario : DevComponents.DotNetBar.Office2007Form
    {
        public FRMListaUsuario()
        {
            InitializeComponent();
        }

        private void BTNAgregar_Click(object sender, EventArgs e)
        {
            FRMRegistrarUsuario f1 = new FRMRegistrarUsuario();
            //f1.Modificar = false;
            f1.ShowDialog();
        }
    }
}
