using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Entidad
{
    public class ECliente
    {

       public long IdCliente { get; set; }
       public string Nombre { get; set; }
       public string Apellido { get; set; }
       public int Edad { get; set; }
       public string Correo { get; set; }
       public string DocumentoIdentidad { get; set; }
       public string  Nacionalidad { get; set; } 

    }

    public class EClientePost
    {
        public long IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Nacionalidad { get; set; }

    }

    public enum EstadoCliente: int
    {

        Activo = 1,
        NoActivo = 0 

    }

}