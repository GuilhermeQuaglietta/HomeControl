using HomeControl.Identity.Jwt;
using HomeControl.Identity.UnitTest.Seedwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HomeControl.Identity.UnitTest.Jwt
{
    [TestClass]
    public class TokenGeneratorTest
    {
        private IJwtConfiguration _configuration;
        private IJwtHandler _handler;

        [TestInitialize]
        public void OnTestBegin()
        {
            _configuration = _generateConfiguration();
            _handler = new JwtHandler();
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_ValidConfiguration_VerifyTokenWithSuccess()
        {
            var tokenString = _handler.GenerateToken(_configuration, TestConstants.ValidId, TestConstants.ValidUserName, TestConstants.ValidEmail);
            var validationResult = _handler.VerifyToken(_configuration, tokenString);

            AssertValidationResultIsValid(validationResult);
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_MalFormedSecurityKey_ValidationResultFalse()
        {
            var tokenString = _handler.GenerateToken(_configuration, TestConstants.ValidId, TestConstants.ValidUserName, TestConstants.ValidEmail);
            _configuration.SecretKey = "Invalid";
            var validationResult = _handler.VerifyToken(_configuration, tokenString);

            AssertValidationResultIsInvalid(validationResult, JwtValidationResultCode.MalFormedSecurityKey, typeof(FormatException));
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_InvalidTokenConfiguration_Audience_ValidationResultFalse()
        {
            var tokenString = _handler.GenerateToken(_configuration, TestConstants.ValidId, TestConstants.ValidUserName, TestConstants.ValidEmail);
            _configuration.Audience = "Invalid";
            var validationResult = _handler.VerifyToken(_configuration, tokenString);

            AssertValidationResultIsInvalid(validationResult, JwtValidationResultCode.InvalidToken, typeof(Exception));
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_InvalidTokenConfiguration_Issuer_ValidationResultFalse()
        {
            var tokenString = _handler.GenerateToken(_configuration, TestConstants.ValidId, TestConstants.ValidUserName, TestConstants.ValidEmail);
            _configuration.Issuer = "Invalid";
            var validationResult = _handler.VerifyToken(_configuration, tokenString);

            AssertValidationResultIsInvalid(validationResult, JwtValidationResultCode.InvalidToken, typeof(Exception));
        }

        private JwtConfiguration _generateConfiguration()
        {
            return new JwtConfiguration()
            {
                Audience = "HomeControlServices",
                Issuer = "HomeControl",
                HoursToExpire = 3600,
                SecretKey = "c711e5080f2b58260fe19741a7913e8301c1128ec8e80b8009406e5047e6e1ef",
            };
        }

        public void AssertValidationResultIsValid(JwtValidationResult result)
        {
            Assert.IsTrue(result.IsValid);
            Assert.IsNotNull(result.Identity);
            Assert.AreEqual(JwtValidationResultCode.Ok, result.ValidationResultCode);
            Assert.IsNull(result.Exception);
        }
        public void AssertValidationResultIsInvalid(JwtValidationResult result, JwtValidationResultCode code, Type expectedException)
        {
            Assert.IsFalse(result.IsValid);
            Assert.IsNull(result.Identity);
            Assert.AreEqual(code, result.ValidationResultCode);
            Assert.IsInstanceOfType(result.Exception, expectedException);
        }
    }
}
