using HomeControl.Identity.Jwt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HomeControl.Identity.UnitTest.Jwt
{
    [TestClass]
    public class TokenGeneratorTest
    {

        [TestMethod]
        public void TokenGenerator_GenerateToken_ValidConfiguration_VerifyTokenWithSuccess()
        {

            var tokenConfiguration = _generateConfiguration();
            var generator = new JwtSymmetricHandler();

            var tokenString = generator.GenerateToken(tokenConfiguration, "Guilherme");
            var validationResult = generator.VerifyToken(tokenConfiguration, tokenString);

            Assert.IsTrue(validationResult.IsValid);
            Assert.IsNotNull(validationResult.Identity);
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_InvalidAudience_VerifyTokenWithFailure()
        {
            var tokenConfiguration = _generateConfiguration();
            var generator = new JwtSymmetricHandler();
            var jwt = generator.GenerateToken(tokenConfiguration, "Guilherme");

            tokenConfiguration.Audience = "Invalid";
            var validationResult = generator.VerifyToken(tokenConfiguration, jwt);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResult.ErrorMessage));
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_InvalidKey_VerifyTokenWithFailure()
        {
            var tokenConfiguration = _generateConfiguration();
            var generator = new JwtSymmetricHandler();
            var jwt = generator.GenerateToken(tokenConfiguration, "Guilherme");

            byte[] byt = System.Text.Encoding.UTF8.GetBytes("Invalid");
            tokenConfiguration.SecretKey = Convert.ToBase64String(byt);
            var validationResult = generator.VerifyToken(tokenConfiguration, jwt);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResult.ErrorMessage));
        }

        [TestMethod]
        public void TokenGenerator_GenerateToken_InvalidIssuer_VerifyTokenWithFailure()
        {
            var tokenConfiguration = _generateConfiguration();
            var generator = new JwtSymmetricHandler();
            var jwt = generator.GenerateToken(tokenConfiguration, "Guilherme");
            tokenConfiguration.Issuer = "Invalid";
            var validationResult = generator.VerifyToken(tokenConfiguration, jwt);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResult.ErrorMessage));
        }


        private JwtConfiguration _generateConfiguration()
        {
            return new JwtConfiguration()
            {
                Audience = "HomeControlServices",
                Issuer = "HomeControl",
                MinutesToExpire = 3600,
                SecretKey = "c711e5080f2b58260fe19741a7913e8301c1128ec8e80b8009406e5047e6e1ef",
            };
        }
    }
}
