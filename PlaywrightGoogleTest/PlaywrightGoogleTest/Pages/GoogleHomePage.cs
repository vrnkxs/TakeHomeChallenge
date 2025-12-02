using Microsoft.Playwright;
using PlaywrightGoogleTest.Utilities;
using System.Threading.Tasks;

namespace PlaywrightGoogleTest.Pages;

/*
 * Page Object for the Google homepage. 
 * Provides actions for entering a search term and submitting it.
 */
public class GoogleHomePage
{
    private readonly IPage _page;

    /*
     * Constructor that receives the current Playwright page instance.
     */
    public GoogleHomePage(IPage page)
    {
        _page = page;
    }

    /*
     * Locator for the Google search input field.
     * Uses XPath to find the textarea where users type their search query.
     */
    private ILocator SearchBox => _page.Locator("//textarea[@class='gLFyf']");

    /*
     * Types a search query into the Google search box and submits it.
     */
    public async Task Search(string query)
    {
        Logger.Info("Typing search query into Google search box...");
        Logger.Info($"Search term: {query}");
        
        await SearchBox.FillAsync(query);
        
        Logger.Info("Submitting Google search...");
        await _page.Keyboard.PressAsync("Enter");
        Logger.Success("Search submitted successfully.");
    }
}