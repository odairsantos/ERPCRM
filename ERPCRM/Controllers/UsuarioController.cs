using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ERPCRM.Models;

namespace ERPCRM.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario/Login
        public ActionResult LogIn()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(Usuario objUsuarioForm)
        {
            var User_Store = new UserStore<IdentityUser>();
            var User_Manager = new UserManager<IdentityUser>(User_Store);
            var User = User_Manager.Find(objUsuarioForm.UserName, objUsuarioForm.Password);

            if (User != null)
            {
                var Auth_Manager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var User_Identity = User_Manager.CreateIdentity(User, DefaultAuthenticationTypes.ApplicationCookie);
                
                Auth_Manager.SignIn(new AuthenticationProperties() { IsPersistent = false }, User_Identity);
                return RedirectToAction("Index", "Home", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível fazer o login do usuário.");
                return View(objUsuarioForm);
            }
            
        }

        // GET: Usuario/LogOut
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            var Auth_Manager = System.Web.HttpContext.Current.GetOwinContext().Authentication;

            Auth_Manager.SignOut();

            return RedirectToAction("Index", "Home", "Home");
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Novo(Usuario objUsuarioForm)
        {
            var User_Store = new UserStore<IdentityUser>();
            var User_Manager = new UserManager<IdentityUser>(User_Store);
            var New_User = new IdentityUser() { UserName = objUsuarioForm.UserName };

            IdentityResult User_Result = User_Manager.Create(New_User, objUsuarioForm.Password);
            
            if (User_Result.Succeeded)
            {
                var Auth_Manager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                
                var User_Identity = User_Manager.CreateIdentity(New_User, DefaultAuthenticationTypes.ApplicationCookie);

                Auth_Manager.SignIn(new AuthenticationProperties() { }, User_Identity);

                return RedirectToAction("Index", "Home");
            }
            else{
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível cadastrar o novo usuário.");
                return View(objUsuarioForm);
            }
        }
    }
}