using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Gets Clear all button in Skills input field
        /// </summary>
        private LazyElement ClearAllSkillsButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[title='Clear']"), "Clear all skill button"); }
        }

        /// <summary>
        /// Gets remove icon on skill chip
        /// </summary>
        private IWebElement SkillChipRemoveIcon
        {
            get { return this.GetLazyElement(By.CssSelector("svg[data-testid='CancelIcon']"), "Skill chip remove icon"); }
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
        /// Click skills option randomly
        /// </summary>
        public void ClickRandomSkillsOption()
        {
            var random = new Random();
            var randomOption = random.Next(-1, 139);
            this.DynamicSkillsOption(randomOption.ToString()).Click();
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
        /// Remove skills by option
        /// </summary>
        public void RemoveSkillsByOption(string option)
        {
            ICollection<IWebElement> removeIcon = (ICollection<IWebElement>)SkillChipRemoveIcon;
            switch (option)
            {
                case "SINGLE":
                    removeIcon.Last().Click();
                    break;

                case "REMOVEALL":
                    this.ClickClearAllSkillsButton();
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Click clear all skills in Skills input field
        /// </summary>
        public void ClickClearAllSkillsButton()
        {
            this.SkillsInputField.Click();
            this.ClearAllSkillsButton.Click();
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

