using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using MongoDB.Driver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class MaintenanceSkillsPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "maintenance";

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public MaintenanceSkillsPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets skill table container
        /// </summary>
        private LazyElement SkillsTableContainer
        {
            get { return this.GetLazyElement(By.Id("skills-table-container"), "Skills table container"); }
        }

        /// <summary>
        /// Gets pagination row count controller with initial value provided in current view
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private LazyElement PaginationRowCountController(string value)
        {
            return this.GetLazyElement(By.CssSelector($"input[value='{value}']"), "Pagination row controller");
        }

        /// <summary>
        /// Gets pagination row count selector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private LazyElement PaginationRowCountControllerOption(string value)
        {
            return this.GetLazyElement(By.CssSelector($"li[data-value='{value}']"), "Pagination row controller count selector");
        }
        
        /// <summary>
        /// Gets skills tab search input bar
        /// </summary>
        private LazyElement SkillTabSearchBar
        {
            get { return this.GetLazyElement(By.CssSelector("input[placeholder='Search']"), "Skill tab search input bar"); }
        }

        /// <summary>
        /// Gets skills tab search button
        /// </summary>
        private LazyElement SkillTabSearchButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[type='button'] svg[data-testid='SearchIcon']"), "Skill tab search button"); }
        }

        /// <summary>
        /// Gets skill description text area
        /// </summary>
        private LazyElement SkillDescriptionTextArea
        {
            get { return this.GetLazyElement(By.CssSelector("input[id='peopleskills_desc']"), "Skill description text area"); }
        }

        /// <summary>
        /// Gets Update Skill Success Notification
        /// </summary>
        private LazyElement UpdateSkillSuccessNotification
        {
            get { return this.GetLazyElement(By.XPath("//div[text()='Skill has been updated.']"), "Update success notification"); }
        }

        /// <summary>
        /// Gets skill value in a single row and column dynamically
        /// </summary>
        private LazyElement DynamicSkillValue(string skillValue)
        {
            return this.GetLazyElement(By.XPath($"//td[text()='{skillValue}']"), "Get skill value in a single row and column");
        }

        /// <summary>
        /// Gets Update Skill button
        /// </summary>
        private LazyElement UpdateButton
        {
            get { return this.GetLazyElement(By.XPath("//button[text()='Update']"), "Update skill button"); }
        }

        /// <summary>
        /// Gets Edit Skill icon
        /// </summary>
        private LazyElement EditSkillIcon
        {
            get { return this.GetLazyElement(By.CssSelector("svg[data-testid='EditIcon']"), "Edit skill icon"); }
        }

        /// <summary>
        /// Changes total row count from current view to new desired number of row count
        /// Example: Current view is in 10 rows, change to 100 rows
        /// </summary>
        /// <param name="initialValue"></param>
        /// <param name="newValue"></param>
        public void ChangeNumberOfRowsDisplayedByInitialRowToNewNumberOfRows(string initialValue, string newValue)
        {
            this.PaginationRowCountController(initialValue);
            this.PaginationRowCountControllerOption(newValue);
        }

        /// <summary>
        /// Enter desired skill in search bar with desired value
        /// </summary>
        /// <param name="skill"></param>
        public void EnterDesiredSkill(string skill)
        {
            this.SkillTabSearchBar.SendKeys(skill);
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public void ClickSearchButton()
        {
            this.SkillTabSearchButton.Click();
        }

        /// <summary>
        /// Gets skill value by specific row
        /// </summary>
        /// <param name="skillValue"></param>
        /// <returns></returns>
        public string GetSkillValueByRow(string skillValue)
        {
            return this.DynamicSkillValue(skillValue).GetValue();
        }

        /// <summary>
        /// Gets skill description value
        /// </summary>
        /// <returns>Current value on the skill description placeholder</returns>
        public string GetSkillDescriptionValue()
        {
            return this.SkillDescriptionTextArea.GetValue();
        }

        /// <summary>
        /// Enter desired skill description value
        /// </summary>
        /// <param name="skillValue"></param>
        public void EnterSkillDescriptionValue(string skillValue)
        {
            this.SkillDescriptionTextArea.SendKeys(skillValue);
        }

        /// <summary>
        /// Clicks update skill button
        /// </summary>
        public void ClickUpdateSkillButton()
        {
            this.UpdateButton.Click();
        }

        /// <summary>
        /// Clicks update skill button
        /// </summary>
        public void ClickEditSkillIcon()
        {
            this.EditSkillIcon.Click();
        }

        /// <summary>
        /// Randomly click edit icon of displayed search result
        /// </summary>
        public void ClickEditSkillIconRandomly()
        {
            var random = new Random();
            IList<IWebElement> editIcon = WebDriver.FindElements(By.CssSelector("svg[data-testid='EditIcon']"));
            var randomOption = random.Next(0, editIcon.Count());
            editIcon[randomOption].Click();
        }

        /// <summary>
        /// Navigates to maintenance page
        /// </summary>
        public void NavigateToMaintenancePage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
        }

        /// <summary>
        /// Revert edited skill
        /// </summary>
        /// <param name="skill"></param>
        public void RevertSkillChanges(string skill)
        {
            SkillDescriptionTextArea.SendKeys(Keys.LeftShift + Keys.Home);
            SkillDescriptionTextArea.SendKeys(Keys.Backspace);
            this.SkillDescriptionTextArea.SendKeys(skill);
            this.ClickUpdateSkillButton();
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public bool IsUpdateSuccessNotificationVisible()
        {
            return this.UpdateSkillSuccessNotification.Displayed;
        }

        /// <summary>
        /// Check if the employee list table has been shown
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.SkillsTableContainer.Exists;
        }
    }
}

