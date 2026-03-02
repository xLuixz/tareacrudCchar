//Controlador y usa patron de repositorio, centraliza toda la logica
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using tareacrud.Modelos;
using tareacrud.Datos;
using System.Data;

namespace tareacrud.Controladores
{
    public class PersonaControlador
    {
        //Recibe el objeto con datos, respuesta v o f
        public bool Guardar(Persona p)
        {
            try
            {
                //Creacion de comando, usa el procedimiento almacenado, y obtiene la conexion con el singleton
                MySqlCommand cmd = new MySqlCommand("sp_GuardarPersona", Conexion.GetInstancia());
                //Avisa que es un procedimiento
                cmd.CommandType = CommandType.StoredProcedure;
                //Vinculamos datos, los parametros con "_" estan en la BD con las del objeto Persona
                cmd.Parameters.AddWithValue("_nom", p.Nombre);
                cmd.Parameters.AddWithValue("_ape", p.Apellido);
                cmd.Parameters.AddWithValue("_tel", p.Telefono);
                cmd.Parameters.AddWithValue("_nac", p.Nacionalidad);
                cmd.Parameters.AddWithValue("_gen", p.Genero);
                //Abre conexion
                Conexion.GetInstancia().Open();
                //Orden de ejecucion
                cmd.ExecuteNonQuery();
                return true;
            }catch (Exception)
            {
                return false;
            }
            finally
            {
                //Pregunta si esta abierta, para cerrarlo
                if (Conexion.GetInstancia().State == ConnectionState.Open)
                {
                    Conexion.GetInstancia().Close();
                }
            }
        }

        //Trae informacion para mostrar a la tabla
        public DataTable ListarTodo()
        {
            //Crea la tabla
            DataTable tabla = new DataTable();
            try
            {
                //Recoge informacion y lo trae + Consulta SQL + singleton
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM personas", Conexion.GetInstancia());
                adapter.Fill(tabla);
            }
            //Tabla vacia
            catch (Exception){ }
            return tabla;
        }

        //Lo mismo que guardar, pero pa editar
        public bool Editar(Persona p)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("sp_EditarPersona", Conexion.GetInstancia());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", p.Id);
                cmd.Parameters.AddWithValue("_nom", p.Nombre);
                cmd.Parameters.AddWithValue("_ape", p.Apellido);
                cmd.Parameters.AddWithValue("_tel", p.Telefono);
                cmd.Parameters.AddWithValue("_nac", p.Nacionalidad);
                cmd.Parameters.AddWithValue("_gen", p.Genero);
                Conexion.GetInstancia().Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {  return false; }
            finally { Conexion.GetInstancia().Close(); }
        }

        //Recibe la ID
        public bool Eliminar(int id)
        {
            try
            {
                //Llama al procedimiento de la base de datos + singleton
                MySqlCommand cmd = new MySqlCommand("sp_EliminarPersona", Conexion.GetInstancia());
                cmd.CommandType = CommandType.StoredProcedure;
                //Pasamos la Id
                cmd.Parameters.AddWithValue("_id", id);
                //Abre el singleton
                Conexion.GetInstancia().Open();
                //Accion
                cmd.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
            finally { Conexion.GetInstancia().Close(); }
        }
    }
}
