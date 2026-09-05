using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;

namespace CSharp.Playwright.Api.Bdd.Tests;

[TestFixture]
[Category("api-independent")]
public sealed class ApiIndependentTests
{
    private IPlaywright? playwright;
    private IAPIRequestContext? api;
    private string email = null!;
    private string password = "Demo1234";
    private string newPassword = "Demo9999";
    private string token = null!;
    private const string BaseUrl = "https://practice.expandtesting.com/notes/api/";

    [OneTimeSetUp]
    public async Task SetUp()
    {
        // playwright = await Playwright.CreateAsync();
        playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        email = $"{Guid.NewGuid():N}@example.com";
        api = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = BaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json; charset=UTF-8",
                ["Accept"] = "application/json"
            }
        });
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        if (api != null) await api.DisposeAsync();
        playwright?.Dispose();
    }

    private static async Task<JsonElement> Json(IAPIResponse response) => JsonDocument.Parse(await response.TextAsync()).RootElement.Clone();

    [Test, Order(1)]
    public async Task Register()
    {
        var response = await api!.PostAsync("users/register", new APIRequestContextOptions
        {
            DataObject = new { name = "Mary Smith", email, password }
        });
        var json = await Json(response);
        Assert.That(response.Status, Is.EqualTo(201));
        Assert.That(json.GetProperty("success").GetBoolean(), Is.True);
        Assert.That(json.GetProperty("message").GetString(), Is.EqualTo("User account created successfully"));
    }

    [Test, Order(2)]
    public async Task Login()
    {
        var response = await api!.PostAsync("users/login", new APIRequestContextOptions
        {
            DataObject = new { email, password }
        });
        var json = await Json(response);
        Assert.That(response.Status, Is.EqualTo(200));
        token = json.GetProperty("data").GetProperty("token").GetString()!;
    }

    [Test, Order(3)]
    public async Task Logout()
    {
        var response = await api!.DeleteAsync("users/logout", new APIRequestContextOptions
        {
            Headers = new Dictionary<string, string> { ["X-Auth-Token"] = token },
            DataObject = new { email, password }
        });
        Assert.That(response.Status, Is.EqualTo(200));
    }

    [Test, Order(4)]
    public async Task GetProfile()
    {
        var login = await api!.PostAsync("users/login", new APIRequestContextOptions { DataObject = new { email, password } });
        var loginJson = await Json(login);
        token = loginJson.GetProperty("data").GetProperty("token").GetString()!;
        var response = await api.GetAsync("users/profile", new APIRequestContextOptions
        {
            Headers = new Dictionary<string, string> { ["X-Auth-Token"] = token }
        });
        var json = await Json(response);
        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(json.GetProperty("message").GetString(), Is.EqualTo("Profile successful"));
    }

    [Test, Order(5)]
    public async Task ForgotPassword()
    {
        var login = await api!.PostAsync("users/login", new APIRequestContextOptions { DataObject = new { email, password } });
        var loginJson = await Json(login);
        token = loginJson.GetProperty("data").GetProperty("token").GetString()!;
        var response = await api.PostAsync("users/forgot-password", new APIRequestContextOptions
        {
            Headers = new Dictionary<string, string> { ["X-Auth-Token"] = token },
            DataObject = new { email }
        });
        var json = await Json(response);
        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(json.GetProperty("message").GetString(), Does.Contain("Password reset link successfully sent to"));
    }

    [Test, Order(6)]
    public async Task ChangePassword()
    {
        var login = await api!.PostAsync("users/login", new APIRequestContextOptions { DataObject = new { email, password } });
        var loginJson = await Json(login);
        token = loginJson.GetProperty("data").GetProperty("token").GetString()!;
        var response = await api.PostAsync("users/change-password", new APIRequestContextOptions
        {
            Headers = new Dictionary<string, string> { ["X-Auth-Token"] = token },
            DataObject = new { currentPassword = password, newPassword }
        });
        Assert.That(response.Status, Is.EqualTo(200));
        password = newPassword;
    }

    [Test, Order(7)]
    public async Task DeleteAccount()
    {
        var login = await api!.PostAsync("users/login", new APIRequestContextOptions { DataObject = new { email, password } });
        var loginJson = await Json(login);
        token = loginJson.GetProperty("data").GetProperty("token").GetString()!;
        var response = await api.DeleteAsync("users/delete-account", new APIRequestContextOptions
        {
            Headers = new Dictionary<string, string> { ["X-Auth-Token"] = token }
        });
        var json = await Json(response);
        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(json.GetProperty("message").GetString(), Is.EqualTo("Account successfully deleted"));
    }
}
