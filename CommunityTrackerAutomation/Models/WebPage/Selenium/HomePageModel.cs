using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using OpenQA.Selenium;
using System.Linq;

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
        /// Gets Update Success Notification
        /// </summary>
        private LazyElement UpdateSuccessNotification
        {
            get { return this.GetLazyElement(By.XPath("//div[text()='Member has been updated.']"), "Update success notification"); }
        }

        /// <summary>
        /// Dynamic Engineering Settings
        /// </summary>
        /// <param name="index">Index of the selected community in the url</param>
        private LazyElement DynamicCommunitySettingsButton(string index)
        {
            return this.GetLazyElement(By.CssSelector($"a[href='/communities/update/{index}']"), "Community Settings Button");
        }
        /// <summary>
        /// Dynamic View All Members
        /// </summary>
        /// <param name="index">Index of the selected community in the url</param>
        private LazyElement DynamicCommunityViewAllMembersButton(string index)
        {
            return this.GetLazyElement(By.CssSelector($"a[href='/members/{index}']"), "Community View All Members");
        }
        /// <summary>
        /// Navigating to specific community
        /// </summary>
        /// <param name="index">Index of the selected community in the url</param>
        /// <returns></returns>
        public void navigateToCommunity(int index)
        {
            var newWindow = this.TestObject.WebDriver.WindowHandles.First();
            this.TestObject.WebDriver.SwitchTo().Window(newWindow);
            this.DynamicCommunityViewAllMembersButton(index.ToString()).Click();
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public void ClickSearchButton()
        {
            this.SearchButton.Click();
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public bool IsUpdateSuccessNotificationVisible()
        {
            return this.UpdateSuccessNotification.Displayed;
        }

        /// <summary>
        /// Enters employeeID in search input
        /// </summary>
        /// <param name="employeeId"></param>
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


        /// <summary>
        /// Enter employee name in search input
        /// </summary>
        /// <param name="employeeName"></param>
        public void EnterEmployeeName(string employeeName)
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
            this.SearchEmployeeInput.SendKeys(employeeName);

            this.SearchButton.Click();

        }

        /// <summary>
        /// Switch window
        /// </summary>
        public void SwitchWindow()
        {
            var newWindow = this.TestObject.WebDriver.WindowHandles.Last();
            this.TestObject.WebDriver.SwitchTo().Window(newWindow);
        }

        /// <summary>
        /// Show result window
        /// </summary>
        public string SearchResultWindow()
        {
            return this.TestObject.WebDriver.FindElement(By.TagName("td")).Text;
        }

    }
}

