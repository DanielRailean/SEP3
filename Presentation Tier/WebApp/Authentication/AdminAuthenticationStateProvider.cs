using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Authentication
{
    public class AdminAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;
        private IAdminService adminService;
        private Administrator cachedUser;
        
        public AdminAuthenticationStateProvider(IJSRuntime jsRuntime, IAdminService adminService)
        {
            this.jsRuntime = jsRuntime;
            this.adminService = adminService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        { 
            var identity = new ClaimsIdentity();
            if (cachedUser == null)
            {
                string adminAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if(!string.IsNullOrEmpty(adminAsJson))
                {
                    cachedUser = JsonSerializer.Deserialize<Administrator>(adminAsJson);
                    identity = SetupClaimsForUser(cachedUser);
                }
            }
            else
            {
                identity = SetupClaimsForUser(cachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        public async Task ValidateLogin(string email, string password)
        {
            Console.WriteLine("Validating admin log in");
            if (string.IsNullOrEmpty(email)) throw new Exception("Enter e-mail address");
            if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();

            try
            {
                Administrator administrator = Task.Run(()=> adminService.ValidateAdminAsync(email, password)).Result;
                identity = SetupClaimsForUser(administrator);
                string serializedUser = JsonSerializer.Serialize(administrator);
                jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedUser);
                cachedUser = administrator;
            }
            catch(Exception e)
            {
                throw e;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public void Logout()
        {
            cachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity SetupClaimsForUser(Administrator administrator)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, administrator.Email));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
    }
}