using System.Text.Json;
using NUnit.Framework;

namespace CSharp.Playwright.Api.Bdd.Support;

public static class ApiAssertions
{
    public static async Task<JsonElement> ReadJsonAsync(Microsoft.Playwright.IAPIResponse response)
    {
        var text = await response.TextAsync();
        var json = JsonDocument.Parse(text);
        return json.RootElement.Clone();
    }

    public static async Task AssertJsonAsync(Microsoft.Playwright.IAPIResponse response, int status, bool success, string expectedStatus, string? expectedMessage = null)
    {
        Assert.That(response.Status, Is.EqualTo(status));
        var root = await ReadJsonAsync(response);
        Assert.That(root.GetProperty("success").GetBoolean(), Is.EqualTo(success));
        Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(status));
        if (expectedMessage != null)
            Assert.That(root.GetProperty("message").GetString(), Is.EqualTo(expectedMessage));
    }

    public static void RequireProperty(JsonElement root, string propertyPath)
    {
        var current = root;
        foreach (var part in propertyPath.Split('.'))
        {
            Assert.That(current.TryGetProperty(part, out current), Is.True, $"Missing JSON property: {propertyPath}");
        }
    }
}
