using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.OrdenamientoInterno
{
    public partial class Interno : Form
    {
        #region Instancia de clases para arboles

        Arboles.Dibujar arbol = new Arboles.Dibujar();
        Arboles.Nodo dibujar = new Arboles.Nodo();
        Graphics nodos;
        Arboles.DibujarBalanceado arbolito = new Arboles.DibujarBalanceado();
        #endregion

        #region Clases para grafos
        //para los grafos
        Random random = new Random();
        Grafos.Grafo objeto = new Grafos.Grafo();
        public int ContadorV = 0;
        public int ContadorA = 0;
        
        //elementos del grafico
        private Bitmap bmp;
        private Pen lapiz;
        private Graphics graf;
        #endregion

        #region AlgoritmoFloyd Clase

        //variables para grafos camino corto
        Grafos.Floyd caminoCorto = new Grafos.Floyd();
        public long[,] matriz;
        public int contador1 = 0, contador2 = 1;
        public int num;
        #endregion

        // instancia de la clase para la busqueda Doble direccion hash
        Orden[] clientesHash = new Orden[100];
        //arreglo de registro
        Orden[] clientes;

        //variables a ocupar
        private int cantidadClientes;
        private int clientesAtendidos = 0;
        public string NombreCombo, Tipocom;

        //guarda un diccionario
        private Dictionary<string, (int, int)> comboSeleccionado;
        private string nombreCombo;
        //diccionarios de cada combo
        private Dictionary<string, (int, int)> combosTradicionales = new Dictionary<string, (int, int)>
        {
            {"Familiar" , (500, 1111)},
            {"Gran Familiar" , (600, 2222)},
            {"Super Familiar" , (700,3333)},
            {"Super" , (800,4444)},
        };
        private Dictionary<string, (int, int)> combosEspeciales = new Dictionary<string, (int, int)>
        {
            {"Alitas Picantes" , (300, 5555)},
            {"Burrito Tip Top" , (300, 6666)},
            {"Combo Criollo" , (350,7777)},
        };
        private Dictionary<string, (int, int)> combosCafePostres = new Dictionary<string, (int, int)>
        {
            {"Tres leches" , (200, 8888)},
            {"Frappe" , (100, 9999)},
            {"Muffin Vainilla" , (150,1010)},
        };
        private Dictionary<string, int> comboExtra = new Dictionary<string, int>()
        {
            {"Nada", 0},
            {"Arroz", 100},
            {"Coca cola", 50 },
            {"Ensalada Repollo", 100},
        };
        public Interno()
        {
            InitializeComponent();
            EstadoInicial();
            txtNomArista.Text = "V";
            txtNombreL.Text = "V";
            txtConex.Text = "V";
            txtConexion.Text = "V";
            //arboles
            nodos = CreateGraphics();
            arbol = new Arboles.Dibujar(nodos, Font);
            nodos = CreateGraphics();
            arbolito = new Arboles.DibujarBalanceado(nodos, Font);
            //esto es para grafos
            comboTradicional.SelectedIndex = comboEspecial.SelectedIndex = comboPostres.SelectedIndex = comboEspecial.SelectedIndex = comboTipo.SelectedIndex = comboExtras.SelectedIndex = 0 ;
            TabGrafos.BringToFront();
            InicializarGrafico();
        }

        #region Metodo que indica el estado inicial de algunos botones
        //metodo para ocultar botones
        public void EstadoInicial()
        {
            txtCantClientes.Focus();
            labelTradicional.Visible = comboTradicional.Visible = labelEspeciale.Visible = comboEspecial.Visible = labelPostres.Visible = comboPostres.Visible =
            labelExtras.Visible = comboExtras.Visible = labelCategoriass.Visible = labelEleccion.Visible = false;
            buttonBurbuja.Visible = true;
            buttonSeleccion.Visible = buttonInsercion.Visible = buttonShelll.Visible = buttonHeapsort.Visible = buttonQuicksort.Visible = false;
            buttonDesordenada.Visible = labelHash.Visible = true;
            buttonOrdenada.Visible = buttonBinaria.Visible = buttonHash.Visible =  false;
        }
        #endregion

        
        #region BotonIniciar
        private void ButtonIniciar_Click(object sender, EventArgs e)
        {
            if (txtCantClientes.Text == string.Empty)
            {
                MessageBox.Show("Ingrese una cantidad", "Error de dato");
                return;
            }
            if (int.Parse(txtCantClientes.Text) <= 0)
            {
                MessageBox.Show("Ingrese una mayor que cero", "Error de dato");
                return;
            }
            cantidadClientes = int.Parse(txtCantClientes.Text);
            clientes = new Orden[cantidadClientes];
           

            txtCantClientes.Enabled = false;
            buttonIniciar.Enabled = false;

            comboTipo.Focus();

            MessageBox.Show("El Id del cliente debe contener como minimo 4 digitos");
        }
        #endregion

        private void ComboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelTradicional.Visible = comboTradicional.Visible = labelEspeciale.Visible = comboEspecial.Visible = labelPostres.Visible = comboPostres.Visible = labelCategoriass.Visible = labelEleccion.Visible = false;

            switch (comboTipo.SelectedIndex)
            {
                case 0:
                    labelTradicional.Visible = comboTradicional.Visible = labelExtras.Visible = comboExtras.Visible = labelCategoriass.Visible = labelEleccion.Visible = true;
                    break;
                case 1:
                    labelEspeciale.Visible = comboEspecial.Visible = labelExtras.Visible = comboExtras.Visible = labelCategoriass.Visible = labelEleccion.Visible = true;
                    break;
                case 2:
                    labelPostres.Visible = comboPostres.Visible = labelExtras.Visible = comboExtras.Visible = labelCategoriass.Visible = labelEleccion.Visible = true;
                    break;
            }
        }

        //Ordenamientos y arboles
        #region BotonAgregar    
        private void ButtonAgregar_Click(object sender, EventArgs e)
        {

            Tipocom = comboTipo.SelectedItem.ToString();

            switch (comboTipo.SelectedIndex)
            {
                case 0:
                    comboSeleccionado = combosTradicionales;
                    nombreCombo = comboTradicional.SelectedItem.ToString();
                    break;
                case 1:
                    comboSeleccionado = combosEspeciales;
                    nombreCombo = comboEspecial.SelectedItem.ToString();
                    break;
                case 2:
                    comboSeleccionado = combosCafePostres;
                    nombreCombo = comboPostres.SelectedItem.ToString();
                    break;
            }



            if (txtID.Text == string.Empty || txtNombreCli.Text == string.Empty || txtCantProd.Text == string.Empty)
            {
                MessageBox.Show("Por favor rellene los campos");
                return;
            }
            if (!(clientesAtendidos < cantidadClientes))
            {
                return;
            }
            if (comboTipo.Text == string.Empty)
            {
                MessageBox.Show("Elija una opcion");
                return;
            }
            else
            {
                try
                {
                    Orden cliente = new Orden();
                    cliente.Id = int.Parse(txtID.Text);
                    cliente.Nombre = txtNombreCli.Text;
                    cliente.Codigo = comboSeleccionado[nombreCombo].Item2;
                    cliente.TipoCombo = Tipocom;
                    cliente.NombreCombo = nombreCombo;
                    cliente.CantiProd = int.Parse(txtCantProd.Text);
                    cliente.Total = (comboSeleccionado[nombreCombo].Item1 + comboExtra[comboExtras.SelectedItem.ToString()]) * int.Parse(txtCantProd.Text);

                    clientes[clientesAtendidos] = cliente;
                    
                    if (!arbol.Insertar(cliente.Total)) /*|| (!arbolito.InsertaDatos(cliente.Total)))*/
                    {
                        MessageBox.Show("ha ocurrido un error, ingrese nuevamente");
                        return;
                    }
                    else
                    {
   
                        int llave = DobreDireccion.Recibe(clientesHash, cliente);
                        clientes[clientesAtendidos].Llave = llave;
                        MessageBox.Show($"El combo {clientes[clientesAtendidos].NombreCombo} se ha ingresado en la posicion {llave}");
                        dataGridView1.Rows.Add(clientes[clientesAtendidos].Id, clientes[clientesAtendidos].Nombre, clientes[clientesAtendidos].Codigo, clientes[clientesAtendidos].TipoCombo, clientes[clientesAtendidos].NombreCombo, clientes[clientesAtendidos].CantiProd, clientes[clientesAtendidos].Total, clientes[clientesAtendidos].Llave);
                        Refresh();
                        clientesAtendidos++;  
                        txtID.Clear();
                        txtNombreCli.Clear();
                        txtCantProd.Clear();

                        if (arbolito.InsertaDatos(cliente.Total))
                        {
                            MessageBox.Show("Su valor esta duplicado, ingrese nuevamente");
                            return;
                        }

                        //recorridosj
                        labelPreOrden.Text = labelInOrden.Text = labelPosOrden.Text = "";
                        arbol.In_Orden(arbol.raiz, labelInOrden);
                        arbol.preOrden(arbol.raiz, labelPreOrden);
                        arbol.PosOrden(arbol.raiz, labelPosOrden);

                        Refresh();
                        Refresh();
                        Refresh();

                       
                        //recorridos balanceados
                        LabelPreOrdenBalanceado.Text = labelInOrdenBalanceado.Text = labelPosOrdenBalanceado.Text = "";
                        arbolito.In_Orden(arbolito.raiz, labelInOrdenBalanceado);
                        arbolito.preOrden(arbolito.raiz, LabelPreOrdenBalanceado);
                        arbolito.PosOrden(arbolito.raiz, labelPosOrdenBalanceado);

                        Refresh();
                        Refresh();
                        Refresh();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Datos erroneos");
                }
            }

        }
        #endregion

        #region Eliminar Binario
        private void ButtonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                double x;
                x = Convert.ToDouble(txtRefere.Text);
                arbol.Elimina(x);
                labelPreOrden.Text = labelInOrden.Text = labelPosOrden.Text = "";
                arbol.In_Orden(arbol.raiz, labelInOrden);
                arbol.preOrden(arbol.raiz, labelPreOrden);
                arbol.PosOrden(arbol.raiz, labelPosOrden);
                txtRefere.Clear();

                Refresh();
                Refresh();

            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            
            }
            Refresh(); Refresh(); Refresh();
            txtRefere.Clear();
        }
        #endregion

        #region Eliminar Balanceado
        private void ButtonEliminaBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double x;
                x = Convert.ToDouble(txtEliminaReferencia.Text);
                arbolito.EliminarBalanceado(x);
                LabelPreOrdenBalanceado.Text = labelInOrdenBalanceado.Text = labelPosOrdenBalanceado.Text = "";
                arbolito.In_Orden(arbolito.raiz, labelInOrdenBalanceado);
                arbolito.preOrden(arbolito.raiz, LabelPreOrdenBalanceado);
                arbolito.PosOrden(arbolito.raiz, labelPosOrdenBalanceado);
                txtEliminaReferencia.Clear();

                Refresh();
                Refresh();

            }
            catch (Exception)
            {

                MessageBox.Show("Error"); ;
            }
            Refresh(); Refresh(); Refresh();
            txtEliminaReferencia.Clear();
        }
        #endregion

        #region Busquedas
        private void ButtonDesordenada_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text == "")
            {
                MessageBox.Show("Ingrese la referencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferencia.Focus();
            }
            else
            {
                Busquedas.LinealDesordenada(clientes, int.Parse(txtReferencia.Text));
            }
        }
        private void ButtonOrdenada_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text == "")
            {
                
                MessageBox.Show("Ingrese la referencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferencia.Focus();
            }
            else
            {
                Busquedas.OrdenarBurbuja(clientes);
                Busquedas.Mostrar(dataGridView1, clientes);
                Busquedas.LinealOrdenada(clientes, int.Parse(txtReferencia.Text));
            }
        }
        private void ButtonBinaria_Click(object sender, EventArgs e)
        {
          
            if (txtReferencia.Text == "")
            {
                MessageBox.Show("Ingrese la referencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferencia.Focus();
            }
            else
            { 
                Busquedas.BusquedaBinaria(clientes, int.Parse(txtReferencia.Text));
            }
        }
        private void ButtonHash_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text == "")
            {
                MessageBox.Show("Ingrese como referencia el Id del cliente que lleve 4  digitos ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferencia.Focus();
            }
            else
            {
                string mensaje = OrdenamientoInterno.DobreDireccion.DobleDireccionHash(Convert.ToInt32(txtReferencia.Text), clientesHash);
                MessageBox.Show(mensaje);
            }
        }
        #endregion

        #region OrdenamientoInterno
        private void ButtonBurbuja_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Ordenamientos.OrdenarBurbuja(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
            
        }
        private void ButtonSeleccion_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Ordenamientos.OrdenarSeleccion(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
        }
        private void ButtonInsercion_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Ordenamientos.OrdenarInsercion(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
        }
        private void ButtonShelll_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Ordenamientos.OrdenarShell(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
        }
        private void ButtonQuicksort_Click(object sender, EventArgs e)
        {

            OrdenamientoInterno.Ordenamientos.OrdenarQuicksort(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
        }
        private void ButtonHeapsort_Click(object sender, EventArgs e)
        {
            OrdenamientoInterno.Ordenamientos.OrdenarHeapsort(clientes);
            OrdenamientoInterno.Ordenamientos.Mostrar(dataGridView1, clientes);
        }
        private void VolverToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1 Regresar = new Form1();
            Regresar.Show();
            this.Hide();
        }
        #endregion

        //Caminos cortos con floyd
        #region Botones para mostrar los caminos mas cortos algoritmoFloyd
        private void BtnIngresarNumeros_Click(object sender, EventArgs e)
        {
            num = Convert.ToInt32(txtNumeroVertices.Text);
            matriz = new long[Convert.ToInt32(txtNumeroVertices.Text), Convert.ToInt32(txtNumeroVertices.Text)];
            MessageBox.Show("Ahora ingresa las distancias");
            comboSi.Enabled = true;
            btnConfirmar.Enabled = true;
            txtNumeroVertices.Enabled = false;
            btnIngresarNumeros.Enabled = false;
            LabelVertice1.Text = Convert.ToString(contador1 + 1);
            LabelVerice2.Text = Convert.ToString(contador2 + 1);
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (comboSi.Text == "No")
            {
                MessageBox.Show("No hay distancia entre los vertices " + (contador1 + 1) + " y " + (contador2 + 1));
                comboSi.Text = "";
                matriz[contador1, contador2] = 999999999;
                contador2++;

                if (contador2 != num)
                {
                    labelVer2.Text = Convert.ToString(contador2 + 1);
                }
                else
                {
                    contador2 = 0;
                    MessageBox.Show("Distancias del vertice completadas" + (contador2 + 1) + " Continue con el siguiente vertice");
                    LabelVerice2.Text = Convert.ToString(contador2 + 1);
                    contador1++;
                    if (contador1 == num)
                    {
                        
                        LabelVertice1.Visible = false;
                        LabelVerice2.Visible = false;
                        comboSi.Enabled = false;
                        btnIngresarDistancia.Enabled = false;
                        btnConfirmar.Enabled = false;
                        contador1 = 0;
                        MessageBox.Show("Se ha completado la  distancia");
                        buttonCaminoCorto.Enabled = true;
                        comboSi.Enabled = false;
                    }
                    else
                    {
                        LabelVertice1.Text = Convert.ToString(contador1 + 1);
                    }
                }

            }
            else
            {
                comboSi.Text = "";
                btnIngresarDistancia.Enabled = true;
                txtDistancia.Enabled = true;
                LabellVertice3.Text = Convert.ToString(contador1 + 1);
                LabelVertice4.Text = Convert.ToString(contador2 + 1);
                MessageBox.Show("Si hay distancia del vertice " + (contador1 + 1) + " al vertice " + (contador2 + 1) +
                    " , continue a ingresar la distancia");
            }
        }
        private void BtnIngresarDistancia_Click(object sender, EventArgs e)
        {

            matriz[contador1, contador2] = int.Parse(txtDistancia.Text);
            txtDistancia.Clear();
            btnIngresarDistancia.Enabled = false;
            LabellVertice3.Enabled = false;
            LabelVertice4.Visible = false;
            MessageBox.Show("Distancia ingresada");
            contador2++;

            if (contador1 == contador2)
            {
                matriz[contador1, contador2] = 0;
                contador2++;
            }
            if (contador2 != num)
            {
                LabelVerice2.Text = Convert.ToString(contador2 + 1);
            }
            else
            {
                contador2 = 0;
                MessageBox.Show("Distancias completadas " + (contador1 + 1) + " continue con el siguiente vertice");
                LabelVerice2.Text = Convert.ToString(contador2 + 1);
                contador1++;
                if (contador1 == num)
                {
                    LabelVertice1.Visible = false;
                    LabelVerice2.Visible = false;
                    comboSi.Enabled = false;
                    btnIngresarDistancia.Enabled = false;
                    txtDistancia.Enabled = false;
                    contador1 = 0;
                    MessageBox.Show("Se ha completado al matriz de distancias");
                    btnIngresarDistancia.Visible = true;
                    buttonCaminoCorto.Enabled = true;
                }
                else
                {
                    LabelVerice2.Text = Convert.ToString(contador1 + 1);
                }
            }
        }
        private void ButtonVerCaminos_Click(object sender, EventArgs e)
        {
            richMostrar.Text = caminoCorto.AlgoritmoFloyd(matriz, num);
        }
        #endregion

        //Metodos para grafos
        #region Dibujar el vertice 
        private void DibujarVertice(string nombreL, string color,int tamaño, int posX, int posY)
        {
            switch (comboColorVertice.SelectedIndex)
            {
                case 0:
                    lapiz = new Pen(Color.Yellow);
                    break;
                case 1:
                    lapiz = new Pen(Color.Blue);
                    break;
                case 2:
                    lapiz = new Pen(Color.Green);
                    break;
            }
            SolidBrush S = new SolidBrush(Color.Wheat);
            FontFamily ff = new FontFamily("Consolas");
            Font font = new Font(ff, 15);

            graf = TabGrafos.CreateGraphics();
            graf.DrawArc(lapiz, new Rectangle(posX, posY, tamaño, tamaño), 0, 360);
            graf.DrawString(nombreL, Font, S, posX - 10, posY - 30);
        }
        #endregion

        #region Dibujar Arista
        public void DibujarArista(string nombreA, int peso, string color, int xi, int yi, int xf, int yf )
        {
            switch (comboColorArista.SelectedIndex)
            {
                case 0:
                    lapiz = new Pen(Color.Yellow);
                    break;
                case 1:
                    lapiz = new Pen(Color.Blue);
                    break;
                case 2:
                    lapiz = new Pen(Color.Green);
                    break;
            }
            lapiz.StartCap = LineCap.Flat;
            lapiz.EndCap = LineCap.ArrowAnchor;

            SolidBrush S = new SolidBrush(Color.Azure);
            FontFamily f = new FontFamily("Consolas");
            Font font = new Font(f, 15);

            graf = TabGrafos.CreateGraphics();
            graf.DrawLine(lapiz, xi + 5, yi + 6, xf + 5, yf + 6);
            graf.DrawString($"{peso},", font , S, (xi + xf) / 2, (yi + yf) / 2);
        }
        #endregion

        #region Boton que agrega el vertice 
        private void Button1Agregar_Click(object sender, EventArgs e)
        {
            if (TabOrdenar.SelectedTab.Text == "Grafos Dibujo")
            {
                try
                {
                    graf = TabGrafos.CreateGraphics();
                    string nombreLugar = txtNombreL.Text;
                    nombreLugar = nombreLugar.Trim();
                    int posX  = random.Next(100, 765);
                    int PosY = random.Next(100, 615);
                    int tamaño = 8;
                    int grosor = 8;


                    string color = comboColorVertice.Text;
                   
                    if (nombreLugar != "" && posX <= 765 && PosY <= 615 ) 
                    {
                        Grafos.NodoGrafos vertice = new Grafos.NodoGrafos();
                        vertice.NombreLugar = nombreLugar;
                        vertice.PosX = posX;
                        vertice.PosY = PosY;
                        vertice.Color = color;
                        vertice.tamaño = tamaño;
                        vertice.grosor = grosor;

                        objeto.InsertaVertices(vertice);
                        objeto.MostarVertices(listBox1);
                        DibujarVertice(nombreLugar, color, tamaño, posX, PosY);
                        
                        if (objeto.hayVert(vertice))
                        {
                            ContadorV += 1;
                            txtNombreL.Text = "V" + ContadorV;
                        }    
                    }
                    //else
                    //{
                    //    int n = Convert.ToInt32(comboColorVertice.Text);
                    //}
                }
                catch (Exception)
                {

                    //MessageBox.Show("Propiedades del vertice invalidas");
                }

            }


        }
        #endregion

        #region Boton para agregar el arista
        private void ButtonAgreArista_Click(object sender, EventArgs e)
        {
            //if (TabOrdenar.SelectedTab.Text == "Grafos")
            //{
            //    try
            //    { 
                    string nombre = txtNomArista.Text;
                    nombre = nombre.Trim();
                    string color = comboColorArista.Text;      
                    string conexion1 = txtConexion.Text;
                    conexion1 = conexion1.Trim();
                    string conexion2 = txtConex.Text;
                    conexion2 = conexion2.Trim();
                    int peso = random.Next(1, 10);

                    if (nombre!= "" && conexion1 != "" && conexion2 != "")
                    {
                        Grafos.NodoGrafos nuevo = new Grafos.NodoGrafos();
                        nuevo.Color = color;
                        nuevo.NombreLugar = txtNomArista.Text;
                        nuevo.peso = peso;

                        Grafos.NodoGrafos antecesor = objeto.LocalizaVertices(conexion1);
                        Grafos.NodoGrafos adyacente = objeto.LocalizaVertices(conexion2);
                        nuevo.VerticeAdyacente = adyacente;
                        nuevo.VerticeAntecesor = antecesor;

                        if (conexion1 != conexion2)
                        {

                            objeto.InsertarAristaDirigida(nuevo, antecesor, adyacente);
                            DibujarArista(nombre, peso, color, antecesor.PosX, antecesor.PosY, adyacente.PosX, adyacente.PosY);
                            objeto.MostarAristas(listBox2);


                        }
                        else
                        {
                            if (nuevo.dirigido == true)
                            {
                                objeto.InsertarAristaDirigida(nuevo, antecesor, adyacente);
                                DibujarArista(nombre, peso, color, antecesor.PosX, antecesor.PosY, adyacente.PosX, adyacente.PosY);
                                objeto.MostarAristas(listBox2);
                            }

                            ContadorA += 1;

                        }
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado uno de los vertices");
                    }
                    txtNomArista.Clear();
                    txtConex.Clear();
                    txtConexion.Clear();
            //    }   
            //    catch (Exception)
            //    {

            //        MessageBox.Show("Propiedades del arista invalidas");
            //    }
            //}
        }
        #endregion

        #region Inicializa el grafo
        private void InicializarGrafico()
        {
            bmp = new Bitmap(TabGrafos.Width, TabGrafos.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            graf = Graphics.FromImage(bmp);
            graf.Clear(Color.Black);
            graf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            TabGrafos.BackColor = Color.White;
            TabGrafos.BackgroundImage = bmp;
        }
        #endregion

        #region Metodo Dijkastra
        private void ButtonCaminoCorto_Click(object sender, EventArgs e)
        {
            //Grafos.Dijkastra camino = new Grafos.Dijkastra();
            listBox3.Items.Clear();
            labelCosto.Text = "";
            try
            {

                if (txtInicio.Text != "" && txtFin.Text != "")
                {
                    objeto.Dijkastr(txtInicio.Text, txtFin.Text, listBox3, labelCosto);
                   
                }
                else
                {
                    MessageBox.Show("Debes colocar como inicio y fin el nombre de los lugares de tu recorrido");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No exixte ese vertice o tu grafo esta vacio, intenta nuevamente");
            }
            txtInicio.Clear();
            txtFin.Clear();
            
            //listBox3.Items.Add(camino.Floyd(matrizPesos, nombreL));
        }
        #endregion

        private void ButtonFloyd_Click(object sender, EventArgs e)
        {
            //nombreL = txtNombreL.Text;
            //matrizPesos = new char[Convert.ToChar(txtInicio.Text),Convert.ToChar(txtFin.Text)];
            //Grafos.Floyd caminar = new Grafos.Floyd();
            //listBox3.Items.Clear();
            //try
            //{
            //    if (txtInicio.Text != "" && txtFin.Text != "" )
            //    {
            //        richTextBox1.Text =  caminar.AlgoritmoFloyd(matrizPesos,Convert.ToChar( nombreL));
            //    }
            //    else
            //    {
            //        MessageBox.Show("Debes colocar como inicio y fin el nombre de los lugares de tu recorrido");
            //    }
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("No exixte ese vertice o tu grafo esta vacio, intenta nuevamente");
            //}
        }

        #region OcultaBotones
        private void BurbujaAscendenteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            buttonBurbuja.Visible = true;
            buttonSeleccion.Visible = buttonInsercion.Visible = buttonShelll.Visible = buttonHeapsort.Visible = buttonQuicksort.Visible = false;
        }

        private void SeleccionAscendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonSeleccion.Visible = true;
            buttonBurbuja.Visible = buttonInsercion.Visible = buttonShelll.Visible = buttonHeapsort.Visible = buttonQuicksort.Visible = false;
        }
        private void InsercionDescendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonInsercion.Visible = true;
            buttonBurbuja.Visible = buttonSeleccion.Visible = buttonShelll.Visible = buttonHeapsort.Visible = buttonQuicksort.Visible = false;
        }

        private void ShellDescendenteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            buttonShelll.Visible = true;
            buttonBurbuja.Visible = buttonSeleccion.Visible = buttonInsercion.Visible = buttonHeapsort.Visible = buttonQuicksort.Visible = false;
        }

        private void QuicksortAscendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonQuicksort.Visible = true;
            buttonBurbuja.Visible = buttonSeleccion.Visible = buttonInsercion.Visible = buttonHeapsort.Visible = buttonShelll.Visible = false;
        }

        private void HeapsortDescendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonHeapsort.Visible = true;
            buttonBurbuja.Visible = buttonSeleccion.Visible = buttonInsercion.Visible = buttonQuicksort.Visible = buttonShelll.Visible = false;
        }
        private void DesordenadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonDesordenada.Visible = true;
            buttonOrdenada.Visible = buttonBinaria.Visible = buttonHash.Visible = false;
        }
        private void OrdenadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonOrdenada.Visible = true;
            buttonDesordenada.Visible = buttonBinaria.Visible = buttonHash.Visible = false;
        }
        private void BinariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonBinaria.Visible = true;
            buttonDesordenada.Visible = buttonOrdenada.Visible = buttonHash.Visible = false;
        }

       
        private void TabPage1_Paint(object sender, PaintEventArgs e)
        {
            arbol.MostarA(e, this.BackColor);
        }
        
       
        private void ArbolBalanceado_Paint(object sender, PaintEventArgs e)
        {
            arbolito.MostrarArbolo(e, this.BackColor);
        }

        private void HashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonHash.Visible = true;
            buttonDesordenada.Visible = buttonOrdenada.Visible = buttonBinaria.Visible = false;
        }
        #endregion
    }

}
