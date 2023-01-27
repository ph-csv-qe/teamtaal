using AngleSharp.Common;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class EmployeeRecordPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public EmployeeRecordPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets inputs grid container
        /// </summary>
        private LazyElement InputsGridContainer
        {
            get { return this.GetLazyElement(By.Id("inputs-grid"), "Inputs grid container"); }
        }

        /// <summary>
        /// Gets Skills input field
        /// </summary>
        private LazyElement SkillsInputField
        {
            get { return this.GetLazyElement(By.CssSelector("input#auto-complete-chip"), "Skills input field"); }
        }

        /// <summary>
        /// Gets Project input field
        /// </summary>
        private LazyElement ProjectDropdown
        {
            get { return this.GetLazyElement(By.CssSelector("#mui-component-select-project"), "Project dropdown field"); }
        }

        /// <summary>
        /// Gets Clear all button in Skills input field
        /// </summary>
        private LazyElement ClearAllSkillsButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[title='Clear']"), "Clear all skill button"); }
        }

        /// <summary>
        /// Gets Save button
        /// </summary>
        private LazyElement SaveButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[type='submit'] svg[data-testid='SaveIcon']"), "Save button"); }
        }

        /// <summary>
        /// Gets Skills auto-complete options by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private LazyElement DynamicSkillsOption(string index)
        {
            return this.GetLazyElement(By.Id($"auto-complete-chip-option-{index}"), "Skills input options auto-complete");
        }

        /// <summary>
        /// Clicks random skill by option, SINGLE or MULTIPLE
        /// </summary>
        /// <param name="option"></param>
        public void ClickRandomSkillsOption(string option)
        {
            var random = new Random();
       
            switch (option)
            {
                case "SINGLE":
                    var randomOption = random.Next(-1, 139);
                    this.DynamicSkillsOption(randomOption.ToString()).Click();
                    break;

                case "MULTIPLE":
                    for (int i = 0; i < 3; i++)
                    {
                        var multiOption = random.Next(-1, 139);
                        this.DynamicSkillsOption(multiOption.ToString()).Click();
                        ClickSkillsInputField();
                    }
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Click skills input field
        /// </summary>
        public void ClickSkillsInputField()
        {
            this.SkillsInputField.Click();
        }

        /// <summary>
        /// Click Save button
        /// </summary>
        public void ClickSaveButton()
        {
            this.SaveButton.Click();
        }

        /// <summary>
        /// Gets total count of skill chip icon
        /// </summary>
        public int GetTotalCountOfSkills()
        {
            WebDriver.Navigate().Refresh();
            //Thread.Sleep is temporarily used due to element cannot be found without it, will do code refactoring for this page action
            System.Threading.Thread.Sleep(3000);
            int numberOfElements = WebDriver.FindElements(By.CssSelector("svg[data-testid='CancelIcon']")).Count;
            return numberOfElements;
        }

        /// <summary>
        /// Remove skills by option, SINGLE or REMOVEALL
        /// </summary>
        /// <param name="option"></param>
        public void RemoveSkillsByOption(string option)
        {
            IList<IWebElement> removeIcon = WebDriver.FindElements(By.CssSelector("svg[data-testid='CancelIcon']"));
            switch (option)
            {
                case "SINGLE":
                    removeIcon.Last().Click();
                    break;

                case "REMOVEALL":
                    this.SkillsInputField.Click();
                    this.ClearAllSkillsButton.Click();
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Gets current value of Project
        /// </summary>
        public string GetCurrentProject()
        {
            string currentProject = ProjectDropdown.Text;
            return currentProject;
        }

        /// <summary>
        /// Gets current value of Project
        /// </summary>
        public string SelectDifferentProject(string currentProject)
        {
            IWebElement randomProject;
            string updatedProject;
            ProjectDropdown.Click();

            var random = new Random();
            var randomProjectIndex = random.Next(2, 20);

            do
            {
                randomProject = this.GetLazyElement(By.CssSelector($"li[role='option'][data-value='{randomProjectIndex}']"));
                randomProject.Click();

                updatedProject = ProjectDropdown.Text;
            }
            while (currentProject == updatedProject);

            return updatedProject;
        }

        /// <summary>
        /// Check if the employee list table has been shown
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.InputsGridContainer.Exists;
        }
    }
}

