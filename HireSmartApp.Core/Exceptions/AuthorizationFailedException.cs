using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Exceptions
{
    public class AuthorizationFailedException : SecurityException
    {
        public AuthorizationFailedException()
        {
        }

        public AuthorizationFailedException(string message) : base(message)
        {
        }
    }
}
