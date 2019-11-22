namespace Signawel.Domain.Constants
{
    public static class ErrorCodes
    {
        #region Generic

        public static string NotFoundError = "ObjectNotFound";

        public static string ParameterEmptyError = "ParameterEmpty";

        public static string InvalidOperationError = "InvalidOperation";

        #endregion

        #region Authentication

        public static string AuthenticationIncorrectCredentialsError = "AuthenticationIncorrectCredentials";

        public static string EmailNotConfirmedError = "EmailNotConfirmed";

        public static string LoginError = "LoginError";

        public static string RegisterError = "RegisterError";

        public static string UserCreationError = "UserCreationError";

        public static string RefreshTokenError = "RefreshTokenError";

        public static string JwtTokenError = "JwtTokenError";

        public static string PrincipalTokenError = "PrincipalTokenError";

        public static string LoginRecordError = "LoginRecordError";

        #endregion

        #region Mail

        public static string MailError = "MailError";

        #endregion

        #region Report

        public static string ReportCreationError = "Failed to create a report.";

        public static string ReportDeletionError = "Failed to delete a report.";

        public static string ReportGetError = "Failed to retrieve a report.";

        public static string ReportModificationError = "Failed to modify a report.";

        #endregion
    }
}