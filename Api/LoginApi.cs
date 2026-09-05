using NUnit.Framework;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public sealed class LoginApi : BaseApi
{
    public LoginApi(ScenarioContextData state) : base(state) { }

    public async Task ExecuteAsync()
    {
        var payload = new { email = Data.Email, password = Data.Password };
        await PostAsync(Data.EndpointLogin, payload);
    }

    public async Task ValidateAsync(bool validUser = true)
    {
        var root = await JsonAsync(State.Response!);
        if (validUser)
        {
            Assert.That(State.Response!.Status, Is.EqualTo(200));
            Assert.That(root.GetProperty("success").GetBoolean(), Is.True);
            Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(200));
            Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("Login successful"));
            ApiAssertions.RequireProperty(root, "data.id");
            ApiAssertions.RequireProperty(root, "data.name");
            ApiAssertions.RequireProperty(root, "data.email");
            ApiAssertions.RequireProperty(root, "data.token");
            Data.Token = root.GetProperty("data").GetProperty("token").GetString();
        }
        else
        {
            Assert.That(State.Response!.Status, Is.EqualTo(401));
            Assert.That(root.GetProperty("success").GetBoolean(), Is.False);
            Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(401));
            Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("Incorrect email address or password"));
        }
    }
}
