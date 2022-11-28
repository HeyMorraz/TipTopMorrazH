using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.Arboles
{
    class DibujarBalanceado
    {
        public NodoBalanceado raiz;

        Graphics nodo;
        Font font;

        int X = 720;
        int Y = 35;
        bool doble = false;
        bool encontrado = false;

        #region Constructores
        public DibujarBalanceado()
        {

        }
        public DibujarBalanceado( NodoBalanceado nuevo)
        {
            raiz = nuevo;
                 
        }
        public DibujarBalanceado(Graphics nodo, Font font)
        {
            this.nodo = nodo;
            this.font = font;
        }
        #endregion

        #region Metodo Equilibrar
        private int Equilibrio(NodoBalanceado X)
        {
            if (X == null)
            {
                return -1;
            }
            else
            {
                return X.f;
            }
        }
        #endregion

        #region Rotaciones para balancear
        private NodoBalanceado RotacionIzquierda(NodoBalanceado X)
        {
            NodoBalanceado aux = X.Izquierda;
            X.Izquierda = aux.Derecha;
            aux.Derecha = X;
            X.f = Math.Max(Equilibrio(X.Izquierda), Equilibrio(X.Derecha)) + 1;
            aux.f = Math.Max(Equilibrio(aux.Izquierda), Equilibrio(aux.Derecha)) + 1;
            return aux;

        }
        private NodoBalanceado RotacionDerecha(NodoBalanceado X)
        {
            NodoBalanceado aux = X.Derecha;
            X.Derecha = aux.Izquierda;
            aux.Izquierda = X;
            X.f = Math.Max(Equilibrio(X.Izquierda), Equilibrio(X.Derecha)) + 1;
            aux.f = Math.Max(Equilibrio(aux.Izquierda), Equilibrio(aux.Derecha)) + 1;
            return aux;
        }


        private NodoBalanceado DerechaDerecha(NodoBalanceado X)
        {
            NodoBalanceado temporal;
            X.Derecha = RotacionIzquierda(X.Derecha);
            temporal = RotacionDerecha(X);
            return temporal;
        }

        private NodoBalanceado IzquierdaIzquierda(NodoBalanceado X)
        {
            NodoBalanceado temporal;
            X.Izquierda = RotacionDerecha(X.Izquierda);
            temporal = RotacionIzquierda(X);
            return temporal;
        }
        #endregion

        #region Insertar Arbol
        private NodoBalanceado InsertarAB(NodoBalanceado nuevo, NodoBalanceado subArbol)
        {
            NodoBalanceado newFather = subArbol;
            if (nuevo.ValorNodo < subArbol.ValorNodo)
            {
                if (subArbol.Izquierda == null)
                {
                    subArbol.Izquierda = nuevo;
                }
                else
                {
                    subArbol.Izquierda = InsertarAB(nuevo, subArbol.Izquierda);
                    if (Equilibrio(subArbol.Izquierda) - Equilibrio(subArbol.Derecha) == 2)
                    {
                        if (nuevo.ValorNodo < subArbol.Izquierda.ValorNodo)
                        {
                            newFather = RotacionIzquierda(subArbol);
                        }
                        else
                        {
                            newFather = IzquierdaIzquierda(subArbol);
                        }
                    }
                }
                doble = false;
            }
            else if (nuevo.ValorNodo >  subArbol.ValorNodo )
            {
                if (subArbol.Derecha == null)
                {
                    subArbol.Derecha = nuevo;
                }
                else
                {
                    subArbol.Derecha = InsertarAB(nuevo, subArbol.Derecha);
                    if (Equilibrio(subArbol.Derecha) - Equilibrio(subArbol.Izquierda)  == 2)
                    {
                        if (nuevo.ValorNodo > subArbol.Derecha.ValorNodo)
                        {
                            newFather = RotacionDerecha(subArbol);
                        }
                        else
                        {
                            newFather = DerechaDerecha(subArbol);
                        }
                    }
                }
                doble = false;
            }
            else
            {
                doble = true;    
            }
            if ((subArbol.Izquierda == null) && (subArbol.Derecha != null))
            {
                subArbol.f = subArbol.Derecha.f + 1;
            }
            else if((subArbol.Derecha == null) && (subArbol.Izquierda != null))
            {
                subArbol.f = subArbol.Izquierda.f + 1;

            }
            else
            {
                subArbol.f = Math.Max(Equilibrio(subArbol.Izquierda), Equilibrio(subArbol.Derecha)) + 1;
            }
            return newFather;
        }
        #endregion

        #region Metodo insertar Datos
        public bool InsertaDatos(double total)
        {
            NodoBalanceado neew = new NodoBalanceado(total);
            if (raiz == null)
            {
                raiz = neew;
            }
            else
            {
                raiz = InsertarAB(neew, raiz);
            }
            return doble;
        }
        #endregion

        #region Mettodo Eliminar
        public bool EliminarBalanceado(double total)
        {
            raiz = EliminaElNodo(raiz, total);
            return encontrado;
        }
        #endregion


        #region Elimina el nodo
        private NodoBalanceado EliminaElNodo(NodoBalanceado raiz, double total)
        {
            if (raiz == null)
            {
                encontrado = false;
            }
            else if (total < raiz.ValorNodo)
            {
                NodoBalanceado izq = EliminaElNodo(raiz.Izquierda, total);
                raiz.Izquierda = izq;
            }
            else if (total > raiz.ValorNodo)
            {
                NodoBalanceado derech = EliminaElNodo(raiz.Derecha, total);
                raiz.Derecha = derech;
            }
            else
            {
                NodoBalanceado aux = raiz;
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
                    aux = Cambiar(aux);
                }
                aux = null;
                encontrado = true;
            }
            return raiz;
        }
        #endregion

        #region Metodo cambiar
        private NodoBalanceado Cambiar(NodoBalanceado aux)
        {
            NodoBalanceado temporal = aux, temporal1 = aux.Izquierda;
            while (temporal1.Derecha != null)
            {
                temporal = temporal1;
                temporal1 = temporal1.Derecha;
            }
            aux.ValorNodo = temporal1.ValorNodo;

            if (temporal == aux)
            {
                temporal.Izquierda = temporal1.Izquierda;
            }
            else
            {
                temporal.Derecha = temporal1.Izquierda;
            }
            return temporal;
        }
        #endregion

        #region Mostrar Arbol
        public void MostrarArbolo(PaintEventArgs e, Color c)
        {
            e.Graphics.Clear(c);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            nodo = e.Graphics;
            DibujarArbol(nodo, font, Brushes.Blue, Brushes.White, Pens.Black, Brushes.Black);
        }
        #endregion

        #region Dibuja Arbol
        private void DibujarArbol(Graphics graf, Font fuente, Brush colorRelleno, Brush colorFuente, Pen relaciones, Brush borde)
        {
            if (raiz == null)
            {
                return;
            }
            raiz.UbicaNodo(X, Y);
            raiz.DibujarRelaciones(graf, relaciones);
            raiz.DibujarNodo(graf, fuente, colorRelleno, colorFuente, relaciones, borde);
        }
        #endregion

        #region Recorridos
        public void In_Orden(NodoBalanceado temporal, Label leibol)
        {
            if (temporal != null)
            {

                In_Orden(temporal.Izquierda, leibol);
                leibol.Text += Convert.ToString(temporal.ValorNodo) + "   ";
                In_Orden(temporal.Derecha, leibol);
            }
        }


        public void PosOrden(NodoBalanceado temporal, Label label)
        {
            if (temporal != null)
            {

                PosOrden(temporal.Izquierda, label);
                PosOrden(temporal.Derecha, label);
                label.Text += Convert.ToString(temporal.ValorNodo) + "   ";
                
            }
        }
        public void preOrden(NodoBalanceado temporal, Label preorden)
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
