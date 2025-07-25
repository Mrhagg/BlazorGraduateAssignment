﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;
using WebAppBlazor.Models;

namespace WebAppBlazor.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private readonly ISyncLocalStorageService localStorage;

    public CustomAuthStateProvider(HttpClient httpClient, ISyncLocalStorageService localStorage)
    {
        this.httpClient = httpClient;
        this.localStorage = localStorage;


        var accessToken = localStorage.GetItem<string>("accessToken");
        if(accessToken != null )
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {


        //var user = new ClaimsPrincipal(new ClaimsIdentity());

        /*var claims = new List<Claim> { new Claim(ClaimTypes.Name, "William") };
        var identity = new ClaimsIdentity(claims, "Any");
        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
        */

        var user = new ClaimsPrincipal(new ClaimsIdentity());

        try
        {
            var response = await httpClient.GetAsync("manage/info");
            if (response.IsSuccessStatusCode)
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonNode.Parse(strResponse);
                var email = jsonResponse!["email"]!.ToString();

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, email),
                    new(ClaimTypes.Email, email),
                };

                var identity = new ClaimsIdentity(claims, "Token");
                user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
        }
        catch(Exception ex) 
        { 
        
        }

        return new AuthenticationState(user);
    }

    public async Task<FormResult> LoginAsync(string email, string password)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("login", new { email, password });
            if(response.IsSuccessStatusCode)
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonNode.Parse(strResponse);
                var accessToken = jsonResponse?["accessToken"]?.ToString();
                var refreshToken = jsonResponse?["refreshToken"]?.ToString();

                localStorage.SetItem("accessToken", accessToken);
                localStorage.SetItem("refreshToken", refreshToken);


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                return new FormResult { Succeded = true };  
            }
            else
            {
                return new FormResult { Succeded = false, Errors = ["Wrong Email or Password"] };
            }
        }
        
        catch { }

        return new FormResult { Succeded = false, Errors = ["Connection Error"] };
    }

    public void Logout()
    {
        localStorage.RemoveItem("accessToken");
        localStorage.RemoveItem("refreshToken");
        httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<FormResult> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("register", registerDto);
            if(response.IsSuccessStatusCode)
            {
                var loginResponse = await LoginAsync(registerDto.Email, registerDto.Password);
                return loginResponse;
            }

            var strResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(strResponse);
            var jsonResponse = JsonNode.Parse(strResponse);
            var errorObject = jsonResponse!["errors"]!.AsObject();
            var errorList = new List<string>();
            foreach (var error in errorObject)
            {
                errorList.Add(error.Value![0]!.ToString());
            }

            var formResults = new FormResult
            {
                Succeded = false,
                Errors = errorList.ToArray()
            };

            return formResults; 
        }
        catch { }

        return new FormResult { Succeded = false, Errors = ["Connection Error"] };
    }

    public class FormResult
    {
        public bool Succeded { get; set; }

        public string[] Errors { get; set; } = [];
    }
}
