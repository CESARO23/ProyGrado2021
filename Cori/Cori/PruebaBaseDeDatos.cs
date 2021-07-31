using CapaRN;
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
    public partial class PruebaBaseDeDatos : Form
    {
        aperson p;
        public PruebaBaseDeDatos()
        {
            InitializeComponent();
        }

        private void BTNPersona_Click(object sender, EventArgs e)
        {
            p = new aperson();
            p.papsidpers = "PE-000000000001";
            p.capsapepat = "Mamani";
            p.capsapemat = "Villena";
            p.capsnomper = "Julio Cesar";
            p.capscipers = "7222710";
            p.capscelula = "65831858";
            p.capstelefo = "65831858";
            p.capsdirecc = "Siempre viva";
            if (p.Grabar())
            {
                MessageBox.Show("se registro con exito");
            }
            else
            {
                MessageBox.Show("fallo al registrar");
            }
        }

        private void BTNUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
