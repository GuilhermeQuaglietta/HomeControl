using HomeControl.Identity.Jwt;
using Moq;

namespace HomeControl.AccessControl.UnitTest.Seedwork
{
    public static class JwtHelper
    {
        public static string GenerateJwtUserToken()
        {
            IJwtConfiguration configuration = GenerateIJwtConfiguration();
            IJwtHandler handler = new JwtHandler();
            return handler.GenerateToken(configuration, TestConstants.UserId, TestConstants.Name, TestConstants.Email);
        }
        public static JwtUser GenerateJwtUser()
        {
            IJwtConfiguration configuration = GenerateIJwtConfiguration();
            IJwtHandler handler = new JwtHandler();
            string jwtUser = handler.GenerateToken(configuration, TestConstants.UserId, TestConstants.Name, TestConstants.Email);
            return handler.VerifyToken(configuration, jwtUser).Identity;
        }
        public static IJwtHandler GenerateIJwtHandler()
        {
            string jwtUserToken = GenerateJwtUserToken();
            JwtUser jwtUser = GenerateJwtUser();
            return GenerateIJwtHandler(jwtUser, jwtUserToken);
        }
        public static IJwtHandler GenerateIJwtHandler(JwtUser user, string jwtUser)
        {
            Mock<IJwtHandler> mock = new Mock<IJwtHandler>();
            mock.Setup(x => x.GenerateToken(It.IsAny<IJwtConfiguration>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(jwtUser);
            mock.Setup(x => x.VerifyToken(It.IsAny<IJwtConfiguration>(), It.IsAny<string>())).Returns(new JwtValidationResult(user));
            return mock.Object;
        }
        public static IJwtConfiguration GenerateIJwtConfiguration()
        {
            return GenerateIJwtConfiguration(TestConstants.Audience, TestConstants.Issuer, TestConstants.SecretKey, TestConstants.HoursToExpire);
        }
        public static IJwtConfiguration GenerateIJwtConfiguration(string audience, string issuer, string secretKey, int hoursToExpire)
        {
            Mock<IJwtConfiguration> mock = new Mock<IJwtConfiguration>();
            mock.SetupGet(x => x.Audience).Returns(audience);
            mock.SetupGet(x => x.Issuer).Returns(issuer);
            mock.SetupGet(x => x.SecretKey).Returns(secretKey);
            mock.SetupGet(x => x.HoursToExpire).Returns(hoursToExpire);
            return mock.Object;
        }
    }
}
