using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.Arboles
{
    class Nodo
    {
        //variables que manejan la posicion del elemento
        public Nodo Izquierda { get; set; }
        public Nodo Derecha { get; set; }

        public double ValorNodo;

        //dibujar el arbol con estas variables
        //130..........10..............................35
        int X = 110, Y = 10, XDerecha, YDerecha, Elipse = 35;

        public int Nivel { get; set; }

        //constructores 
        public Nodo()
        {
            ValorNodo = 0;
            Izquierda = null;
            Derecha = null;
        }
        //constructor que pasa parametros
        public Nodo(double dato, Nodo izq, Nodo der, Nodo padre)
        {
            ValorNodo = dato;
            this.Izquierda = izq;
            this.Derecha = der;
        }

        #region Metodo que ubica el nodo
        public void UbicaNodo(int PosX, int PosY)
        {
            int aux1,
                aux2;
            YDerecha = Convert.ToInt32(PosY + Elipse / 2);
            if (Izquierda != null)
            {
                Izquierda.UbicaNodo(PosX, PosY + Elipse + Y);
            }
            if (Izquierda != null && Derecha != null)
            {
                PosX += X;
            }
            if (Derecha != null)
            {
                Derecha.UbicaNodo(PosX, PosY + Elipse + Y);
            }
            if (Izquierda != null && Derecha != null)
            {
                XDerecha = Convert.ToInt32((Izquierda.XDerecha + Derecha.XDerecha) /2);
            }
            else if (Izquierda != null)
            {
                aux1 = Izquierda.XDerecha;
                Izquierda.XDerecha = XDerecha - 80;
                XDerecha = aux1;
            }
            else if (Derecha != null)
            {
                aux2 = Derecha.XDerecha;
                Derecha.XDerecha = XDerecha + 80;
                XDerecha = aux2;
            }
            else
            {
                XDerecha = Convert.ToInt32(PosX + Elipse / 2);
                PosX += Elipse;
            }
        }
        #endregion


        #region Dibuja las ramas o relaciones
        public void DibujarRelaciones(Graphics grafico, Pen relaciones)
        {
            if (Izquierda != null)
            {
                grafico.DrawLine(relaciones, XDerecha, YDerecha, Izquierda.XDerecha, Izquierda.YDerecha);
                Izquierda.DibujarRelaciones(grafico, relaciones);
            }
            if (Derecha != null)
            {
                grafico.DrawLine(relaciones, XDerecha, YDerecha, Derecha.XDerecha, Derecha.YDerecha);
                Derecha.DibujarRelaciones(grafico, relaciones);
            }
        }
        #endregion


        #region Dibuja el nodo
        public void DibujarNodo(Graphics grafico, Font fuente, Brush color, Brush colorFuente, Pen relacion, Brush B )
        {
            Rectangle T = new Rectangle(Convert.ToInt32(XDerecha - Elipse / 2), Convert.ToInt32(YDerecha - Elipse / 2), Elipse, Elipse);

            grafico.FillEllipse(B, T);
            grafico.FillEllipse(color, T);
            grafico.DrawEllipse(relacion, T);
            grafico.FillEllipse(color, T);
            grafico.DrawEllipse(relacion, T);

            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;

            grafico.DrawString(ValorNodo.ToString(), fuente, colorFuente, XDerecha, YDerecha, formato);

            if (Izquierda != null) 
            {
                Izquierda.DibujarNodo(grafico, fuente, color, colorFuente, relacion, B);

            }
            if (Derecha != null)
            {
                Derecha.DibujarNodo(grafico, fuente, color, colorFuente, relacion, B);
            }
        }
        #endregion



    }
}
