using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        public JobseekerFacade JobseekerFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterJobseeker(JobseekerRegistrationDTO userRegistrationDTO)
        {
            try
            {
                await JobseekerFacade.RegisterJobSeeker(userRegistrationDTO);

                var authTicket = new FormsAuthenticationTicket(1, userRegistrationDTO.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> RegisterJobseeker(CompanyRegistrationDTO userRegistrationDTO)
        {
            try
            {
                await CompanyFacade.RegisterCompany(userRegistrationDTO);

                var authTicket = new FormsAuthenticationTicket(1, userRegistrationDTO.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {

            (bool success, string roles) = await JobseekerFacade.Login(model.Username, model.Password);
            if (success)
            {

                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    decodedUrl = Server.UrlDecode(returnUrl);
                }

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            var user = await JobseekerFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}