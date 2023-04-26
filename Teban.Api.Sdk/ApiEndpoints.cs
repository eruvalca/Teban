namespace Teban.Api.Sdk;

public static class ApiEndpoints
{
    private const string ApiBase = "/api";

    public static class Identity
    {
        private const string Base = $"{ApiBase}/identity";

        public const string Register = $"{Base}/register";
        public const string LogIn = $"{Base}/login";
    }

    public static class Contacts
    {
        private const string Base = $"{ApiBase}/contacts";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Import = $"{Base}/import";
        public const string BulkDelete = $"{Base}/bulkdelete";
    }

    public static class CommunicationSchedules
    {
        private const string Base = $"{ApiBase}/communicationSchedules";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
}
