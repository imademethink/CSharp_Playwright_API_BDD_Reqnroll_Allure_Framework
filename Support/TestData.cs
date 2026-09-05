namespace CSharp.Playwright.Api.Bdd.Support;

public sealed class TestData
{
    public string Name { get; set; } = "Jon Doe";
    public string Email { get; set; } = $"{Guid.NewGuid():N}@example.com";
    public string Password { get; set; } = "Demo1234";
    public string NewPassword { get; set; } = "Demo9999";
    public string BaseUrl { get; set; } = "https://practice.expandtesting.com/notes/api/";
    public string EndpointRegister { get; set; } = "users/register";
    public string EndpointLogin { get; set; } = "users/login";
    public string EndpointLogout { get; set; } = "users/logout";
    public string EndpointProfile { get; set; } = "users/profile";
    public string EndpointForgotPassword { get; set; } = "users/forgot-password";
    public string EndpointChangePassword { get; set; } = "users/change-password";
    public string EndpointDeleteAccount { get; set; } = "users/delete-account";
    public string? Token { get; set; }
}
