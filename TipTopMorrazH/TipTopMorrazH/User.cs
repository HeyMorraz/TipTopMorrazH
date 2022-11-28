using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TipTopMorrazH
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        private void ButtonIniciar_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            if (txtUser.Text == "" || txtContraseña.Text == "")
            {
                MessageBox.Show("Rellene los campos");
            }
            else if(txtUser.Text == "administrador" && txtContraseña.Text == "guiselle")
            {
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error, verifique sus datos");
            }
        }
        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
