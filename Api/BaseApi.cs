using System.Text.Json;
using Microsoft.Playwright;
using CSharp.Playwright.Api.Bdd.Support;

namespace CSharp.Playwright.Api.Bdd.Api;

public abstract class BaseApi
{
    protected readonly ScenarioContextData State;
    protected TestData Data => State.Data;

    protected BaseApi(ScenarioContextData state) => State = state;

    protected static object Json(object value) => value;

    protected async Task<IAPIResponse> PostAsync(string endpoint, object payload, IDictionary<string, string>? headers = null)
    {
        State.Payload = payload;
        State.Headers = headers?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, string>();
        State.Response = await State.ApiRequest!.PostAsync(endpoint, new APIRequestContextOptions
        {
            Headers = State.Headers,
            DataObject = payload
        });
        await AttachRequestResponseAsync(endpoint, "POST", payload, State.Response);
        return State.Response;
    }

    protected async Task<IAPIResponse> GetAsync(string endpoint, object? payload = null, IDictionary<string, string>? headers = null)
    {
        State.Payload = payload;
        State.Headers = headers?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, string>();
        State.Response = await State.ApiRequest!.GetAsync(endpoint, new APIRequestContextOptions
        {
            Headers = State.Headers
        });
        await AttachRequestResponseAsync(endpoint, "GET", payload, State.Response);
        return State.Response;
    }

    protected async Task<IAPIResponse> DeleteAsync(string endpoint, object? payload = null, IDictionary<string, string>? headers = null)
    {
        State.Payload = payload;
        State.Headers = headers?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, string>();
        State.Response = await State.ApiRequest!.DeleteAsync(endpoint, new APIRequestContextOptions
        {
            Headers = State.Headers,
            DataObject = payload
        });
        await AttachRequestResponseAsync(endpoint, "DELETE", payload, State.Response);
        return State.Response;
    }

    protected static async Task<JsonElement> JsonAsync(IAPIResponse response) => await ApiAssertions.ReadJsonAsync(response);

    private async Task AttachRequestResponseAsync(string endpoint, string method, object? payload, IAPIResponse response)
    {
        var body = await response.TextAsync();
        var text = $"Method: {method}\nEndpoint: {endpoint}\nStatus: {response.Status}\nPayload: {JsonSerializer.Serialize(payload)}\nResponse: {body}";
        Allure.Net.Commons.AllureApi.AddAttachment("api-request-response.txt", "text/plain", System.Text.Encoding.UTF8.GetBytes(text));
    }
}
