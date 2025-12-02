using Microsoft.Playwright;
using PlaywrightGoogleTest.Utilities;
using System.Threading.Tasks;

namespace PlaywrightGoogleTest.Pages;

/*
 * Page Object for the target website's Contact Us page.
 * Provides methods for entering basic user information and validating required fields.
 */
public class ContactUsPage
{
    private readonly IPage _page;

    /*
     * Constructor that receives the current Playwright page instance.
     */
    public ContactUsPage(IPage page)
    {
        _page = page;
    }

    private ILocator FirstName => _page.Locator("input[name='firstname'], input#firstname");
    private ILocator LastName  => _page.Locator("input[name='lastname'], input#lastname");
    private ILocator SubmitButton => _page.Locator("button[type='submit'], input[type='submit']");
    private ILocator RequiredErrors => _page.Locator(".error, [aria-required='true'], .required");

    /*
     * Fills in the First Name and Last Name fields on the form.
     */
    public async Task FillNames(string first, string last)
    {
        Logger.Info($"Entering first name: {first}");
        await FirstName.FillAsync(first);
        
        Logger.Info($"Entering last name: {last}");
        await LastName.FillAsync(last);
        
        Logger.Success("Name fields filled.");
    }

    /*
     * Submits the Contact Us form.
     */
    public async Task Submit()
    {
        Logger.Info("Submitting Contact Us form...");
        await SubmitButton.ClickAsync();
        Logger.Success("Form submitted.");
    }

    /*
     * Counts how many required field indicators or validation errors remain.
     */
    public async Task<int> CountAdditionalRequiredFields()
    {
        Logger.Info("Counting remaining required fields...");
        int count = await RequiredErrors.CountAsync();
        Logger.Info($"Required field count: {count}");
        return count;
    }
}