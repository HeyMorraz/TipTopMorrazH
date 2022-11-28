using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTopMorrazH.Grafos
{
    class Floyd
    {
        public string AlgoritmoFloyd(long[,] array, int num)
        {
            int vertices = num;
            long[,] matrizAdy = array;
            string[,] caminos = new string[vertices, vertices];
            string[,] caminosAux = new string[vertices, vertices];
            string caminoRecorrido = "", cadena = "", caminitos = "";
            int i, j, k;
            float tempo0, tempo1, tempo2, tempo3, min;

            for (i = 0; i < vertices; i++)
            {
                for (j = 0; j < vertices; j++)
                {
                    caminos[i, j] = "";
                    caminosAux[i, j] = "";
                }
            }
            for (k = 0; k < vertices; k++)
            {
                for (i = 0; i < vertices; i++)
                {
                    for (j = 0; j < vertices; j++)
                    {
                        tempo0 = matrizAdy[i, j];
                        tempo1 = matrizAdy[i, k];
                        tempo2 = matrizAdy[k, j];
                        tempo3 = tempo1 + tempo2;

                        min = Math.Min(tempo0, tempo3);

                        if (tempo0 != tempo3)
                        {
                            if (min == tempo3)
                            {
                                caminoRecorrido = "";
                                caminosAux[i, j] = k + "";
                                caminos[i, j] = CaminoR(i, k, caminosAux, caminoRecorrido) + (k + 1);
                            }
                        }
                        matrizAdy[i, j] = (long)min;
                    }
                }
            }

            for (i = 0; i < vertices; i++)
            {
                for (j = 0; j < vertices; j++)
                {
                    cadena = cadena + "[" + matrizAdy[i, j] + "]";
                }
                cadena = cadena + "\n";
            }

            for (i = 0; i < vertices; i++)
            {
                for (j = 0; j < vertices; j++)
                {
                    if (matrizAdy[i, j] != 1000000000)
                    {
                        if (i != j)
                        {
                            if (caminos[i, j].Equals(""))
                            {
                                caminitos += "De (" + (i + 1) + "------>" + (j + 1) + ") " +
                                    "ir por (" + (i + 1) + ",   " + (j + 1) + ")\n";
                            }
                            else
                            {
                                caminitos += "De (" + (i + 1) + "------>" + (j + 1) + ") " +
                                    "ir por (" + (i + 1) + ",   " + caminos[i, j] + ", " + (j + 1) + ")\n";
                            }
                        }
                    }
                }
            }
            return "Los  caminos mas cortos entre los diferentes vertices es: \n" + cadena +
                "\nlos diferentes caminos mas cortos entre vertices son: \n" + caminitos;
        }
        public string CaminoR(int i, int k, string[,] caminosAux, string caminoRecorrido)
        {
            if (caminosAux[i, k].Equals(""))
            {
                return "";
            }
            else
            {
                caminoRecorrido += CaminoR(i, int.Parse(caminosAux[i, k]), caminosAux, caminoRecorrido) +
                    (int.Parse(caminosAux[i, k]) + 1) + ",  ";
                return caminoRecorrido;
            }

        }

    }
       
    
}
