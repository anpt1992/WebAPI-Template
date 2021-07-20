using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v{version:apiVersion}";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string GetAllAsync = Base + "/async-posts";
            public const string GetAll2 = Base + "/posts2";//only version 2
            public const string Get = Base + "/posts/{postId}";
            public const string Create = Base + "/posts";
            public const string Update = Base + "/posts/{postId}";
            public const string Delete = Base + "/posts/{postId}";
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

        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            public const string Get = Base + "/tags/{tagName}";
            public const string Create = Base + "/tags";
            public const string Delete = Base + "/tags/{tagName}";
        }
    }
}
