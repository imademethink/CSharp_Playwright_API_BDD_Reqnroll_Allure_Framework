using System.Text;
using Allure.Net.Commons;
using Microsoft.Playwright;
using Reqnroll;

namespace CSharp.Playwright.Api.Bdd.Support;

[Binding]
public sealed class TestHooks
{
    private readonly ScenarioContextData state;

    public TestHooks(ScenarioContextData state) => this.state = state;

    [BeforeScenario(Order = 0)]
    public async Task BeforeScenarioAsync(ScenarioContext scenario)
    {
        // state.Playwright = await Playwright.CreateAsync();
        state.Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        state.Data = new TestData
        {
            Name = "Jon Doe",
            Email = $"{Guid.NewGuid():N}@example.com"
        };

        state.ApiRequest = await state.Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = state.Data.BaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json; charset=UTF-8",
                ["Accept"] = "application/json"
            }
        });

        AllureApi.AddAttachment("test-data.txt", "text/plain",
            Encoding.UTF8.GetBytes($"Name={state.Data.Name}\nEmail={state.Data.Email}"));
    }

    [AfterScenario(Order = 1000)]
    public async Task AfterScenarioAsync(ScenarioContext scenario)
    {
        if (scenario.TestError != null && state.Response != null)
        {
            var body = await state.Response.TextAsync();
            AllureApi.AddAttachment("last-response.json", "application/json", Encoding.UTF8.GetBytes(body));
        }

        if (state.ApiRequest != null)
            await state.ApiRequest.DisposeAsync();
        state.Playwright?.Dispose();
    }
}
