using Microsoft.Playwright;
using NUnit.Framework;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public sealed class RegistrationApi : BaseApi
{
    public RegistrationApi(ScenarioContextData state) : base(state) { }

    public async Task<IAPIResponse> ExecuteAsync()
    {
        var payload = new { name = Data.Name, email = Data.Email, password = Data.Password };
        return await PostAsync(Data.EndpointRegister, payload);
    }

    public async Task ValidateAsync()
    {
        var root = await JsonAsync(State.Response!);
        Assert.That(State.Response!.Status, Is.EqualTo(201));
        Assert.That(root.GetProperty("success").GetBoolean(), Is.True);
        Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(201));
        Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("User account created successfully"));
        ApiAssertions.RequireProperty(root, "data.id");
    }
}
