# C# + Microsoft Playwright API + Reqnroll + NUnit + REST API CRUD + Allure Reporting Automation Framework

Ready to Use API Automation Framework.

---

<img width="1672" height="941" alt="Ready To Use Automation Framework - C sharp, Playwright, REST API, BDD Reqnroll Nunit" src="https://github.com/user-attachments/assets/fbe41f48-7a12-4d10-add4-b6987a630959" />


# YouTube Video Link

https://youtu.be/mvhMsN-U7YA
---

## Stack
- C# / .NET 8
- Microsoft Playwright 1.62.0
- Reqnroll 3.3.4 (Gherkin/Cucumber-compatible BDD)
- NUnit
- Allure.Reqnroll 2.15.0

## Swagger link for API Details (User Management API Automation, CRUD Operation)

https://practice.expandtesting.com/notes/api/api-docs/

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
---

# 🚀 Getting Started

## Clone Repository

```bash
git clone https://github.com/imademethink/CSharp_Playwright_API_BDD_Reqnroll_Allure_Framework.git
```

---

## Navigate to Folder

```bash
cd CSharp_Playwright_API_BDD_Reqnroll_Allure_Framework
```

---

## Do the setup

```powershell
dotnet restore
dotnet build
pwsh .\bin\Debug\net8.0\playwright.ps1 install
```

## Run a specific tag

```bash
dotnet test --framework net8.0 --filter "TestCategory=simple"   

dotnet test --framework net8.0 --filter "TestCategory=case1"
```

## Run all BDD tests
```powershell
dotnet test --framework net8.0 
```

## Allure Report

The Reqnroll Allure adapter writes results to `allure-results` at the solution/project level using `allureConfig.json`.

The framework captures a PNG screenshot in Allure when a Gherkin step fails.

```bash
Download Allure report binary from path : https://github.com/allure-framework/allure2/releases and add this path on System variable.

allure generate bin/Debug/net8.0/allure-results -o allure-report --clean
allure open allure-report
```

## Cleanup (optional)

```bash
cmd /c rmdir /s /q bin

cmd /c rmdir /s /q allure-report

cmd /c rmdir /s /q allure-results
```


