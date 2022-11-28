using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TipTopMorrazH.OrdenamientoInterno;

namespace TipTopMorrazH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void OrdenamientoInternoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Interno orden = new OrdenamientoInterno.Interno();
            orden.Show();
            this.Hide();
        }
        private void OrdenamientoExternoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdenamientoExterno.Externo orden2 = new OrdenamientoExterno.Externo();
            orden2.Show();
            this.Hide();
        }

        private void VolverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User Regresar = new User();
            Regresar.Show();
            this.Hide();
        }
    }
}
