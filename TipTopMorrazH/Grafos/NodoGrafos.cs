using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTopMorrazH.Grafos
{
    class NodoGrafos
    {
        public string NombreLugar { get; set; }
        public int PosX  { get; set; }
        public int PosY { get; set; }
        public string Color { get; set; }
        public int tamaño { get; set; }
        public int grosor { get; set; }
        public int peso { get; set; }
        public bool dirigido { get; set; }
        public NodoGrafos Anterior { set; get; }
        public NodoGrafos VerticeAntecesor { get; set; }
        public NodoGrafos VerticeAdyacente { get; set; }
        public NodoGrafos Siguiente { get; set; }

        //constructor que inicializa parametros
        public NodoGrafos()
        {
            NombreLugar = null;
            PosX = 0;
            PosY = 0;
            Color = "";
            tamaño = 0;
            grosor = 0;
            peso = 0;
            Anterior = null;
            VerticeAdyacente = null;
            VerticeAntecesor = null;
            Siguiente = null;
        }


    }
}
