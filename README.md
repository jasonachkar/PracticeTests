# Selenium + NUnit Test Suite for PracticeExpandTesting.com

This repository contains an automated C# Selenium/WebDriver test suite (with NUnit) that exercises the Registration → Login → Dashboard flows on [practice.expandtesting.com](https://practice.expandtesting.com). You can run it on **macOS**, **Windows**, or **Linux** using the `.NET CLI` or your IDE of choice.

---

## 📋 Prerequisites

1. **.NET 6.0 SDK (or later)**  
   - Download & install from https://dotnet.microsoft.com/download  
   - Verify with:
     ```bash
     dotnet --version
     ```
2. **Git**  
   - [Install Git](https://git-scm.com/downloads) if you haven’t already.
3. **Google Chrome** (or another supported browser)  
   - Download from https://www.google.com/chrome
4. **ChromeDriver** (matching your Chrome version)  
   - **macOS** (via Homebrew):
     ```bash
     brew install --cask chromedriver
     ```
   - **Windows**:
     - Option A: NuGet package `Selenium.WebDriver.ChromeDriver` (automatically pulled in below)  
     - Option B: Download from https://sites.google.com/a/chromium.org/chromedriver/downloads and unzip to a folder on your `PATH`
   - **Linux**:
     ```bash
     sudo apt-get install chromium-chromedriver
     ```
5. **IDE or Editor** (optional but recommended)  
   - Visual Studio 2022+ (Windows) or Visual Studio for Mac  
   - VS Code + [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)  
   - JetBrains Rider

---

## 🔧 Setup & Restore

1. **Clone this repo**  
   ```bash
   git clone https://github.com/<your‑username>/PracticeExpandTesting.git
   cd PracticeExpandTesting

	2.	Inspect the project file
Ensure your PracticeExpandTesting.csproj references:

<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="NUnit" Version="3.*" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="Selenium.WebDriver" Version="4.*" />
    <PackageReference Include="Selenium.WebDriver.ChromeDriver" Version="*" />
  </ItemGroup>
</Project>


	3.	Restore NuGet packages

dotnet restore



⸻

🚀 Build & Run Tests

You can choose either the .NET CLI or your IDE:

A) .NET CLI

# Build the project
dotnet build

# Run all tests
dotnet test

# (Optional) Run a single test by name
dotnet test --filter "AuthTests.REG_01_Registration_Valid"

B) Visual Studio / Rider / VS Code
	1.	Open the folder/solution in your IDE.
	2.	Build the solution (Ctrl+Shift+B on Windows, ⌘B on macOS).
	3.	Open Test Explorer (VS: Test → Windows → Test Explorer; Rider: Unit Tests).
	4.	Click Run All or right‑click individual tests.

⸻

🗂 Project Structure

PracticeExpandTesting/
├── PracticeExpandTesting.csproj    # C# test project
├── AuthTests.cs                    # Selenium + NUnit test code
└── README.md                       # This file

	•	AuthTests.cs
	•	[OneTimeSetUp] creates a ChromeDriver.
	•	[Test] methods cover:
	1.	Valid registration
	2.	Blank‑field rejection
	3.	Duplicate registration
	4.	Valid login
	5.	Wrong‑password rejection
	6.	Direct‑access guard on dashboard
	7.	Logout/session clear

⸻

⚡ Troubleshooting
	•	“Only one project can be specified”
	•	Ensure you cd into the folder containing exactly one .csproj and run dotnet test.
	•	“Assert does not contain definition for ‘IsTrue’”
	•	Confirm your project uses NUnit, not xUnit:

dotnet add package NUnit
dotnet add package NUnit3TestAdapter


	•	Make sure your test file begins with:

using NUnit.Framework;


	•	ChromeDriver version mismatch
	•	Check that your local Chrome’s version matches the installed ChromeDriver:

chromedriver --version
google-chrome --version


	•	Browser does not launch
	•	Verify Chrome/Chromedriver are on your PATH, or specify the absolute path when instantiating new ChromeDriver(path).

⸻

🎯 Next Steps
	•	Extend tests to cover Registration edge‑cases (invalid email, password mismatch, etc.).
	•	Refactor to a Page Object Model for maintainability.
	•	Integrate into a CI pipeline (GitHub Actions, Azure DevOps, etc.) to run tests on every push.

⸻

Happy testing! 🚀

