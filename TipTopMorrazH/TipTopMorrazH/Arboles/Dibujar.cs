using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace TipTopMorrazH.Arboles
{
    class Dibujar
    {
        public Nodo raiz;

        Graphics nodo;
        Font font;

        //740.....45
        int X = 720;
        int Y = 35;

        bool encontrado = false;

        //sobrecargamos los costructores
        public Dibujar()
        {
            raiz = null;
        }
        public Dibujar(Graphics nodo, Font font)
        {
            this.nodo = nodo;
            this.font = font;
        }

        #region Insertar datos al nodo
        public bool Insertar(double total)
        {
            Nodo temp = new Nodo();

            temp.ValorNodo = total;
            temp.Izquierda = null;
            temp.Derecha = null;

            if (raiz == null)
            {
                raiz = temp;
                temp.Nivel = 1;
                return true;
            }
            else
            {
                Nodo anterior = null, ant;
                ant = raiz;

                while (ant != null)
                {
                    anterior = ant;
                    if (total < ant.ValorNodo)
                    {
                        ant = ant.Izquierda;
                    }
                    else
                    {
                        ant = ant.Derecha;
                    }
                }
                if (total < anterior.ValorNodo)
                {
                    temp.Nivel++;
                    anterior.Izquierda = temp;
                    return true;
                }
                else if (total > anterior.ValorNodo)
                {
                    temp.Nivel++;
                    anterior.Derecha = temp;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion


        #region Mostrar Arbol
        public void MostarA(PaintEventArgs e, Color color)
        {
            e.Graphics.Clear(color);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            nodo = e.Graphics;
            DibujaElArbol(nodo, font, Brushes.Blue, Brushes.White, Pens.Black, Brushes.Black);
        }
        #endregion

        #region Eliminar
        public bool Elimina(double total)
        {
            raiz = EliminarNodo(raiz, total);
            return encontrado;
        }
        #endregion

        #region Eliminar Nodo
        public Nodo EliminarNodo(Nodo raiz, double total)
        {
            if (raiz == null)
            {
                MessageBox.Show("No se encontro el nodo");
                encontrado = false;
            }
            else if (total < raiz.ValorNodo)
            {
                Nodo izq = EliminarNodo(raiz.Izquierda, total);
                raiz.Izquierda = izq;
            }
            else if (total > raiz.ValorNodo)
            {
                Nodo der = EliminarNodo(raiz.Derecha, total);
                raiz.Derecha = der;
            }
            else
            {
                Nodo aux = raiz;

                if (aux.Derecha == null)
                {
                    raiz = aux.Izquierda;
                }
                else if (aux.Izquierda == null)
                {
                    raiz = aux.Derecha;
                }
                else
                {
                    aux = Cambio(aux);
                }
                aux = null;
                encontrado = true;
            }
            return raiz;
        }
        #endregion

        #region Metodo Cambio
        protected Nodo Cambio(Nodo aux)
        {
            Nodo temporal0 = aux;
            Nodo Temporal1 = aux.Izquierda;

            while (Temporal1.Derecha != null)
            {
                temporal0 = Temporal1;
                Temporal1 = Temporal1.Derecha;
            }
            aux.ValorNodo = Temporal1.ValorNodo;

            if (temporal0 == aux)
            {
                temporal0.Izquierda = Temporal1.Izquierda;
            }
            else
            {
                temporal0.Derecha = Temporal1.Izquierda;
            }
            return Temporal1;
        }
        #endregion

        #region Dibujar Arbol
        public void DibujaElArbol(Graphics grafic, Font fuente, Brush relleno, Brush colorFuente, Pen relaciones, Brush borde)
        {
            if (raiz == null)
            {
                return;
            }
            raiz.UbicaNodo(X, Y);
            raiz.DibujarRelaciones(grafic, relaciones);
            raiz.DibujarNodo(grafic, fuente, relleno, colorFuente, relaciones, borde);
        }
        #endregion

        #region Recorridos

        public void In_Orden(Nodo temporal, Label leibol)
        {
            if (temporal != null)
            {
                
                In_Orden(temporal.Izquierda, leibol); 
                leibol.Text += Convert.ToString(temporal.ValorNodo) + "   ";
                In_Orden(temporal.Derecha, leibol);
            }
        }
        

        public void PosOrden(Nodo temporal, Label label)
        {
            if (temporal != null)
            {
                
                PosOrden(temporal.Izquierda, label);
                PosOrden(temporal.Derecha, label);
                label.Text += Convert.ToString(temporal.ValorNodo) + "   ";
                  
            }
        }
        public void preOrden(Nodo temporal, Label preorden)
        {
            if (temporal != null)
            {
                preorden.Text += Convert.ToString(temporal.ValorNodo) + "   ";
                preOrden(temporal.Izquierda, preorden);
                preOrden(temporal.Derecha, preorden);
            }
        }
        #endregion





    }
}
