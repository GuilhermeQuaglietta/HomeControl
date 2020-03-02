using HomeControl.AccessControl.Domain.Users;
using HomeControl.Identity.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

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
