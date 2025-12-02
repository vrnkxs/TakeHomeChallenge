# UI Test Automation Framework (C#, NUnit, Playwright)

This repository contains a structured UI automation framework built with C#, .NET, and Microsoft Playwright.  
It demonstrates how to design maintainable and scalable end-to-end UI tests using modern tooling and Page Object Model best practices.

The implemented scenario performs automated validation across Google Search and a target contact form.

---

## Technologies Used

- C# / .NET 8
- NUnit (testing framework)
- Microsoft Playwright (browser automation)
- Page Object Model (POM) architecture

---

## Overview

The framework demonstrates:

- Navigation to external websites
- Searching for dynamic content
- Validating search results
- Cross-site navigation (Google → target site)
- Form input handling
- Required field validation
- Clean separation of test logic from UI locators

The goal is to keep the design lightweight while still following good architectural practices.

---

## Project Structure

PlaywrightGoogleTest/
- Config/ 
  - TestSettings.cs
- Pages/
   - GoogleHomePage.cs
   - SearchResultsPage.cs
   - ContactUsPage.cs
- Tests/
    - GoogleSearchTests.cs
- Utilities/
   - TestBase.cs

Methods and Annotations Reference

This section provides a consolidated list of all test lifecycle annotations and all custom methods implemented across the framework.

---

## NUnit Annotations Used

- [SetUp]
Runs before each test. Used in TestBase and GoogleSearchTests.

- [TearDown]
Runs after each test. Used in TestBase.

- [Test]
Marks a method as an executable test. Used in GoogleSearchTests.

## Framework Methods
- TestBase.cs
- SetUp() – Initializes Playwright, launches browser, creates context and page
- TearDown() – Closes the browser after each test

## GoogleSearchTests.cs
- Init() – Loads test settings before each test
- Google_Search_And_Validate_ContactUs_Form() – Full end-to-end scenario 

## GoogleHomePage.cs
- Search(string query) – Types a search term into Google and submits it

## SearchResultsPage.cs
- ContainsText(string text) – Checks whether search results contain expected content
- ClickContactUs() – Clicks the "Contact Us" link inside Google results

## ContactUsPage.cs
- FillNames(string first, string last) – Fills first and last name fields 
- Submit() – Submits the contact form
- CountAdditionalRequiredFields() – Returns number of validation errors still displayed

## Logger.cs
- Info(string message) – Logs informational actions
- Success(string message) – Logs successful checks
- Warning(string message) – Logs non-critical issues
- Error(string message) – Logs failures or unexpected behavior