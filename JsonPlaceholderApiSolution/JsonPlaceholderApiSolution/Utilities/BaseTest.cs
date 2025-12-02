using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using ApiTests.Client;
using ApiTests.Config;
using NUnit.Framework;

namespace ApiTests.Tests
{
    /*
     * BaseTest sets up shared test initialization.
     * Includes ExtentReports setup and API client initialization.
     */
    public class BaseTest
    {
        protected JsonPlaceholderClient _api;
        protected int _userId = 11;
        private static ExtentReports extent;
        protected ExtentTest test;

        /*
         * One-time setup for the entire test run.
         * Initializes ExtentReports and configures the HTML report.
         */
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _api = new JsonPlaceholderClient(ConfigManager.BaseUrl);

            var reportPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "report.html");
            var reporter = new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            Console.WriteLine("REPORT PATH: " + TestContext.CurrentContext.WorkDirectory);
        }

        /*
         * Runs before each test.
         * Initializes the API client and starts a new test entry in the report.
         */
        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        /*
         * Runs after the whole test run.
         * Flushes the Extent report to save it to disk.
         */
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }
    }
}