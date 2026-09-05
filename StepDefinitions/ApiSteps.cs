using CSharp.Playwright.Api.Bdd.Api;
using CSharp.Playwright.Api.Bdd.Support;
using NUnit.Framework;
using Reqnroll;

namespace CSharp.Playwright.Api.Bdd.StepDefinitions;

[Binding]
public sealed class ApiSteps
{
    private readonly ScenarioContextData state;
    private RegistrationApi? registration;
    private LoginApi? login;
    private LogoutApi? logout;
    private ProfileApi? profile;
    private ForgotPasswordApi? forgotPassword;
    private ChangePasswordApi? changePassword;
    private DeleteAccountApi? deleteAccount;

    public ApiSteps(ScenarioContextData state) => this.state = state;

    [Given("Prepare header and payload for registration")]
    public void PrepareRegistration() => Assert.That(state.ApiRequest, Is.Not.Null);

    [When("Call api registration")]
    public async Task CallRegistration() { registration = new RegistrationApi(state); await registration.ExecuteAsync(); }

    [Then("Validate response for registration")]
    public async Task ValidateRegistration() => await registration!.ValidateAsync();

    [Given("Complete formal registration")]
    public async Task CompleteRegistration()
    {
        registration = new RegistrationApi(state);
        await registration.ExecuteAsync();
        await registration.ValidateAsync();
    }

    [When("Process profile login")]
    public async Task ProcessLogin() { login = new LoginApi(state); await login.ExecuteAsync(); }

    [Then("Validate response for login")]
    public async Task ValidateLogin() => await login!.ValidateAsync();

    [When("Complete formal login")]
    public async Task CompleteLogin() { login = new LoginApi(state); await login.ExecuteAsync(); await login.ValidateAsync(); }

    [When("Process profile logout")]
    public async Task ProcessLogout() { logout = new LogoutApi(state); await logout.ExecuteAsync(); }

    [Then("Validate response for logout")]
    public async Task ValidateLogout() => await logout!.ValidateAsync();

    [When("Complete formal logout")]
    [Then("Complete formal logout")]
    public async Task CompleteLogout() { logout = new LogoutApi(state); await logout.ExecuteAsync(); await logout.ValidateAsync(); }

    [When("Process get profile")]
    public async Task ProcessGetProfile() { profile = new ProfileApi(state); await profile.ExecuteAsync(); }

    [Then("Validate response for get profile")]
    public async Task ValidateGetProfile() => await profile!.ValidateAsync();

    [When("Process forget password")]
    public async Task ProcessForgotPassword() { forgotPassword = new ForgotPasswordApi(state); await forgotPassword.ExecuteAsync(); }

    [Then("Validate response for forget password")]
    public async Task ValidateForgotPassword() => await forgotPassword!.ValidateAsync();

    [When("Process change password")]
    public async Task ProcessChangePassword() { changePassword = new ChangePasswordApi(state); await changePassword.ExecuteAsync(); }

    [Then("Validate response for change password")]
    public async Task ValidateChangePassword() => await changePassword!.ValidateAsync();

    [When("Process delete account")]
    public async Task ProcessDeleteAccount() { deleteAccount = new DeleteAccountApi(state); await deleteAccount.ExecuteAsync(); }

    [Then("Validate response for delete account")]
    public async Task ValidateDeleteAccount() => await deleteAccount!.ValidateAsync();

    [Then("Validate response for invalid login")]
    public async Task ValidateInvalidLogin()
    {
        login = new LoginApi(state);
        await login.ExecuteAsync();
        await login.ValidateAsync(false);
    }
}
