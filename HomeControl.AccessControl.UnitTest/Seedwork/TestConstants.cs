using System;

namespace HomeControl.AccessControl.UnitTest.Seedwork
{
    public static class TestConstants
    {
        //LibertyIdentity
        public const string Audience = "HomeControlSystems";
        public const string Issuer = "HomeControl";
        public const int HoursToExpire = 5;
        public const string SecretKey = "c711e5080f2b58260fe19741a7913e8301c1128ec8e80b8009406e5047e6e1ef";

        public const int RecoverExpirationSeconds = 600;

        //User
        public const string Email = "test@test.com";
        public const string Name = "Test of Test";
        public const string Password = "testpassword";
        public const string RecoveryAnswer = "testrecoveryanswer";
        public static DateTime RecoveryExpiration = DateTime.Now.AddMinutes(15);
        public const string RecoveryKey = "testrecoverykey";
        public const int UserId = 1;
    }
}
