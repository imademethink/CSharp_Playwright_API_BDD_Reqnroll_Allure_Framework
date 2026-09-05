using NUnit.Framework;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public sealed class DeleteAccountApi : BaseApi
{
    public DeleteAccountApi(ScenarioContextData state) : base(state) { }

    public async Task ExecuteAsync()
    {
        await DeleteAsync(Data.EndpointDeleteAccount, null, new Dictionary<string, string> { ["X-Auth-Token"] = Data.Token! });
    }

    public async Task ValidateAsync()
    {
        var root = await JsonAsync(State.Response!);
        Assert.That(State.Response!.Status, Is.EqualTo(200));
        Assert.That(root.GetProperty("success").GetBoolean(), Is.True);
        Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(200));
        Assert.That(root.GetProperty("message").GetString(), Is.EqualTo("Account successfully deleted"));
    }
}
