using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using OpenQA.Selenium;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class HomePageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public HomePageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets Search Employee Input Box
        /// </summary>
        private LazyElement SearchEmployeeInput
        {
            get { return this.GetLazyElement(By.CssSelector("input[placeholder='Search Employee']"), "Search Employee Input"); }
        }

        /// <summary>
        /// Gets Search Button
        /// </summary>
        private LazyElement SearchButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[type='submit'] svg[data-testid='SearchIcon']"), "Search Button"); }
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public void ClickSearchButton()
        {
            this.SearchButton.Click();
        }

        /// <summary>
        /// Enter employeeID in search input
        /// </summary>
        public void EnterEmployeeID(string employeeId)
        {
            this.SearchEmployeeInput.SendKeys(employeeId);
        }

        /// <summary>
        /// Check if the home page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
            return this.SearchEmployeeInput.Exists;
        }
    }
}

