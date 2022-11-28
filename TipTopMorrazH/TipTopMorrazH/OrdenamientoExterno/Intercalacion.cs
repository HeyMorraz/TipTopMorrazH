using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TipTopMorrazH.OrdenamientoExterno
{
    public partial class Intercalacion : Form
    {
        int numeroArchivo = 1, Cantidad, cantF1, cantF2, a = 0;
        int[] Elemento;

        TextWriter documento;
        public Intercalacion()
        {
            InitializeComponent();
            buttonOrdenar.Enabled = buttonLimpiar.Enabled = false;
        }

        private void ButtonCrear_Click(object sender, EventArgs e)
        {
            CrearDoc();
        }
        private void ButtonIniciar_Click(object sender, EventArgs e)
        {
            Canti();
            MessageBox.Show("Proceso de ordenacion inicializado");
        }
        private void ButtonInsertar_Click(object sender, EventArgs e)
        {
            GuardarN();
        }
        private void ButtonOrdenar_Click(object sender, EventArgs e)
        {
            IntercalacionA();
        }

        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            numeroArchivo = 1;
            buttonCrear.Enabled = true;
            buttonLimpiar.Enabled = true;
            buttonOrdenar.Enabled = false;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            MessageBox.Show("Nuevo registro, listo");
        }

        public void CrearDoc()
        {
            MessageBox.Show($"Listo!! El archivo F{numeroArchivo} se ha creado con exito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (numeroArchivo == 1)
            {
                documento = new StreamWriter($"F{numeroArchivo}.txt");
            }
            else
            {
                documento = new StreamWriter($"F2.txt");
            }
            txtCanti.Focus();
            txtCanti.Clear();
            buttonLimpiar.Enabled = true;
        }

        private void ButtonVolver_Click(object sender, EventArgs e)
        {
            Externo Regresar = new Externo();
            Regresar.Show();
            this.Hide();
        }

        #region Metodo Insertar
        public void Insertar(int [] elemento)
        {
            int aux0, h;
            for (int i = (elemento.Length-1); i >0; i--)
            {
                for (int j = (i-1); j >=0 ; j--)
                {
                    if (elemento[i] < elemento[j])
                    {
                        aux0 = elemento[i];
                        elemento[i] = elemento[j];
                        elemento[j] = aux0;
                    }
                }
            }
            foreach (int E in elemento)
            {
                if (numeroArchivo == 1)
                {
                    listBox1.Items.Add(E);
                }
                else
                {
                    listBox2.Items.Add(E);
                }
                documento.WriteLine(E);
            }
            documento.Close();
            buttonCrear.Enabled = true;
            if (numeroArchivo ==1)
            {
                numeroArchivo = 2;
            }
            else
            {
                numeroArchivo = 3;
                buttonCrear.Enabled = false;
            }
        }
        #endregion

        #region MetodoGuardar
        public void GuardarN()
        {
            int elem;
            if (a!=Cantidad && txtNum.Text!= "")
            {
                elem = Convert.ToInt32(txtNum.Text);
                if (elem <= 0 || txtNum.Text.Length >9)
                {
                    MessageBox.Show("Numero no permitido, intente de nuevo");
                }
                else
                {
                    Elemento[a] = elem;
                    a++;
                }
                if (a == Cantidad)
                {
                    Insertar(Elemento);
                }
                if (a == Cantidad && numeroArchivo == 2)
                {
                    MessageBox.Show($"Pulse crear para el nuevo archivo F{numeroArchivo}");
                }
                if (a == Cantidad && numeroArchivo ==3)
                {
                    buttonOrdenar.Enabled = true;
                }
                txtNum.Clear();
            }
        }
        #endregion

        #region MetodoIntercalacion
        public void IntercalacionA()
        {
            StreamReader F1 = new StreamReader("F1.txt");
            StreamReader F2 = new StreamReader("F2.txt");
            TextWriter DocF3 = new StreamWriter("F3.txt");

            int[] ff1 = new int[cantF1 + 1];
            int[] ff2 = new int[cantF2 + 1];

            int i = 0, j = 0, k;
            while (!F1.EndOfStream)
            {
                ff1[i] = Convert.ToInt32(F1.ReadLine());
                i++;
            }
            while (!F2.EndOfStream)
            {
                ff2[j] = Convert.ToInt32(F2.ReadLine());
                j++;
            }
            for ( i = k=j=0; i < (ff1.Length-1)&& j<(ff2.Length-1); k++)
            {
                if (ff1[i] < ff2[j])
                {
                    DocF3.WriteLine(ff1[i]);
                    i++;
                }
                else
                {
                    DocF3.WriteLine(ff2[j]);
                    j++;
                }
            }
            for (; i < ff1.Length - 1; i++)
            {
                DocF3.WriteLine(ff1[i]);
            }
            for(;j<ff2.Length-1;j++)
            {
                DocF3.WriteLine(ff2[j]);
            }
            F1.Close();
            F2.Close();
            DocF3.Close();

            StreamReader LF3 = new StreamReader("F3.txt");
            while (!LF3.EndOfStream)
            {
                listBox3.Items.Add(LF3.ReadLine());
            }
            LF3.Close();

        }
        #endregion

        #region MetodoCantidad
        public void Canti()
        {
            a= 0;
            if (txtCanti.Text!="")
            {
                Cantidad = Convert.ToInt32(txtCanti.Text);
                if (Cantidad <= 0 || txtCanti.Text.Length > 9 || txtCanti.Text == "")
                {
                    MessageBox.Show("Cantidad incorrecta, ingrese nuevamente por favor");
                }
                else
                {
                    Elemento = new int[Cantidad];
                    txtNum.Focus();
                    if (numeroArchivo == 1)
                    {
                        cantF1 = Cantidad;
                    }
                    else
                    {
                        cantF2 = Cantidad;     
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese el numero de elementos");
            }
        }
        #endregion

    }
}
