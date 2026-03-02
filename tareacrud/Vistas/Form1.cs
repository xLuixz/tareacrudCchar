using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tareacrud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Tabla con datos
            RefrescarTabla();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Guarda
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Crea nuevo cotenedor (DTO) para meter datos
            tareacrud.Modelos.Persona p = new tareacrud.Modelos.Persona();
            //Guardar lo que hay en el cuadro de texto
            p.Nombre = txtNombre.Text;
            p.Apellido = txtApellido.Text;
            p.Telefono = txtTelefono.Text;
            p.Nacionalidad = txtNacionalidad.Text;
            //Convierte la palabra en un caracter, porque temenos CHAR(1)
            if (cmbGenero.Text == "Masculino") p.Genero = 'M';
            else if (cmbGenero.Text == "Femenino") p.Genero = 'F';
            else p.Genero = 'O';

            // Instancia el controlador para guardar la persona
            tareacrud.Controladores.PersonaControlador control = new tareacrud.Controladores.PersonaControlador();
            //Guarda los datos
            if (control.Guardar(p))
            {
                MessageBox.Show("Guardado éxitoso!!!");
                Limpiar();
                RefrescarTabla();
            }
            else
            {
                MessageBox.Show("ERROR!!!");
            }
        }

        //Limpia
        private void Limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtNacionalidad.Clear();
            cmbGenero.SelectedIndex = -1;
            lblId.Text = "0";
        }

        private void RefrescarTabla()
        {
            tareacrud.Controladores.PersonaControlador control = new tareacrud.Controladores.PersonaControlador();
            TablaPersona.DataSource = control.ListarTodo();

            TablaPersona.Columns["id"].HeaderText = "ID";
            TablaPersona.Columns["nombre"].HeaderText = "Nombres";
            TablaPersona.Columns["apellido"].HeaderText = "Apellidos";
            TablaPersona.Columns["telefono"].HeaderText = "Teléfono";
            TablaPersona.Columns["nacionalidad"].HeaderText = "País";
            TablaPersona.Columns["genero"].HeaderText = "Género";
        }

        //Se activa al hacer click en cualquier celda
        private void TablaPersona_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Obtiene la fila completa seleccionada
                DataGridViewRow fila = TablaPersona.Rows[e.RowIndex];
                //Pasa los datos al formulario
                lblId.Text = fila.Cells["id"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["apellido"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtNacionalidad.Text = fila.Cells["nacionalidad"].Value.ToString();
                //Convierte el caracter a esas cadenas de texto
                string gen = fila.Cells["genero"].Value.ToString();
                cmbGenero.Text = (gen == "M") ? "Masculino" : (gen == "F") ? "Femenino" : "Otro";
            }
        }

        //Editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //Instanciamos para empaquetar los nuevos datos
            tareacrud.Modelos.Persona p = new tareacrud.Modelos.Persona();
            p.Id = int.Parse(lblId.Text);
            p.Nombre = txtNombre.Text;
            p.Apellido = txtApellido.Text;
            p.Telefono = txtTelefono.Text;
            p.Nacionalidad = txtNacionalidad.Text;
            p.Genero = (cmbGenero.Text == "Masculino") ? 'M' : (cmbGenero.Text == "Femenino") ? 'F' : 'O';
            //Instanciamos el controlador para ejecutar la actualización
            tareacrud.Controladores.PersonaControlador control = new tareacrud.Controladores.PersonaControlador();
            if (control.Editar(p))
            {
                MessageBox.Show("Actualizado!!!");
                Limpiar();
                RefrescarTabla();
            }
        }

        //Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(lblId.Text);
            var respuesta = MessageBox.Show("¿Quieres eliminarlo?", "Confirmar", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                tareacrud.Controladores.PersonaControlador control = new tareacrud.Controladores.PersonaControlador();
                if (control.Eliminar(id))
                {
                    RefrescarTabla();
                    lblId.Text = "0";
                }
            }
        }

        //Solo numeros
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
