using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servicios.Models;
using Servicios.Entidad;
using System.Security.Cryptography;
using System.Text;

namespace Servicios.Logica
{
    public class LCliente
    {

        public List<ECliente> Listar()
        {

            try
            {
                using (var db = new dbEvaluacionEntities())
                {

                    List<ECliente> listeclientes = new List<ECliente>();

                    var clientes = (from x in db.Cliente
                                    where x.Estado == (int)EstadoCliente.Activo
                                          select x).ToList();

                    foreach (var i in clientes)
                    {
                        ECliente ecliente = new ECliente();
                        ecliente.IdCliente = i.IdCliente;
                        ecliente.Nombre = i.Nombre;
                        ecliente.Apellido = i.Apellido;
                        ecliente.Correo = i.Correo;
                        ecliente.Edad = i.Edad;
                        ecliente.DocumentoIdentidad = i.DocumentoIdentidad;
                        ecliente.Nacionalidad = i.Nacionalidad;
                       

                        listeclientes.Add(ecliente);
                    }

                    return listeclientes;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string Registro(EClientePost ecliente)
        {

            try
            {
                using (var db = new dbEvaluacionEntities())
                {

                    Validar(ecliente);
                    ValidarCorreo(ecliente.Correo);

                    Cliente cliente = new Cliente();
                    cliente.Nombre = ecliente.Nombre;
                    cliente.Apellido = ecliente.Apellido;
                    cliente.Correo = ecliente.Correo;
                    cliente.DocumentoIdentidad = ecliente.DocumentoIdentidad;
                    cliente.Edad = ecliente.Edad;
                    cliente.Estado = (int)EstadoCliente.Activo;
                    cliente.Contrasena = MD5Hash(ecliente.Contrasena);
                    cliente.Nacionalidad = ecliente.Nacionalidad;

                    db.Cliente.Add(cliente);
                    db.SaveChanges();

                    return "Registro Exitoso";

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Modificar(EClientePost ecliente)
        {

            try
            {
                using (var db = new dbEvaluacionEntities())
                {

                    Validar(ecliente);
                    ValidarCorreo(ecliente.Correo);

                    var cliente = (from x in db.Cliente
                                   where x.IdCliente == ecliente.IdCliente
                                   && x.Estado == (int)EstadoCliente.Activo
                                   select x).FirstOrDefault();

                    if(cliente != null)
                    {
                        cliente.Nombre = ecliente.Nombre;
                        cliente.Apellido = ecliente.Apellido;
                        cliente.Correo = ecliente.Correo;
                        cliente.DocumentoIdentidad = ecliente.DocumentoIdentidad;
                        cliente.Edad = ecliente.Edad;
                        cliente.Estado = (int)EstadoCliente.Activo;
                        cliente.Contrasena = MD5Hash(ecliente.Contrasena);
                        cliente.Nacionalidad = ecliente.Nacionalidad;
                        db.SaveChanges();

                        return "Modificacion Exitoso";

                    }
                    else
                    {
                        throw new Exception("No se puedo obtener el cliente");
                    }

                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Eliminar(EClientePost ecliente)
        {

            try
            {
                using (var db = new dbEvaluacionEntities())
                {


                    var cliente = (from x in db.Cliente
                                   where x.IdCliente == ecliente.IdCliente
                                   && x.Estado == (int)EstadoCliente.Activo
                                   select x).FirstOrDefault();

                    if (cliente != null)
                    {
                        cliente.Estado = (int)EstadoCliente.NoActivo;
                        db.SaveChanges();

                        return "Eliminacion Exitoso";

                    }
                    else
                    {
                        throw new Exception("No se puedo obtener el cliente");
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string Login(EClientePost ecliente)
        {

            try
            {
                using (var db = new dbEvaluacionEntities())
                {


                    string contrasena = MD5Hash(ecliente.Contrasena);

                    var cliente = (from x in db.Cliente
                                   where x.Correo.ToUpper().Equals(ecliente.Correo.ToUpper())
                                   && x.Contrasena.Equals(contrasena)
                                   && x.Estado == (int)EstadoCliente.Activo
                                   select x).FirstOrDefault();

                    if (cliente != null)
                    {
                        

                        return "Login Exitoso";

                    }
                    else
                    {
                        throw new Exception("El correo o la contraseña son incorrectos");
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Validar (EClientePost ecliente)
        {


            if (string.IsNullOrEmpty(ecliente.Nombre))
            {
                throw new Exception("El nombre es obligatorio");
            }

            if (string.IsNullOrEmpty(ecliente.Apellido))
            {
                throw new Exception("El apellido es obligatorio");
            }

            if (string.IsNullOrEmpty(ecliente.Correo))
            {
                throw new Exception("El correo es obligatorio");
            }

            if (string.IsNullOrEmpty(ecliente.DocumentoIdentidad))
            {
                throw new Exception("El correo es obligatorio");
            }

            if (string.IsNullOrEmpty(ecliente.Contrasena))
            {
                throw new Exception("La contraseña es obligatoria");
            }

            if (string.IsNullOrEmpty(ecliente.ConfirmarContrasena))
            {
                throw new Exception("La confirmacion de contraseña es obligatoria");
            }

            if (ecliente.Edad <= 0)
            {
                throw new Exception("La edad no es valida");
            }

         

            if (ecliente.Contrasena != ecliente.ConfirmarContrasena)
            {
                throw new Exception("Las contraseñas no son iguales");
            }



        }


        public static string MD5Hash(string entrada)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(entrada));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


        public void ValidarCorreo(string correo)
        {
            using (var db = new dbEvaluacionEntities())
            {



                var cliente = (from x in db.Cliente
                                  where x.Correo.ToUpper().Equals(correo.ToUpper())
                                  select x).FirstOrDefault();

                if (cliente != null)
                {
                    throw new Exception("El correo ya existe");
                }
               

            }
        }

    }
}