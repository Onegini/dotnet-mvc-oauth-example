﻿using System.Security.Claims;
using AspNet.Security.OAuth.Onegini;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace AspNet.Security.OAuth.Onegini
{
    
    /// <summary>
    /// Defines a set of options used by <see cref="OneginiAuthenticationHandler"/>.
    /// </summary>
    public class OneginiAuthenticationOptions : OAuthOptions{
        
        /// <summary>
        /// Initialize a new instance of the <see cref="OneginiAuthenticationOptions"/> class.
        /// </summary>
        public OneginiAuthenticationOptions()
        {
            ClaimsIssuer = OneginiAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(OneginiAuthenticationDefaults.CallbackPath);
            
            //todo: Set default endpoints -> there is no deafult endpoint -> endpoints differ per customer
            AuthorizationEndpoint = OneginiAuthenticationDefaults.AuthorizationEndPoint;
            TokenEndpoint = OneginiAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = OneginiAuthenticationDefaults.UserInformationEndpoint;
            SaveTokens = true; //Now we can access the access token later on in the application
            
            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "sub");
            ClaimActions.MapJsonKey(ClaimTypes.Email, "sub");
            //ClaimActions.MapJsonKey(ClaimTypes.DateOfBirth, "date_of_birth");
            //ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            //ClaimActions.MapJsonKey(ClaimTypes.StreetAddress, "address");
            //ClaimActions.MapJsonKey(ClaimTypes.PostalCode, "zip_code");
        }
        
    }
}