using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.OrdenamientoInterno
{
    public static class Busquedas
    {
        #region LinealDesordenada
        public static void LinealDesordenada(Orden[] arreglo, int codigoProducto)
        {
            int cantidad = arreglo.Length;
            int i = 0;
            bool bandera = false;

            while (i < cantidad && bandera == false)
            {

                if (arreglo[i].Id == codigoProducto)
                {
                    bandera = true;
                }
                else
                {
                    i++;
                }
            }
            if (bandera == true)
            {
                MessageBox.Show($"La factura que busca tiene la siguiente informacion: Cliente: {arreglo[i].Nombre}\n Id: {arreglo[i].Id} \n Codigo:{arreglo[i].Codigo} \n Nombre Combo:" +
                    $" {arreglo[i].NombreCombo} \n Total: {arreglo[i].NombreCombo} esta en a posicion {i}");
            }
            else
            {
                MessageBox.Show("Elemento no encontrado");
            }
        }
        #endregion

        #region LinealOrdenada
        public static void LinealOrdenada(Orden[] arreglo, int codigoProducto)
        {
            int cantidad = arreglo.Length;
            
            int i = 0;
            while ((i <= cantidad +1) && (codigoProducto > arreglo[i].Id))
            {
                i++; 
            }
            if ((i > cantidad) || (codigoProducto < arreglo[i].Id))
            {
                MessageBox.Show("No encontrado ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show($"La factura que busca tiene la siguiente informacion: Cliente: {arreglo[i].Nombre}\n Id: {arreglo[i].Id} \n en la posicion :{i+1} \n Codigo:{arreglo[i].Codigo} \n Nombre Combo: " +
                    $" {arreglo[i].NombreCombo} \n Total: {arreglo[i].Total}");
                
            }

        }
        #endregion
        
        #region Binaria
        public static void BusquedaBinaria(Orden[] arreglo, int codigoProducto)
        {
            int cantidad = arreglo.Length;
            for (int i = 0; i < cantidad - 1; i++)
            {
                for (int j = 0; j < cantidad - 1 - i; j++)
                {
                    if (arreglo[j].Id.CompareTo(arreglo[j + 1].Id) > 0)
                    {
                        Orden aux = arreglo[j];
                        arreglo[j] = arreglo[j + 1];
                        arreglo[j + 1] = aux;
                    }
                }
            }
            int Izquierda,
                Centro,
                Derecha;
            bool bandera = false;

            Izquierda = 0; Derecha = cantidad - 1;

            while (Izquierda <= Derecha && !bandera)
            {
                Centro = (Izquierda + Derecha) / 2;
                if (arreglo[Centro].Id == codigoProducto)
                {
                    bandera = true;
                    MessageBox.Show($"La factura que busca tiene la siguiente informacion: Cliente: {arreglo[Centro].Nombre}\n Codigo:{arreglo[Centro].Codigo} \n Nombre Combo: " +
                        $" {arreglo[Centro].NombreCombo} \n Total: {arreglo[Centro].Total}");
                }
                if (codigoProducto > arreglo[Centro].Id)
                {
                    Izquierda = Centro + 1;
                }
                else
                {
                    Derecha = Centro - 1;
                }
                
            }
            if (bandera == false)
            {
                MessageBox.Show("No se ha encontrado el elemento");
            }
            
        }
        #endregion

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
        public static void Mostrar(DataGridView datos, Orden[] arreglo)
        {
            datos.Rows.Clear();
            foreach (Orden orden in arreglo)
            {
                datos.Rows.Add(orden.Id, orden.Nombre, orden.Codigo, orden.TipoCombo, orden.NombreCombo, orden.CantiProd, orden.Total, orden.Llave);
            }
        }
    }

}
