using NUnit.Framework;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public sealed class LogoutApi : BaseApi
{
    public LogoutApi(ScenarioContextData state) : base(state) { }

    public async Task ExecuteAsync()
    {
        var payload = new { email = Data.Email, password = Data.Password };
        await DeleteAsync(Data.EndpointLogout, payload, new Dictionary<string, string> { ["X-Auth-Token"] = Data.Token! });
    }

    public async Task ValidateAsync()
    {
        var root = await JsonAsync(State.Response!);
        Assert.That(State.Response!.Status, Is.EqualTo(200));
        Assert.That(root.GetProperty("success").GetBoolean(), Is.True);
        Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(200));
        Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("User has been successfully logged out"));
    }
}
