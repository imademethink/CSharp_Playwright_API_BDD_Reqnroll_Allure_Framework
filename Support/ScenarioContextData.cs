using Microsoft.Playwright;

namespace CSharp.Playwright.Api.Bdd.Support;

public sealed class ScenarioContextData
{
    public IPlaywright? Playwright { get; set; }
    public IAPIRequestContext? ApiRequest { get; set; }
    public TestData Data { get; set; } = new();
    public IAPIResponse? Response { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();
    public object? Payload { get; set; }
    public object? LastResponseJson { get; set; }
    public object? LastApi { get; set; }
}
