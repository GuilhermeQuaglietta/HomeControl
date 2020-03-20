using HomeControl.AccessControl.Domain.Seedwork;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.UnitTest.Seedwork;
using HomeControl.AccessControl.WebApi.Controllers;
using HomeControl.AccessControl.WebApi.Requests.Login;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace HomeControl.AccessControl.UnitTest.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {

        [TestMethod]
        public void LoginController_Login_InvalidModelState_ReturnUnprocessableEntityObjectResult()
        {
            LoginController controller = GenerateController();
            LoginRequest request = GenerateValidLoginRequest();
            controller.ModelState.AddModelError("Email", "Email must not be null");

            var result = controller.Login(request);
            Assert.IsInstanceOfType(result, typeof(UnprocessableEntityObjectResult));
        }
        [TestMethod]
        public void LoginController_Login_UserFound_RetornOkObjectResult()
        {
            LoginController controller = GenerateController();
            LoginRequest request = GenerateValidLoginRequest();

            var result = controller.Login(request);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public void LoginController_Login_UserNotFound_ReturnUnauthorizedResult()
        {
            LoginController controller = GenerateController(null);
            LoginRequest request = GenerateValidLoginRequest();

            var result = controller.Login(request);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void LoginController_SendRecoveryMail_InvalidModelState_ReturnUnprocessableEntityObjectResult()
        {
            LoginController controller = GenerateController();
            controller.ModelState.AddModelError("email", "Email must not be null");

            var result = controller.SendRecoveryEmail(null);
            Assert.IsInstanceOfType(result, typeof(UnprocessableEntityObjectResult));
        }
        [TestMethod]
        public void LoginController_SendRecoveryMail_WithPreviousRecoveryKeyNotGenerated_ReturnOkResult()
        {
            var queriesUser = EntityHelper.GenerateUser();
            queriesUser.RecoveryExpiration = null;
            LoginController controller = GenerateController(queriesUser);

            var result = controller.SendRecoveryEmail(TestConstants.Email);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        [TestMethod]
        public void LoginController_SendRecoveryMail_WithPreviousRecoveryKeyNotExpired_ReturnOkResult()
        {
            var queriesUser = EntityHelper.GenerateUser();
            queriesUser.RecoveryExpiration = DateTime.Now.AddDays(1);
            LoginController controller = GenerateController(queriesUser);

            var result = controller.SendRecoveryEmail(TestConstants.Email);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        [TestMethod]
        public void LoginController_SendRecoveryMail_WithPreviousRecoveryKeyExpired_ReturnOkResult()
        {
            var queriesUser = EntityHelper.GenerateUser();
            queriesUser.RecoveryExpiration = DateTime.Now.AddDays(-11);
            LoginController controller = GenerateController(queriesUser);

            var result = controller.SendRecoveryEmail(TestConstants.Email);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        [TestMethod]
        public void LoginController_SendRecoveryMail_UserNotFound_ReturnOkResult()
        {
            LoginController controller = GenerateController(null);
            var result = controller.SendRecoveryEmail(TestConstants.Email);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void LoginController_ValidateRecoveryKey_FoundUserRelatedToKey_RetornOkResult()
        {
            LoginController controller = GenerateController();
            var result = controller.ValidateRecoveryKey(TestConstants.RecoveryKey);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        [TestMethod]
        public void LoginController_ValidateRecoveryKey_InvalidModelState_ReturnUnprocessableEntityObjectResult()
        {
            LoginController controller = GenerateController();
            controller.ModelState.AddModelError("recoveryKey", "Email must not be null");

            var result = controller.ValidateRecoveryKey(TestConstants.RecoveryKey);
            Assert.IsInstanceOfType(result, typeof(UnprocessableEntityObjectResult));
        }
        [TestMethod]
        public void LoginController_ValidateRecoveryKey_UserNotFound_ReturnGoneResult()
        {
            LoginController controller = GenerateController(null);
            var result = controller.ValidateRecoveryKey(TestConstants.RecoveryKey);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.IsTrue(((StatusCodeResult)result).StatusCode == (int)HttpStatusCode.Gone);
        }

        [TestMethod]
        public void LoginController_ChangePassword_ValidRequest_ReturnOkResult()
        {
            var request = GenerateRecoveryPasswordChangeRequest();
            LoginController controller = GenerateController();
            var result = (StatusCodeResult)controller.ChangePassword(request);

            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void LoginController_ChangePassword_InvalidModelState_ReturnBadRequestResult()
        {
            var request = GenerateRecoveryPasswordChangeRequest();
            LoginController controller = GenerateController();
            controller.ModelState.AddModelError("NewPassword", "NewPassword must not be null");

            var result = controller.ChangePassword(request);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public void LoginController_ChangePassword_NoAccountFoundForRecoveryKey_ReturnGoneResult()
        {
            var request = GenerateRecoveryPasswordChangeRequest();
            LoginController controller = GenerateController(null);
            var result = (StatusCodeResult)controller.ChangePassword(request);
            Assert.AreEqual((int)HttpStatusCode.Gone, result.StatusCode);
        }

        private LoginRequest GenerateValidLoginRequest()
        {
            return new LoginRequest()
            {
                Email = TestConstants.Email,
                Password = TestConstants.Password
            };
        }
        private RecoveryPasswordChangeRequest GenerateRecoveryPasswordChangeRequest()
        {
            return new RecoveryPasswordChangeRequest()
            {
                NewPassword = TestConstants.Password,
                NewPasswordConfirmation = TestConstants.Password,
                Recoverykey = TestConstants.RecoveryKey
            };
        }
        private LoginController GenerateController()
        {
            var testUser = EntityHelper.GenerateUser();
            return GenerateController(testUser);
        }
        private LoginController GenerateController(User queriesUser)
        {
            var testUser = EntityHelper.GenerateUser();
            IUserQueries queries = MockHelper.GenerateIUserQueries(queriesUser);
            IUserRepository repository = MockHelper.GenerateIUserRepositoryMock(testUser).Object;
            IJwtHandler jwtHandler = JwtHelper.GenerateIJwtHandler();
            IJwtConfiguration jwtConfiguration = JwtHelper.GenerateIJwtConfiguration();
            LoginSettings settings = SettingsHelper.GenerateLoginSettings();

            LoginController controller = new LoginController(queries, repository, jwtHandler, jwtConfiguration, settings);
            return controller;
        }
    }
}
