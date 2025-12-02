using Microsoft.Playwright;
using Microsoft.Playwright;
using PlaywrightGoogleTest.Utilities;
using PlaywrightGoogleTest.Pages;
using PlaywrightGoogleTest.Utilities;
using PlaywrightGoogleTest.Config;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightGoogleTest.Utilities;

/*
 * Base class that handles Playwright browser setup and cleanup for all tests.
 * Ensures each test runs in a fresh browser context.
 */
public class TestBase
{
    protected IPlaywright playwright;
    protected IBrowser browser;
    protected IPage page;
    protected IBrowserContext context;

    /*
     * Initializes Playwright, launches the browser.
     * Creates a new context, and opens a fresh page.
     * Runs before every test.
     */
    [SetUp]
    public async Task SetUp()
    {
        playwright = await Playwright.CreateAsync();

        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 50
        });
        Logger.Info("Browser launched. Creating context and new page...");

        context = await browser.NewContextAsync();
        page = await context.NewPageAsync();
        Logger.Success("Browser and page successfully initialized.");
        
        context = await browser.NewContextAsync();
    }

    /*
     * Closes the browser after each test to ensure isolation.
     */
    [TearDown]
    public async Task TearDown()
    {
        await browser.CloseAsync();
        Logger.Success("Browser closed.");
    }
}