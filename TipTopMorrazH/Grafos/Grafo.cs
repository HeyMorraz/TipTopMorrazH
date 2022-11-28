using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.Grafos
{
    class Grafo
    {
        public NodoGrafos Vprimero = new NodoGrafos();
        public NodoGrafos Vultimo = new NodoGrafos();
        public NodoGrafos Aprimero = new NodoGrafos();
        public NodoGrafos Aultimo = new NodoGrafos();
        public int NumV;
        public int numA;
        

        public Grafo()
        {
            Vprimero = null;
            Vultimo = null;
            Aprimero = null;
            Aultimo = null;
            NumV = 0;
            numA = 0;
        }

        public bool ExisteVertice()
        {
            return (NumV != 0);
        }
        public bool ExisteArista()
        {
            return (numA != 0);
        }
        public int Vertice()
        {
            return NumV;
        }
        public int Arista()
        {
            return numA;
        }

        #region Metodo que localiza el vertice
        public NodoGrafos LocalizaVertices(string nombre)
        {
            NodoGrafos act = Vprimero;
            while (act != null)
            {
                if (act.NombreLugar.Equals(nombre))
                {
                    return act;
                }
                act = act.Siguiente;

            }
            return null;
        }
        #endregion

        #region  si hay vertice
        public bool hayVert(NodoGrafos ver)
        {
            NodoGrafos buscar = LocalizaVertices(ver.NombreLugar);
            if (buscar != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Metodo Insertar el vertice
        public void InsertaVertices(NodoGrafos ver)
        {
            NodoGrafos nuevo = ver;
            if (Vprimero== null)
            {
                Vprimero = nuevo;
                Vprimero.Siguiente = null;
                Vprimero.Anterior = null;
                Vultimo = Vprimero;
            }
            else
            {
                Vultimo.Siguiente = nuevo;
                nuevo.Siguiente = null;
                nuevo.Anterior = Vultimo;
                Vultimo = nuevo;
            }
            NumV++;
        }
        #endregion

        #region Metodo Mostrar vertice
        public void MostarVertices(ListBox componentes)
        {
            if (ExisteVertice() == true)
            {
                componentes.Items.Clear();
                NodoGrafos q = Vprimero;
                componentes.Items.Add("Grafos");

                while (q != null)
                {
                    componentes.Items.Add("- Vertice: " + q.NombreLugar + "\t█Pos(x): " + q.PosX + "\t█Pos(y): " + q.PosY);
                    q = q.Siguiente;
                }
            }
           
        }
        #endregion

        #region Metodo adyacente
        public int Adyacentes(NodoGrafos v)
        {
            int n = 0;
            NodoGrafos q = LocalizaVertices(v.NombreLugar);
            while (q != null)
            {
                q = q.VerticeAdyacente;
                n++;
            }
            return n;
        }
        #endregion

        #region Metodo antecesores
        public int Antecesores(NodoGrafos v)
        {
            int n = 0;
            NodoGrafos q = LocalizaVertices(v.NombreLugar);
            while (q != null)
            {
                q = q.VerticeAntecesor;
                n++;
            }
            return n;
        }
        #endregion

        #region Inserta arista dirigida
        public void InsertarAristaDirigida(NodoGrafos arista, NodoGrafos a, NodoGrafos s)
        {
            NodoGrafos A = LocalizaVertices(s.NombreLugar);
            NodoGrafos nuevo = new NodoGrafos();
            nuevo.NombreLugar = a.NombreLugar;
            if (Adyacentes(A) == 0)
            {
                A.VerticeAdyacente = nuevo;
            }
            else
            {
                nuevo.VerticeAdyacente = A.VerticeAdyacente;
                A.VerticeAdyacente = nuevo;
            }
            NodoGrafos S = LocalizaVertices(a.NombreLugar);
            NodoGrafos nuevoA = new NodoGrafos();
            nuevoA.NombreLugar = s.NombreLugar;
            if (Antecesores(S) == 0)
            {
                S.VerticeAntecesor = nuevoA;
            }
            else
            {
                nuevoA.VerticeAntecesor = S.VerticeAntecesor;
                S.VerticeAntecesor = nuevoA; ;
            }
            //inswrtando aristas a la lista de aristas
            NodoGrafos neww = new NodoGrafos();
            neww = arista;
            if (Aprimero == null)
            {
                Aprimero = neww;
                Aprimero.Siguiente = null;
                Aprimero.Anterior = null;
                Aultimo = Aprimero;
            }
            else
            {
                Aultimo.Siguiente = neww;
                neww.Siguiente = null;
                neww.Anterior = Aultimo;
                Aultimo = neww;
            }
            numA++;
            
        }
        #endregion

        #region Metodo Mostrar el arista
        public void MostarAristas(ListBox componente)
        {
            if (ExisteArista() == true)
            {
                componente.Items.Clear();
                NodoGrafos q = Aprimero;
                componente.Items.Add("Grafos");
                while (q != null)
                {
                    componente.Items.Add("- Arista: " + "{" + q.VerticeAntecesor.NombreLugar + "," + q.VerticeAdyacente.NombreLugar + "}" + " Dirigido" + "\t\t█Peso{" + q.peso + "}");
                    q = q.Siguiente;
                }
            }
        }
        #endregion

        #region Posicion del vertice
        //Retorna la posicion dado un nombre de vertice
        public int posicionVertice(string nombre)
        {

            int n = 0;
            NodoGrafos Act = Vprimero;
            while (Act != null)
            {
                if (Act.NombreLugar.Equals(nombre))
                    return n;
                Act = Act.Siguiente;
                n++;
            }
            return -1;
        }
        #endregion

        #region Arista
        public string[] GetAristas() //Devuelve el nombre de todos las aristas en un vector tipo string
        {
            string[] vecNombres = new string[numA];
            NodoGrafos Actual = Aprimero;
            for (int i = 0; i < numA; i++)
            {
                vecNombres[i] = Actual.NombreLugar;
                Actual = Actual.Siguiente;
            }
            return vecNombres;
        }
        #endregion

        #region Localiza el vertice
        public NodoGrafos LocalizaArista(string nombre) //Localiza dado un nombre de Arista
        {
            NodoGrafos Act = Aprimero;
            while (Act != null)
            {
                if (Act.NombreLugar.Equals(nombre))
                    return Act;
                Act = Act.Siguiente;
            }
            return null;
        }
        #endregion

        #region Verifica si hay arista y vertice
        public NodoGrafos existeAristaYVertices(NodoGrafos ant, NodoGrafos ady) //Devuelve la arista si existe dado 2 vertices
        {
            string[] buscados = GetAristas();
            for (int i = 0; i < numA; i++)
            {
                NodoGrafos actual = LocalizaArista(buscados[i]);
                if (ant.NombreLugar == actual.VerticeAntecesor.NombreLugar && ady.NombreLugar == actual.VerticeAdyacente.NombreLugar)
                    return actual;
                else if (ady.NombreLugar == actual.VerticeAntecesor.NombreLugar && ant.NombreLugar == actual.VerticeAdyacente.NombreLugar)
                    return actual;
            }
            return null;
        }
        #endregion

        #region Segun la posicion del vertice

        //Retorna el vertice dado una posicion
        public NodoGrafos VerticeSegunPosicion(int n)
        {
            int contador = 0;
            NodoGrafos Act = Vprimero;
            while (contador != n)
            {
                Act = Act.Siguiente;
                contador++;
            }
            return Act;
        }
        #endregion

        #region Metodo Dijkrastra
        public void Dijkastr(string A, string B, ListBox L, Label T)
        {
            int inicio = posicionVertice(A);
            int final = posicionVertice(B);
            int distancia = 0;
            int n = 0;
            int cantNodos = NumV;
            int actual = 0;
            int columna = 0;

            int[,] tabla = new int[cantNodos, 3];

            for (n = 0; n < cantNodos; n++)
            {
                tabla[n, 0] = 0;
                tabla[n, 1] = int.MaxValue;
                tabla[n, 2] = 0;
            }
            tabla[inicio, 1] = 0;
            actual = inicio;

            do
            {
                tabla[actual, 0] = 1;
                for (columna = 0; columna < cantNodos; columna++)
                {
                    if (existeAristaYVertices(VerticeSegunPosicion(actual), VerticeSegunPosicion(columna)) != null)
                    {
                        distancia = existeAristaYVertices(VerticeSegunPosicion(actual), VerticeSegunPosicion(columna)).peso + tabla[actual, 1];
                        // Colocamos las distancias
                        if (distancia < tabla[columna, 1])
                        {
                            tabla[columna, 1] = distancia;
                            // Colocamos la informacion del padre
                            tabla[columna, 2] = actual;
                        }
                    }
                }
                int indiceMenor = -1;
                int distMenor = int.MaxValue;
                for (int x = 0; x < cantNodos; x++)
                {
                    if (tabla[x, 1] < distMenor && tabla[x, 0] == 0)
                    {
                        indiceMenor = x;
                        distMenor = tabla[x, 1];
                    }
                }
                actual = indiceMenor;
            } while (actual != -1);


            List<int> ruta = new List<int>();
            int nodo = final;

            while (nodo != inicio)
            {
                ruta.Add(nodo);
                nodo = tabla[nodo, 2];
            }
            ruta.Add(inicio);

            ruta.Reverse();

            int sumaDistancias = tabla[ruta.ElementAt(ruta.Count() - 1), 1];

            if (sumaDistancias == 0 || sumaDistancias == int.MaxValue)
            {
                L.Items.Add("No existe ruta disponible");
            }
            else
            {
                foreach (int posicion in ruta)
                {
                    L.Items.Add(" Su ruta: {" + VerticeSegunPosicion(posicion).NombreLugar + "}");
                    L.Items.Add("  ↓ ↓  ");
                }
                T.Text = Convert.ToString(sumaDistancias);
            }
        }
        #endregion



    }
}
