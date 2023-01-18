﻿using CognizantSoftvision.Maqs.BaseSeleniumTest;
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
        /// Gets welcome message
        /// </summary>
        private LazyElement CommunityHomepage
        {
            get { return this.GetLazyElement(By.CssSelector(".MuiTypography-root"), "Community homepage"); }
        }

        /// <summary>
        /// Check if the home page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.CommunityHomepage.Displayed;
        }
    }
}

