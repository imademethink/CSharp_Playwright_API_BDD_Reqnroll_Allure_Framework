# C# Playwright API BDD Automation Framework

C# + Microsoft Playwright API + Reqnroll + NUnit + Allure Reporting.

## Stack
- C# / .NET 8
- Microsoft Playwright 1.62.0
- Reqnroll 3.3.4 (Gherkin/Cucumber-compatible BDD)
- NUnit
- Allure.Reqnroll 2.15.0

## Structure

```text
Features/
  Demo.feature
  DemoProfileMngt.feature
StepDefinitions/
  ApiSteps.cs
Api/
  BaseApi.cs
  RegistrationApi.cs
  LoginApi.cs
  LogoutApi.cs
  ProfileApi.cs
  ForgotPasswordApi.cs
  ChangePasswordApi.cs
  DeleteAccountApi.cs
Support/
  TestData.cs
  ScenarioContextData.cs
  ApiAssertions.cs
  TestHooks.cs
Tests/
  ApiIndependentTests.cs
allureConfig.json
reqnroll.json
CSharp.Playwright.Api.Bdd.csproj
```

## Setup
```powershell
dotnet restore
dotnet build
pwsh .\bin\Debug\net8.0\playwright.ps1 install
```

## Restore

```bash
dotnet restore
```

## Install Playwright

```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

## Cleanup

```bash
cmd /c rmdir /s /q bin

cmd /c rmdir /s /q allure-report

cmd /c rmdir /s /q allure-results
```

## Run all BDD tests

```bash
dotnet test
```

## Run a specific tag

```bash
dotnet test --filter "TestCategory=case1"
```

## Allure

The Reqnroll Allure adapter writes results to `allure-results` at the solution/project level using `allureConfig.json`.

```bash
allure generate bin/Debug/net8.0/allure-results -o allure-report --clean
allure open allure-report
```


