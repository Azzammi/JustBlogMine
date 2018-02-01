using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using JustBlog;
using JustBlog.Controllers;
using JustBlog.Providers;

using NUnit.Framework;
using Rhino.Mocks;
using System.Net.Http;
using JustBlog.Models;

namespace JustBlog.Test
{
    [TestFixture]
    public class AdminControllerTests
    {
        #region Declaration
        private AdminController _adminController;
        private IAuthProvider _authProvider;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _authProvider = MockRepository.GenerateMock<IAuthProvider>();
            _adminController = new AdminController(_authProvider);

            var httpContextMock = MockRepository.GenerateMock<HttpContextBase>();
            _adminController.Url = new UrlHelper(new RequestContext(httpContextMock, new RouteData()));
        }

        [Test]
        public void Login_IsLoggedIn_True_Test()
        {
            // Arrange
            _authProvider.Stub(s => s.IsLoggedIn).Return(true);

            // Act
            var actual = _adminController.Login("/admin/manage");

            // Assert
            Assert.IsInstanceOf<RedirectResult>(actual);
            Assert.AreEqual("/admin/manage", ((RedirectResult)actual).Url);
        }

        [Test]
        public void Login_IsLoggedIn_False_Test()
        {
            _authProvider.Stub(s => s.IsLoggedIn).Return(false);
            var actual = _adminController.Login("/");

            Assert.IsInstanceOf<ViewResult>(actual);
            Assert.AreEqual("/", ((ViewResult)actual).ViewBag.ReturnUrl);
        }

        [Test]
        public void Login_Post_Model_Invalid_Test()
        {
            var model = new LoginModel();
            _adminController.ModelState.AddModelError("UserName", "Username is required");
            var actual = _adminController.Login(model, "/");

            Assert.IsInstanceOf<ViewResult>(actual);
        }

        [Test]
        public void Login_Post_User_Invalid_Test()
        {
            var model = new LoginModel
            {
                UserName = "invaliduser",
                Password = "password"
            };
            _authProvider.Stub(s => s.Login(model.UserName, model.Password))
                .Return(false);

            var actual = _adminController.Login(model, "/");

            Assert.IsInstanceOf<ViewResult>(actual);
            var modelStateErrors = _adminController.ModelState[""].Errors;
            Assert.IsTrue(modelStateErrors.Count > 0);
            Assert.AreEqual("The username or password provided is incorrect.",
                modelStateErrors[0].ErrorMessage);
        }

        [Test]
        public void Login_Post_User_Valid_Test()
        {
            var model = new LoginModel
            {
                UserName = "invaliduser",
                Password = "password"
            };
            _authProvider.Stub(s => s.Login(model.UserName, model.Password))
                .Return(true);

            var actual = _adminController.Login(model, "/");

            Assert.IsInstanceOf<RedirectResult>(actual);
            Assert.AreEqual("/", ((RedirectResult)actual).Url);
        }
    }
}
