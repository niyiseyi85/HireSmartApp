﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Extensions.Constants
{
    public static partial class UserConstants
    {
        public static class Messages
        {
            public const string UserValid = "User record Fetch";
            public const string UserUpdateSuccessful = "User Updated successfully.";
            public const string UserSavedSuccessful = "User saved successfully.";
            public const string SubmitApplicationSucessful = "User application saved successfully.";
        }
        public static class ErrorMessages
        {
            public const string UserNotFoundWithID = "The provided ID is not attached to a user.";
            public const string UserNotFoundWithIDandPolicyNo = "The provided ID and Policy number combination is invalid.";
            public const string UserExists = "User with the information exists.";
            public const string PasswordIncorrect = "User password incorrect.";
            public const string UserIDNotGreaterThanZero = "'User Id' must be greater than '0'.";
            public const string InvalidUserRole = "Role specified for user is not allowed. Policy holder with Role ID 3 is the only allowed role for this scope.";
        }
    }
}
