namespace HomeControl.Identity.Jwt
{
    public class JwtValidationResult
    {
        public bool IsValid { get; }

        public string ErrorMessage { get; }

        public JwtIdentity Identity { get; }

        public JwtValidationResult(string errorMessage)
        {
            IsValid = false;
            ErrorMessage = errorMessage;
        }

        public JwtValidationResult(JwtIdentity identity)
        {
            IsValid = true;
            Identity = identity;
        }
    }
}
