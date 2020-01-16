using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeControl.AccessControl.WebApi.Requests.Recovery
{
    public class RecoveryPasswordChangeRequest
    {
        public string Recoverykey { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
