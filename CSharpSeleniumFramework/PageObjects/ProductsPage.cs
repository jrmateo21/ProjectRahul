using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace CSharpSeleniumFramework.PageObjects
{
    public class ProductsPage
    {
        IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCartButton = By.CssSelector(".card-footer button");
     

        public ProductsPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }


        // IList<IWebElement> products = driver.FindElements(By.TagName("app-card")); - converted to below
        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;


        [FindsBy(How = How.XPath, Using = "//a[@class='nav-link btn btn-primary']")]
        private IWebElement checkOutButton;


        public void waitForPageDisplay()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

        }

        public IList<IWebElement> getCards()
        {
            return cards;
        }

        public By getCardTitle()
        {
            return cardTitle;

        }

        public By getAddToCartButton()
        {
            return addToCartButton;
        }

        public CheckOutPage checkOut()
        {
            checkOutButton.Click();
           return  new CheckOutPage(driver);

        }
    }
}
