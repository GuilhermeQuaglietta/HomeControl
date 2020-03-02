using System;

namespace HomeControl.Identity.Jwt
{
    public class JwtValidationResult
    {
        public bool IsValid { get; }
        public JwtUser Identity { get; }
        public Exception Exception { get; }
        public JwtValidationResultCode ValidationResultCode { get; }

        public JwtValidationResult(JwtUser identity)
        {
            IsValid = true;
            ValidationResultCode = JwtValidationResultCode.Ok;
            Identity = identity;
        }
        public JwtValidationResult(Exception exception)
        {
            IsValid = false;
            Exception = exception;
            ValidationResultCode = JwtValidationResultCode.Unknown;
        }
        public JwtValidationResult(JwtValidationResultCode validationResultCode, Exception exception)
        {
            IsValid = false;
            Exception = exception;
            ValidationResultCode = validationResultCode;
        }

    }
}
