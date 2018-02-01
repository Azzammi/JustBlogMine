using JustBlog.Models;
using JustBlog.Providers;

using System.Web.Mvc;
using System.Web.Security;

namespace JustBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        #region Declaration
        private readonly IAuthProvider _authProvider;
        #endregion

        #region Constructor
        public AdminController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }
        #endregion

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {          
            if(_authProvider.IsLoggedIn)
                return RedirectToUrl(returnUrl);                          
            
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && _authProvider.Login(model.UserName, model.Password))
            {
                return RedirectToUrl(returnUrl);          
            }

            ModelState.AddModelError("", "The username or password provided is incorrect.");
            return View(model);
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Admin");
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Manage");
            }
        }
        
    }
}