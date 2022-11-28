using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TipTopMorrazH.OrdenamientoExterno;

namespace TipTopMorrazH.OrdenamientoExterno
{
    public partial class Externo : Form
    {
        public Externo()
        {
            InitializeComponent();
        }

        private void ButtonIntercalacion_Click(object sender, EventArgs e)
        {
            OrdenamientoExterno.Intercalacion Intercalacion = new OrdenamientoExterno.Intercalacion();
            Intercalacion.Show();
            this.Hide();
        }

        private void ButtonMeazclaDirecta_Click(object sender, EventArgs e)
        {
            OrdenamientoExterno.MezclaDirecta MezclaDirect = new OrdenamientoExterno.MezclaDirecta();
            MezclaDirect.Show();
            this.Hide();
        }

        private void ButtonEquilibrada_Click(object sender, EventArgs e)
        {
            OrdenamientoExterno.MezclaEquilibrada MezclaEqui = new OrdenamientoExterno.MezclaEquilibrada();
            MezclaEqui.Show();
            this.Hide();
        }

        private void ButtonVolver_Click(object sender, EventArgs e)
        {
            Form1 Regresar = new Form1();
            Regresar.Show();
            this.Hide();
        }
    }
}
