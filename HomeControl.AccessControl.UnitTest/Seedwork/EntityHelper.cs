using HomeControl.AccessControl.Domain.Users;

namespace HomeControl.AccessControl.UnitTest.Seedwork
{
    public static class EntityHelper
    {
        public static User GenerateUser()
        {
            return new User
            {
                Email = TestConstants.Email,
                Name = TestConstants.Name,
                Password = TestConstants.Password,
                RecoveryAnswer = TestConstants.RecoveryAnswer,
                RecoveryExpiration = TestConstants.RecoveryExpiration,
                RecoveryKey = TestConstants.RecoveryKey,
                UserId = TestConstants.UserId
            };
        }

    }
}
