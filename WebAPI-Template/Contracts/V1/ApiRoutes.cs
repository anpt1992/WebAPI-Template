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

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Get = Base + "/posts/{testId}";
            public const string Create = Base + "/posts";
            public const string Update = Base + "/posts/{testId}";
            public const string Delete = Base + "/posts/{testId}";
            public const string GetAllWithClaims = Base + "/posts_claims";
            public const string GetAllWithRoles = Base + "/posts_roles";
            public const string GetAllWithAuthorizationHandles = Base + "/posts_authorization_handlers";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}
