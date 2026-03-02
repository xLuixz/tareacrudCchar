//Usamos DTO (DATA TRANSFER OBJECT)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareacrud.Modelos
{
    //Contenedor, get y set para que los demas puedan leer y modiicar la variable
    public class Persona
    {
        //Llave primaria, tiene autoincremento en la BD
        public int Id { get; set; }

        //Cadena de texto
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }

        //CHAR porque solo tendra un caracter
        public char Genero { get; set; }
    }
}
