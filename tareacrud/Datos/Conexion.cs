//Gestiona la conexion con la BD, usa singleton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace tareacrud.Datos
{
    public class Conexion
    {
        //Esta variable guarda la conexion unica
        private static MySqlConnection _conexion = null;
        //Define servidor, base de datos y creedenciales
        private static string cadena = "Server=localhost; Database=prueba; Uid=root; Pwd=root;";
        //Impide que hagan new Conexion, obligando usar el metodo oficial
        private Conexion() { }
        //Objeto que contiene la conexion, si no existe conexion la crea y si existe la reutiliza
        public static MySqlConnection GetInstancia()
        {
            if (_conexion == null)
            {
                _conexion = new MySqlConnection(cadena);
            }
            return _conexion;
        }
    }
}
