using HomeControl.AccessControl.Domain.Seedwork;

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
