using HomeControl.Core.Infrastructure.Contract;
using System;

namespace HomeControl.AccessControl.Domain.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string RecoveryKey { get; set; }
        public DateTime? RecoveryExpiration { get; set; }
        public string RecoveryAnswer { get; set; }

        public bool RecoveryKeyExpired()
        {
            return DateTime.Now > RecoveryExpiration || RecoveryExpiration == null;
        }
    }
}
