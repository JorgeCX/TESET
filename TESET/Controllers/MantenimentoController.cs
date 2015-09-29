using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TESET.BD;
using TESET.Models;

namespace TESET.Controllers
{
    [RoutePrefix("api/Manteniemto")]
    public class MantenimentoController : ApiController
    {
        [Route("getManteniento")]
        [HttpGet]
        public IHttpActionResult getManteniento()
        {
            dbManteniemnto db = new dbManteniemnto();

            var list = db.solicitudmantenimiento.Select(s => new { s.idSolicitud, dep = s.areas.Nombre, s.tiposolicitud.Tipo, s.FechaElaboracion,s.status }).ToList();

            return Ok(list);

        } 
        #region Gestor de Usuarios.


        dbManteniemnto db = new dbManteniemnto();

        [AcceptVerbs("GET")]
        [Route("getUsuarios")]
        ////[Authorize(Roles = "Administrador,Desarrollo")]
        public IHttpActionResult getUsuarios()
        {
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            var data = (from C in db.Users

                        select new
                        {
                            id = C.Id
                            ,
                            UserName = C.UserName
                            ,
                            Email = C.Email
                            ,
                            PhoneNumber = C.PhoneNumber
                            ,
                            Perfil = ""
                            ,
                            LockoutEnabled = C.LockoutEnabled
                        });

            return Ok(data);
        }

        [AcceptVerbs("POST")]
        [Route("AltaUser")]
        ////[Authorize(Roles = "Administrador,Desarrollo")]
        public HttpResponseMessage AltaUser(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if (usuario.Password != usuario.ConfirmPassword)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                Models.ApplicationDbContext context = new Models.ApplicationDbContext();
                AltaUsuario(context, new Models.ApplicationUser() { Email = usuario.Email, UserName = usuario.UserName }, usuario.Password, usuario.Perfil);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message, Encoding.Unicode);
                return response;
            }
            HttpResponseMessage okresponse = Request.CreateResponse(HttpStatusCode.OK);
            okresponse.Content = new StringContent("Usuario Registrado: " + usuario.UserName, Encoding.Unicode);
            return okresponse;
        }
        private bool AltaUsuario(Models.ApplicationDbContext context, Models.ApplicationUser User, string contras, string rool)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            usermanager.Create(User, contras);
            usermanager.AddToRole(User.Id, rool);

            return true;
        }


        [AcceptVerbs("GET")]
        [Route("getRoles")]
        ////[Authorize(Roles = "Administrador,Desarrollo")]
        public List<System.Web.Mvc.SelectListItem> GetPerfiles()
        {
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            var Result = new List<System.Web.Mvc.SelectListItem>();
            var Grupo = new System.Web.Mvc.SelectListGroup() { Name = "Perfiles" };
            var ListaRoles = from U in db.Roles
                             select U;

            foreach (var Role in ListaRoles)
                Result.Add(new System.Web.Mvc.SelectListItem() { Group = Grupo, Text = Role.Name, Value = Role.Name });

            return Result;
        }

        [AcceptVerbs("POST")]
        [Route("modUser")]
        ////[Authorize(Roles = "Administrador,Desarrollo")]
        public HttpResponseMessage MUsuario(V_Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Models.ApplicationDbContext context = new Models.ApplicationDbContext();
            Models.ApplicationUser Usuario = context.Users.FirstOrDefault((u) => u.Id == usuario.id);
            if (Usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Usuario.LockoutEnabled = usuario.LockoutEnabled;
            Usuario.Email = usuario.Email;
            Usuario.PhoneNumber = usuario.PhoneNumber;
            Usuario.UserName = usuario.UserName;
            context.Entry(Usuario).State = EntityState.Modified;
            try
            {
                context.SaveChanges();
                CambiarRool(context, Usuario.Id, usuario.Perfil);
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message, Encoding.Unicode);
                return response;
            }
            HttpResponseMessage okresponse = Request.CreateResponse(HttpStatusCode.OK);
            okresponse.Content = new StringContent("Ha Sido Modificado el Usuario: " + usuario.UserName, Encoding.Unicode);
            return okresponse;
        }
        [AcceptVerbs("PUT")]
        [Route("modStatusUser")]
        //[Authorize(Roles = "Administrador,Desarrollo")]
        public HttpResponseMessage ADUsuario(V_Usuarios usuario)
        {
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var Usuario = db.Users.FirstOrDefault((u) => u.Id == usuario.id);
            if (Usuario == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent("Usuario Invalido", Encoding.Unicode);
                return response;
            }
            if (Usuario.LockoutEnabled)
                Usuario.LockoutEnabled = false;
            else
                Usuario.LockoutEnabled = true;
            db.Entry(Usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message, Encoding.Unicode);
                return response;
            }

            HttpResponseMessage response2 = Request.CreateResponse(HttpStatusCode.OK);
            if (Usuario.LockoutEnabled)
                response2.Content = new StringContent("EL Usuario: " + Usuario.UserName + " ha sido Desactivado Temporalmente", Encoding.Unicode);
            if (!Usuario.LockoutEnabled)
                response2.Content = new StringContent("EL Usuario: " + Usuario.UserName + " ha sido Activado", Encoding.Unicode);
            return response2;

        }
        [AcceptVerbs("Delete")]
        [Route("DeleteUser")]
        //[Authorize(Roles = "Administrador,Desarrollo")]
        public HttpResponseMessage DeleteUser(V_Usuarios usuario)
        {
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var Usuario = db.Users.FirstOrDefault((u) => u.Id == usuario.id);
            if (Usuario == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent("Usuario Invalido", Encoding.Unicode);
                return response;
            }

            db.Users.Remove(Usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message, Encoding.Unicode);
                return response;
            }

            HttpResponseMessage response2 = Request.CreateResponse(HttpStatusCode.OK);
            response2.Content = new StringContent("EL Usuario: " + Usuario.UserName + " ha sido Eliminado", Encoding.Unicode);
            return response2;

        }

        bool CambiarRool(Models.ApplicationDbContext context, string id, string role)
        {
            var UserManager = new UserManager<Models.ApplicationUser>(new UserStore<Models.ApplicationUser>(context));
            var resul = UserManager.GetRoles(id);
            if (resul.Count() == 0)
            {
                var rs = UserManager.AddToRole(id, role);
                return rs.Succeeded;
            }
            else
            {
                if (UserManager.RemoveFromRole(id, resul.First()).Succeeded)
                    return UserManager.AddToRole(id, role).Succeeded;
                else
                    return false;
            }
        }
        #endregion
    }
    #region Clases Auxiliares
    public class V_Usuarios
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool LockoutEnabled { get; set; }
        public string Perfil { get; set; }
    }
    public class Usuario
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }


        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Perfil")]
        public string Perfil { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
    public class Porcentajes
    {
        public decimal id { get; set; }
        public string location_name { get; set; }
        public string Validada { get; set; }
        public string Evidencia { get; set; }
        public string Planaccion { get; set; }

    }
    public class PlanAccionRequest
    {



        public decimal id { get; set; }
        public string fecha_compromiso { get; set; }
        public string desc_plan_accion { get; set; }
    }
    #endregion
}
