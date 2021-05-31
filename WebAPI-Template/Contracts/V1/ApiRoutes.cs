using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Tests
        {
            public const string GetAll = Base + "/tests";
            public const string Get = Base + "/tests/{testId}";
            public const string Create = Base + "/tests";
            public const string Update = Base + "/tests/{testId}";
            public const string Delete = Base + "/tests/{testId}";
            public const string GetAllWithClaims = Base + "/tests_claims";
            public const string GetAllWithRoles = Base + "/tests_roles";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}
