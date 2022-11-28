using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipTopMorrazH.OrdenamientoInterno
{
    public partial class Interno : Form
    {
        Arboles.Dibujar arbol = new Arboles.Dibujar();
        Arboles.Nodo dibujar = new Arboles.Nodo();
        Graphics nodos;


        // instancia de la clase para la busqueda Doble direccion hash
        Orden[] clientesHash = new Orden[100];
        //arreglo de registro
        Orden[] clientes;

        private int cantidadClientes;
        private int clientesAtendidos = 0;
        public string NombreCombo, Tipocom;

        //guarda un diccionario
        private Dictionary<string, (int, int)> comboSeleccionado;
        private string nombreCombo;

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
            nodos = CreateGraphics();
            arbol = new Arboles.Dibujar(nodos, Font);
            comboTradicional.SelectedIndex = comboEspecial.SelectedIndex = comboPostres.SelectedIndex = comboEspecial.SelectedIndex = comboTipo.SelectedIndex = comboExtras.SelectedIndex = 0 ;
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

                    if (!arbol.Insertar(cliente.Total))
                    {
                        MessageBox.Show("Su valor esta duplicado, ingrese nuevamente");
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

                        //recorridos
                        labelPreOrden.Text = labelInOrden.Text = labelPosOrden.Text = "";
                        arbol.In_Orden(arbol.raiz, labelInOrden);
                        arbol.preOrden(arbol.raiz, labelPreOrden);
                        arbol.PosOrden(arbol.raiz, labelPosOrden);

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

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TabPage1_Paint(object sender, PaintEventArgs e)
        {
            arbol.MostarA(e, this.BackColor);
        }

        private void HashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonHash.Visible = true;
            buttonDesordenada.Visible = buttonOrdenada.Visible = buttonBinaria.Visible = false;
        }
        #endregion
    }

}
