using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    public class PurchasePage
    {
        IWebDriver driver;
        public PurchasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        //IWebElement addressTextBox = driver.FindElement(By.XPath("//input[@id='country']"));
        //addressTextBox.SendKeys("ind");

        [FindsBy(How = How.XPath, Using = "//input[@id='country']")]
        private IWebElement addressTextBox;

        
        //driver.FindElement(By.LinkText("India")).Click();

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement country;


        //IWebElement iAgreeTerms = driver.FindElement(By.XPath("//label[@for='checkbox2']"));
        //iAgreeTerms.Click();

        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement iAgreeTerms;


        //IWebElement purchaseBtn = driver.FindElement(By.XPath("//input[@value='Purchase']"));
        //purchaseBtn.Click();

        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement purchaseButton;




        public void getAddress()
        {

            addressTextBox.SendKeys("ind");


        }

        public  void waitCountryToDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            country.Click();    

        }

        public void  iAgree()
        {
            iAgreeTerms.Click();
        }

        public void purchase()
        {
            purchaseButton.Click();

        }

        public void waitUntilSuccess()
        {
           WebDriverWait waitSuccess = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           waitSuccess.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='alert alert-success alert-dismissible']")));

        }






    }
}
