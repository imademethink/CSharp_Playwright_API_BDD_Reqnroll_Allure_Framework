using NUnit.Framework;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public sealed class ProfileApi : BaseApi
{
    public ProfileApi(ScenarioContextData state) : base(state) { }

    public async Task ExecuteAsync()
    {
        var payload = new { email = Data.Email, password = Data.Password };
        await GetAsync(Data.EndpointProfile, payload, new Dictionary<string, string> { ["X-Auth-Token"] = Data.Token! });
    }

    public async Task ValidateAsync()
    {
        var root = await JsonAsync(State.Response!);
        Assert.That(State.Response!.Status, Is.EqualTo(200));
        Assert.That(root.GetProperty("success").GetBoolean(), Is.True);
        Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(200));
        Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("Profile successful"));
        ApiAssertions.RequireProperty(root, "data.id");
        ApiAssertions.RequireProperty(root, "data.name");
        ApiAssertions.RequireProperty(root, "data.email");
    }
}
