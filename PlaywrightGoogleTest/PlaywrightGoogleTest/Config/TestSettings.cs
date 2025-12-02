namespace PlaywrightGoogleTest.Config;

/*
 * Collection of test input values and configuration settings.
 * Keeps test data separate from test logic.
 */
public class TestSettings
{
    public string BaseUrl { get; set; } = "https://www.google.com";
    public string SearchTerm { get; set; } = "Prometheus Group";
    public string FirstName { get; set; } = "Veronika";
    public string LastName { get; set; } = "Kuliba";
}