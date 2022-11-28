using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.OrdenamientoInterno
{
    public static class Ordenamientos
    {
        #region Burbuja
        //Ordenamiento burbuja//ascendente
        public static void OrdenarBurbuja(Orden[] arreglo)
        {
            int cantidad = arreglo.Length;
            for (int i = 0; i < cantidad - 1; i++)
            {
                for (int j = 0; j < cantidad - 1 - i; j++)
                {
                    if (arreglo[j].Total.CompareTo(arreglo[j + 1].Total) > 0)
                    {
                        Orden aux = arreglo[j];
                        arreglo[j] = arreglo[j + 1];
                        arreglo[j + 1] = aux;
                    }
                }
            }
        }
        #endregion

        #region InsercionDirecta
        //Ordenamiento insercion directa//descendente
        public static void OrdenarInsercion(Orden[] arreglo)
        {
            int cantidad = arreglo.Length;
            for (int i = 1; i < cantidad; i++)
            {
                Orden aux = arreglo[i];
                int j = i - 1;
                while (j >= 0 && arreglo[j].Total.CompareTo(aux.Total) < 0)
                {
                    arreglo[j + 1] = arreglo[j];
                    j--;
                }
                arreglo[j + 1] = aux;
            }
        }
        #endregion

        #region SeleccionDirecta
        //ordenamiento seleccion directa//ascendente
        public static void OrdenarSeleccion(Orden[] arreglo)
        {
            int cantidad = arreglo.Length;
            for (int i = 0; i < cantidad - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < cantidad; j++)
                {
                    if (arreglo[j].Total.CompareTo(arreglo[i].Total) <= 0)
                    {
                        min = j;
                    }

                }
                if (i != min)
                {
                    Orden aux = arreglo[i];
                    arreglo[i] = arreglo[min];
                    arreglo[min] = aux;
                }
            }
        }
        #endregion

        #region Shell
        //ordenamiento shell//descendente
        public static void OrdenarShell(Orden[] arreglo)
        {
            int salto, k, j;
            int cantidad = arreglo.Length;
            salto = cantidad / 2; //aca se parte el arreglo
            while (salto > 0)
            {
                for (int i = salto; i < cantidad; i++)
                {
                    j = i - salto;
                    while (j >= 0)
                    {
                        k = j + salto;
                        if (arreglo[j].Total.CompareTo(arreglo[k].Total) >= 0)
                        {
                            j = -1;
                        }
                        else
                        {
                            Orden aux = arreglo[j];
                            arreglo[j] = arreglo[k];
                            arreglo[k] = aux;

                            j -= salto;
                        }
                    }
                }
                salto = salto / 2;
            }
        }
        #endregion

        #region Quicksort
        //ordenamiento quicksort//ascendente
        public static void OrdenarQuicksort(Orden[] arreglo)
        {
            int cantidad = arreglo.Length;
            RapidoRecur();
            void RapidoRecur()
            {
                int i = 0, f = cantidad - 1;
                ReduceRecursivo(i, f);

            }
            void ReduceRecursivo(int Inic, int Fin)
            {
                int Izq, Der, Pos;
                bool Bandera = true;
                Orden aux;
                Izq = Inic; Der = Fin; Pos = Inic;
                while (Bandera == true)
                {
                    Bandera = false;
                    while (arreglo[Pos].Total <= arreglo[Der].Total && Pos != Der)
                    {
                        Der--;
                    }
                    if (Pos != Der)
                    {
                        aux = arreglo[Pos];
                        arreglo[Pos] = arreglo[Der];
                        arreglo[Der] = aux;

                        Pos = Der;

                        while (arreglo[Pos].Total >= arreglo[Izq].Total && Pos != Izq)
                        {
                            Izq++;
                        }
                        if (Pos != Izq)
                        {
                            aux = arreglo[Pos];
                            arreglo[Pos] = arreglo[Izq];
                            arreglo[Izq] = aux;
                            Pos = Izq;
                        }
                    }
                }
                if ((Pos - 1) > Inic)
                {
                    ReduceRecursivo(Inic, Pos - 1);
                }
                if (Fin > (Pos + 1))
                {
                    ReduceRecursivo(Pos + 1, Fin);
                }
            }
        }
        #endregion

        #region Heapsort
        //ordenamiento heapsort//descendente
        public static void OrdenarHeapsort(Orden[] arreglo)
        {
            int cantidad = arreglo.Length;
            int Ultimo = cantidad - 1;
            Orden aux;

            for (int i = (Ultimo - 1) / 2; i >= 0; i--)
            {
                Sort(i, Ultimo, arreglo);
            }
            for (int i = Ultimo; i >= 1; i--)
            {
                aux = arreglo[0];
                arreglo[0] = arreglo[i];
                arreglo[i] = aux;

                Sort(0, i - 1, arreglo);
            }
        }
        private static void Sort(int inicio, int n, Orden[] arreglo)
        {
            int i, j;
            bool bandera = false;

            i = inicio;
            j = 2 * i + 1;

            Orden aux = arreglo[i];

            while (j <= n && (!bandera))
            {
                if (j < n)
                {
                    if( arreglo[j].Total > arreglo[j+1].Total)
                    {
                        j++;
                    }
                }
                if (aux.Total <= arreglo[j].Total)
                {
                    bandera = true;
                }
                else
                {
                    arreglo[i] = arreglo[j];

                    i = j;
                    j = 2 * i + 1;
                }
            }
            arreglo[i] = aux;
        }
        #endregion

        #region MetodoMostrar
        public static void Mostrar(DataGridView datos, Orden[] arreglo)
        {
            datos.Rows.Clear();
            foreach(Orden orden in arreglo)
            {
                datos.Rows.Add(orden.Id, orden.Nombre, orden.Codigo, orden.TipoCombo, orden.NombreCombo, orden.CantiProd, orden.Total, orden.Llave);
            }
        }
        #endregion
    }
}
