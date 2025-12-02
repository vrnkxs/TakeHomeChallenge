using PlaywrightGoogleTest.Pages;
using PlaywrightGoogleTest.Utilities;
using PlaywrightGoogleTest.Config;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightGoogleTest.Tests;

/*
 * Test class that performs an end-to-end Google search flow
   and validates the Contact Us form on the target website.
 */

public class GoogleSearchTests : TestBase
{
    private TestSettings settings;

    /*
     * Initializes test settings before each test run.
     */
    [SetUp]
    public void Init()
    {
        settings = new TestSettings();
        Logger.Info("Test settings initialized.");
    }

    /*
     * Main test: searches Google, validates results, navigates to Contact Us,
       fills basic fields, submits the form, and checks required field count.
     */
    [Test]
    public async Task Google_Search_And_Validate_ContactUs_Form()
    {
        Logger.Info("Starting Google search test scenario...");
        var home = new GoogleHomePage(page);
        var results = new SearchResultsPage(page);
        var contact = new ContactUsPage(page);

        // 1. Go to Google
        await page.GotoAsync(settings.BaseUrl);

        // 2. Search 'Prometheus Group'
        await home.Search(settings.SearchTerm);

        // 3. Check search results contain text
        Assert.IsTrue(await results.ContainsText("Prometheus Group"),
            "Search results did not contain the expected text.");

        // 4. Click Contact Us link
        await results.ClickContactUs();

        // 5. Fill names
        await contact.FillNames(settings.FirstName, settings.LastName);

        // 6. Submit
        await contact.Submit();

        // 7. Validate there are 4 additional required fields
        int missingFields = await contact.CountAdditionalRequiredFields();
        Assert.AreEqual(4, missingFields, "The Contact Us page did not show 4 required fields.");
        
        Logger.Success("Required field validation passed. Test scenario completed.");
    }
}
