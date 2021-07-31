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
    public partial class FRMListaClientes : DevComponents.DotNetBar.Office2007Form
    {
        public FRMListaClientes()
        {
            InitializeComponent();
        }

        private void BTNAgregar_Click(object sender, EventArgs e)
        {
            FRMRegistrarCliente f1 = new FRMRegistrarCliente();
            f1.ShowDialog();
        }
    }
}
