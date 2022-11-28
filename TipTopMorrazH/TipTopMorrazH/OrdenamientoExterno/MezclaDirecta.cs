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
    public partial class MezclaDirecta : Form
    {
        public int Cantidad, a;

        static StreamReader DocLectura;
        static StreamReader DocLectura1;
        static StreamReader DocLectura2;

        static StreamWriter DocEscritura;
        static StreamWriter DocEscritura1;
        static StreamWriter DocEscritura2;

        public struct OrdenamientoExterno
        {
            public int num;
        }
        OrdenamientoExterno[] Datos;
        public MezclaDirecta()
        {
            InitializeComponent();
            Cantidad = 0;
            a = 0;
            buttonMostrar.Visible = false;
        }


        private void ButtonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Datos[a].num = Convert.ToInt32(txtNumero.Text);
                DocEscritura.WriteLine(txtNumero.Text);
                DocEscritura.Flush();
                a++;
                MessageBox.Show($"Se ha ingresado el numero {Convert.ToInt32(txtNumero.Text)}");
                txtNumero.Clear();
                txtNumero.Focus();
            }
            catch 
            {

                MessageBox.Show("Todos los numeros han sido ingresados");
                txtNumero.Clear();
            }
        }

        private void ButtonMostrar_Click(object sender, EventArgs e)
        {
            DocLectura1 = new StreamReader("Doc.txt");
            while (!DocLectura1.EndOfStream)
            {
                listBox1.Items.Add(DocLectura1.ReadLine());
            }
            DocLectura1.Close();
        }

        private void ButtonVolver_Click(object sender, EventArgs e)
        {
            Externo Regresar = new Externo();
            Regresar.Show();
            this.Hide();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonAceptar_Click(object sender, EventArgs e)
        {
            Cantidad = Convert.ToInt32(txtCantidad.Text);
            Datos = new OrdenamientoExterno[Cantidad];
            DocEscritura = new StreamWriter("Doc.txt");
            buttonAceptar.Enabled = false;
            MessageBox.Show("Proceso inicializado");
            txtNumero.Focus();
        }

        #region MetodoMezclar
        public static void Mezcla(StreamReader Lectura, StreamWriter Escritura, StreamReader Lectura1, StreamWriter Escritura1,
            StreamReader Lectura2, StreamWriter Escritura2)
        {
            int Particionar = 1, vueltas = Cantidades(Lectura);
            while (Particionar <= vueltas)
            {
                Lectura = new StreamReader("Doc.txt");
                Escritura1 = new StreamWriter("Doc1.txt");
                Escritura2 = new StreamWriter("Doc2.txt");

                Particiones(Lectura, Escritura1, Escritura2, Particionar);
                Lectura.Close();
                Escritura1.Close();
                Escritura2.Close();

                Escritura = new StreamWriter("Doc.txt");
                Lectura1 = new StreamReader("Doc1.txt");
                Lectura2 = new StreamReader("Doc2.txt");

                Fusionar(Escritura, Lectura1, Lectura2, Particionar);
                Particionar *= 2;
            }
            Console.WriteLine("\n***Fin archivo***");
        }
        #endregion

        #region MetodoParticionar
        public static void Particiones(StreamReader Lectura, StreamWriter Escritura1, StreamWriter Escrtura2, int particionar)
        {
            int indicador = 0;
            while (!Lectura.EndOfStream)
            {
                int indicadorF1 = 0;
                while (indicadorF1 < particionar && !Lectura.EndOfStream)
                {
                    indicador = Convert.ToInt32(Lectura.ReadLine());
                    Escritura1.WriteLine(indicador);
                    indicadorF1++;
                }
                int indicadorF2 = 0;
                while (indicadorF2 < particionar && !Lectura.EndOfStream)
                {
                    indicador = Convert.ToInt32(Lectura.ReadLine());
                    Escrtura2.WriteLine(indicador);
                    indicadorF2++;
                }
            }
        }
        #endregion

        #region MetodoParaObtenerCantidades
        public static int Cantidades(StreamReader ArchivoP)
        {
            ArchivoP = new StreamReader("Doc.txt");
            int canti = 0;
            while (!ArchivoP.EndOfStream)
            {
                string Cadena = ArchivoP.ReadLine();
                canti++;
            }
            ArchivoP.Close();
            return canti;
        }
        #endregion

        #region MetodoOrdenar
        private void ButtonOrdenar_Click(object sender, EventArgs e)
        {
            try
            {
                DocEscritura.Close();
                Mezcla(DocLectura, DocEscritura, DocLectura1, DocEscritura1, DocLectura2, DocEscritura2);
                MessageBox.Show("Datos ordenados");
            }
            catch (Exception H)
            {

                MessageBox.Show("Error: " + H, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            buttonMostrar.Visible = true;
        }
        #endregion

        #region MetodoFusionar
        //metodo fusionar 
        public static void Fusionar(StreamWriter Escritura, StreamReader Lectura1, StreamReader Lectura2, int particionar)
        {
            bool band1 = true, band2 = true;
            int leer1 = 0, leer2 = 0;
            if (!Lectura1.EndOfStream)
            {
                leer1 = Convert.ToInt32(Lectura1.ReadLine());
                band1 = false;
            }
            if (!Lectura2.EndOfStream)
            {
                leer2 = Convert.ToInt32(Lectura2.ReadLine());
                band2 = false;
            }
            while ((!Lectura1.EndOfStream || band1 == false) && (!Lectura2.EndOfStream || band2 == false))
            {
                int indicador1 = 0, indicador2 = 0;
                while ((indicador1 < particionar && band1 == false) && (indicador2 < particionar && band2 == false))
                {
                    if (leer1 <= leer2)
                    {
                        Escritura.WriteLine(leer1);
                        band1 = true; indicador1++;
                        if (!Lectura1.EndOfStream)
                        {
                            leer1 = Convert.ToInt32(Lectura1.ReadLine());
                            band1 = false;
                        }
                    }
                    else
                    {
                        Escritura.WriteLine(leer2);
                        band2 = true; indicador2++;
                        if (!Lectura2.EndOfStream)
                        {
                            leer2 = Convert.ToInt32(Lectura2.ReadLine());
                            band2 = false;
                        }
                    }
                }

                while (indicador1 < particionar && band1 == false)
                {
                    Escritura.WriteLine(leer1);
                    band1 = true; indicador1++;
                    if (!Lectura1.EndOfStream)
                    {
                        leer1 = Convert.ToInt32(Lectura1.ReadLine());
                        band1 = false;
                    }

                }
                while (indicador2 < particionar && band2 == false)
                {
                    Escritura.WriteLine(leer2);
                    band2 = true; indicador2++;
                    if (!Lectura2.EndOfStream)
                    {
                        leer2 = Convert.ToInt32(Lectura2.ReadLine());
                        band2 = false;
                    }
                }
            }
            if (band1 == false) { Escritura.WriteLine(leer1); }
            if (band2 == false) { Escritura.WriteLine(leer2); }
            while (!Lectura1.EndOfStream)
            {
                leer1 = Convert.ToInt32(Lectura1.ReadLine());
                Escritura.WriteLine(leer1);
            }
            while (!Lectura2.EndOfStream)
            {
                leer2 = Convert.ToInt32(Lectura2.ReadLine());
                Escritura.WriteLine(leer2);
            }
            Escritura.Close();
            Lectura1.Close();
            Lectura2.Close();


        }
        #endregion
    }
}
