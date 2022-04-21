using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Servicios.Entidad;
using Servicios.Logica;

namespace Servicios.Controllers
{
    public class ClienteController : ApiController
    {

        [Route("api/cliente")]
        [HttpGet]
        public IHttpActionResult ListarCliente()
        {
            try
            {
                LCliente lCliente = new LCliente();
                var respuesta = lCliente.Listar();
                return Ok(ERespuesta<List<ECliente>>.RespuestaExitosa("", respuesta));

            }
            catch (Exception ex)
            {
                return Ok(ERespuesta<string>.RespuestaError(1, ex.Message));
            }

        }

        [Route("api/cliente/registro")]
        [HttpPost]
        public IHttpActionResult RegistroCliente(EClientePost ecliente)
        {
            try
            {
                LCliente lCliente = new LCliente();
                var respuesta = lCliente.Registro(ecliente);
                return Ok(ERespuesta<string>.RespuestaExitosa("", respuesta));

            }
            catch (Exception ex)
            {
                return Ok(ERespuesta<string>.RespuestaError(1, ex.Message));
            }

        }

        [Route("api/cliente/modificar")]
        [HttpPost]
        public IHttpActionResult ModificarCliente(EClientePost ecliente)
        {
            try
            {
                LCliente lCliente = new LCliente();
                var respuesta = lCliente.Modificar(ecliente);
                return Ok(ERespuesta<string>.RespuestaExitosa("", respuesta));

            }
            catch (Exception ex)
            {
                return Ok(ERespuesta<string>.RespuestaError(1, ex.Message));
            }

        }

        [Route("api/cliente/eliminar")]
        [HttpPost]
        public IHttpActionResult EliminarCliente(EClientePost ecliente)
        {
            try
            {
                LCliente lCliente = new LCliente();
                var respuesta = lCliente.Eliminar(ecliente);
                return Ok(ERespuesta<string>.RespuestaExitosa("", respuesta));

            }
            catch (Exception ex)
            {
                return Ok(ERespuesta<string>.RespuestaError(1, ex.Message));
            }

        }

        [Route("api/cliente/login")]
        [HttpPost]
        public IHttpActionResult LoginCliente(EClientePost ecliente)
        {
            try
            {
                LCliente lCliente = new LCliente();
                var respuesta = lCliente.Login(ecliente);
                return Ok(ERespuesta<string>.RespuestaExitosa("", respuesta));

            }
            catch (Exception ex)
            {
                return Ok(ERespuesta<string>.RespuestaError(1, ex.Message));
            }

        }

    }
}
