﻿namespace Signawel.Domain.Constants
{
    public static class ErrorCodes
    {
        #region Generic

        public const string NotFoundError = "ObjectNotFound";

        public const string ParameterEmptyError = "ParameterEmpty";

        public const string InvalidOperationError = "InvalidOperation";

        #endregion

        #region Authentication

        public const string AuthenticationIncorrectCredentialsError = "AuthenticationIncorrectCredentials";

        public const string EmailNotConfirmedError = "EmailNotConfirmed";

        public const string LoginError = "LoginError";

        public const string RegisterError = "RegisterError";

        public const string UserCreationError = "UserCreationError";

        public const string RefreshTokenError = "RefreshTokenError";

        public const string JwtTokenError = "JwtTokenError";

        public const string PrincipalTokenError = "PrincipalTokenError";

        public const string LoginRecordError = "LoginRecordError";

        public const string ForgotPasswordTokenError = "ForgotPasswordTokenError";

        public const string IdentityError = "IdentityError";

        #endregion

        #region Mail

        public const string MailError = "MailError";

        #endregion

        #region Report

        public const string ReportCreationError = "Failed to create a report.";

        public const string ReportDeletionError = "Failed to delete a report.";

        public const string ReportGetError = "Failed to retrieve a report.";

        public const string ReportModificationError = "Failed to modify a report.";

        #endregion

        #region RoadworkSchemas

        public const string RoadworkSchemaCreationError = "Failed to create a roadworkschema";

        public const string RoadworkSchemaNotFoundError = "Roadwork schema is not found.";

        #endregion

        #region PriorityEmails

        public const string PriorityEmailDeletionError = nameof(PriorityEmailDeletionError);

        public const string PriorityEmailCreationError = nameof(PriorityEmailCreationError);

        #endregion PriorityEmails

        #region BlacklistEmails

        public const string BlacklistEmailCreationError = nameof(BlacklistEmailCreationError);

        public const string BlacklistEmailDeletionError = nameof(BlacklistEmailDeletionError);

        #endregion

        #region Images

        public const string FailedToSaveImage = "FailedToSaveImage";

        public const string InvalidFileFormat = "InvalidFileFormat";

        #endregion

        #region DefaultIssue

        public const string DefaultIssueCreationError = "Failed to create a default issue";

        public const string DefaultIssueDeletionError = "Failed to delete a default issue";

        #endregion DefaultIssue
    }
}