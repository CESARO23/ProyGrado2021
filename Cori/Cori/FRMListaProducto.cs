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
    public partial class FRMListaProducto : DevComponents.DotNetBar.Office2007Form
    {
        public FRMListaProducto()
        {
            InitializeComponent();
        }

        private void BTNAgregar_Click(object sender, EventArgs e)
        {
            FRMRegistrarProducto f1 = new FRMRegistrarProducto();
            f1.ShowDialog();
        }
    }
}
