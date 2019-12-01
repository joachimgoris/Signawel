namespace Signawel.Mobile.Constants
{
    public static class ApiConstants
    {
        public static string BaseApiAddress => "https://10.95.133.233:5005/api/";

        #region Authentication

        public static string LoginEndpoint => BaseApiAddress + "authentication/login";

        public static string RegisterEndpoint => BaseApiAddress + "authentication/register";

        public static string RefreshEndpoint => BaseApiAddress + "authentication/refresh";

        #endregion

        #region DeterminationGraph

        public static string GetDeterminationGraph => BaseApiAddress + "determination-graph";

        public static string PostDeterminationGraph => BaseApiAddress + "determination-graph";

        #endregion

        #region Images

        public static string GetImage(string imageId) => $"{BaseApiAddress}images/{imageId}";

        public static string PostImage => BaseApiAddress + "images";

        #endregion

        #region Categories

        public static string GetCategories => $"{BaseApiAddress}categories";

        #endregion

        #region RoadworkSchemas

        public static string GetRoadworkSchema(string roadworkSchemaId) =>
            $"{BaseApiAddress}roadwork-schemas/{roadworkSchemaId}";

        public static string GetAllRoadworkSchemas => BaseApiAddress + "roadwork-schemas";

        public static string PostRoadworkSchema => BaseApiAddress + "roadwork-schemas";

        public static string PutRoadworkSchema(string roadworkSchemaId) =>
            $"{BaseApiAddress}roadwork-schemas/{roadworkSchemaId}";

        public static string DeleteRoadworkSchema(string roadworkSchemaId) =>
            $"{BaseApiAddress}roadwork-schemas/{roadworkSchemaId}";

        #endregion
    }
}