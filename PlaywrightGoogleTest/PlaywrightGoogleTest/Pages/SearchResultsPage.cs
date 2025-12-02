using Microsoft.Playwright;
using PlaywrightGoogleTest.Utilities;
using System.Threading.Tasks;
using System;

namespace PlaywrightGoogleTest.Pages;

/*
 * Page Object for handling Google search results.
 * Provides methods to validate results content and navigate
   to the “Contact Us” page.
 */
public class SearchResultsPage
{
    private readonly IPage _page;

    /*
     * Constructor that receives the current Playwright page instance.
     */
    public SearchResultsPage(IPage page)
    {
        _page = page;
    }

    /*
     * Locator representing the main search results container.
     */
    public ILocator Results => _page.Locator("#search");

    /*
     * Validates that the search results text contains a specific string.
     * Helps confirm that Google returned the expected content.
     */
    public async Task<bool> ContainsText(string text)
    {
        Logger.Info($"Validating that search results contain: '{text}'");
        
        string content = await Results.InnerTextAsync();
        Logger.Info("Search results text extracted:");
        Logger.Info(content);

        bool contains = content.Contains(text, StringComparison.OrdinalIgnoreCase);
        Logger.Success($"Contains expected text: {contains}");

        return contains;
    }

    /*
     * Clicks the “Contact Us” link found within the search results.
     * This navigates the user from Google to the target website.
     */
    public async Task ClickContactUs()
    {
        Logger.Info("Looking for 'Contact Us' link in search results...");

        var link = _page.GetByRole(AriaRole.Link, new() { Name = "Contact Us" });

        Logger.Info("Clicking 'Contact Us' link...");
        await link.ClickAsync();
    }
}