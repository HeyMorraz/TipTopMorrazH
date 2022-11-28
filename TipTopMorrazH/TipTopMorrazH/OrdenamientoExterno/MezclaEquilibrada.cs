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
    public partial class MezclaEquilibrada : Form
    {
        public string UbicacionA;
        public int Cantidad, NombreA, i = 0, NumA;
        TextWriter NuevoA;

        public int[] DatosArchivo;

        OpenFileDialog Archivo = new OpenFileDialog();

        public MezclaEquilibrada()
        {
            InitializeComponent();
        }

        private void ButtonCrear_Click(object sender, EventArgs e)
        {
            CrearA();
            txtCantidad.Focus();
        }
        private void ButtonAbrir_Click(object sender, EventArgs e)
        {
            AbrirA();
        }
        private void ButtonAceptar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "")
            {
                Cantidad = Convert.ToInt32(txtCantidad.Text);
                DatosArchivo = new int[Cantidad];
                MessageBox.Show("Proceso inicializado");
                txtNum.Focus();
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad por favor");
            }
        }
        private void ButtonAgregar_Click(object sender, EventArgs e)
        {
            if (txtNum.Text != "")
            {
                DatosArchivo[i] = Convert.ToInt32(txtNum.Text);
                txtNum.Clear();
                i++;

                if (i == Cantidad)
                {
                    Mostrar(listBox1, DatosArchivo);
                }
                else
                {
                    MessageBox.Show("Ingrese los numeros completos");
                }
            }
        }
        private void ButtonReiniciar_Click(object sender, EventArgs e)
        {
            Cantidad = 0;
            listBox1.Items.Clear();
        }
        private void ButtonVolver_Click(object sender, EventArgs e)
        {
            Externo Regresar = new Externo();
            Regresar.Show();
            this.Hide();
        }
        #region MeazclaEquilibrada
        public void Leer(ListBox list, string ubicacion)
        {
            StreamReader Leer = new StreamReader(ubicacion);
            list.Items.Clear();

            string CuerpoT = Leer.ReadToEnd();
            string[] linea = CuerpoT.Split('\r');

            int[] numCant = Array.ConvertAll(linea, item => Convert.ToInt32(item));
            int extencion = numCant.Length;
            Ordenar(numCant, 0, extencion - 1);

            foreach (var datos in numCant)
            {
                list.Items.Add(datos);
            }
            Leer.Close();
        }
        public void Ordenar(int[] num, int izq, int der)
        {
            int pivote;
            if (der > izq)
            {
                pivote = (der + izq) / 2;
                Ordenar(num, izq, pivote);
                Ordenar(num, (pivote + 1), der);
                Mezcla(num,izq, (pivote + 1), der);
            }
        }
        public void Mezcla(int[] arreglo, int izq, int pivote, int der)
        {
            int[] aux0 = new int[100];
            int i, izquierda, numero, aux1;
            izquierda = (pivote - 1);
            aux1 = izq;
            numero = (der - izq + 1);

            while ((izq<=izquierda) && (pivote<=der))
            {
                if (arreglo[izq]<=arreglo[pivote])
                {
                    aux0[aux1++] = arreglo[izq++];
                }
                else
                {
                    aux0[aux1++] = arreglo[pivote++];
                }
            }
            while (izq<=izquierda)
            {
                aux0[aux1++] = arreglo[izq++];
            }
            while (pivote<=der)
            {
                aux0[aux1++] = arreglo[pivote++];
            }
            for ( i = 0; i < numero; i++)
            {
                arreglo[der] = aux0[der];
                der--;
            }
        }

        public void Mostrar(ListBox list, int[] arreglo)
        {
            int extension = arreglo.Length;
            Ordenar(arreglo, 0, extension - 1);
            foreach (var datos in arreglo)
            {
                list.Items.Add(datos);
            }
            NuevoA.Close();
        }
        public void CrearA()
        {
            NuevoA = new StreamWriter($"F{NumA = 0}.txt");
            MessageBox.Show($"se ha creado el archivo f{NumA = 0} correctamente");
        }
        public void AbrirA()
        {
            Archivo.ShowDialog();
            if (!string.IsNullOrEmpty(Archivo.FileName))
            {
                UbicacionA = Archivo.FileName;
                Leer(listBox1, UbicacionA);
            }
            else
            {
                MessageBox.Show("No se encontro con elementos");
            }
        }
        #endregion
    }
}
