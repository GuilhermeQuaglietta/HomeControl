using HomeControl.AccessControl.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.AccessControl.UnitTest.Seedwork
{
    public static class SettingsHelper
    {
        public static LoginSettings GenerateLoginSettings()
        {
            return new LoginSettings()
            {
                RecoverExpirationSeconds = TestConstants.RecoverExpirationSeconds
            };
        }
    }
}
