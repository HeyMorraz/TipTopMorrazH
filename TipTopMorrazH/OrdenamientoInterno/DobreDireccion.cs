using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.OrdenamientoInterno
{
    static class DobreDireccion
    {
        #region Funcion Plegamiento
        public static int Plegamiento(int codigo)
        {
            string d = codigo.ToString();
            int longitud = d.Length;
            int longitudIzq = longitud / 2;
            int longitudDer = longitud - longitudIzq;
            string izquierda = d.Substring(0, longitudIzq);
            string derecha = d.Substring(longitudIzq, longitudDer);
            string value = ((Convert.ToDouble(izquierda) + Convert.ToDouble(derecha)) + 1).ToString();
            return Convert.ToInt32(value.Substring(1, value.Length - 1));
        }
        #endregion

        #region Metodo Doble Direccion Hash
        public static string DobleDireccionHash(int codigo, Orden[] cliente)
        {
            int d = Plegamiento(codigo);

            if (cliente[d] == null)
            {
                return $"El producto con el Id {codigo}, no se encontro";
            }

            if (cliente[d].Id == codigo)
            {
                return $"El produto con el Id {cliente[d].Id} esta en la posicion, {d}";
            }

            int dx = Colision(d);
            while (cliente[d].Id != codigo && cliente[dx] != null && dx!= d)
            {
                dx = Colision(dx);
                if (dx > cliente.Length)
                {
                    return $"no se encontro el producto con Id {codigo}";
                }
            }
            if (cliente[dx].Id== codigo)
            {
                return $"El produto con el Id {cliente[d].Id}, esta en la posicion, {d}";
            }
            return $"No se encontro el producto con Id {codigo}";

        }
        #endregion

        public static int ObtenerDobleD(int d, Orden[] cliente)
        {
            int dx = Colision(d);
            while (dx<= cliente.Length && cliente[dx]!= null)
            {
                dx = Colision(dx);
            }
            return dx;
        }

        public static int Colision(int d)
        {
            MessageBox.Show($"Se esta solucionando una colision");
            return ((d + 1) % 10) + 1;
        }
        public static int Recibe(Orden[] cliente, Orden producto)
        {
            int d = Plegamiento(producto.Id);
            if (cliente[d]== null)
            {
                cliente[d] = producto;
                return d;
            }

            int dx = ObtenerDobleD(d, cliente);
            
            cliente[dx] = producto;
            return dx;
        }

    }
}
