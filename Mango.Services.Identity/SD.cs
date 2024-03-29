﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace Mango.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("Mango", "Mango Server"),
                new ApiScope(name: "read", displayName: "Read data."),
                new ApiScope(name: "write", displayName: "Write data."),
                new ApiScope(name: "delete", displayName: "Delete data.")
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "mango",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:12758/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:12758/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                },
            };
    }
}
