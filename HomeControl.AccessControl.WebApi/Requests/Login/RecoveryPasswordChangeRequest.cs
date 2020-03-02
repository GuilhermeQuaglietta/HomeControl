namespace HomeControl.AccessControl.WebApi.Requests.Login
{
    public class RecoveryPasswordChangeRequest
    {
        public string Recoverykey { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
