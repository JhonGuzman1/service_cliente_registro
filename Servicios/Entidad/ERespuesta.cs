using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Servicios.Entidad
{
    [DataContract]
    public class ERespuesta<T>
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public T Respuesta { get; set; }

       
        public static ERespuesta<T> RespuestaError(int codigo, string mensaje)
        {
            ERespuesta<T> respuesta = new ERespuesta<T>();
            respuesta.Codigo = codigo;
            respuesta.Mensaje = mensaje;
            return respuesta;
        }

        
        public static ERespuesta<T> RespuestaExitosa(string mensaje, T respuesta)
        {
            ERespuesta<T> erespuesta = new ERespuesta<T>();
            erespuesta.Codigo = 0;
            erespuesta.Mensaje = mensaje;
            erespuesta.Respuesta = respuesta;
            return erespuesta;
        }

        
    }
}