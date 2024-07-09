﻿namespace HrAspire.Web.Client.Services.Account;

using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

public class AppAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagementService
{
    private static readonly ClaimsPrincipal Unauthenticated = new(new ClaimsIdentity());

    private readonly AccountApiClient accountApiClient;

    public AppAuthenticationStateProvider(AccountApiClient accountApiClient)
    {
        this.accountApiClient = accountApiClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userInfo = await this.accountApiClient.GetUserInfoAsync();
            if (userInfo is not null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, userInfo.Id),
                    new(ClaimTypes.Name, userInfo.Email),
                    new(ClaimTypes.Email, userInfo.Email)
                };

                claims.AddRange(userInfo.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

                var identity = new ClaimsIdentity(claims, nameof(AppAuthenticationStateProvider));
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
        }
        catch
        {
            // Ignore and return unauthenticated user state
        }

        return new AuthenticationState(Unauthenticated);
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var loggedIn = await this.accountApiClient.LoginAsync(email, password);
        if (loggedIn)
        {
            this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
        }

        return loggedIn;
    }

    public async Task<bool> LogoutAsync()
    {
        var loggedOut = await this.accountApiClient.LogoutAsync();
        if (loggedOut)
        {
            this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
        }

        return loggedOut;
    }
}
